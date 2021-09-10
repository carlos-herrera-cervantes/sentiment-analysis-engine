using System.Threading.Tasks;
using Microsoft.Azure.Cosmos.Table;
using SentimentAnalysisEngine.Repository.Models;

namespace SentimentAnalysisEngine.Repository.Repositories
{
    public interface IAzureTableClient
    {
        Task<CloudTable> CreateIfNotExists(string table);

        Task<CloudTable> CreateIfNotExists(CloudTableOptions cloudTableOptions);

        Task<CloudTable> CreateIfNotExists(IndexingTableOptions indexingTableOptions);
    }
}