# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project file and restore dependencies
COPY *.csproj .
RUN dotnet restore

# Copy the rest of the app and build it
COPY . .
RUN dotnet publish -c Release -o out

# Use the official runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Expose port 80 for the web API
EXPOSE 80

# Define the entry point for the app
ENTRYPOINT ["dotnet", "YourWebApiProjectName.dll"]
