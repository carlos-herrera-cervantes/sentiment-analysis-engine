using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace SentimentAnalysisEngine.Web.Models
{
    public class PointQueryDto
    {
        [FromQuery(Name = "partitionKey")]
        [Required]
        public string PartitionKey { get; set; }

        [FromQuery(Name = "rowKey")]
        [Required]
        public string RowKey { get; set; }
    }
}