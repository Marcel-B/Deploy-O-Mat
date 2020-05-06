#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
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

# Copy only API for restore packages
COPY ["Deploy_O_Mat.Web.API/Deploy-O-Mat.Web.API.csproj", "Deploy-O-Mat.Web.API/"]

# Restore packages
RUN dotnet restore "Deploy_O_Mat.Web.API/Deploy-O-Mat.Web.API.csproj"

# Copy rest of the Project
COPY . .

# Build API
WORKDIR "/src/Deploy-O-Mat.Web.API"
RUN dotnet build "Deploy-O-Mat.Web.API.csproj" -c Release -o /app/build

# Restore and Build SPA
WORKDIR "/src/deploy-o-mat"
RUN ls
RUN npm install
RUN npm run build

# Publish
WORKDIR "/src/Deploy-O-Mat.Web.API"
FROM build AS publish
RUN dotnet publish "Deploy-O-Mat.Web.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Deploy-O-Mat.Web.API.dll"]
