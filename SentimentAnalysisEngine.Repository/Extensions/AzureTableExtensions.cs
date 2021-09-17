using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SentimentAnalysisEngine.Repository.Models;
using SentimentAnalysisEngine.Repository.Repositories;

namespace SentimentAnalysisEngine.Repository.Extensions
{
    public static class AzureTableExtensions
    {
        #region snippet_ServicesExtensions

        public static IServiceCollection AddAzureTableStorage(
            this IServiceCollection services,
            Action<AzureTableClientOptions> setupOptions
        )
        {
            services.Configure<AzureTableClientOptions>(setupOptions);
            services.AddScoped<IAzureTableClient>(provider =>
            {
                var options = provider.GetRequiredService<IOptions<AzureTableClientOptions>>();
                return new AzureTableClient(options.Value);
            });

            return services;
        }

        #endregion

        #region snippet_CloudTableExtensions

        public static async Task<T> InsertOrMergeEntityAsync<T>(this CloudTable table, T entity)
            where T : TableEntity, ITableEntity
        {
            if (entity is null)
            {
                throw new ArgumentNullException("entity", "entity cannot be null");
            }

            if (table is null)
            {
                throw new ArgumentNullException("table", "table cannot be null");
            }

            var operation = TableOperation.InsertOrMerge(entity);
            var result = await table.ExecuteAsync(operation);
            
            return (T)result.Result;
        }

        public static async Task<T> GetEntityUsingPointQueryAsync<T>(
            this CloudTable table,
            string partitionKey,
            string rowKey
        ) where T : TableEntity, ITableEntity
        {
            if (table is null)
            {
                throw new ArgumentNullException("table", "table cannot be null");
            }

            if (string.IsNullOrEmpty(partitionKey))
            {
                throw new ArgumentNullException("partitionKey", "partitionKey cannot be null");
            }

            if (string.IsNullOrEmpty(rowKey))
            {
                throw new ArgumentNullException("rowKey", "rowKey cannot be null");
            }

            var operation = TableOperation.Retrieve<T>(partitionKey, rowKey);
            var result = await table.ExecuteAsync(operation);

            return result.Result as T;
        }

        public static async Task DeleteEntityAsync<T>(this CloudTable table, T entity)
            where T : TableEntity, ITableEntity
        {
            if (table is null)
            {
                throw new ArgumentNullException("table", "table cannot be null");
            }

            if (entity is null)
            {
                throw new ArgumentNullException("entity", "entity cannot be null");
            }

            var operation = TableOperation.Delete(entity);
            await table.ExecuteAsync(operation);
        }

        #endregion
    }
}