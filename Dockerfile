# Multi-stage build za optimizaciju veličine imagea

# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Kopiraj .csproj fajlove
COPY ["MyProperty.API/MyProperty.API.csproj", "MyProperty.API/"]
COPY ["Persistence/Persistence.csproj", "Persistence/"]
COPY ["Presentation/Presentation.csproj", "Presentation/"]
COPY ["Services/Services.csproj", "Services/"]
COPY ["Services.Abstractions/Services.Abstractions.csproj", "Services.Abstractions/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Contract/Contract.csproj", "Contract/"]

# Restore dependencije
RUN dotnet restore "MyProperty.API/MyProperty.API.csproj"

# Kopiraj sve source fajlove
COPY . .

# Build aplikacije
WORKDIR "/src/MyProperty.API"
RUN dotnet build "MyProperty.API.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "MyProperty.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Kopiraj published app
COPY --from=publish /app/publish .

# Health check
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
    CMD curl -f http://localhost/health || exit 1

# Expose port
EXPOSE 8080

# Pokreni aplikaciju
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "MyProperty.API.dll"]
