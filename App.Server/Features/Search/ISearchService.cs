namespace App.Server.Features.Search
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using App.Server.Features.Search.Models;

    public interface ISearchService
    {
        public Task<IEnumerable<ProfileSearchServiceModel>> Profiles(string query);
    }
}