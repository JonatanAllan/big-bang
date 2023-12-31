#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CaliberFS.Template.IoC/CaliberFS.Template.IoC.csproj", "CaliberFS.Template.IoC/"]
COPY ["CaliberFS.Template.Consumer/CaliberFS.Template.Consumer.csproj", "CaliberFS.Template.Consumer/"]
COPY ["CaliberFS.Template.Application/CaliberFS.Template.Application.csproj", "CaliberFS.Template.Application/"]
COPY ["CaliberFS.Template.Core/CaliberFS.Template.Core.csproj", "CaliberFS.Template.Core/"]
COPY ["CaliberFS.Template.Domain/CaliberFS.Template.Domain.csproj", "CaliberFS.Template.Domain/"]
COPY ["CaliberFS.Template.Data/CaliberFS.Template.Data.csproj", "CaliberFS.Template.Data/"]
RUN dotnet restore "./CaliberFS.Template.Consumer/./CaliberFS.Template.Consumer.csproj"
COPY . .
WORKDIR "/src/CaliberFS.Template.Consumer"
RUN dotnet build "./CaliberFS.Template.Consumer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CaliberFS.Template.Consumer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CaliberFS.Template.Consumer.dll"]