using BAMCIS.GeoJSON.Serde;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PdvApi.Validators;
using System;
using System.Collections.Generic;
using JsonConverter = Newtonsoft.Json.JsonConverter;

namespace PdvApi
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(j =>
                {
                    j.SerializerSettings.Converters = new List<JsonConverter>
                    {
                        new MultiPolygonConverter(),
                        new InheritanceBlockerConverter()
                    };
                })
                .AddFluentValidation(
                    fv =>
                    {
                        fv.RegisterValidatorsFromAssemblyContaining<PdvRequestValidator>();
                        fv.ImplicitlyValidateChildProperties = true;
                    });

            services.AddHealthChecks();
            services.AddSwaggerGen(c => {

                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "PDV's Api",
                        Version = "v1",
                        Description = "Api para cadastro e consulta de PDVs",
                        Contact = new OpenApiContact
                        {
                            Name = "Luiz Augusto Frambach",
                            Url = new Uri("https://github.com/luizallen")
                        }
                    });
            });

            ConfigureDependencyInjection(services);
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
            app.UseHealthChecks("/healthcheck");

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pdv's Api v1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
