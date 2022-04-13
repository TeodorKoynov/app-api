namespace App.Server.Features.Profiles
{
    using System.Linq;
    using System.Threading.Tasks;
    using App.Server.Data;
    using App.Server.Features.Profiles.Models;
    using Microsoft.EntityFrameworkCore;

    public class ProfileService : IProfileService
    {
        private readonly AppDbContext data;

        public ProfileService(AppDbContext data) => this.data = data;

        public async Task<ProfileServiceModel> ByUser(string userId)
            => await this.data
                .Users
                .Where(u => u.Id == userId)
                .Select(u => new ProfileServiceModel
                {
                    Biography = u.Profile.Biography,
                    MainPhotoUrl = u.Profile.MainPhotoUrl
                })
                .FirstOrDefaultAsync();
    }
}