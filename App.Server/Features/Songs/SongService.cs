namespace App.Server.Features.Songs
{
    using App.Server.Data;
    using App.Server.Data.Models;
    using App.Server.Features.Songs.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SongService : ISongService
    {
        private readonly AppDbContext data;

        public SongService(AppDbContext data) => this.data = data;

        public async Task<IEnumerable<SongListingServiceModel>> ByUser(string userId)
            => await this.data
                .Songs
                .Where(s => s.UserId == userId)
                .Select(s => new SongListingServiceModel
                {
                    Id = s.Id,
                    Title = s.Title,
                    AudioUrl = s.AudioUrl,
                    ImageUrl = s.ImageUrl
                })
                .ToListAsync();

        public async Task<int> Create(string title, string description, string imageUrl, string audioUrl, string userId)
        {
            var song = new Song
            {
                Title = title,
                Description = description,
                ImageUrl = imageUrl,
                AudioUrl = audioUrl,
                UserId = userId
            };

            this.data.Add(song);

            await this.data.SaveChangesAsync();

            return song.Id;
        }

        public async Task<bool> Update(int id, string title, string description, string imageUrl, string userId)
        {
            var song = await this.GetSongByIdAndByUserId(id, userId);

            if (song is null)
            {
                return false;
            }

            song.Title = title;
            song.Description = description;
            song.ImageUrl = imageUrl;

            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<SongDetailsServiceModel> Details(int id)
            => await this.data
                .Songs
                .Where(s => s.Id == id)
                .Select(s => new SongDetailsServiceModel
                {
                    Id = s.Id,
                    Title = s.Title,
                    Description = s.Description,
                    ImageUrl = s.ImageUrl,
                    AudioUrl = s.AudioUrl,
                    UserId = s.UserId,
                    UserName = s.User.UserName
                })
                .FirstOrDefaultAsync();

        public async Task<bool> Delete(int id, string userId)
        {
            var song = await this.GetSongByIdAndByUserId(id, userId);

            if (song is null)
            {
                return false;
            }

            this.data.Songs.Remove(song);

            await this.data.SaveChangesAsync();

            return true;
        }

        private async Task<Song> GetSongByIdAndByUserId(int id, string userId)
            => await this.data
                .Songs
                .Where(s => s.Id == id && s.UserId == userId)
                .FirstOrDefaultAsync();
    }
}
