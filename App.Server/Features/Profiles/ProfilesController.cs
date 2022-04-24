namespace App.Server.Features.Profiles
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Infrastructure.Services;
    using Models;
    
    public class ProfilesController : ApiController
    {
        private readonly IProfileService profileService;
        private readonly ICurrentUserService currentUserService;

        public ProfilesController(IProfileService profileService,
            ICurrentUserService currentUserService)
        {
            this.profileService = profileService;
            this.currentUserService = currentUserService;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ProfileServiceModel>> Mine()
            => await this.profileService.ByUser(this.currentUserService.GetId());

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> Update(UpdateProfileRequestModel model)
        {
            var userId = this.currentUserService.GetId();

            var result = await this.profileService.Update(
                userId,
                model.UserName,
                model.Email,
                model.Name,
                model.Biography,
                model.MainPhotoUrl);

            if (result.Failure)
            {
                return BadRequest(result.Error);
            }

            return Ok();
        }
    }
}