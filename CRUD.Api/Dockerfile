FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Build stage: restaura dependências e compila o projeto
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CRUD.Api/CRUD.Api.csproj", "CRUD.Api/"]
RUN dotnet restore "./CRUD.Api/CRUD.Api.csproj"
COPY . .
WORKDIR "/src/CRUD.Api"
RUN dotnet build "./CRUD.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage: publica o aplicativo para o ambiente final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CRUD.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Runtime stage: prepara o ambiente de execução
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Configura a string de conexão
ENV ConnectionStrings__DefaultConnection="Server=db;Database=TaskItemDb;User Id=sa;Password=Your@@_password123;TrustServerCertificate=True;"

ENTRYPOINT ["dotnet", "CRUD.Api.dll"]
