using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Soulgram.Posts.Infrastracture.Mapper.Profiles;

namespace Soulgram.Posts.Infrastracture
{
    public static class ServiceInjector
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services) =>
            services.AddSingleton(_ => CreateMapper());

        public static IMapper CreateMapper()
        {
            var mapperConfiguration =
                new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<UserInfoDomainUserInfoProfile>();
                    cfg.AddProfile<PostDomainPostProfile>();
                });

            mapperConfiguration.AssertConfigurationIsValid();
            return mapperConfiguration.CreateMapper();
        }
    }
}
