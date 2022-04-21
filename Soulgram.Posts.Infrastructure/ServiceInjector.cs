using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Soulgram.File.Manager;
using Soulgram.File.Manager.Interfaces;
using Soulgram.File.Manager.Models;
using Soulgram.Posts.Application.Services;

namespace Soulgram.Posts.Infrastructure;

public static class ServiceInjector
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddFileManager(configuration); TODO add after AZURE Integration
        services.AddLocalFileManager();
        services.AddScoped<ICurrentDateProvider, CurrentDateProvider>();
        return services;
    }

    private static void AddFileManager(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<BlobStorageOptions>(options => configuration.GetSection("BlobStorageOptions").Bind(options));
        services.AddScoped<IContainerNameResolver, ContainerNameResolver>();
        services.AddScoped<IFileManager, FileManager>();
    }
    
    private static void AddLocalFileManager(this IServiceCollection services)
    {
        services.AddScoped<IFileManager, LocalFileManager>();
    }
}