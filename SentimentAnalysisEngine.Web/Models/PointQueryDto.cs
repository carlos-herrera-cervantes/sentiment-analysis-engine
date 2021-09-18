using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace SentimentAnalysisEngine.Web.Models
{
    public class PointQueryDto
    {
        #region snippet_Properties

        [Required]
        public string PartitionKey { get; set; }

        [Required]
        public string RowKey { get; set; }

        #endregion

        #region snippet_Deconstructors

        public void Deconstruct(out string partitionKey, out string rowKey)
            => (partitionKey, rowKey) = (PartitionKey, RowKey);

        #endregion
    }
}