using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.ML;
using SentimentAnalysisEngine.Domain.Models;
using SentimentAnalysisEngine.Repository.Extensions;
using SentimentAnalysisEngine.Repository.Repositories;

namespace SentimentAnalysisEngine.Web
{
    public class Startup
    {
        public IConfiguration Configuration;

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<IApiKey, ApiKey>();
            services.AddAutoMapperConfiguration();
            services.AddAzureTableStorage(options =>
            {
                options.AzureStorageConnectionString = Configuration
                    .GetSection("ConnectionStrings")
                    .GetSection("TableStorage").Value;
            });
            services.AddPredictionEnginePool<SentimentData, SentimentPrediction>()
                .FromFile
                (
                    modelName: "SentimentAnalysisModel",
                    filePath: "MLModels/MLModel.zip",
                    watchForChanges: true
                );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
