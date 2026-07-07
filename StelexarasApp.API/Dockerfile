FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY StelexarasApp.API/StelexarasApp.API.csproj StelexarasApp.API/
COPY StelexarasApp.Services/StelexarasApp.Services.csproj StelexarasApp.Services/
COPY StelexarasApp.DataAccess/StelexarasApp.DataAccess.csproj StelexarasApp.DataAccess/
COPY StelexarasApp.Library/StelexarasApp.Library.csproj StelexarasApp.Library/

RUN dotnet restore StelexarasApp.API/StelexarasApp.API.csproj
COPY . .

WORKDIR /src/StelexarasApp.API
RUN dotnet build StelexarasApp.API.csproj -c $BUILD_CONFIGURATION -o /app/build --no-restore

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish StelexarasApp.API.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StelexarasApp.API.dll"]