# StelexarasApp

A management system for summer camps. It helps camp staff (team leaders and above) track children, staff, camp structure (tents, communities, areas), duties and expenses, with changes pushed to clients in real time.

<img width="948" height="713" alt="Screenshot 2026-09-16 195355" src="https://github.com/user-attachments/assets/2f22994a-ffb9-405d-a36f-5522e65663cf" />

## Features

- **Children (Paidia)** – store age, sex, parent phone, leave permission (`seAdeia`) and the assigned tent (Skini).
- **Staff (Stelexi)** – manage roles: **Omadarxis** (team leader), **Koinotarxis** (community leader), **Tomearxis** (area leader), **Ekpaideutis** (trainer), **Anwtatos** (senior) and **EidikoStelexos** (special staff).
- **Camp structure** – Skini (tent) → Koinotita (community) → Tomeas (area), plus Xwroi (places) for trainees and seniors.
- **Duties and expenses** – task list and expense tracking for team leaders.
- **Real-time updates** – SignalR hub (`/myHub`) keeps clients in sync.
- **Auth** – JWT login (`POST /api/Auth/login`).
- **Logging** – CRUD and error logs, viewable in a WPF dashboard.
- **Health checks** – `/health` and a UI at `/health-ui`.
- **Experimental AI chat** – local Ollama (`llama3.2`) integration.

## Architecture

Layered/clean architecture: controllers → services (AutoMapper, FluentValidation, FluentResults) → repositories (EF Core) → SQL Server. DTOs separate the API contract from the entities.

| Project | Purpose |
| --- | --- |
| `StelexarasApp.API` | ASP.NET Core Web API: controllers, JWT auth, Swagger, SignalR hub, health checks |
| `StelexarasApp.Application` | Blazor web UI; calls the API through Refit clients |
| `StelexarasApp.Mobile` | .NET MAUI client (Android, iOS, Mac Catalyst) |
| `StelexarasApp.Services` | Business logic, mappers, validators |
| `StelexarasApp.DataAccess` | `AppDbContext`, repositories, EF Core migrations |
| `StelexarasApp.Library` | Shared models, DTOs, query parameters |
| `StelexarasApp.LogsDashboard` | WPF viewer for CRUD and error logs |
| `StelexarasApp.AI` | Ollama-based chat service (experimental) |
| `StelexarasApp.ClientApp`, `StelexarasApp.ConsoleApp` | Small client/console utilities |
| `StelexarasApp.Tests` | xUnit tests: repository, service, integration and Selenium UI tests |

Structure diagrams are in `DomiStructure.drawio` and `StelexiStructure.drawio`.

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- SQL Server (local or via Docker)
- Optional: Docker, the MAUI workload (`dotnet workload install maui`) for the mobile app, and [Ollama](https://ollama.com) for the AI project

## Getting Started

```bash
git clone https://github.com/yourusername/StelexarasApp.git
cd StelexarasApp
dotnet restore
```

### Configuration

Settings live in `StelexarasApp.API/appsettings.json`:

- `ConnectionStrings:DefaultConnection` – SQL Server connection string
- `Jwt:UserName`, `Jwt:Key`, `Jwt:Issuer`, `Jwt:Audience` – token settings
- `Admin:Password` – login password

The committed values are development placeholders. **Override them** (user secrets or environment variables) for any real deployment.

`StelexarasApp.Application/appsettings.json` has `ApiBaseUrl` (default `https://localhost:7049`).

### Run locally

Pending EF migrations are applied automatically when the API starts.

```bash
# 1. API (Swagger at https://localhost:7049/swagger)
dotnet run --project StelexarasApp.API --launch-profile https

# 2. Blazor UI
dotnet run --project StelexarasApp.Application
```

### Run with Docker

```bash
export MSSQL_SA_PASSWORD='<strong-password>'   # PowerShell: $env:MSSQL_SA_PASSWORD='...'
docker compose up --build
```

| Service | URL |
| --- | --- |
| API / Swagger | http://localhost:8080/swagger |
| Blazor frontend | http://localhost:3000 |
| SQL Server | localhost:1433 |
| Portainer | http://localhost:9000 |
| Heimdall dashboard | http://localhost:8090 |

## Testing

```bash
dotnet test
```

The suite covers repositories (in-memory/SQLite), services (Moq), integration tests and Selenium UI tests (these need Chrome and a running app). CI runs build and tests on pull requests to `master` (`.github/workflows/`).

## Contributing

1. Fork the repository and create a branch: `git checkout -b feature/your-feature`
2. Make your changes and add tests.
3. Run `dotnet test`.
4. Commit, push and open a pull request against `master`.

## API Response Example

A child (`Paidi`):

```json
{
  "id": 1,
  "firstName": "Maria",
  "lastName": "Papadopoulou",
  "parentTel": "6912345678",
  "age": 12,
  "seAdeia": false,
  "sex": 0,
  "paidiType": 0,
  "skiniId": 3,
  "skini": {
    "id": 3,
    "name": "Skini A"
  }
}
```
