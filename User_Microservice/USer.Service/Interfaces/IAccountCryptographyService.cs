namespace User.API.Interfaces
{
    public interface IAccountCryptographyService
    {
        string HashPassword(string password);

        bool Authorize(string password, string dbPassword);
    }
}
