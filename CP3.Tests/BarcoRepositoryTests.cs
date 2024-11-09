using CP3.Data.AppData;
using CP3.Data.Repositories;
using CP3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;  
using Xunit;

namespace CP3.Tests
{
    public class BarcoRepositoryTests
    {
        private readonly BarcoApplicationContext _context;
        private readonly BarcoRepository _barcoRepository;

        public BarcoRepositoryTests()
        {
            // Criando uma instância de IConfiguration (simulando o comportamento de configuração)
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection()  
                .Build();

            var options = new DbContextOptionsBuilder<BarcoApplicationContext>()
                .UseInMemoryDatabase(databaseName: "BarcoDatabase")
                .Options;

            // Passando tanto DbContextOptions quanto IConfiguration no construtor
            _context = new BarcoApplicationContext(options, configuration);
            _barcoRepository = new BarcoRepository(_context);
        }

        [Fact]
        public void Adicionar_DeveAdicionarEretornarBarco()
        {
            // Arrange
            var barco = new BarcoEntity { Id = 1, Nome = "Barco A", Modelo = "Modelo X", Ano = 2021, Tamanho = 20.5 };

            // Act
            var resultado = _barcoRepository.Adicionar(barco);

            // Assert
            var barcoNoDb = _context.Barcos.FirstOrDefault(b => b.Id == resultado.Id);
            Assert.NotNull(barcoNoDb);
            Assert.Equal(barco.Nome, barcoNoDb.Nome);
            Assert.Equal(barco.Modelo, barcoNoDb.Modelo);
            Assert.Equal(barco.Ano, barcoNoDb.Ano);
            Assert.Equal(barco.Tamanho, barcoNoDb.Tamanho);
        }

        [Fact]
        public void ObterPorId_DeveRetornarBarco_QuandoIdExistir()
        {
            // Arrange
            var barco = new BarcoEntity { Id = 2, Nome = "Barco B", Modelo = "Modelo Y", Ano = 2022, Tamanho = 25.0 };
            _context.Barcos.Add(barco);
            _context.SaveChanges();

            // Act
            var result = _barcoRepository.ObterPorId(2);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(barco.Id, result.Id);
            Assert.Equal(barco.Nome, result.Nome);
            Assert.Equal(barco.Modelo, result.Modelo);
        }

        [Fact]
        public void Editar_DeveAtualizarEretornarBarco_QuandoBarcoExistir()
        {
            // Arrange
            var barco = new BarcoEntity { Id = 3, Nome = "Barco C", Modelo = "Modelo Z", Ano = 2023, Tamanho = 30.0 };
            _context.Barcos.Add(barco);
            _context.SaveChanges();

            // Alterando os dados para a edição
            barco.Nome = "Barco C Atualizado";
            barco.Modelo = "Modelo A";
            
            // Act
            var result = _barcoRepository.Editar(barco);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Barco C Atualizado", result.Nome);
            Assert.Equal("Modelo A", result.Modelo);
        }

        [Fact]
        public void Remover_DeveRemoverEretornarBarco_QuandoBarcoExistir()
        {
            // Arrange
            var barco = new BarcoEntity { Id = 4, Nome = "Barco D", Modelo = "Modelo W", Ano = 2024, Tamanho = 35.0 };
            _context.Barcos.Add(barco);
            _context.SaveChanges();

            // Act
            var result = _barcoRepository.Remover(4);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Barco D", result.Nome);

            // Verificar se o barco foi removido
            var barcoNoDb = _context.Barcos.FirstOrDefault(b => b.Id == barco.Id);
            Assert.Null(barcoNoDb);
        }
    }
}
