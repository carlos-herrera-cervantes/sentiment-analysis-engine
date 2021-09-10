using System.Threading;
using Microsoft.Azure.Cosmos.Table;

namespace SentimentAnalysisEngine.Repository.Models
{
    public class CloudTableOptions
    {
        #region snippet_Properties

        public string Table { get; set; }

        public CancellationToken CancellationToken { get; set; }

        public TableRequestOptions TableRequestOptions { get; set; }

        public OperationContext OperationContext { get; set; }

        public string SerializedIndexingPolicy { get; set; }

        public int? ThroughPut { get; set; } = null;

        public int? DefaultTimeToLive { get; set; } = null;

        #endregion

        #region snippet_Destructures

        public void Deconstruct(
            out string table,
            out CancellationToken cancellationToken,
            out TableRequestOptions tableRequestOptions,
            out OperationContext operationContext,
            out string serializedIndexingPolicy,
            out int? throughPut,
            out int? defaultTimeToLive
        )
        {
            table = Table;
            cancellationToken = CancellationToken;
            tableRequestOptions = TableRequestOptions;
            operationContext = OperationContext;
            serializedIndexingPolicy = SerializedIndexingPolicy;
            throughPut = ThroughPut;
            defaultTimeToLive = DefaultTimeToLive;
        }

        #endregion
    }
}