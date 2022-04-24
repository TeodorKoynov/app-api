namespace App.Server.Features.Follows
{
    using App.Server.Infrastructure.Services;
    using System.Threading.Tasks;

    public interface IFollowService
    {
        public Task<Result> Follow(string userId, string followerId);
    }
}