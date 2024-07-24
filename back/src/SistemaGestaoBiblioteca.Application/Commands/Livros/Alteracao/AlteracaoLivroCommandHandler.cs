using MediatR;
using SistemaGestaoBiblioteca.Application.Interfaces;

namespace SistemaGestaoBiblioteca.Application.Commands.Livros.Alteracao
{
    public class AlteracaoLivroCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AlteracaoLivroCommand, AlteracaoLivroResponse>
    {
        public async Task<AlteracaoLivroResponse> Handle(AlteracaoLivroCommand request, CancellationToken cancellationToken)
        {
            var livro = await unitOfWork.LivroRepository.GetAsync(request.Livro.Id)
                ?? throw new ArgumentException("Livro não encontrado para realizar alteração!");

            var autor = await unitOfWork.AutorRepository.GetAsync(request.Livro.Autor.Id)
            ?? throw new ArgumentException($"Autor {request.Livro.Autor.Nome} {request.Livro.Autor.Sobrenome} não encontrado, é requerido para alterar livro");

            var genero = await unitOfWork.GeneroRepository.GetAsync(request.Livro.Genero.Id)
                ?? throw new ArgumentException($"Gênero {request.Livro.Genero.Nome} não encontrado, é requerido para alterar livro");

            livro.AlterarNome(request.Livro.Nome);
            livro.AlterarAutor(autor);
            livro.AlterarGenero(genero);

            unitOfWork.LivroRepository.Update(livro);
            await unitOfWork.CommitAsync();

            return await Task.FromResult(new AlteracaoLivroResponse("Livro atualizado com sucesso!"));
        }
    }
}
