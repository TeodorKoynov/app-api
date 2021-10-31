namespace App.Server.Features.Playlist
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    using static Infrastructure.WebConstants;


    public class PlaylistController : ApiController
    {
        [HttpGet]
        public IActionResult Mine()
        {
            return Ok();
        }

        [HttpGet]
        [Route(Id)]
        public async Task<ActionResult> Details(int id)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> Create()
        {
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update()
        {
            return Ok();
        }

        [HttpDelete]
        [Route(Id)]
        public async Task<ActionResult> Delete(int id)
        {
            return Ok();
        }

        [HttpGet]
        [Route(SpecificSong)]
        public async Task<ActionResult> Song(int? albumId, int songId)
        {
            return Ok();
        }

        [HttpGet]
        [Route(AllSongs)]
        public async Task<ActionResult> GetAllSongs(int albumId)
        {
            return Ok();
        }
    }
}
