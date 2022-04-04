using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Soulgram.Posts.Application;

public static class ServiceInjector
{
    public static IServiceCollection AddApplicationLayerDependencies(this IServiceCollection services)
    {
        var currentAssembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(currentAssembly);
        services.AddValidatorsFromAssembly(currentAssembly);
        return services;
    }
}