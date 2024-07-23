using SistemaGestaoBiblioteca.Application.Model;

namespace SistemaGestaoBiblioteca.Application.CadastroDeLivro;

public record CadastrarLivroRequest(string Nome, AutorModel Autor, GeneroModel Genero);
