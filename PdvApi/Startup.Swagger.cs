using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace PdvApi
{
    public partial class Startup
    {
        public void ConfigureSwagger(IServiceCollection services)
        {
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
        }
    }
}
