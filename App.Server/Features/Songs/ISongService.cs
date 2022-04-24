namespace App.Server.Features.Songs
{
    using Models;
    using Infrastructure.Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISongService
    {
        public Task<int> Create(string title, string description, string imageUrl, string audioUrl, int duration, string userId);

        public Task<Result> Update(int id, string title, string description, string imageUrl, string userId);

        public Task<Result> Delete(int id, string userId);

        public Task<IEnumerable<SongListingServiceModel>> ByUser(string userId);

        public Task<SongDetailsServiceModel> Details(int id);

        public Task<PlaySongResponseModel> GetById(int id);

    }
}
