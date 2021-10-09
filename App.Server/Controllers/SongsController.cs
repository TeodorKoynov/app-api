namespace App.Server.Controllers
{
    using App.Server.Data;
    using App.Server.Data.Models;
    using App.Server.Infrastructure;
    using App.Server.Models.Songs;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class SongsController : ApiController
    {
        private readonly AppDbContext data;
        public SongsController(AppDbContext data)
        {
            this.data = data;
        }


        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create(CreateSongRequestModel model)
        {
            var userId = this.User.GetId();

            var song = new Song
            {
                Title = model.Title,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                AudioUrl = model.AudioUrl,
                UserId = userId
            };

            this.data.Add(song);

            await this.data.SaveChangesAsync();

            return Created(nameof(this.Create), song.Id);
        }
    }
}
