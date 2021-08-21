using System;
using Microsoft.ML.Data;
using Newtonsoft.Json;

namespace SentimentAnalysisEngine.Domain.Models
{
    public class SentimentPrediction
    {
        [ColumnName("PredictedLabel")]
        public String Prediction { get; set; }

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
