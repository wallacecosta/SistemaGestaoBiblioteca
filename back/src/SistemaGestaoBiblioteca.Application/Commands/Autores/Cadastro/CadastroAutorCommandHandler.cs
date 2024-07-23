using MediatR;
using SistemaGestaoBiblioteca.Application.Interfaces;
using SistemaGestaoBiblioteca.Application.Model;
using SistemaGestaoBiblioteca.Domain.Entidades;

namespace SistemaGestaoBiblioteca.Application.Commands.Autores.CadastroDeAutor
{
    public class CadastroAutorCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CadastroAutorCommand, CadastroAutorResponse>
    {
        public Task<CadastroAutorResponse> Handle(CadastroAutorCommand request, CancellationToken cancellationToken)
        {
            var autor = new Autor(request.Nome, request.Sobrenome);

            unitOfWork.AutorRepository.AddAsync(autor);

            return Task.FromResult(new CadastroAutorResponse(AutorModel.MapFrom(autor)));
        }
    }
}
