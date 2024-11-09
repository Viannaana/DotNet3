using CP3.Application.Services;
using CP3.Domain.Entities;
using CP3.Domain.Interfaces;
using CP3.Domain.Interfaces.Dtos;
using Moq;
using Xunit;

namespace CP3.Tests
{
    public class BarcoApplicationServiceTests
    {
        private readonly Mock<IBarcoRepository> _repositoryMock;
        private readonly BarcoApplicationService _barcoService;

        public BarcoApplicationServiceTests()
        {
            _repositoryMock = new Mock<IBarcoRepository>();
            _barcoService = new BarcoApplicationService(_repositoryMock.Object);
        }

        [Fact]
        public void ObterTodosBarcos_DeveRetornarListaDeBarcos()
        {
            // Arrange
            var barcos = new List<BarcoEntity>
            {
                new BarcoEntity { Id = 1, Nome = "Barco A", Modelo = "Modelo X", Ano = 2021, Tamanho = 20.5 },
                new BarcoEntity { Id = 2, Nome = "Barco B", Modelo = "Modelo Y", Ano = 2022, Tamanho = 25.0 }
            };
            _repositoryMock.Setup(repo => repo.ObterTodos()).Returns(barcos);

            // Act
            var result = _barcoService.ObterTodosBarcos();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("Barco A", result.First().Nome);
            Assert.Equal("Barco B", result.Last().Nome);
        }

        [Fact]
        public void ObterBarcoPorId_DeveRetornarBarco_QuandoIdExistir()
        {
            // Arrange
            var barco = new BarcoEntity { Id = 1, Nome = "Barco A", Modelo = "Modelo X", Ano = 2021, Tamanho = 20.5 };
            _repositoryMock.Setup(repo => repo.ObterPorId(1)).Returns(barco);

            // Act
            var result = _barcoService.ObterBarcoPorId(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Barco A", result.Nome);
        }

        [Fact]
        public void AdicionarBarco_DeveAdicionarEretornarBarco()
        {
            // Arrange
            var dto = new Mock<IBarcoDto>();
            var barco = new BarcoEntity { Id = 1, Nome = "Barco A", Modelo = "Modelo X", Ano = 2021, Tamanho = 20.5 };
            _repositoryMock.Setup(repo => repo.Adicionar(It.IsAny<BarcoEntity>())).Returns(barco);

            // Act
            var result = _barcoService.AdicionarBarco(dto.Object);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Barco A", result.Nome);
        }

        [Fact]
        public void RemoverBarco_DeveRemoverEretornarBarco()
        {
            // Arrange
            var barco = new BarcoEntity { Id = 1, Nome = "Barco A", Modelo = "Modelo X", Ano = 2021, Tamanho = 20.5 };
            _repositoryMock.Setup(repo => repo.Remover(1)).Returns(barco);

            // Act
            var result = _barcoService.RemoverBarco(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Barco A", result.Nome);
        }
    }
}
