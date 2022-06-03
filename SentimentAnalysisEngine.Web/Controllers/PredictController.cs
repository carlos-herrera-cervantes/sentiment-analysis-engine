using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ML;
using SentimentAnalysisEngine.Domain.Models;

namespace SentimentAnalysisEngine.Web.Controllers
{
    [Route("api/v1/predictions")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ApiController]
    public class PredictController : ControllerBase
    {
        #region snippet_Properties

        private readonly PredictionEnginePool<SentimentData, SentimentPrediction> _predictionEnginePool;

        private readonly ILogger _logger;

        #endregion

        #region snippet_Constructors

        public PredictController
        (
            PredictionEnginePool<SentimentData, SentimentPrediction> predictionEnginePool,
            ILogger<PredictController> logger
        )
            => (_predictionEnginePool, _logger) = (predictionEnginePool, logger);

        #endregion

        #region snippet_ActionMethods

        [HttpPost]
        public IActionResult Predict([FromBody] SentimentData input)
        {
            var prediction = _predictionEnginePool
                .Predict(modelName: "SentimentAnalysisModel", example: input);
            var sentiment = prediction?.PredictedLabel;

            _logger.LogInformation($"PREDICTION FOR: {input.Col0}");
            _logger.LogInformation($"RESULT IN: {sentiment == 1}");

            return Ok(new SuccessResponse<SentimentPrecitionResponse>
            {
                Data = new SentimentPrecitionResponse
                {
                    Sentiment = sentiment == 1,
                    SentimentText = sentiment == 1 ? "Positive" : "Negative",
                }
            });
        }

        #endregion
    }
}
