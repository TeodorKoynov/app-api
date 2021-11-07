namespace App.Server.Features.Playlist
{
    using App.Server.Data;
    using App.Server.Data.Models;
    using App.Server.Features.Playlist.Models;
    using App.Server.Features.Songs.Models;
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
                    .Include(p => p.PlaylistSong)
                    //.ThenInclude(ps => ps.Song) Referance
                    .Where(p => p.CreatorId == userId)
                    .ToListAsync();

            List<PlaylistListingServiceModel> playlistList = new List<PlaylistListingServiceModel>();

            foreach (Playlist playlist in playlists)
            {
                PlaylistListingServiceModel playlistDto = new PlaylistListingServiceModel
                {
                    Id = playlist.Id,
                    Title = playlist.Title,
                    ImageUrl = playlist.ImageUrl,
                    ReleaseYear = playlist.ReleaseDate.Year.ToString()
                };

                playlistList.Add(playlistDto);
            }

            return playlistList;
        }

        public async Task<int> Create(string title, string imageUrl, string userId)
        {
            Playlist playlist = new Playlist
            {
                Title = title,
                ImageUrl = imageUrl,
                CreatorId = userId,
                ReleaseDate = DateTime.UtcNow,
            };

            this.data.Add(playlist);

            await this.data.SaveChangesAsync();

            return playlist.Id;
        }

        public async Task<PlaylistDetailsServiceModel> Details(int id)
        {
            Playlist playlist = await data
                .Playlists
                .Include(p => p.PlaylistSong)
                //.ThenInclude(ps => ps.Song)
                .FirstOrDefaultAsync(p => p.Id == id);

            List<PlaylistSong> playlistSongs = await data
                .PlaylistSongs
                .Include(ps => ps.Song)
                .ThenInclude(s => s.User)
                .Where(ps => ps.PlaylistId == id)
                .ToListAsync();

            PlaylistDetailsServiceModel playlistDto = new PlaylistDetailsServiceModel
            {
                Id = playlist.Id,
                Title = playlist.Title,
                ImageUrl = playlist.ImageUrl,
                ReleaseYear = playlist.ReleaseDate.Year.ToString()
            };

            foreach (PlaylistSong playlistSong in playlistSongs)
            {
                Song song = playlistSong.Song;

                SongListingServiceModel songDto = new SongListingServiceModel
                {
                    Id = song.Id,
                    Title = song.Title,
                    CreatedOn = song.CreatedOn,
                    ImageUrl = song.ImageUrl,
                    UserName = song.User.UserName
                };

                playlistDto.Songs.Add(songDto);
            }

            playlistDto.SongCount = playlistDto.Songs.Count;

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

        public async Task<Playlist> GetPlaylistByIdAndByUserId(int id, string userId)
            => await this.data
                .Playlists
                .Where(p => p.Id == id && p.CreatorId == userId)
                .FirstOrDefaultAsync();
    }
}
