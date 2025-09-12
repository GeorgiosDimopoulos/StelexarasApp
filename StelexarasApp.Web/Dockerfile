# Use the official .NET runtime as a base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Use the .NET SDK to build the project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["StelexarasApp.Web.csproj", "./"]
RUN dotnet restore "StelexarasApp.Web.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "StelexarasApp.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StelexarasApp.Web.csproj" -c Release -o /app/publish

# Use the final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StelexarasApp.Web.dll"]
