using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Soulgram.File.Manager;
using Soulgram.File.Manager.Interfaces;
using Soulgram.File.Manager.Models;
using Soulgram.Posts.Infrastructure.Mapper.Profiles;

namespace Soulgram.Posts.Infrastructure;

public static class ServiceInjector
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(_ => CreateMapper());
        services.AddFileManager(configuration);

        return services;
    }

    private static IMapper CreateMapper()
    {
        var mapperConfiguration =
            new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<PostsProfile>();
                cfg.AddProfile<PostMetadataProfile>();
                cfg.AddProfile<FileManagementProfile>();
            });

        mapperConfiguration.AssertConfigurationIsValid();
        return mapperConfiguration.CreateMapper();
    }

    private static void AddFileManager(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<BlobStorageOptions>(options => configuration.GetSection("BlobStorageOptions").Bind(options));
        services.AddScoped<IContainerNameResolver, ContainerNameResolver>();
        services.AddScoped<IFileManager, FileManager>();
    }
}