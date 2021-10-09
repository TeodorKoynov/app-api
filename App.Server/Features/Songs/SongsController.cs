namespace App.Server.Features.Songs
{
    using App.Server.Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class SongsController : ApiController
    {
        private readonly ISongService songService;

        public SongsController(ISongService songService)
        {
            this.songService = songService;
        }

        [Authorize]
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
