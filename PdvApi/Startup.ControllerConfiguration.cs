using BAMCIS.GeoJSON.Serde;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PdvApi.Validators;
using System.Collections.Generic;

namespace PdvApi
{
    public partial class Startup
    {
        public void ConfigureController(IServiceCollection services)
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
        }
    }
}
