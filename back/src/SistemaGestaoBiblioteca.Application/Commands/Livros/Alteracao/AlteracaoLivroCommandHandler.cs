using MediatR;
using SistemaGestaoBiblioteca.Application.Interfaces;

namespace SistemaGestaoBiblioteca.Application.Commands.Livros.Alteracao
{
    public class AlteracaoLivroCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<AlteracaoLivroCommand, AlteracaoLivroResponse>
    {
        public async Task<AlteracaoLivroResponse> Handle(AlteracaoLivroCommand request, CancellationToken cancellationToken)
        {
            var livro = await unitOfWork.LivroRepository.GetAsync(request.LivroAlterado.Id)
                ?? throw new ArgumentException("Livro não encontrado para realizar alteração!");

            var autor = await unitOfWork.AutorRepository.GetAsync(request.LivroAlterado.Autor.Id)
            ?? throw new ArgumentException($"Autor {request.LivroAlterado.Autor.Nome} {request.LivroAlterado.Autor.Sobrenome} não encontrado, é requerido para alterar livro");

            var genero = await unitOfWork.GeneroRepository.GetAsync(request.LivroAlterado.Genero.Id)
                ?? throw new ArgumentException($"Gênero {request.LivroAlterado.Genero.Nome} não encontrado, é requerido para alterar livro");

            livro.AlterarNome(request.LivroAlterado.Nome);
            livro.AlterarAutor(autor);
            livro.AlterarGenero(genero);

            unitOfWork.LivroRepository.Update(livro);

            return await Task.FromResult(new AlteracaoLivroResponse("Livro atualizado com sucesso!"));
        }
    }
}
