
namespace AccountService.API.Helpers;

public static class CryptoHelper
{
    public static string PasswordHash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword("Pa$$w0rd");
    }

    public static bool VerifyPassword(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash); ;
    }
}
