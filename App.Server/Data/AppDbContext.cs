using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using App.Server.Data.Models.Base;
using App.Server.Infrastructure.Services;

namespace App.Server.Data
{
    using App.Server.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class AppDbContext : IdentityDbContext<User>
    {
        private readonly ICurrentUserService currentUserService;
        
        public AppDbContext(DbContextOptions<AppDbContext> options, 
            ICurrentUserService currentUserService)
            : base(options)
        {
            this.currentUserService = currentUserService;
        }

        public DbSet<Profile> Profiles { get; set; }
        
        public DbSet<Follow> Follows { get; set; }

        public DbSet<Song> Songs { get; set; }

        public DbSet<AudioFile> AudioFiles { get; set; }

        public DbSet<Playlist> Playlists { get; set; }

        public DbSet<PlaylistSong> PlaylistSongs { get; set; }

        public DbSet<ActivePlayingSong> ActivePlayingSongs { get; set; }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            this.ApplyAuditInformation();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }
        
        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = new CancellationToken())
        {
            this.ApplyAuditInformation();

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<User>()
                .HasOne(u => u.Profile)
                .WithOne()
                .HasForeignKey<Profile>(p => p.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder
                .Entity<User>()
                .HasOne(u => u.ActivePlayingSong)
                .WithOne(aps => aps.User)
                .HasForeignKey<ActivePlayingSong>(aps => aps.UserId);

            builder
                .Entity<Follow>()
                .HasOne(f => f.User)
                .WithMany()
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);
                

            builder
                .Entity<Follow>()
                .HasOne(f => f.Follower)
                .WithMany()
                .HasForeignKey(f => f.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);
                
            builder
                .Entity<Song>()
                .HasQueryFilter(s => !s.IsDeleted)
                .HasOne(s => s.User)
                .WithMany(u => u.Songs)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Song>()
                .HasOne(s => s.AudioFile)
                .WithOne(a => a.Song)
                .HasForeignKey<AudioFile>(a => a.SongId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Entity<Song>()
                .HasOne(s => s.ActivePlayingSong)
                .WithOne(aps => aps.Song)
                .HasForeignKey<ActivePlayingSong>(aps => aps.SongId);

            builder
                .Entity<Playlist>()
                .HasQueryFilter(p => !p.IsDeleted)
                .HasOne(p => p.Creator)
                .WithMany(c => c.Playlists)
                .HasForeignKey(p => p.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Playlist>()
                .HasOne(p => p.ActivePlayingSong)
                .WithOne(aps => aps.Playlist)
                .HasForeignKey<ActivePlayingSong>(aps => aps.PlaylistId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            builder
                .Entity<PlaylistSong>()
                .HasKey(ps => new {ps.PlaylistId, ps.SongId});

            builder
                .Entity<PlaylistSong>()
                .HasOne(ps => ps.Playlist)
                .WithMany(p => p.PlaylistSong)
                .HasForeignKey(ps => ps.PlaylistId);

            builder
                .Entity<PlaylistSong>()
                .HasOne(ps => ps.Song)
                .WithMany(s => s.PlaylistSong)
                .HasForeignKey(ps => ps.SongId);

            base.OnModelCreating(builder);
        }

        private void ApplyAuditInformation()
            => this.ChangeTracker
            .Entries()
            .ToList()
                .ForEach(entry =>
            {
                var userName = this.currentUserService.GetUserName();
                
                if (entry.Entity is IDeletableEntity deletableEntity)
                {
                    if (entry.State is EntityState.Deleted)
                    {
                        deletableEntity.DeletedOn = DateTime.UtcNow;
                        deletableEntity.DeletedBy = userName;
                        deletableEntity.IsDeleted = true;
                        
                        entry.State = EntityState.Modified;
                    }
                }
                
                if (entry.Entity is IEntity entity)
                {
                    if (entry.State is EntityState.Added)
                    {
                        entity.CreatedOn = DateTime.UtcNow;
                        entity.CreatedBy = userName;
                    }
                    else if (entry.State is EntityState.Modified)
                    {
                        entity.ModifiedOn = DateTime.Now;
                        entity.ModifiedBy = userName;
                    }
                }
            });


}
}
