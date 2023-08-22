using System.Diagnostics;

namespace TestPlatform.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApi(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<Stopwatch>();

        return services;
    }
    public static IServiceCollection AddDomain(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services;
    }
    public static IServiceCollection AddServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services;
    }
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services;
    }
    public static IServiceCollection AddTelegramBot(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services;
    }
}