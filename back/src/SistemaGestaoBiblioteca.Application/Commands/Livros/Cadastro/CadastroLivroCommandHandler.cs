using MediatR;
using SistemaGestaoBiblioteca.Application.Interfaces;
using SistemaGestaoBiblioteca.Application.Model;
using SistemaGestaoBiblioteca.Domain.Entidades;

namespace SistemaGestaoBiblioteca.Application.Commands.Livros.Cadastro
{
    public class CadastroLivroCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CadastroLivroCommand, CadastroLivroResponse>
    {
        public async Task<CadastroLivroResponse> Handle(CadastroLivroCommand command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(command.Nome))
                throw new ArgumentException("Nome do livro deve ser informado");

            var autor = await unitOfWork.AutorRepository.GetAsync(command.Autor.Id)
                ?? throw new ArgumentException($"Autor {command.Autor.Nome} {command.Autor.Sobrenome} não encontrado, é requerido para criar livro");

            var genero = await unitOfWork.GeneroRepository.GetAsync(command.Genero.Id)
                ?? throw new ArgumentException($"Gênero {command.Genero.Nome} não encontrado, é requerido para criar livro");

            var livro = new Livro(command.Nome, genero, autor);

            await unitOfWork.LivroRepository.AddAsync(livro);
            await unitOfWork.CommitAsync();

            return await Task.FromResult(new CadastroLivroResponse(LivroModel.MapFrom(livro)));
        }
    }
}
