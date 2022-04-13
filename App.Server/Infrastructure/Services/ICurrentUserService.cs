namespace App.Server.Infrastructure.Services
{
    public interface ICurrentUserService
    {
        public string GetUserName();

        public string GetId();
    }
}