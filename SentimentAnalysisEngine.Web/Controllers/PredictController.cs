using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ML;
using SentimentAnalysisEngine.Domain.Models;
using SentimentAnalysisEngine.Web.Attributes;

namespace SentimentAnalysisEngine.Web.Controllers
{
    [Route("api/v1/predictions")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ApiController]
    public class PredictController : ControllerBase
    {
        private readonly PredictionEnginePool<SentimentData, SentimentPrediction> _precitionEnginePool;

        public PredictController(PredictionEnginePool<SentimentData, SentimentPrediction> predictionEnginePool)
            => _precitionEnginePool = predictionEnginePool;

        [HttpPost]
        [AuthorizeApiKey]
        public IActionResult Predict([FromBody] SentimentData input)
        {
            var prediction = _precitionEnginePool.Predict(modelName: "SentimentAnalysisModel", example: input);
            var sentiment = prediction?.Prediction;

            return Ok(new SuccessResponse<SentimentPrecitionResponse>
            {
                Data = new SentimentPrecitionResponse
                {
                    Sentiment = sentiment == "1",
                    SentimentText = sentiment == "1" ? "Positive" : "Negative",
                }
            });
        }
    }
}
