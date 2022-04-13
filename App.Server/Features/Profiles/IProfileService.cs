namespace App.Server.Features.Profiles
{
    using System.Threading.Tasks;
    using Models;
    
    public interface IProfileService
    {
        public Task<ProfileServiceModel> ByUser(string userId);
    }
}