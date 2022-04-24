using App.Server.Infrastructure.Services;

namespace App.Server.Features.Songs
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    using static Infrastructure.WebConstants;

    [Authorize]
    public class SongsController : ApiController
    {
        private readonly ISongService songService;
        private readonly ICurrentUserService currentUserService;

        public SongsController(ISongService songService, 
            ICurrentUserService currentUserService)
        {
            this.songService = songService;
            this.currentUserService = currentUserService;
        }

        [HttpGet]
        public async Task<IEnumerable<SongListingServiceModel>> Mine()
            => await this.songService.ByUser(this.currentUserService.GetId());

        [HttpGet]
        [Route(SongDetails)]
        public async Task<ActionResult<SongDetailsServiceModel>> Details(int id)
            => await this.songService.Details(id);


        [HttpPut]
        public async Task<ActionResult> Update(UpdateSongRequestModel model)
        {
            var userId = this.currentUserService.GetId();

            var result = await this.songService.Update(
                model.Id, 
                model.Title, 
                model.Description, 
                model.ImageUrl, 
                userId);

            if (result.Failure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateSongRequestModel model)
        {
            var userId = this.currentUserService.GetId();

            var songId = await this.songService.Create(
                model.Title,
                model.Description,
                model.ImageUrl, 
                model.AudioUrl,
                model.Duration,
                userId);

            return Created(nameof(this.Create), songId);
        }

        [HttpDelete]
        [Route(Id)]
        public async Task<ActionResult> Delete(int id)
        {
            var userId = this.currentUserService.GetId();

            var result = await this.songService.Delete(id, userId);

            if (result.Failure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }

        [HttpGet]
        [Route(Id)]
        public async Task<ActionResult<PlaySongResponseModel>> GetSong(int id)
            => await this.songService.GetById(id);
        
    }
}
