using System.Linq;

namespace App.Server.Features.Follows
{
    using App.Server.Infrastructure.Services;
    using App.Server.Data.Migrations;
    using System.Threading.Tasks;
    using App.Server.Data;

    public class FollowService : IFollowService
    {
        private readonly AppDbContext data;

        public FollowService(AppDbContext data) => this.data = data;

        public async Task<Result> Follow(string userId, string followerId)
        {
            var alreadyFollowed = this.data
                .Follows
                .Any(f => f.UserId == userId && f.FollowerId == followerId);

            if (alreadyFollowed)
            {
                return "The user is already followed!";
            }
            
            this.data.Follows.Add(new Data.Models.Follow()
            {
                UserId = userId,
                FollowerId = followerId
            });

            await this.data.SaveChangesAsync();

            return true;
        }
    }
}