namespace App.Server.Infrastructure.Extentions
{
    using System.Linq;
    using System.Security.Claims;

    public static class IdentityExtentions
    {
        public static string GetId(this ClaimsPrincipal user)
            => user
                .Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                ?.Value;
    }
}
