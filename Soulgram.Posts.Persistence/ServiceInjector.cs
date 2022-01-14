using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;

namespace Soulgram.Posts.Persistence
{
    public static class ServiceInjector
    {
        public static void AddElasticContext(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IElasticClient>(sp =>
            {
                // pass parameters via configuration
                var url = new Uri("http://localhost:9200/");
                var settings = new ConnectionSettings(url);

                var client = new ElasticClient(settings);

                return client;
            });


        }
    }
}
