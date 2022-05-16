# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# copy csproj and restore as distinct layers
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY *.sln .
COPY ["ProEventos.API/ProEventos.API.csproj", "ProEventos.API/"]
COPY ["ProEventos.Application/ProEventos.Application.csproj", "ProEventos.Application/"]
COPY ["ProEventos.Persistence/ProEventos.Persistence.csproj", "ProEventos.Persistence/"]
COPY ["ProEventos.Domain/ProEventos.Domain.csproj", "ProEventos.Domain/"]
RUN dotnet restore
COPY . .
WORKDIR "/src/ProEventos.API"
RUN dotnet build "ProEventos.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProEventos.API.csproj" -c Release -o /app/publish

# final stage/image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProEventos.API.dll"]