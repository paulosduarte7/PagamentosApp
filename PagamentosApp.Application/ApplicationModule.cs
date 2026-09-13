using Microsoft.Extensions.DependencyInjection;
using PagamentosApp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace PagamentosApp.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ITextProcessingAppService, TextProcessingAppService>();
            return services;
        }
    }
}

