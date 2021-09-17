using Microsoft.AspNetCore.Mvc;
using SentimentAnalysisEngine.Domain.Models;
using SentimentAnalysisEngine.Repository.Repositories;
using SentimentAnalysisEngine.Repository.Extensions;
using System.Threading.Tasks;
using SentimentAnalysisEngine.Web.Models;
using AutoMapper;

namespace SentimentAnalysisEngine.Web.Controllers
{
    [Route("api/v1/consumers")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [ApiController]
    public class ConsumerController : ControllerBase
    {
        #region snippet_Properties

        private readonly IAzureTableClient _azureTableClient;

        private readonly IMapper _mapper;

        #endregion

        #region snippet_Constructors

        public ConsumerController(IAzureTableClient azureTableClient, IMapper mapper)
            => (_azureTableClient, _mapper) = (azureTableClient, mapper);

        #endregion

        #region snippet_Get

        [HttpGet]
        public async Task<IActionResult> GetOneAsync([FromQuery] PointQueryDto query)
        {
            var cloudTable = await _azureTableClient
                .CreateIfNotExists($"{typeof(Consumer).Name}s");

            var finded = await cloudTable
                .GetEntityUsingPointQueryAsync<Consumer>(query.PartitionKey, query.RowKey);

            return Ok(new SuccessResponse<SingleConsumerDto>
            {
                Data = _mapper.Map<SingleConsumerDto>(finded)
            });
        }

        #endregion

        #region snippet_Post

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Consumer consumer)
        {
            var cloudTable = await _azureTableClient
                .CreateIfNotExists($"{typeof(Consumer).Name}s");

            var created = await cloudTable.InsertOrMergeEntityAsync<Consumer>(consumer);

            return Ok(new SuccessResponse<SingleConsumerDto>
            {
                Data = _mapper.Map<SingleConsumerDto>(created)
            });
        }

        #endregion

        #region snippet_Delete

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOneAsync([FromQuery] PointQueryDto query)
        {
            var cloudTable = await _azureTableClient
                .CreateIfNotExists($"{typeof(Consumer).Name}s");

            var finded = await cloudTable
                .GetEntityUsingPointQueryAsync<Consumer>(query.PartitionKey, query.RowKey);

            await cloudTable.DeleteEntityAsync(finded);

            return NoContent();
        }

        #endregion
    }
}