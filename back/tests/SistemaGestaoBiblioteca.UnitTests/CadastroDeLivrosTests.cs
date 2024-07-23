using Moq;
using SistemaGestaoBiblioteca.Application.CadastroDeLivro;
using SistemaGestaoBiblioteca.Application.Interfaces;
using SistemaGestaoBiblioteca.Application.Model;
using SistemaGestaoBiblioteca.Domain.Entidades;

namespace SistemaGestaoBiblioteca.UnitTests
{
    public class CadastroDeLivrosTests
    {
        [Fact]
        public async Task FalhaAoCadastrarLivroSemNome()
        {
            var autor = new Autor("Escritor", "Fantasma");
            var genero = new Genero("Ficção");
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var request = new CadastrarLivroRequest("", AutorModel.MapFrom(autor), GeneroModel.MapFrom(genero));
            var command = new CadastroLivroCommand(request);

            var commandHandler = new CadastroLivroCommandHandler(mockUnitOfWork.Object);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commandHandler.Handle(command, CancellationToken.None));
            Assert.Equal("Nome do livro deve ser informado", ex.Message);
        }

        [Fact]
        public async Task FalhaAoCadastrarLivroSemAutorExistir()
        {
            var autorModel = new AutorModel(Guid.NewGuid(), "Escritor", "Fantasma", []);
            var genero = new Genero("Ficção");
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var request = new CadastrarLivroRequest("Jornada Fantasma", autorModel, GeneroModel.MapFrom(genero));
            var command = new CadastroLivroCommand(request);

            mockUnitOfWork.Setup(u => u.AutorRepository.GetAsync(autorModel.Id)).ReturnsAsync(null as Autor);

            var commandHandler = new CadastroLivroCommandHandler(mockUnitOfWork.Object);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commandHandler.Handle(command, CancellationToken.None));
            Assert.Equal("Autor Escritor Fantasma não encontrado, é requerido para criar livro", ex.Message);
        }

        [Fact]
        public async Task FalhaAoCadastrarLivroSemGeneroExistir()
        {
            var autor = new Autor("Escritor", "Fantasma");
            var genero = new Genero("Ficção");
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var request = new CadastrarLivroRequest("Jornada Fantasma", AutorModel.MapFrom(autor), GeneroModel.MapFrom(genero));
            var command = new CadastroLivroCommand(request);

            mockUnitOfWork.Setup(u => u.AutorRepository.GetAsync(autor.Id)).ReturnsAsync(autor);
            mockUnitOfWork.Setup(u => u.GeneroRepository.GetAsync(genero.Id)).ReturnsAsync(null as Genero);

            var commandHandler = new CadastroLivroCommandHandler(mockUnitOfWork.Object);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commandHandler.Handle(command, CancellationToken.None));
            Assert.Equal("Gênero Ficção não encontrado, é requerido para criar livro", ex.Message);
        }

        [Fact]
        public async Task CadastrarLivroComSucesso()
        {
            var autor = new Autor("Escritor", "Fantasma");
            var genero = new Genero("Ficção");
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var request = new CadastrarLivroRequest("Jornada Fantasma", AutorModel.MapFrom(autor), GeneroModel.MapFrom(genero));
            var command = new CadastroLivroCommand(request);

            mockUnitOfWork.Setup(u => u.GeneroRepository.GetAsync(genero.Id)).ReturnsAsync(genero);
            mockUnitOfWork.Setup(u => u.AutorRepository.GetAsync(autor.Id)).ReturnsAsync(autor);
            mockUnitOfWork.Setup(u => u.LivroRepository.AddAsync(It.IsAny<Livro>())).Returns(Task.CompletedTask);

            var commandHandler = new CadastroLivroCommandHandler(mockUnitOfWork.Object);

            CadastroLivroResponse response = await commandHandler.Handle(command, CancellationToken.None);

            Assert.NotNull(response);
            Assert.Equal("Jornada Fantasma", response.Livro.Nome);
            Assert.Equal("Escritor", response.Livro.Autor.Nome);
            Assert.Equal("Fantasma", response.Livro.Autor.Sobrenome);
            Assert.Equal("Ficção", response.Livro.Genero.Nome);
        }
    }
}