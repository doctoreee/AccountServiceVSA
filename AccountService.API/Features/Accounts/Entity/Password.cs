using AccountService.API.Common;

namespace AccountService.API.Features.Accounts.Entity;

public class Password: Entity<long>
{
    public long AccountId { get; set; }
    public string PasswordHash { get; set; }
    public string CreateDate { get; set; }

    public Password() { }

    public Password(long accountId, string passwordHash)
    {
        AccountId = accountId;
        PasswordHash = passwordHash;
        CreateDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
}

