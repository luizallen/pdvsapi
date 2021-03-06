﻿using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PdvApi.AutoMapper.Profiles;
using PdvApi.Infrastructure.Repositories;
using PdvApi.Infrastructure.Repositories.Abstractions;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using System;

namespace PdvApi
{
    public partial class Startup
    {
        public void ConfigureDependencyInjection(IServiceCollection services)
        {
            var postgresConnectionString = Configuration.GetConnectionString("Postgres");

            services.AddTransient<IPdvQueryRepository>(c => new PdvQueryRepository(postgresConnectionString));
            services.AddTransient<IPdvCommandRepository>(c => new PdvCommandRepository(postgresConnectionString));
            services.AddTransient<IMapper>(c => GetAutoMapperInstance());
            services.AddTransient<ILogger>(c => GetLoggerInstance());
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

        private Logger GetLoggerInstance()
        {
            var elasticUri = Configuration["ElasticConfiguration:Uri"];

            return new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Debug()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200/"))
                {
                    AutoRegisterTemplate = true,
                    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
                    MinimumLogEventLevel = LogEventLevel.Verbose
                })
                .CreateLogger();
        }
    }
}
