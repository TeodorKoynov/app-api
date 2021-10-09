namespace App.Server.Features.Songs
{
    using System.Threading.Tasks;

    public interface ISongService
    {
        public Task<int> Create(string title, string description, string imageUrl, string audioUrl, string userId);
    }
}
