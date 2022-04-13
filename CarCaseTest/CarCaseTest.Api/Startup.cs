using CarCaseTest.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using CarCaseTest.Infrastructure.Repositories;
using CarCaseTest.Business.Interfaces;
using CarCaseTest.Business.Services;
using CarCaseTest.Business.Search;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace CarCaseTest.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AdvertContext>(item => item.UseSqlServer(Configuration.GetConnectionString("sqlserver")));
            services.AddControllers();

            #region Dependencies
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ElasticSearchManager>();

            services.AddTransient<IAdvertRepository, AdvertRepository>();
            services.AddTransient<IAdvertVisitRepository, AdvertVisitRepository>();
            services.AddTransient<IAdvertService, AdvertService>();
            services.AddTransient<IAdvertVisitService, AdvertVisitService>();
            #endregion

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CarCaseTest.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CarCaseTest.Api v1"));
            }

            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync("Internal Server Error");
                });
            });

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
