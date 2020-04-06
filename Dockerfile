#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
ENV APT_KEY_DONT_WARN_ON_DANGEROUS_USAGE=1

# install NodeJS 13.x
# see https://github.com/nodesource/distributions/blob/master/README.md#deb
RUN apt-get update -yq
RUN apt-get install curl gnupg -yq
RUN curl -sL https://deb.nodesource.com/setup_13.x | bash -
RUN apt-get install -y nodejs

#COPY ["Deploy_O_Mat.API/Deploy_O_Mat.API.csproj", "Deploy_O_Mat.API/"]
COPY . .
#COPY ["NuGet.config", "Deploy_O_Mat.API/"]
RUN ls
RUN dotnet restore "Deploy_O_Mat.API/Deploy_O_Mat.API.csproj"

#WORKDIR "/src/Deploy_O_Mat.API"
WORKDIR "/src/deploy-o-mat"
RUN npm install
RUN npm run build
RUN ls
WORKDIR "/src/Deploy_O_Mat.API"
RUN dotnet build "Deploy_O_Mat.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Deploy_O_Mat.API.csproj" -c Release -o /app/publish



FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Deploy_O_Mat.API.dll"]
