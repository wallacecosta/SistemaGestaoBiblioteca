using MediatR;
using SistemaGestaoBiblioteca.Application.Interfaces;

namespace SistemaGestaoBiblioteca.Application.Commands.Livros.Exclusao
{
    public class ExclusaoLivroCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ExclusaoLivroCommand, ExclusaoLivroResponse>
    {
        public async Task<ExclusaoLivroResponse> Handle(ExclusaoLivroCommand request, CancellationToken cancellationToken)
        {
            var livroExistente = await unitOfWork.LivroRepository.GetAsync(request.LivroId)
                ?? throw new ArgumentException("Livro não encontrado para excluir.");

            unitOfWork.LivroRepository.Delete(livroExistente);
            await unitOfWork.CommitAsync();

            return await Task.FromResult(new ExclusaoLivroResponse("Livro excluído com sucesso!"));
        }
    }
}
