using Moq;
using SistemaGestaoBiblioteca.Application.Commands.Generos.Cadastro;
using SistemaGestaoBiblioteca.Application.Interfaces;
using SistemaGestaoBiblioteca.Domain.Entidades;

namespace SistemaGestaoBiblioteca.UnitTests.Commands.Generos
{
    public class CadastroDeGeneros
    {
        [Fact]
        public async Task FalhaAoCadastrarAutorPoisNomeInvalido()
        {
            var command = new CadastroGeneroCommand("");
            var commandHandler = new CadastroGeneroCommandHandler(new Mock<IUnitOfWork>().Object);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commandHandler.Handle(command, CancellationToken.None));
            Assert.Equal("Nome do Gênero deve ser informado", ex.Message);
        }

        [Fact]
        public async Task CadastrarAutorSucesso()
        {
            var command = new CadastroGeneroCommand("Ficção");
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var commandHandler = new CadastroGeneroCommandHandler(mockUnitOfWork.Object);

            mockUnitOfWork.Setup(u => u.GeneroRepository.AddAsync(It.IsAny<Genero>())).Returns(Task.CompletedTask);

            var response = await commandHandler.Handle(command, CancellationToken.None);
            Assert.NotNull(response);
            Assert.Equal(command.Nome, response.Genero.Nome);
            mockUnitOfWork.Verify(u => u.GeneroRepository.AddAsync(It.IsAny<Genero>()), Times.Once);
        }
    }
}
