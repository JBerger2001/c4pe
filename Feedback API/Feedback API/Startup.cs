using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Feedback_API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Feedback_API.Configuration;
using Microsoft.OpenApi.Models;
using AutoMapper;

namespace Feedback_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly string AllowAllOrigins = "AllowAllOrigins";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(AllowAllOrigins,
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            var connectionString = Configuration["ConnectionString"];
            services.AddDbContext<FeedbackContext>(opt => opt.UseMySql(connectionString));

            services.AddAutoMapper(typeof(Startup));

            services.AddControllers()
                    .AddJsonOptions(options =>
                        options.JsonSerializerOptions.Converters.Add(new TimeSpanToStringConverter()));

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "Feedback API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var swaggerConfig = new SwaggerConfig();
            Configuration.GetSection(nameof(SwaggerConfig)).Bind(swaggerConfig);

            app.UseSwagger(options => options.RouteTemplate = swaggerConfig.JsonRoute);

            app.UseSwaggerUI(options => options.SwaggerEndpoint(swaggerConfig.UIEndpoint, swaggerConfig.Description));

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(AllowAllOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
