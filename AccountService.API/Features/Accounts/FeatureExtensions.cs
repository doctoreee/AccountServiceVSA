using AccountService.API.Common.Configuration;
using AccountService.API.Features.Accounts.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace AccountService.API.Features.Accounts;

public static class FeatureExtensions
{

    public static IServiceCollection AddAccountFeature(this IServiceCollection services, IConfiguration configuration)
    {
        var sqliteSetting = configuration.GetSection("ServiceSettings:DatabaseSettings:SqliteSettings").Get<SqliteSettings>();
        var sqliteFilePath = string.Format(sqliteSetting.AccountDbFileName, Directory.GetParent(Environment.CurrentDirectory));
        return services
            .AddDbContext<AccountDbContext>(options => options.UseSqlite(sqliteFilePath))
            .AddScoped<IAccountRepository, AccountRepository>()
            .AddScoped<IPasswordRepository, PasswordRepository>();

    }
}
