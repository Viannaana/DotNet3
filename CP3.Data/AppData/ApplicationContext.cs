using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using CP3.Domain.Entities;

namespace CP3.Data.AppData
{
    public class BarcoApplicationContext : DbContext // Renomeado para ser mais específico
    {
        private readonly IConfiguration _configuration;

        // Construtor para injeção de dependência
        public BarcoApplicationContext(DbContextOptions<BarcoApplicationContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        // Configurando a conexão com o banco de dados Oracle
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration.GetConnectionString("OracleConnection");
                optionsBuilder.UseOracle(connectionString);
            }
        }

        // DbSet para acessar a tabela Barco
        public DbSet<BarcoEntity> Barcos { get; set; }
    }
}