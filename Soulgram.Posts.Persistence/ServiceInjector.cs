using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using Soulgram.Posts.Domain;
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

            CreateIndexIfNoExist(elasticOption);

            serviceCollection.AddSingleton<IElasticClient>(_ => GetElasticClient(elasticOption));
        }

        private static void CreateIndexIfNoExist(ElasticOption elasticOption)
        {
            var client = GetElasticClient(elasticOption);

            if (client.Indices.Exists(elasticOption.Index).Exists)
            {
                return;
            }

            var response = client
                .Indices
                .Create(elasticOption.Index, c => c.Map<Post>(m => m.AutoMap()));

            if (!response.IsValid)
            {
                // TODO create own exception classes and log exception
                throw new Exception("Elastic Search create index exception", response.OriginalException);
            }
        }

        private static ElasticClient GetElasticClient(ElasticOption elasticOption)
        {
            var url = new Uri(elasticOption.Url);
            var settings = new ConnectionSettings(url)
                .DefaultIndex(elasticOption.Index);

            var client = new ElasticClient(settings);
            return client;
        }
    }
}
