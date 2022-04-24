namespace App.Server.Features.Playlist
{
    using App.Server.Features.Playlist.Models;
    using App.Server.Infrastructure.Services;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPlaylistService {
        public Task<IEnumerable<PlaylistListingServiceModel>> ByUser(string userId);

        public Task<PlaylistDetailsServiceModel> Details(int id);

        public Task<int> Create(string userId);

        public Task<Result> Update(int id, string title, string imageUrl, string userId);

        public Task<Result> Delete(int id, string userId);

        public Task<Result> AddSongToPlaylist(int playlistId, int songId, string userId);

        public Task<Result> RemoveSongFromPlaylist(int playlistId, int songId, string userId);

        public Task<SongFromPlaylistResponseModel> GetNextOrPreviousSong(int playlistId, int songId, bool next);

    }
}
