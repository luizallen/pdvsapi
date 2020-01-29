using Microsoft.Extensions.DependencyInjection;
using PdvApi.Infrastructure.Repositories;
using PdvApi.Infrastructure.Repositories.Abstractions;

namespace PdvApi
{
    public partial class Startup
    {
        public void ConfigureDependencyInjection(IServiceCollection services)
        {
            services.AddTransient<IPdvRepository, PdvRepository>();
        }
    }
}
