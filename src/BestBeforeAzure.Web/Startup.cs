using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BestBeforeAzure.Application.Products;
using BestBeforeAzure.Domain.Products;
using BestBeforeAzure.Domain.SharedKernel;
using BestBeforeAzure.Infrastructure.SharedKernel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using BestBeforeAzure.Web.Data;
using MediatR;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BestBeforeAzure.Web
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(
                    _configuration.GetConnectionString("DefaultConnection")));
            
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
                .AddScoped<IRepository<Product>, Repository<Product>>()
                .AddMediatR(typeof(AddProductCommand));
            
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
