using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RefrigeratorServerSide.Data;
using RefrigeratorServerSide.Data.RefriRepo;

namespace RefrigeratorServerSide
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public bool IsDevelopment => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<RefrigeratorContext>(opt => opt.UseSqlServer
                (Configuration.GetConnectionString("RefrigeratorConnection")));

            Console.WriteLine($"{nameof(IsDevelopment)}={IsDevelopment}");
            var foo = Environment.GetEnvironmentVariables();
            if (this.IsDevelopment)
            {
                //services.AddScoped<IPlaceRepo, MockPlaceRepo>();                
            }
            else 
            {
                //services.AddScoped<IPlaceRepo, SqlPlaceRepo>();    
                services.AddScoped<IRefriRepo, SqlReftiRepo>();    
            }


            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
