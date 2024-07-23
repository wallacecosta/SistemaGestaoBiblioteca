using MediatR;
using SistemaGestaoBiblioteca.Application.Interfaces;
using SistemaGestaoBiblioteca.Application.Model;
using SistemaGestaoBiblioteca.Domain.Entidades;

namespace SistemaGestaoBiblioteca.Application.CadastroDeLivro
{
    public class CadastroLivroCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CadastroLivroCommand, CadastroLivroResponse>
    {
        public async Task<CadastroLivroResponse> Handle(CadastroLivroCommand command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(command.Request.Nome))
                throw new ArgumentException("Nome do livro deve ser informado");

            var autor = await unitOfWork.AutorRepository.GetAsync(command.Request.Autor.Id)
                ?? throw new ArgumentException($"Autor {command.Request.Autor.Nome} {command.Request.Autor.Sobrenome} não encontrado, é requerido para criar livro");

            var genero = await unitOfWork.GeneroRepository.GetAsync(command.Request.Genero.Id)
                ?? throw new ArgumentException($"Gênero {command.Request.Genero.Nome} não encontrado, é requerido para criar livro");

            var livro = new Livro(command.Request.Nome, genero, autor);

            await unitOfWork.LivroRepository.AddAsync(livro);
            await unitOfWork.CommitAsync();

            return await Task.FromResult(new CadastroLivroResponse(LivroModel.MapFrom(livro)));
        }
    }
}
