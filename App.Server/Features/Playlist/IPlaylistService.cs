namespace App.Server.Features.Playlist
{
    using App.Server.Features.Playlist.Models;
    using App.Server.Features.Songs.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IPlaylistService {
        public Task<IEnumerable<PlaylistListingServiceModel>> ByUser(string userId);

        public Task<PlaylistDetailsServiceModel> Details(int id);

        public Task<int> Create(string userId);

        public Task<bool> Update(int id, string title, string imageUrl, string userId);

        public Task<bool> Delete(int id, string userId);

        public Task<bool> AddSongToPlaylist(int playlistId, int songId, string userId);

        public Task<bool> RemoveSongFromPlaylist(int playlistId, int songId, string userId);

        public Task<SongFromPlaylistResponseModel> GetNextOrPreviousSong(int playlistId, int songId, bool next);

    }
}
