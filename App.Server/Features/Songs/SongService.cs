namespace App.Server.Features.Songs
{
    using App.Server.Data;
    using App.Server.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SongService : ISongService
    {
        private readonly AppDbContext data;

        public SongService(AppDbContext data) => this.data = data;

        public async Task<IEnumerable<SongListingResponseModel>> ByUser(string id)
            => await data
                .Songs
                .Where(s => s.UserId == id)
                .Select(s => new SongListingResponseModel
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
    }
}
