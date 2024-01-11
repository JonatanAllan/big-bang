#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Enterprise.Template.IoC/Enterprise.Template.IoC.csproj", "Enterprise.Template.IoC/"]
COPY ["Enterprise.Template.WebApi/Enterprise.Template.WebApi.csproj", "Enterprise.Template.WebApi/"]
COPY ["Enterprise.Template.Application/Enterprise.Template.Application.csproj", "Enterprise.Template.Application/"]
COPY ["Enterprise.Template.Core/Enterprise.Template.Core.csproj", "Enterprise.Template.Core/"]
COPY ["Enterprise.Template.Domain/Enterprise.Template.Domain.csproj", "Enterprise.Template.Domain/"]
COPY ["Enterprise.Template.Data/Enterprise.Template.Data.csproj", "Enterprise.Template.Data/"]
RUN dotnet restore "./Enterprise.Template.WebApi/./Enterprise.Template.WebApi.csproj"
COPY . .
WORKDIR "/src/Enterprise.Template.WebApi"
RUN dotnet build "./Enterprise.Template.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Enterprise.Template.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Enterprise.Template.WebApi.dll"]