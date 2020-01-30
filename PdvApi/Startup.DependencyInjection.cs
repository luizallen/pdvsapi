using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using PdvApi.AutoMapper.Profiles;
using PdvApi.Infrastructure.Repositories;
using PdvApi.Infrastructure.Repositories.Abstractions;

namespace PdvApi
{
    public partial class Startup
    {
        public void ConfigureDependencyInjection(IServiceCollection services)
        {
            services.AddTransient<IPdvRepository, PdvRepository>();
            services.AddTransient<IMapper>(c => GetAutoMapperInstance());
        }

        private Mapper GetAutoMapperInstance()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<PdvProfile>();
                cfg.AddProfile<PdvDtoProfile>();
            });

            config.AssertConfigurationIsValid();

           return new Mapper(config);
        }
    }
}
