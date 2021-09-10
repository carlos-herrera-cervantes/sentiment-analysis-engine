using System.Threading;
using Microsoft.Azure.Cosmos;

namespace SentimentAnalysisEngine.Repository.Models
{
    public class IndexingTableOptions
    {
        #region snippet_Properties

        public string Table { get; set; }

        public CancellationToken CancellationToken { get; set; }

        public IndexingMode IndexingMode { get; set; }

        public int? ThroughPut { get; set; } = null;

        public int? DefaultTimeToLive { get; set; } = null; 

        #endregion

        #region snippet_Destructures

        public void Deconstruct(
            out string table,
            out CancellationToken cancellationToken,
            out IndexingMode indexingMode,
            out int? throughPut,
            out int? defaultTimeToLive
        )
        {
            table = Table;
            cancellationToken = CancellationToken;
            indexingMode = IndexingMode;
            throughPut = ThroughPut;
            defaultTimeToLive = DefaultTimeToLive;
        }

        #endregion
    }
}