using Core.Application.UseCases.Rentals.ListRentalPlans.Inbounds;

using Microsoft.Extensions.DependencyInjection;

namespace Core.Application.UseCases.Rentals.ListRentalPlans;

public static class ListRentalPlansSetup
{
    public static IServiceCollection AddListRentalPlansUseCase(this IServiceCollection services)
    {

        services.AddScoped<IListRentalPlansUseCase, ListRentalPlansUseCase>();

        return services;
    }
}
