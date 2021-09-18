using Microsoft.AspNetCore.Mvc;
using SentimentAnalysisEngine.Domain.Models;
using SentimentAnalysisEngine.Repository.Repositories;
using SentimentAnalysisEngine.Repository.Extensions;
using System.Threading.Tasks;
using SentimentAnalysisEngine.Web.Models;
using AutoMapper;
using System;
using Microsoft.Azure.Cosmos.Table;
using System.Collections.Generic;

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

        private readonly IApiKey _apiKey;

        private readonly IMapper _mapper;

        #endregion

        #region snippet_Constructors

        public ConsumerController(
            IAzureTableClient azureTableClient,
            IMapper mapper,
            IApiKey apiKey
        )
        {
            _azureTableClient = azureTableClient;
            _apiKey = apiKey;
            _mapper = mapper;
        }

        #endregion

        #region snippet_Get

        [HttpGet("one")]
        public async Task<IActionResult> GetOneAsync([FromQuery] PointQueryDto query)
        {
            var cloudTable = await _azureTableClient
                .CreateIfNotExists($"{typeof(Consumer).Name}s");

            var (partitionKey, rowKey) = query;

            var finded = await cloudTable
                .GetEntityUsingPointQueryAsync<Consumer>(partitionKey, rowKey);

            return Ok(new SuccessResponse<SingleConsumerDto>
            {
                Data = _mapper.Map<SingleConsumerDto>(finded)
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] TableContinuationToken token)
        {
            var cloudTable = await _azureTableClient
                .CreateIfNotExists($"{typeof(Consumer).Name}s");

            var entities = await cloudTable
                .ExecuteQuerySegmentedAsync(new TableQuery<Consumer>(), token);

            return Ok(new SuccessListResponse<List<SingleConsumerDto>>
            {
                Data = _mapper.Map<List<SingleConsumerDto>>(entities.Results),
                ContinuationToken = entities.ContinuationToken
            });
        }

        #endregion

        #region snippet_Post

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] Consumer consumer)
        {
            var cloudTable = await _azureTableClient
                .CreateIfNotExists($"{typeof(Consumer).Name}s");

            consumer.RowKey = _apiKey.GenerateApiKey();

            var created = await cloudTable.InsertOrMergeEntityAsync<Consumer>(consumer);

            return Created("", new SuccessResponse<SingleConsumerDto>
            {
                Data = _mapper.Map<SingleConsumerDto>(created)
            });
        }

        #endregion

        #region snippet_Update

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] PointQueryDto query)
        {
            var cloudTable = await _azureTableClient
                .CreateIfNotExists($"{typeof(Consumer).Name}s");

            var (partitionKey, rowKey) = query;

            var finded = await cloudTable
                .GetEntityUsingPointQueryAsync<Consumer>(partitionKey, rowKey);

            if (finded is null) return NotFound();

            await cloudTable.DeleteEntityAsync(finded);

            finded.RowKey = _apiKey.GenerateApiKey();
            finded.UpdatedAt = DateTime.UtcNow;

            var created = await cloudTable.InsertOrMergeEntityAsync<Consumer>(finded);

            return Ok(new SuccessResponse<SingleConsumerDto>
            {
                Data = _mapper.Map<SingleConsumerDto>(created)
            });
        }

        #endregion

        #region snippet_Delete

        [HttpDelete]
        public async Task<IActionResult> DeleteOneAsync([FromQuery] PointQueryDto query)
        {
            var cloudTable = await _azureTableClient
                .CreateIfNotExists($"{typeof(Consumer).Name}s");

            var (partitionKey, rowKey) = query;

            var finded = await cloudTable
                .GetEntityUsingPointQueryAsync<Consumer>(partitionKey, rowKey);

            await cloudTable.DeleteEntityAsync(finded);

            return NoContent();
        }

        #endregion
    }
}