using MediatR;
using SistemaGestaoBiblioteca.Application.Interfaces;
using SistemaGestaoBiblioteca.Application.Model;
using SistemaGestaoBiblioteca.Domain.Entidades;

namespace SistemaGestaoBiblioteca.Application.Commands.Generos.Cadastro
{
    public class CadastroGeneroCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CadastroGeneroCommand, CadastroGeneroResponse>
    {
        public async Task<CadastroGeneroResponse> Handle(CadastroGeneroCommand request, CancellationToken cancellationToken)
        {
            var genero = new Genero(request.Nome);

            await unitOfWork.GeneroRepository.AddAsync(genero);

            return await Task.FromResult(new CadastroGeneroResponse(GeneroModel.MapFrom(genero)));
        }
    }
}
