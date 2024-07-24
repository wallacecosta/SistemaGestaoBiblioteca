using MediatR;
using SistemaGestaoBiblioteca.Application.Interfaces;
using SistemaGestaoBiblioteca.Application.Model;
using SistemaGestaoBiblioteca.Domain.Entidades;

namespace SistemaGestaoBiblioteca.Application.Commands.Autores.CadastroDeAutor
{
    public class CadastroAutorCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CadastroAutorCommand, CadastroAutorResponse>
    {
        public async Task<CadastroAutorResponse> Handle(CadastroAutorCommand request, CancellationToken cancellationToken)
        {
            var autor = new Autor(request.Nome, request.Sobrenome);

            await unitOfWork.AutorRepository.AddAsync(autor);
            await unitOfWork.CommitAsync();

            return await Task.FromResult(new CadastroAutorResponse(AutorModel.MapFrom(autor)));
        }
    }
}
