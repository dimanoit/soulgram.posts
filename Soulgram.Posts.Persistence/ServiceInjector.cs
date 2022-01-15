using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;

namespace Soulgram.Posts.Persistence
{
    public static class ServiceInjector
    {
        public static void AddElasticContext(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var elasticOption = configuration
                .GetSection("Elastic")
                .Get<ElasticOption>();

            serviceCollection.AddSingleton<IElasticClient>(sp =>
            {
                // pass parameters via configuration
                var url = new Uri(elasticOption.Url);
                var settings = new ConnectionSettings(url).DefaultIndex(elasticOption.Index);

                var client = new ElasticClient(settings);
                return client;
            });
        }
    }
}
