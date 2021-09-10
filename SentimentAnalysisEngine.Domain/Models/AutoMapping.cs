using AutoMapper;

namespace SentimentAnalysisEngine.Domain.Models
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Consumer, SingleConsumerDto>().PreserveReferences();
        }
    }
}