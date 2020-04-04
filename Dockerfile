#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src


COPY ["Deploy_O_Mat.API/Deploy_O_Mat.API.csproj", "Deploy_O_Mat.API/"]
COPY ["NuGet.config", "Deploy_O_Mat.API/"]

RUN dotnet restore "Deploy_O_Mat.API/Deploy_O_Mat.API.csproj" --configfile "Deploy_O_Mat.API/NuGet.config"
COPY . .
WORKDIR "/src/Deploy_O_Mat.API"


RUN dotnet build "Deploy_O_Mat.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Deploy_O_Mat.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Deploy_O_Mat.API.dll"]