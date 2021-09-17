using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SentimentAnalysisEngine.Domain.Models;

namespace SentimentAnalysisEngine.Repository.Extensions
{
    public static class AutoMapperExtensions
    {
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new AutoMapping()));
            var mapper = mapperConfig.CreateMapper();

            services.AddSingleton(mapper);
            return services;
        }
    }
}