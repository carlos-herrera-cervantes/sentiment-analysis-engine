using Microsoft.ML.Data;
using Newtonsoft.Json;

namespace SentimentAnalysisEngine.Domain.Models
{
    public class SentimentPrediction
    {
        [ColumnName(@"col0")]
        public float[] Col0 { get; set; }

        [ColumnName(@"col1")]
        public uint Col1 { get; set; }

        [ColumnName(@"Features")]
        public float[] Features { get; set; }

        [ColumnName(@"PredictedLabel")]
        public float PredictedLabel { get; set; }

        [ColumnName(@"Score")]
        public float[] Score { get; set; }
    }

    public class SentimentPrecitionResponse
    {
        [JsonProperty("sentiment")]
        public bool Sentiment { get; set; }

        [JsonProperty("sentimentText")]
        public string SentimentText { get; set; }
    }
}
