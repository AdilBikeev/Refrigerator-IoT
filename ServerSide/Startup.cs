using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
                services.AddSingleton<IPlaceRepo, MockPlaceRepo>();                
            }
            else 
            {
                services.AddSingleton<IPlaceRepo, SqlPlaceRepo>();    
                services.AddSingleton<IRefriRepo, SqlReftiRepo>();    
            }

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(c => {
                c.SerializeAsV2 = true;
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", nameof(RefrigeratorServerSide));
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
