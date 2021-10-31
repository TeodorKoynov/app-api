namespace App.Server.Data
{
    using App.Server.Data.Models;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Song> Songs { get; set; }

        public DbSet<AudioFile> AudioFiles { get; set; }

        public DbSet<Playlist> Playlists { get; set; }

        public DbSet<PlaylistSong> PlaylistSongs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<User>()
                .HasOne(u => u.ActivePlayingSong)
                .WithOne(aps => aps.User)
                .HasForeignKey<ActivePlayingSong>(aps => aps.UserId);

            builder
                .Entity<Song>()
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
                .HasOne(p => p.Creator)
                .WithMany(c => c.Playlists)
                .HasForeignKey(p => p.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Entity<Playlist>()
                .HasOne(p => p.ActivePlayingSong)
                .WithOne(aps => aps.Playlist)
                .HasForeignKey<ActivePlayingSong>(aps => aps.PlaylistId)
                .IsRequired(false);

            builder
                .Entity<PlaylistSong>()
                .HasKey(ps => new { ps.PlaylistId, ps.SongId });
            
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
    }
}
