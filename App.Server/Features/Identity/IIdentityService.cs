namespace App.Server.Features.Identity
{
    using App.Server.Data.Models;

    public interface IIdentityService
    {
        string GenerateJwtToken(string userId, string userName, string secret);
    }
}
