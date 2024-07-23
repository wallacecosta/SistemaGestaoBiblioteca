using Moq;
using SistemaGestaoBiblioteca.Application.Commands.Livros.Exclusao;
using SistemaGestaoBiblioteca.Application.Interfaces;
using SistemaGestaoBiblioteca.Domain.Entidades;

namespace SistemaGestaoBiblioteca.UnitTests.Commands.Livros
{
    public class ExcluirCadastroLivroTests
    {
        [Fact]
        public async Task FalhaExcluirCadastroLivroNaoEncontrado()
        {
            var livroIdParaExcluir = Guid.NewGuid();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var command = new ExclusaoLivroCommand(livroIdParaExcluir);

            mockUnitOfWork.Setup(u => u.LivroRepository.GetAsync(livroIdParaExcluir)).ReturnsAsync(null as Livro);

            var commandHandler = new ExclusaoLivroCommandHandler(mockUnitOfWork.Object);

            var ex = await Assert.ThrowsAsync<ArgumentException>(() => commandHandler.Handle(command, CancellationToken.None));
            Assert.Equal("Livro não encontrado para excluir.", ex.Message);
        }

        [Fact]
        public async Task ExcluirCadastroLivroComSucesso()
        {
            var autor = new Autor("Escritor", "Fantasma");
            var genero = new Genero("Ficção");
            var livroExistente = new Livro("Jornada Fantasma", genero, autor);
            var livroIdParaExcluir = livroExistente.Id;
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var command = new ExclusaoLivroCommand(livroIdParaExcluir);

            mockUnitOfWork.Setup(u => u.LivroRepository.GetAsync(livroIdParaExcluir)).ReturnsAsync(livroExistente);
            mockUnitOfWork.Setup(u => u.LivroRepository.Delete(livroExistente));

            var commandHandler = new ExclusaoLivroCommandHandler(mockUnitOfWork.Object);

            var response = await commandHandler.Handle(command, CancellationToken.None);

            Assert.NotNull(response);
            Assert.Equal("Livro excluído com sucesso!", response.Resposta);
            mockUnitOfWork.Verify(u => u.LivroRepository.Delete(livroExistente), Times.Once);
        }
    }
}
