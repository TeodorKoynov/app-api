namespace App.Server.Features.Songs
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISongService
    {
        public Task<int> Create(string title, string description, string imageUrl, string audioUrl, string userId);

        public Task<IEnumerable<SongListingResponseModel>> ByUser(string id);
    }
}
