FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY StelexarasApp.Application/StelexarasApp.Application.csproj StelexarasApp.Application/
COPY StelexarasApp.Library/StelexarasApp.Library.csproj StelexarasApp.Library/
COPY StelexarasApp.Services/StelexarasApp.Services.csproj StelexarasApp.Services/
COPY StelexarasApp.DataAccess/StelexarasApp.DataAccess.csproj StelexarasApp.DataAccess/

RUN dotnet restore StelexarasApp.Application/StelexarasApp.Application.csproj
COPY . .

WORKDIR /src/StelexarasApp.Application
RUN dotnet build StelexarasApp.Application.csproj -c $BUILD_CONFIGURATION -o /app/build --no-restore

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish StelexarasApp.Application.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false --no-restore

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StelexarasApp.Application.dll"]