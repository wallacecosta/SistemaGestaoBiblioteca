using Moq;
using SistemaGestaoBiblioteca.Application.Commands.Livros.Cadastro;
using SistemaGestaoBiblioteca.Application.Interfaces;
using SistemaGestaoBiblioteca.Application.Model;
using SistemaGestaoBiblioteca.Domain.Entidades;

namespace SistemaGestaoBiblioteca.UnitTests.Commands.Livros
{
    public class CadastroDeLivrosTests
    {
        [Fact]
        public async Task FalhaAoCadastrarLivroSemNome()
        {
            var autor = new Autor("Escritor", "Fantasma");
            var genero = new Genero("Fic��o");
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var command = new CadastroLivroCommand("", AutorModel.MapFrom(autor), GeneroModel.MapFrom(genero));
            var commandHandler = new CadastroLivroCommandHandler(mockUnitOfWork.Object);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commandHandler.Handle(command, CancellationToken.None));
            Assert.Equal("Nome do livro deve ser informado", ex.Message);
        }

        [Fact]
        public async Task FalhaAoCadastrarLivroSemAutorExistir()
        {
            var autorModel = new AutorModel(Guid.NewGuid(), "Escritor", "Fantasma", []);
            var genero = new Genero("Fic��o");
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var command = new CadastroLivroCommand("Jornada Fantasma", autorModel, GeneroModel.MapFrom(genero));

            mockUnitOfWork.Setup(u => u.AutorRepository.GetAsync(autorModel.Id)).ReturnsAsync(null as Autor);

            var commandHandler = new CadastroLivroCommandHandler(mockUnitOfWork.Object);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commandHandler.Handle(command, CancellationToken.None));
            Assert.Equal("Autor Escritor Fantasma n�o encontrado, � requerido para criar livro", ex.Message);
        }

        [Fact]
        public async Task FalhaAoCadastrarLivroSemGeneroExistir()
        {
            var autor = new Autor("Escritor", "Fantasma");
            var genero = new Genero("Fic��o");
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var command = new CadastroLivroCommand("Jornada Fantasma", AutorModel.MapFrom(autor), GeneroModel.MapFrom(genero));

            mockUnitOfWork.Setup(u => u.AutorRepository.GetAsync(autor.Id)).ReturnsAsync(autor);
            mockUnitOfWork.Setup(u => u.GeneroRepository.GetAsync(genero.Id)).ReturnsAsync(null as Genero);

            var commandHandler = new CadastroLivroCommandHandler(mockUnitOfWork.Object);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commandHandler.Handle(command, CancellationToken.None));
            Assert.Equal("G�nero Fic��o n�o encontrado, � requerido para criar livro", ex.Message);
        }

        [Fact]
        public async Task CadastrarLivroComSucesso()
        {
            var autor = new Autor("Escritor", "Fantasma");
            var genero = new Genero("Fic��o");
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var command = new CadastroLivroCommand("Jornada Fantasma", AutorModel.MapFrom(autor), GeneroModel.MapFrom(genero));

            mockUnitOfWork.Setup(u => u.GeneroRepository.GetAsync(genero.Id)).ReturnsAsync(genero);
            mockUnitOfWork.Setup(u => u.AutorRepository.GetAsync(autor.Id)).ReturnsAsync(autor);
            mockUnitOfWork.Setup(u => u.LivroRepository.AddAsync(It.IsAny<Livro>())).Returns(Task.CompletedTask);

            var commandHandler = new CadastroLivroCommandHandler(mockUnitOfWork.Object);

            CadastroLivroResponse response = await commandHandler.Handle(command, CancellationToken.None);

            Assert.NotNull(response);
            Assert.Equal("Jornada Fantasma", response.Livro.Nome);
            Assert.Equal("Escritor", response.Livro.Autor.Nome);
            Assert.Equal("Fantasma", response.Livro.Autor.Sobrenome);
            Assert.Equal("Fic��o", response.Livro.Genero.Nome);
        }
    }
}