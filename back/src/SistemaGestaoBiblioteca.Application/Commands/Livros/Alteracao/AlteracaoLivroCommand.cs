﻿using MediatR;
using SistemaGestaoBiblioteca.Application.Model;

namespace SistemaGestaoBiblioteca.Application.Commands.Livros.Alteracao
{
    public record AlteracaoLivroCommand(LivroModel Livro) : IRequest<AlteracaoLivroResponse>;
}
