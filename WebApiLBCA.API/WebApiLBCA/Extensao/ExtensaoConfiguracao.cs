using Microsoft.Extensions.DependencyInjection;
using System;
using WebApiLBCA.Aplicacao;
using WebApiLBCA.Aplicacao.Contrato;
using WebApiLBCA.Application;
using WebApiLBCA.Infraestrutura;
using WebApiLBCA.Infraestrutura.Contrato;

namespace WebApiLBCA.Extensao
{
    public static class ExtensaoConfiguracao
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<INoticiaServico, NoticiaServico>();
            services.AddScoped<IUsuarioServico, UsuarioServico>();
            services.AddScoped<IClassificServico, ClassificServico>();            
            return services;
        }

        public static IServiceCollection AddInfraestructure(this IServiceCollection services)
        {
            services.AddScoped<INoticiaRepo, NoticiaRepo>();
            services.AddScoped<IUsuarioRepo, UsuarioRepo>();
            services.AddScoped<IClassificRepo, ClassificRepo>();
            return services;
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }
    }
}
