using System.Threading.Tasks;
using App.Server.Infrastructure.Services;

namespace App.Server.Features.Follows
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using App.Server.Features.Follows.Models;

    [Authorize]
    public class FollowsController : ApiController
    {
        private readonly IFollowService followService;
        private readonly ICurrentUserService currentUserService;

        public FollowsController(
            IFollowService followService, 
            ICurrentUserService currentUserService)
        {
            this.followService = followService;
            this.currentUserService = currentUserService;
        }

        [HttpPost]
        public async Task<IActionResult> Follow(FollowRequestModel model)
        {
            var result = await followService.Follow(
                model.UserId,
                this.currentUserService.GetId());

            if (result.Failure)
            {
                return BadRequest(result.Error);
            }
            
            return Ok();
        }
    }
}