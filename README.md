# SistemaGestaoBiblioteca
Sistemas para gestão de bibliotecas 

## Regra de Negócio
Nossa solução deve permitir o cadastro, consulta e manutenção de gêneros, autores e livros. 

As regras de negócio a serem implementadas são as seguintes:

- Entidade Gênero: Um gênero pode possuir N livros.
- Entidade Autor: Um autor pode possuir N livros.
- Entidade Livro: Cada livro pertence a apenas um autor e um gênero.

## Escolha seu banco de dados: 
- Mysql
- SQL Server
- PostgreSQL

## Backend
Desenvolver uma API Rest (.NET Core ou Java com Spring) que permita a criação, leitura, atualização e exclusão (CRUD) de gêneros, autores e livros.
- Boas práticas são sempre bem-vindas (Exemplo: Single responsibility, Dependency injection, ...)
- ViewModel 
- Versionamento da Api (Rotas)
- Documentação (Exemplo swagger)
- Respostas padronizadas (Http Status Codes)

## Frontend 
Descrição: Desenvolver uma aplicação SPA (Angular, Reactjs ou Vuejs) para consumir a nossa API e permitir a interação do usuário com os dados de gêneros, autores e livros.
- Boas práticas são sempre bem-vindas (Exemplo: Divisão da estrutura do componente e ciclo de vida)
- Rotas
- Models
- Serviços
- Interfaces