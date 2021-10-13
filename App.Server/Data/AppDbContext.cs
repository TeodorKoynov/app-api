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

        protected override void OnModelCreating(ModelBuilder builder)
        {
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

            base.OnModelCreating(builder);
        }
    }
}
