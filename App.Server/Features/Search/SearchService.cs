namespace App.Server.Features.Search
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using App.Server.Features.Search.Models;
    using System.Linq;
    using App.Server.Data;
    using Microsoft.EntityFrameworkCore;

    public class SearchService : ISearchService
    {
        private readonly AppDbContext data;

        public SearchService(AppDbContext data) => this.data = data;

        public async Task<IEnumerable<ProfileSearchServiceModel>> Profiles(string query)
            => await this.data
                .Users
                .Where(u => u.UserName.Contains(query) || 
                            u.Profile.Name.Contains(query))
                .Select(u => new ProfileSearchServiceModel()
                {
                    UserId = u.Id,
                    UserName = u.UserName,
                    ProfilePhotoUrl = u.Profile.MainPhotoUrl
                })
                .ToListAsync();
    }
}