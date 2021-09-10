using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Azure.Cosmos.Table;
using Newtonsoft.Json;

namespace SentimentAnalysisEngine.Domain.Models
{
    public class Consumer : TableEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [JsonProperty("name")]
        [Required]
        public string Name { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

    public class SingleConsumerDto
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("partitionKey")]
        public string PartitionKey { get; set; }

        [JsonProperty("rowKey")]
        public string RowKey { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }
    }
}