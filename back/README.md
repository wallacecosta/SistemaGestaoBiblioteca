# Introduction 
API que disponibiliza interface para Sistema de Gerenciamento de Biblioteca.

# Getting Started
1.	Installation process
	1. Criação de Docker Image: docker build -t sistema-gerenciamento-biblioteca-api -f Dockerfile .
	2. Execução de container: docker run -p 7520:80 --name sistema-gerenciamento-biblioteca-api sistema-gerenciamento-biblioteca-api *Tem referência com banco de dados, sugiro subir o banco antes ou executar docker-compose do projeto.
	3. Executando assim na porta 9090 é possível ver o swagger e conhecer as rotas. 
		http://localhost:7520/swagger/index.html

# EF Core

Os comados de Migragtions precisam ser executados na pasta do projeto Infrastructure com o commando especificado o --startup-project do projeto SistemaGestaoBiblioteca.Api

1. Add Migrations 
	1. dotnet ef --startup-project ../SistemaGestaoBiblioteca.Api/SistemaGestaoBiblioteca.Api.csproj migrations add mensagem
2. Update Database (O projeto aplica as migrations adicionadas programaticamente na execução)
	1. dotnet ef --startup-project ../SistemaGestaoBiblioteca.Api/SistemaGestaoBiblioteca.Api.csproj database update
3. Remove Migrations 
	1. dotnet ef --startup-project ../SistemaGestaoBiblioteca.Api/SistemaGestaoBiblioteca.Api.csproj migrations remove