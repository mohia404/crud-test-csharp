using Microsoft.AspNetCore.Mvc.Infrastructure;
using Phonebook.API.Common.Errors;

namespace Phonebook.API;

public static class DependencyInjectionRegister
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory, ErrorOrProblemDetailsFactory>();

        services.AddControllers();

        return services;
    }
}