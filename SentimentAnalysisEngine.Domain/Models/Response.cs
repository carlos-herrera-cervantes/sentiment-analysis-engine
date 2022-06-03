using Newtonsoft.Json;

namespace SentimentAnalysisEngine.Domain.Models
{
    public class BaseResponse
    {
        [JsonProperty("status")]
        public bool Status { get; set; } = true;
    }

    public class FailedResponse : BaseResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }
    }

    public class SuccessResponse<T> : BaseResponse where T : class
    {
        [JsonProperty("data")]
        public T Data { get; set; }
    }
}
