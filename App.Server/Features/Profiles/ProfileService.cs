namespace App.Server.Features.Profiles
{
    using System.Linq;
    using System.Threading.Tasks;
    using Data;
    using App.Server.Data.Models;
    using Models;
    using Microsoft.EntityFrameworkCore;
    using Infrastructure.Services;

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
                    Name = u.Profile.Name,
                    Biography = u.Profile.Biography,
                    MainPhotoUrl = u.Profile.MainPhotoUrl
                })
                .FirstOrDefaultAsync();

        public async Task<Result> Update(
            string userId,
            string userName,
            string email,
            string name,
             string biography,
              string mainPhotoUrl)
        {
            var user = await this.data
                .Users
                .Include(u => u.Profile)
                .FirstOrDefaultAsync(u => u.Id == userId);
            
            if (user is null)
            {
                return "User does not exists!";
            }

            if (user.Profile is null)
            {
                user.Profile = new Profile();
            }
            
            var result = await this.ChangeEmail(user, userId, email);
            if (result.Failure)
            {
                return result;
            }

            result = await this.ChangeUserName(user, userId, userName);
            if (result.Failure)
            {
                return result;
            }

            this.ChangeProfile(
                user.Profile,
                name,
                biography,
                mainPhotoUrl);
   
            await this.data.SaveChangesAsync();

            return true;
        }

        private async Task<Result> ChangeEmail(User user, string userId, string email)
        {
            if (!string.IsNullOrWhiteSpace(email) && user.Email != email)
            {
                var emailExists = await this.data
                    .Users
                    .AnyAsync(u => u.Id != userId && u.Email == email);

                if (emailExists)
                {
                    return "The provided email is already taken";
                }

                user.Email = email;
            }

            return true;
        }

        private async Task<Result> ChangeUserName(User user, string userId, string userName)
        {
            if (!string.IsNullOrWhiteSpace(userName) && user.UserName != userName)
            {
                var userNameExists = await this.data
                    .Users
                    .AnyAsync(u => u.Id != userId && u.UserName == userName);

                if (userNameExists)
                {
                    return "The provided username is already taken";
                }

                user.UserName = userName;
            }
            
            return true;
        }

        private void ChangeProfile(
            Profile profile,
            string name,
            string biography,
            string mainPhotoUrl)
        {
            if (profile.Name != name)
            {
                profile.Name = name;
            }
            
            if (profile.Biography != biography)
            {
                profile.Biography = biography;
            }
            
            if (profile.MainPhotoUrl != mainPhotoUrl)
            {
                profile.MainPhotoUrl = mainPhotoUrl;
            }
        }
    }
}