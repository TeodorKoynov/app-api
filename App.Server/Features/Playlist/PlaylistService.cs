namespace App.Server.Features.Playlist
{
    using App.Server.Data;
    using App.Server.Data.Models;
    using App.Server.Features.Playlist.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class PlaylistService : IPlaylistService
    {
        private readonly AppDbContext data;

        public PlaylistService(AppDbContext data) => this.data = data;

        public async Task<IEnumerable<PlaylistListingServiceModel>> ByUser(string userId)
        {
            var playlists = await data
                    .Playlists
                    .Where(p => p.CreatorId == userId)
                    .Include(p => p.Creator)
                    .ToListAsync();

            List<PlaylistListingServiceModel> playlistList = new List<PlaylistListingServiceModel>();

            foreach (Playlist playlist in playlists)
            {
                PlaylistListingServiceModel playlistDto = new PlaylistListingServiceModel
                {
                    Id = playlist.Id,
                    Title = playlist.Title,
                    ImageUrl = playlist.ImageUrl,
                    ReleaseYear = playlist.ReleaseDate.Year.ToString(),
                    CreatorName = playlist.Creator.UserName
                };

                playlistList.Add(playlistDto);
            }

            return playlistList;
        }

        public async Task<int> Create(string userId)
        {
            string title = $"My Playlist #{((await this.GetLastPlaylistIndex(userId) + 1))}";

            Playlist playlist = new Playlist
            {
                Title = title,
                CreatorId = userId,
                ReleaseDate = DateTime.UtcNow
            };

            this.data.Add(playlist);

            await this.data.SaveChangesAsync();

            return playlist.Id;
        }

        public async Task<PlaylistDetailsServiceModel> Details(int id)
        {
            Playlist playlist = await data
                .Playlists
                .Include(p => p.Creator)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (playlist is null)
            {
                return null;
            }

            List<PlaylistSong> playlistSongs = await data
                .PlaylistSongs
                .Where(ps => ps.PlaylistId == id)
                .Include(ps => ps.Song)
                .ThenInclude(s => s.User)
                .ToListAsync();

            PlaylistDetailsServiceModel playlistDto = new PlaylistDetailsServiceModel
            {
                Id = playlist.Id,
                Title = playlist.Title,
                ImageUrl = playlist.ImageUrl,
                ReleaseYear = playlist.ReleaseDate.Year.ToString(),
                CreatorName = playlist.Creator.UserName
            };

            foreach (PlaylistSong playlistSong in playlistSongs)
            {
                Song song = playlistSong.Song;

                SongFromPlaylistResponseModel songDto = new SongFromPlaylistResponseModel
                {
                    Id = song.Id,
                    Title = song.Title,
                    CreatedOn = song.CreatedOn,
                    ImageUrl = song.ImageUrl,
                    Duration = song.Duration,
                    TotalTime = this.FormatSeconds(song.Duration),
                    UserName = song.User.UserName,
                    Index = playlistSong.SongIndex
                };

                playlistDto.Songs.Add(songDto);
            }

            playlistDto.SongCount = playlistDto.Songs.Count;
            playlistDto.Songs = playlistDto.Songs.OrderBy(song => song.Index).ToList<SongFromPlaylistResponseModel>();

            return playlistDto;
        }

        public async Task<bool> Update(int id, string title, string imageUrl, string userId)
        {
            Playlist playlist = await this.GetPlaylistByIdAndByUserId(id, userId);

            if (playlist is null)
            {
                return false;
            }

            playlist.Title = title;
            playlist.ImageUrl = imageUrl;

            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(int id, string userId)
        {
            Playlist playlist = await this.GetPlaylistByIdAndByUserId(id, userId);

            if (playlist is null)
            {
                return false;
            }

            data.Remove(playlist);

            await data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddSongToPlaylist(int playlistId, int songId, string userId)
        {
            Playlist playlist = await this.GetPlaylistByIdAndByUserId(playlistId, userId);

            Song song = await this.data
                .Songs
                .Where(s => s.Id == songId)
                .FirstOrDefaultAsync();

            if (playlist is null || song is null)
            {
                return false;
            }

            PlaylistSong playlistSong = new PlaylistSong
            {
                PlaylistId = playlist.Id,
                SongId = songId
            };

            var playlistSongs = await this.data
                .PlaylistSongs
                .Where(ps => ps.PlaylistId == playlistId)
                .ToListAsync();

            playlistSong.SongIndex = playlistSongs.Count + 1;

            this.data.Add(playlistSong);

            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveSongFromPlaylist(int playlistId, int songId, string userId)
        {
            Playlist playlist = await this.GetPlaylistByIdAndByUserId(playlistId, userId);

            var playlistSongToDelete = await this.data
                .PlaylistSongs
                .Where(ps => ps.PlaylistId == playlistId && ps.SongId == songId)
                .FirstOrDefaultAsync();

            if (playlist is null || playlistSongToDelete is null)
            {
                return false;
            }

            var playlistSongs = await this.data
                .PlaylistSongs
                .Where(ps => ps.SongIndex > playlistSongToDelete.SongIndex)
                .ToListAsync();

            foreach (PlaylistSong playlistSong in playlistSongs)
            {
                playlistSong.SongIndex -= 1; 
            }

            this.data.Remove(playlistSongToDelete);

            await this.data.SaveChangesAsync();

            return true;
        }

        public async Task<SongFromPlaylistResponseModel> GetNextOrPreviousSong(int playlistId, int songId, bool next)
        {
            PlaylistSong playlistSong = await this.data
                .PlaylistSongs
                .Where(ps => ps.PlaylistId == playlistId)
                .Where(ps => ps.SongId == songId)
                .FirstOrDefaultAsync();

            if (playlistSong is null)
            {
                return null;
            }

            List<PlaylistSong> allSonginPlaylist = await this.data
                .PlaylistSongs
                .Where(ps => ps.PlaylistId == playlistId)
                .ToListAsync();

            int songIndex = playlistSong.SongIndex;
            int lastSongIndex = allSonginPlaylist.Count; 

            if (next)
            {
                if (songIndex + 1 > lastSongIndex)
                {
                    songIndex = 0;
                }

                return await this.GetSongBy(songIndex + 1, playlistId);
            }

            if (songIndex == 1)
            {
                return await this.GetSongBy(songIndex, playlistId);
            }

            return await this.GetSongBy(songIndex - 1, playlistId);
        }

        private string FormatSeconds(int seconds)
        {
            return (seconds / 60) + ":" + (seconds % 60);
        }

        private async Task<SongFromPlaylistResponseModel> GetSongBy(int songIndex, int playlistId)
        {
            PlaylistSong playlistSong = await this.data
                    .PlaylistSongs
                    .Where(ps => ps.PlaylistId == playlistId)
                    .Where(ps => ps.SongIndex == songIndex)
                    .FirstOrDefaultAsync();

            Song song = await this.data
                            .Songs
                            .Where(s => s.Id == playlistSong.SongId)
                            .Include(s => s.User)
                            .FirstOrDefaultAsync();

            AudioFile audioFile = this.data.AudioFiles.FirstOrDefault(audioFile => audioFile.SongId == playlistSong.SongId);

            SongFromPlaylistResponseModel songDto = new SongFromPlaylistResponseModel()
            {
                Id = song.Id,
                Title = song.Title,
                AudioFile = audioFile.Content,
                CreatedOn = song.CreatedOn,
                ImageUrl = song.ImageUrl,
                UserName = song.User.UserName
            };

            return songDto;
        }

        private async Task<int> GetLastPlaylistIndex(string userId)
        {
            var index = await this.data.Playlists
                .Where(p => p.CreatorId == userId)
                .ToListAsync();

            return index.Count;
        }

        private async Task<Playlist> GetPlaylistByIdAndByUserId(int id, string userId)
            => await this.data
                .Playlists
                .Where(p => p.Id == id && p.CreatorId == userId)
                .FirstOrDefaultAsync();
    }
}
