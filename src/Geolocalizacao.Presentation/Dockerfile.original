#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
# Instalar NodeJs 
RUN apt-get update && \ 
apt-get install -y wget && \ 
apt-get install -y gnupg2 && \ 
wget -qO- https://deb.nodesource.com/setup_14.x | bash - && \
apt-get install -y build-essential nodejs 
# End Install
WORKDIR /src
COPY ["src/Geolocalizacao.Presentation/Geolocalizacao.Presentation.csproj", "src/Geolocalizacao.Presentation/"]
COPY ["src/Geolocalizacao.Domain.Core/Geolocalizacao.Domain.Core.csproj", "src/Geolocalizacao.Domain.Core/"]
COPY ["src/Geolocalizacao.Infra.CrossCutting.Email/Geolocalizacao.Infra.CrossCutting.Email.csproj", "src/Geolocalizacao.Infra.CrossCutting.Email/"]
COPY ["src/Geolocalizacao.Infra.CrossCutting.IoC/Geolocalizacao.Infra.CrossCutting.IoC.csproj", "src/Geolocalizacao.Infra.CrossCutting.IoC/"]
COPY ["src/Geolocalizacao.Application/Geolocalizacao.Application.csproj", "src/Geolocalizacao.Application/"]
COPY ["src/Geolocalizacao.Domain/Geolocalizacao.Domain.csproj", "src/Geolocalizacao.Domain/"]
COPY ["src/Geolocalizacao.Infra.CrossCutting.Bus/Geolocalizacao.Infra.CrossCutting.Bus.csproj", "src/Geolocalizacao.Infra.CrossCutting.Bus/"]
COPY ["src/Geolocalizacao.Infra.CrossCutting.Identity/Geolocalizacao.Infra.CrossCutting.Identity.csproj", "src/Geolocalizacao.Infra.CrossCutting.Identity/"]
COPY ["src/Geolocalizacao.Infra.CrossCutting.ViaCep/Geolocalizacao.Infra.CrossCutting.ViaCep.csproj", "src/Geolocalizacao.Infra.CrossCutting.ViaCep/"]
COPY ["src/Geolocalizacao.Infra.Data/Geolocalizacao.Infra.Data.csproj", "src/Geolocalizacao.Infra.Data/"]
RUN dotnet restore "src/Geolocalizacao.Presentation/Geolocalizacao.Presentation.csproj"
COPY . .
WORKDIR "/src/src/Geolocalizacao.Presentation"
RUN dotnet build "Geolocalizacao.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Geolocalizacao.Presentation.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Geolocalizacao.Presentation.dll"]