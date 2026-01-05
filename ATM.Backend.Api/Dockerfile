# Estágio de Base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Estágio de Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ATM.Backend.Api/ATM.Backend.Api.csproj", "ATM.Backend.Api/"]
RUN dotnet restore "./ATM.Backend.Api/ATM.Backend.Api.csproj"
COPY . .
WORKDIR "/src/ATM.Backend.Api"
RUN dotnet build "./ATM.Backend.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Estágio de Publicação
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ATM.Backend.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Estágio Final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ATM.Backend.Api.dll"]
