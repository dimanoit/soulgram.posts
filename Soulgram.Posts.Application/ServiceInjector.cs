using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Soulgram.Logging;
using Soulgram.Logging.Models;

namespace Soulgram.Posts.Application;

public static class ServiceInjector
{
    public static IServiceCollection AddApplication(this IServiceCollection services,
        IConfiguration configuration)
    {
        var currentAssembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(currentAssembly);
        services.AddValidatorsFromAssembly(currentAssembly);
        services.AddLogging(configuration);

        return services;
    }


    private static IServiceCollection AddLogging(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var loggingSettings = configuration
            .GetSection("LoggingSettings")
            .Get<LoggingSettings>();

        return services.AddLogging(loggingSettings);
    }
}