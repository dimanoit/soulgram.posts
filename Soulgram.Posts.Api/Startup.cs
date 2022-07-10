using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Soulgram.Posts.Api.Filters;
using Soulgram.Posts.Application;
using Soulgram.Posts.Infrastructure;
using Soulgram.Posts.Persistence;

namespace Soulgram.Posts.Api;

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
        //TODO get all values from config, and policy for token validation
        //TODO add authorization by scope
        //services.AddAuthentication("Bearer")
        //    .AddJwtBearer("Bearer", config =>
        //    {
        //        config.Authority = "https://localhost:5002/";
        //        config.RequireHttpsMetadata = false;
        //        config.TokenValidationParameters = new TokenValidationParameters
        //        {
        //            ValidateAudience = false
        //        };
        //    });

        services
            .AddControllers(o =>
            {
                o.Filters.Add<ValidationFilter>();
                o.Filters.Add(new ProducesAttribute("application/json"));
            })
            .AddNewtonsoftJson()
            .AddFluentValidation();

        services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        }));
        services.AddElasticContext(Configuration);
        services.AddInfrastructure(Configuration);
        services.AddApplication(Configuration);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            IdentityModelEventSource.ShowPII = true;
        }

        app.UseHttpsRedirection();

        app.UseRouting();
        app.UseCors("MyPolicy");
        //app.UseAuthentication();
        //app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}