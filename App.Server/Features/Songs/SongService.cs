namespace App.Server.Features.Songs
{
    using App.Server.Data;
    using App.Server.Data.Models;
    using App.Server.Features.Songs.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SongService : ISongService
    {
        private readonly AppDbContext data;

        public SongService(AppDbContext data) => this.data = data;

        public async Task<IEnumerable<SongListingServiceModel>> ByUser(string userId)
        {
            var songs = await this.data
                .Songs
                .Include(s => s.User)
                .Where(s => s.UserId == userId)
                .ToListAsync();

            List<SongListingServiceModel> songList = new List<SongListingServiceModel>();

            foreach (Song song in songs)
            {
                SongListingServiceModel dtoSong = new SongListingServiceModel
                {
                    Id = song.Id,
                    Title = song.Title,
                    ImageUrl = song.ImageUrl,
                    CreatedOn = song.CreatedOn,
                    UserName = song.User.UserName
                };

                songList.Add(dtoSong);
            }

            return songList;

           // return await this.data
          //      .Songs
          //      .Include(s => s.AudioFile)
          //      .Where(s => s.UserId == userId)
          //      .Select(s => new SongListingServiceModel
         //       {
         //           Id = s.Id,
         //           Title = s.Title,
         //           AudioFile = s.AudioFile.Content,
         //           ImageUrl = s.ImageUrl,
         //           CreatedOn = s.CreatedOn
        //        })
         //       .ToListAsync();
        }


        public async Task<int> Create(string title, string description, string imageUrl, string audioUrl, string userId)
        {
            var song = new Song
            {
                Title = title,
                Description = description,
                ImageUrl = imageUrl,
                AudioFile = new AudioFile()
                {
                    Content = audioUrl
                },
                UserId = userId,
                CreatedOn = DateTime.UtcNow
            };

            this.data.Add(song);

            song.AudioFile.Song = song;

            this.data.AudioFiles.Add(song.AudioFile);

            // look at logic later
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
        {
            Song song = await this.data
                .Songs
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.Id == id);

            AudioFile audioFile = this.data.AudioFiles.FirstOrDefault(audioFile => audioFile.SongId == song.Id);

            SongDetailsServiceModel songDto = new SongDetailsServiceModel
            {
                Id = song.Id,
                Title = song.Title,
                Description = song.Description,
                ImageUrl = song.ImageUrl,
                AudioFile = audioFile.Content,
                UserId = song.UserId,
                UserName = song.User.UserName,
                CreatedOn = song.CreatedOn
            };

            return songDto;
        }

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
        public async Task<SongListingServiceModel> GetById(int id)
        {
            var song = await this.data
                .Songs
                .Where(s => s.Id == id)
                .Include(s => s.User)
                .FirstOrDefaultAsync();

            if (song is null)
            {
                return null;
            }

            AudioFile audioFile = this.data.AudioFiles.FirstOrDefault(audioFile => audioFile.SongId == id);

            SongListingServiceModel songDto = new SongListingServiceModel
            {
                Id = song.Id,
                Title = song.Title,
                AudioFile = audioFile.Content,
                ImageUrl = song.ImageUrl,
                UserName = song.User.UserName,
                CreatedOn = song.CreatedOn
            };

            return songDto;
        }

        private async Task<Song> GetSongByIdAndByUserId(int id, string userId)
            => await this.data
                .Songs
                .Where(s => s.Id == id && s.UserId == userId)
                .FirstOrDefaultAsync();

        //    private async Task<byte[]> FileToByteArray(IFormFile file)
        //    {
        //       if (!(file is null))
        //       {
        //           using (var memoryStream = new MemoryStream())
        //           {
        //              await file.CopyToAsync(memoryStream);
        //              return memoryStream.ToArray();
        //           }
        //      }
        //
        //        return null;
        //   }
    }
}
