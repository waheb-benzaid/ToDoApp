# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NuGet.Config", "."]
COPY ["src/ToDoApp.HttpApi.Host/ToDoApp.HttpApi.Host.csproj", "src/ToDoApp.HttpApi.Host/"]
COPY ["src/ToDoApp.Application/ToDoApp.Application.csproj", "src/ToDoApp.Application/"]
COPY ["src/ToDoApp.Domain/ToDoApp.Domain.csproj", "src/ToDoApp.Domain/"]
COPY ["src/ToDoApp.Domain.Shared/ToDoApp.Domain.Shared.csproj", "src/ToDoApp.Domain.Shared/"]
COPY ["src/ToDoApp.Application.Contracts/ToDoApp.Application.Contracts.csproj", "src/ToDoApp.Application.Contracts/"]
COPY ["src/ToDoApp.EntityFrameworkCore/ToDoApp.EntityFrameworkCore.csproj", "src/ToDoApp.EntityFrameworkCore/"]
COPY ["src/ToDoApp.HttpApi/ToDoApp.HttpApi.csproj", "src/ToDoApp.HttpApi/"]
RUN dotnet restore "./src/ToDoApp.HttpApi.Host/ToDoApp.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/src/ToDoApp.HttpApi.Host"
RUN dotnet build "./ToDoApp.HttpApi.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ToDoApp.HttpApi.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToDoApp.HttpApi.Host.dll"]