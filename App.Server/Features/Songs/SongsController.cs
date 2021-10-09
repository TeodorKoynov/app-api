namespace App.Server.Features.Songs
{
    using App.Server.Features.Songs.Models;
    using App.Server.Infrastructure.Extentions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Authorize]
    public class SongsController : ApiController
    {
        private readonly ISongService songService;

        public SongsController(ISongService songService)
        {
            this.songService = songService;
        }

        [HttpGet]
        public async Task<IEnumerable<SongListingServiceModel>> Mine()
        {
            string userId = this.User.GetId();

            return await songService.ByUser(userId);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<SongDetailsServiceModel>> Details(int id)
        {
            var song = await songService.Details(id);

            if (song is null)
            {
                return NotFound();
            }

            return song;
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateSongRequestModel model)
        {
            var userId = this.User.GetId();

            var updated = await this.songService.Update(
                model.Id, 
                model.Title, 
                model.Description, 
                model.ImageUrl, 
                userId);

            if (!updated)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateSongRequestModel model)
        {
            var userId = this.User.GetId();

            int songId = await songService.Create(
                model.Title,
                model.Description,
                model.ImageUrl, 
                model.AudioUrl, 
                userId);

            return Created(nameof(this.Create), songId);
        }
    }
}
