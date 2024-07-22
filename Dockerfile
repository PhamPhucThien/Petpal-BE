#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE $PORT

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CapstoneProject/CapstoneProject.csproj", "CapstoneProject/"]
COPY ["CapstoneProject.Business/CapstoneProject.Business.csproj", "CapstoneProject.Business/"]
COPY ["CapstoneProject.DTO/CapstoneProject.DTO.csproj", "CapstoneProject.DTO/"]
COPY ["CapstoneProject.Database/CapstoneProject.Database.csproj", "CapstoneProject.Database/"]
COPY ["CapstoneProject.Repository/CapstoneProject.Repository.csproj", "CapstoneProject.Repository/"]
COPY ["CapstoneProject.Infrastructure/CapstoneProject.Infrastructure.csproj", "CapstoneProject.Infrastructure/"]
RUN dotnet restore "./CapstoneProject/CapstoneProject.csproj"
COPY . .
WORKDIR "/src/CapstoneProject"
RUN dotnet build "./CapstoneProject.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CapstoneProject.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CapstoneProject.dll"]