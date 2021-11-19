namespace App.Server.Features.Playlist
{
    using App.Server.Features.Playlist.Models;
    using App.Server.Features.Songs.Models;
    using App.Server.Infrastructure.Extentions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using static Infrastructure.WebConstants;

    [Authorize]
    public class PlaylistController : ApiController
    {
        private readonly IPlaylistService playlistService;

        public PlaylistController(IPlaylistService playlistService)
        {
            this.playlistService = playlistService;
        }

        [HttpGet]
        public async Task<IEnumerable<PlaylistListingServiceModel>> Mine()
        {
            string userId = this.User.GetId();

            var playlists = await this.playlistService.ByUser(userId);

            return playlists;
        }

        [HttpGet]
        [Route(Id)]
        public async Task<ActionResult<PlaylistDetailsServiceModel>> Details(int id)
            => await this.playlistService.Details(id);

        [HttpPost]
        public async Task<ActionResult> Create(CreatePlaylistRequestModel model)
        {
            var userId = this.User.GetId();

            var playlistId = await this.playlistService.Create(
                model.Title,
                model.ImageUrl,
                userId);

            return Created(nameof(this.Create), playlistId);
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdatePlaylistRequestModel model)
        {
            var userId = this.User.GetId();

            var updated = await this.playlistService.Update(
                model.Id,
                model.Title,
                model.ImageUrl,
                userId);

            if (!updated)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete]
        [Route(Id)]
        public async Task<ActionResult> Delete(int id)
        {
            var userId = this.User.GetId();

            var deleted = await this.playlistService.Delete(id, userId);

            if (!deleted)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost]
        [Route(SpecificSong)]
        public async Task<ActionResult> AddSongToPlaylist(int playlistId, int songId)
        {
            var userId = this.User.GetId();

            var added = await this.playlistService.AddSongToPlaylist(playlistId, songId, userId);

            if (!added)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete]
        [Route(SpecificSong)]
        public async Task<ActionResult> RemoveSongFromPlaylist(int playlistId, int songId)
        {
            var userId = this.User.GetId();

            var removed = await this.playlistService.RemoveSongFromPlaylist(playlistId, songId, userId);

            if (!removed)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet]
        [Route(SpecificSong)]
        public async Task<ActionResult<SongListingServiceModel>> SongWithAction(int playlistId, int songId, [FromQuery(Name = "action")] string action)
        {
            switch (action)
            {
                case "next":
                    return await this.playlistService.GetNextOrPreviousSong(playlistId, songId, true);

                case "previous":
                    return await this.playlistService.GetNextOrPreviousSong(playlistId, songId, false);

                default:
                    return BadRequest();
            }
        }
    }
}
