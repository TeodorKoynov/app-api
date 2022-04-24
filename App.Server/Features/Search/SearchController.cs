namespace App.Server.Features.Search
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using App.Server.Features.Search.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class SearchController : ApiController
    {
        private readonly ISearchService searchService;

        public SearchController(ISearchService searchService) => this.searchService = searchService;

        [HttpGet]
        [AllowAnonymous]
        [Route(nameof(Profiles))]
        public async Task<IEnumerable<ProfileSearchServiceModel>> Profiles(string query)
            => await this.searchService.Profiles(query);
    }
}