# StelexarasApp

## Description

**StelexarasApp** is a robust application designed to streamline the management of summer camps. It offers a comprehensive suite of tools tailored for team leaders, allowing them to efficiently manage their tasks, track child information, and ensure smooth camp operations. The app is built with a clean architecture, leveraging DTOs, service layers, and the repository pattern for optimal performance and maintainability.

## Features

- **To-Do List Management**:  
  A powerful tool for team leaders to manage and prioritize tasks, ensuring that all camp activities are executed efficiently.

- **Child Information Management**:  
  Allows camp staff to update and view detailed information about children, including their age, assigned house, and team leader. The system helps in tracking and managing the number of children in each house.

- **Role Management**:  
  Supports various roles such as **Omadarxis** (Team Leader), **Koinotarxis** (Community Leader), and **Tomearxis** (Area Leader), enabling easy assignment and updates to staff roles.

- **Real-time Updates**:  
  Ensures that all data is synchronized in real-time across all devices, providing accurate and up-to-date information for all staff members.

- **Service Layer and Repository Pattern**:  
  Implements a service layer for business logic and a repository pattern for data access, promoting a clean separation of concerns and facilitating easier unit testing.

- **DTO (Data Transfer Object) Usage**:  
  Uses DTOs to ensure secure and efficient data transfer between layers, limiting the amount of data exposed during transactions.

- **AutoMapper Integration**:  
  Integrates AutoMapper for seamless mapping between entities and DTOs, reducing the need for boilerplate code and minimizing the risk of errors.

## Getting Started

Follow these steps to get the project up and running on your local machine.

1. **Clone the repository**:
    ```bash
    git clone https://github.com/yourusername/StelexarasApp.git
    ```

2. **Navigate to the project directory**:
    ```bash
    cd StelexarasApp
    ```

3. **Install dependencies**:
    ```bash
    dotnet restore
    ```

4. **Run the application**:
    ```bash
    dotnet run
    ```

## Project Structure

- **Controllers**:  
  Handle incoming HTTP requests, invoke the necessary services, and return appropriate responses.

- **Services**:  
  Contain the business logic of the application and handle interactions with repositories for data persistence.

- **Repositories**:  
  Manage data access using Entity Framework, interacting directly with the database to perform CRUD operations.

- **DTOs (Data Transfer Objects)**:  
  Used in controllers and services to securely transfer data between layers, ensuring only the necessary fields are exposed.

- **AutoMapper Profiles**:  
  Define mappings between entities and DTOs, simplifying the process of transforming data between different shapes.

## Contributing

We welcome contributions to the StelexarasApp project! To get started, follow these steps:

1. **Fork the repository**.

2. **Create a new branch**:
    ```bash
    git checkout -b feature/your-feature
    ```

3. **Make your changes**.

4. **Commit your changes**:
    ```bash
    git commit -m "Add your message"
    ```

5. **Push to the branch**:
    ```bash
    git push origin feature/your-feature
    ```

6. **Create a new Pull Request**.

## Testing

To maintain the quality of the codebase, we encourage the use of unit and integration tests.

- **Unit Tests**:  
  Focus on testing the service and repository layers, ensuring that business logic and data access operations are functioning correctly.

- **Integration Tests**:  
  Test the interactions between different layers, verifying that the application behaves as expected under various scenarios.

- **Running Tests**:
    ```bash
    dotnet test
    ```
Here is a sample JSON representation of a **Child** (Paidi in greek) entity used in StelexarasApp:

```json
{
  "fullName": "string",
  "age": 0,
  "sex": 0,
  "skini": {
    "name": "string",
    "omadarxis": "string",
    "omadarxisId": 0,
    "paidia": [
      {
        "fullName": "string",
        "age": 0,
        "seAdeia": true,
        "sex": 0,
        "paidiType": 0,
        "skini": "string"
      }
    ],
    "koinotita": {
      "name": "string",
      "koinotarxis": {
        "fullName": "string",
        "age": 0,
        "sex": 0,
        "koinotita": "string",
        "thesi": 0,
        "omadarxes": [
          "string"
        ]
      },
      "tomeas": {
        "name": "string",
        "tomearxis": {
          "fullName": "string",
          "id": 0,
          "age": 0,
          "sex": 0,
          "tomeas": "string",
          "thesi": 0,
          "koinotarxes": [
            {
              "fullName": "string",
              "age": 0,
              "sex": 0,
              "koinotita": "string",
              "thesi": 0,
              "omadarxes": [
                "string"
              ]
            }
          ]
        },
        "koinotites": [
          "string"
        ]
      },
      "skines": [
        "string"
      ]
    }
  },
  "thesi": 0
}
