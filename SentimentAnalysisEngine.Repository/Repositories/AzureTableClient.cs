using System;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;
using SentimentAnalysisEngine.Repository.Models;

namespace SentimentAnalysisEngine.Repository.Repositories
{
    public class AzureTableClient : IDisposable, IAzureTableClient
    {
        #region snippet_Properties

        private AzureTableClientOptions _azureTableClientOptions;

        private CloudStorageAccount _cloudStorageAccount;

        private CloudTableClient _cloudTableClient;

        #endregion

        #region snippet_Constructors

        public AzureTableClient(AzureTableClientOptions azureTableClientOptions)
        {
            _azureTableClientOptions = azureTableClientOptions;
            _cloudStorageAccount = CloudStorageAccount
                .Parse(azureTableClientOptions.AzureStorageConnectionString);
            _cloudTableClient = _cloudStorageAccount.CreateCloudTableClient(azureTableClientOptions);
        }

        #endregion

        #region snippet_PublicMethods

        public async Task<CloudTable> CreateIfNotExists(string table)
        {
            if (string.IsNullOrEmpty(table))
            {
                throw new ArgumentNullException("table", "table name cannot be null or empty");
            }

            var cloudTable = _cloudTableClient.GetTableReference(table);
            await cloudTable.CreateIfNotExistsAsync();

            return cloudTable;
        }

        public async Task<CloudTable> CreateIfNotExists(CloudTableOptions cloudTableOptions)
        {
            var (
                table,
                cancellationToken,
                tableRequestOptions,
                operationContext,
                serializedIndexingPolicy,
                throughPut,
                defaultTimeToLive
            ) = cloudTableOptions;

            if (string.IsNullOrEmpty(table))
            {
                throw new ArgumentNullException("table", "table name cannot be null or empty");
            }

            var cloudTable = _cloudTableClient.GetTableReference(table);

            await cloudTable.CreateIfNotExistsAsync(
                tableRequestOptions,
                operationContext,
                serializedIndexingPolicy,
                throughPut,
                defaultTimeToLive,
                cancellationToken
            );

            return cloudTable;
        }

        public async Task<CloudTable> CreateIfNotExists(IndexingTableOptions indexingTableOptions)
        {
            var (
                table,
                cancellationToken,
                indexingMode,
                throughPut,
                defaultTimeToLive
            ) = indexingTableOptions;

            if (string.IsNullOrEmpty(table))
            {
                throw new ArgumentNullException("table", "table name cannot be null or empty");
            }

            var cloudTable = _cloudTableClient.GetTableReference(table);

            await cloudTable.CreateIfNotExistsAsync(
                indexingMode,
                throughPut,
                defaultTimeToLive,
                cancellationToken
            );

            return cloudTable;
        }

        public void Dispose()
        {
            try
            {
                Dispose(true);
            }
            catch (Exception)
            {

            }
        }

        #endregion

        #region snippet_PrivateMethods

        private void Dispose(bool isDispose)
        {
            if (isDispose)
            {
                _cloudStorageAccount = null;
                _azureTableClientOptions = null;
                _cloudTableClient = null;
            }
        }

        #endregion
    }
}