FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine as build

EXPOSE 80

WORKDIR /app

ENV DOTNET_SYSTEM_NET_HTTP_USESOCKETSHTTPHANDLER=0

COPY ["src/SistemaGestaoBiblioteca.Api/SistemaGestaoBiblioteca.Api.csproj", "src/SistemaGestaoBiblioteca.Api/SistemaGestaoBiblioteca.Api.csproj"]
COPY ["src/SistemaGestaoBiblioteca.Application/SistemaGestaoBiblioteca.Application.csproj", "src/SistemaGestaoBiblioteca.Application/SistemaGestaoBiblioteca.Application.csproj"]
COPY ["src/SistemaGestaoBiblioteca.Domain/SistemaGestaoBiblioteca.Domain.csproj", "src/SistemaGestaoBiblioteca.Domain/SistemaGestaoBiblioteca.Domain.csproj"]
COPY ["src/SistemaGestaoBiblioteca.Infrastructure/SistemaGestaoBiblioteca.Infrastructure.csproj", "src/SistemaGestaoBiblioteca.Infrastructure/SistemaGestaoBiblioteca.Infrastructure.csproj"]
COPY ["tests/SistemaGestaoBiblioteca.UnitTests/SistemaGestaoBiblioteca.UnitTests.csproj", "tests/SistemaGestaoBiblioteca.UnitTests/SistemaGestaoBiblioteca.UnitTests.csproj"]
COPY ["SistemaGestaoBiblioteca.sln", "SistemaGestaoBiblioteca.sln"]

RUN dotnet restore --no-cache

COPY . .

RUN dotnet build \
    && dotnet publish -c Release -o /build --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine

RUN apk update && apk add libgdiplus
RUN apk update && apk add icu-libs

WORKDIR /app

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV ASPNETCORE_URLS=http://+:80

COPY --from=build /build ./

ENTRYPOINT ["dotnet", "./SistemaGestaoBiblioteca.Api.dll"]