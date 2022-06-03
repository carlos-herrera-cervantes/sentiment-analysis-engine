using Microsoft.ML.Data;

namespace SentimentAnalysisEngine.Domain.Models
{
    public class SentimentData
    {
        [ColumnName(@"col0"), LoadColumn(0)]
        public string Col0 { get; set; }

        [ColumnName(@"col1"), LoadColumn(1)]
        public float Col1 { get; set; }
    }
}
