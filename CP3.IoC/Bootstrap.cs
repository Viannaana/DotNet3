using CP3.Application.Services;
using CP3.Data.AppData;
using CP3.Data.Repositories;
using CP3.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CP3.IoC
{
    public static class Bootstrap
    {
        public static void Start(IServiceCollection services, IConfiguration configuration)
        {
            // Configuração do DbContext com a string de conexão para Oracle
            services.AddDbContext<BarcoApplicationContext>(options =>  // Alterado para BarcoApplicationContext
                options.UseOracle(configuration.GetConnectionString("OracleConnection")));

            // Registro de Repositórios
            services.AddScoped<IBarcoRepository, BarcoRepository>();

            // Registro de Serviços de Aplicação
            services.AddScoped<IBarcoApplicationService, BarcoApplicationService>();

            // Adicionar outras injeções de dependência, caso necessário
        }
    }
}