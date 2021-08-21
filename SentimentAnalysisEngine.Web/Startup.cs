using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.ML;
using SentimentAnalysisEngine.Domain.Models;

namespace SentimentAnalysisEngine.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
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
