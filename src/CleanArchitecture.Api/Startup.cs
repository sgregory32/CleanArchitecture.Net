using AutoMapper;
using CleanArchitecture.Api.Filters;
using CleanArchitecture.Core.Interfaces;
using CleanArchitecture.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;

namespace CleanArchitecture.Api
{
    public class Startup
    {
        public Startup(IConfiguration config) => this.Configuration = config;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                builder =>
                {
                    builder.WithOrigins(Configuration["CorsDomains"]).AllowAnyHeader().AllowAnyMethod();
                });
            });
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CleanArchitetureDbContext")));
            services.AddAutoMapper(typeof(Startup));
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "CleanArchitecture V1", Version = "v1" }));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(AsyncRepository<>));
            services.AddScoped<ValidateModelFilter>();
            services.AddScoped<ApiExceptionFilter>();
            services.AddScoped<ValidateCategoryExistsFilter>();
            services.AddScoped<ValidateProductExistsFilter>();
            services.AddControllers();
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory
                .AddSerilog();

            app.UseHsts();
            app.UseRouting();
            app.UseCors();
            app.UseCookiePolicy();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CleanArchitecture V1"));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
