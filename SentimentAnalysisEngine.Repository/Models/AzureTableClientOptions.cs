using Microsoft.Azure.Cosmos.Table;

namespace SentimentAnalysisEngine.Repository.Models
{
    public class AzureTableClientOptions: TableClientConfiguration
    {
        public string AzureStorageConnectionString { get; set; }

        public AzureTableClientOptions() {}

        public AzureTableClientOptions(string azureStorageConnectionString)
            => AzureStorageConnectionString = azureStorageConnectionString;
    }
}