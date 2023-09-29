using AccountService.API.ExternalServices;
using AccountService.API.ExternalServices.Interfaces;

namespace AccountService.API.Features.Accounts;

public static class ExternalServicesExtensions
{

    public static IServiceCollection AddExternalServices(this IServiceCollection services)
    {;
        return services
            .AddScoped<ITCKNValidateService, TCKNValidateService>();

    }
}
