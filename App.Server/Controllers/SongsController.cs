namespace App.Server.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class SongsController : ApiController
    {
        [HttpPost]
        public async Task<ActionResult<int>> Create()
        {
            return;
        }
    }
}
