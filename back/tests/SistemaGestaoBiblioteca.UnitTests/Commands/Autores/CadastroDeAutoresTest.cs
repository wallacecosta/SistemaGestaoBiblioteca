using Moq;
using SistemaGestaoBiblioteca.Application.Commands.Autores.CadastroDeAutor;
using SistemaGestaoBiblioteca.Application.Interfaces;
using SistemaGestaoBiblioteca.Domain.Entidades;

namespace SistemaGestaoBiblioteca.UnitTests.Commands.Autores
{
    public class CadastroDeAutoresTest
    {
        [Fact]
        public async Task FalhaAoCadastrarAutorPoisNomeInvalido()
        {
            var command = new CadastroAutorCommand("", "");
            var commandHandler = new CadastroAutorCommandHandler(new Mock<IUnitOfWork>().Object);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commandHandler.Handle(command, CancellationToken.None));
            Assert.Equal("Nome do Autor deve ser informado", ex.Message);
        }

        [Fact]
        public async Task FalhaAoCadastrarAutorPoisSobrenomeInvalido()
        {
            var command = new CadastroAutorCommand("Escritor", "");
            var commandHandler = new CadastroAutorCommandHandler(new Mock<IUnitOfWork>().Object);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commandHandler.Handle(command, CancellationToken.None));
            Assert.Equal("Sobrenome do Autor deve ser informado", ex.Message);
        }

        [Fact]
        public async Task CadastrarAutorSucesso()
        {
            var command = new CadastroAutorCommand("Escritor", "Fantasma");
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var commandHandler = new CadastroAutorCommandHandler(mockUnitOfWork.Object);

            mockUnitOfWork.Setup(u => u.AutorRepository.AddAsync(It.IsAny<Autor>())).Returns(Task.CompletedTask);

            var response = await commandHandler.Handle(command, CancellationToken.None);
            Assert.NotNull(response);
            Assert.Equal(command.Nome, response.Autor.Nome);
            Assert.Equal(command.Sobrenome, response.Autor.Sobrenome);
            mockUnitOfWork.Verify(u => u.AutorRepository.AddAsync(It.IsAny<Autor>()), Times.Once);
        }
    }
}
