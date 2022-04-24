namespace App.Server.Features.Profiles
{
    using System.Threading.Tasks;
    using Infrastructure.Services;
    using Models;
    
    public interface IProfileService
    {
        public Task<ProfileServiceModel> ByUser(string userId);
        
        public Task<Result> Update(string userId, string userName, string email, string name, string biography, string mainPhotoUrl);

    }
}