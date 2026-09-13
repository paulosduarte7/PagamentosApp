using Microsoft.Extensions.DependencyInjection;
using PagamentosApp.Domain.Interfaces;
using PagamentosApp.Infrastructure.Integrations;
using PagamentosApp.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace PagamentosApp.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // Registro do Repositório MongoDB
            services.AddScoped<ITransacaoRepository, TransacaoRepository>();

            // Cliente da IA com resiliência
            services.AddHttpClient<IAiService, AiClient>();

            return services;
        }
    }
}



