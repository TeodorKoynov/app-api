namespace App.Server.Features.Playlist
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using App.Server.Infrastructure.Services;
    using Models;

    using static Infrastructure.WebConstants;

    [Authorize]
    public class PlaylistController : ApiController
    {
        private readonly IPlaylistService playlistService;
        private readonly ICurrentUserService currentUserService; 

        public PlaylistController(IPlaylistService playlistService, 
            ICurrentUserService currentUserService)
        {
            this.playlistService = playlistService;
            this.currentUserService = currentUserService;
        }

        [HttpGet]
        public async Task<IEnumerable<PlaylistListingServiceModel>> Mine()
            => await this.playlistService.ByUser(this.currentUserService.GetId());

        [HttpGet]
        [Route(Id)]
        public async Task<ActionResult<PlaylistDetailsServiceModel>> Details(int id)
            => await this.playlistService.Details(id);

        [HttpPost]
        public async Task<ActionResult> Create()
        {
            var userId = this.currentUserService.GetId();

            var playlistId = await this.playlistService.Create(userId);

            return Created(nameof(this.Create), playlistId);
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdatePlaylistRequestModel model)
        {
            var userId = this.currentUserService.GetId();

            var result = await this.playlistService.Update(
                model.Id,
                model.Title,
                model.ImageUrl,
                userId);

            if (result.Failure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }

        [HttpDelete]
        [Route(Id)]
        public async Task<ActionResult> Delete(int id)
        {
            var userId = this.currentUserService.GetId();

            var result = await this.playlistService.Delete(id, userId);

            if (result.Failure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }

        [HttpPost]
        [Route(SpecificSong)]
        public async Task<ActionResult> AddSongToPlaylist(int playlistId, int songId)
        {
            var userId = this.currentUserService.GetId();

            var result = await this.playlistService.AddSongToPlaylist(playlistId, songId, userId);

            if (result.Failure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }

        [HttpDelete]
        [Route(SpecificSong)]
        public async Task<ActionResult> RemoveSongFromPlaylist(int playlistId, int songId)
        {
            var userId = this.currentUserService.GetId();

            var result = await this.playlistService.RemoveSongFromPlaylist(playlistId, songId, userId);

            if (result.Failure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }

        [HttpGet]
        [Route(SpecificSong)]
        public async Task<ActionResult<SongFromPlaylistResponseModel>> SongWithAction(int playlistId, int songId, [FromQuery(Name = "action")] string action)
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
