using Moq;
using SistemaGestaoBiblioteca.Application.Commands.Livros.Alteracao;
using SistemaGestaoBiblioteca.Application.Interfaces;
using SistemaGestaoBiblioteca.Application.Model;
using SistemaGestaoBiblioteca.Domain.Entidades;

namespace SistemaGestaoBiblioteca.UnitTests.Commands.Livros
{
    public class AlterarCadatroLivrosTests
    {
        [Fact]
        public async Task FalhaAoAtualizarLivroPoisNaoEncontrado()
        {
            var autor = new Autor("Escritor", "Fantasma");
            var genero = new Genero("Ficção");
            var livro = new Livro("Jornada Fantasma", genero, autor);
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var command = new AlteracaoLivroCommand(LivroModel.MapFrom(livro));
            var commandHandler = new AlteracaoLivroCommandHandler(mockUnitOfWork.Object);

            mockUnitOfWork.Setup(u => u.LivroRepository.GetAsync(command.LivroAlterado.Id)).ReturnsAsync(null as Livro);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commandHandler.Handle(command, CancellationToken.None));
            Assert.Equal("Livro não encontrado para realizar alteração!", ex.Message);
        }

        [Fact]
        public async Task FalhaAoAtualizarLivroPoisAutorNaoEncontrado()
        {
            var autor = new Autor("Escritor", "Fantasma");
            var genero = new Genero("Ficção");
            var livro = new Livro("Jornada Fantasma", genero, autor);
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var autorAlterado = new AutorModel(Guid.NewGuid(), "João", "das Coves", []);
            var livroAlterado = new LivroModel(livro.Id, "Jornada Fantasma", autorAlterado, GeneroModel.MapFrom(genero));
            var command = new AlteracaoLivroCommand(livroAlterado);
            var commandHandler = new AlteracaoLivroCommandHandler(mockUnitOfWork.Object);

            mockUnitOfWork.Setup(u => u.LivroRepository.GetAsync(command.LivroAlterado.Id)).ReturnsAsync(livro);
            mockUnitOfWork.Setup(u => u.AutorRepository.GetAsync(command.LivroAlterado.Autor.Id)).ReturnsAsync(null as Autor);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commandHandler.Handle(command, CancellationToken.None));
            Assert.Equal("Autor João das Coves não encontrado, é requerido para alterar livro", ex.Message);
        }

        [Fact]
        public async Task FalhaAoAtualizarLivroPoisGeneroNaoEncontrado()
        {
            var autor = new Autor("Escritor", "Fantasma");
            var genero = new Genero("Ficção");
            var livro = new Livro("Jornada Fantasma", genero, autor);
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var generoAlterado = new GeneroModel(Guid.NewGuid(), "Guerra", []);
            var livroAlterado = new LivroModel(livro.Id, "Jornada Fantasma", AutorModel.MapFrom(autor), generoAlterado);
            var command = new AlteracaoLivroCommand(livroAlterado);
            var commandHandler = new AlteracaoLivroCommandHandler(mockUnitOfWork.Object);

            mockUnitOfWork.Setup(u => u.LivroRepository.GetAsync(command.LivroAlterado.Id)).ReturnsAsync(livro);
            mockUnitOfWork.Setup(u => u.AutorRepository.GetAsync(command.LivroAlterado.Autor.Id)).ReturnsAsync(autor);
            mockUnitOfWork.Setup(u => u.GeneroRepository.GetAsync(command.LivroAlterado.Genero.Id)).ReturnsAsync(null as Genero);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commandHandler.Handle(command, CancellationToken.None));
            Assert.Equal("Gênero Guerra não encontrado, é requerido para alterar livro", ex.Message);
        }

        [Fact]
        public async Task AlterarLivroComSucesso()
        {
            var autor = new Autor("Escritor", "Fantasma");
            var genero = new Genero("Ficção");
            var livro = new Livro("Jornada Fantasma", genero, autor);
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var livroAlterado = new LivroModel(livro.Id, "Jornada Fantasma 2", AutorModel.MapFrom(autor), GeneroModel.MapFrom(genero));
            var command = new AlteracaoLivroCommand(livroAlterado);
            var commandHandler = new AlteracaoLivroCommandHandler(mockUnitOfWork.Object);

            mockUnitOfWork.Setup(u => u.LivroRepository.GetAsync(command.LivroAlterado.Id)).ReturnsAsync(livro);
            mockUnitOfWork.Setup(u => u.AutorRepository.GetAsync(command.LivroAlterado.Autor.Id)).ReturnsAsync(autor);
            mockUnitOfWork.Setup(u => u.GeneroRepository.GetAsync(command.LivroAlterado.Genero.Id)).ReturnsAsync(genero);

            var response = await commandHandler.Handle(command, CancellationToken.None);

            Assert.NotNull(response);
            Assert.Equal("Livro atualizado com sucesso!", response.Resposta);
            Assert.Equal(livroAlterado.Nome, livro.Nome);
            mockUnitOfWork.Verify(u => u.LivroRepository.Update(It.IsAny<Livro>()), Times.Once);
        }
    }
}
