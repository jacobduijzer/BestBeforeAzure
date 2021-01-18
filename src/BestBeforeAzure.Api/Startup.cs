using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BestBeforeAzure.Application.Products;
using BestBeforeAzure.Domain.Products;
using BestBeforeAzure.Domain.SharedKernel;
using BestBeforeAzure.Infrastructure.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;
using Microsoft.OpenApi.Models;

namespace BestBeforeAzure.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BestBeforeDbContext>(
                    options =>
                    {
                        options.UseCosmos(
                            _configuration["AzureSettings:CosmosDbConnectionString"],
                            _configuration["AzureSettings:CosmosDbDatabase"], cosmosOptions =>
                            {
                                cosmosOptions.ConnectionMode(ConnectionMode.Gateway);
                            });
                    })
                .AddLogging(options =>
                {
                    options.AddConsole();
                    options.SetMinimumLevel(LogLevel.Trace);
                    
                    // hook the Application Insights Provider
                    options.AddFilter<ApplicationInsightsLoggerProvider>("", LogLevel.Trace);

                    // pass the InstrumentationKey provided under the appsettings
                    options.AddApplicationInsights(_configuration["ApplicationInsights:InstrumentationKey"]);
                })
                .AddScoped<IRepository<Product>, Repository<Product>>()
                .AddMediatR(typeof(AddProductCommand))
                .AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo {Title = "BestBeforeAzure.Api", Version = "v1"}))
                .AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BestBeforeAzure.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
