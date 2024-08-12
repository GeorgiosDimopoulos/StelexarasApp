# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the project file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the application code
COPY . ./
RUN dotnet build -c Release -o out

# Run tests (if you have a test project)
RUN dotnet test

# Define the entry point (for runtime images)
ENTRYPOINT ["dotnet", "out/MyMauiApp.dll"]
