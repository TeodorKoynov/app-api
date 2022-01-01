namespace App.Server.Features.Songs
{
    using App.Server.Features.Songs.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISongService
    {
        public Task<int> Create(string title, string description, string imageUrl, string audioUrl, int duration, string userId);

        public Task<bool> Update(int id, string title, string description, string imageUrl, string userId);

        public Task<bool> Delete(int id, string userId);

        public Task<IEnumerable<SongListingServiceModel>> ByUser(string userId);

        public Task<SongDetailsServiceModel> Details(int id);

        public Task<PlaySongResponseModel> GetById(int id);

    }
}
