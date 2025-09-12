FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY StelexarasApp.Web/StelexarasApp.Web.csproj StelexarasApp.Web/
COPY StelexarasApp.Services/StelexarasApp.Services.csproj StelexarasApp.Services/
COPY StelexarasApp.DataAccess/StelexarasApp.DataAccess.csproj StelexarasApp.DataAccess/
COPY StelexarasApp.Library/StelexarasApp.Library.csproj StelexarasApp.Library/
# add any other referenced projects here…

RUN dotnet restore StelexarasApp.Web/StelexarasApp.Web.csproj
COPY . .

WORKDIR /src/StelexarasApp.Web
RUN dotnet build "StelexarasApp.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "StelexarasApp.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StelexarasApp.Web.dll"]
