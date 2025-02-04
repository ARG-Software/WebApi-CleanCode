# WebApi-CleanCode

A C# Web API project following clean code principles, using .NET, Entity Framework, and a modular architecture for maintainability and scalability.

## Prerequisites

To run this project on your local machine, ensure you have the following installed:

1. **Visual Studio 2017** - Update to the latest version (recommended: 15.8.4).
2. **.NET Core 2.1 SDK** - Download from the official [Microsoft .NET download page](https://www.microsoft.com/net/download).
3. **PostgreSQL** - Version 9.4 or higher.
4. **Git** - Available for download [here](https://git-scm.com/downloads).
5. **Git Management Tool** - Such as SourceTree or any preferred tool.

## Recommended Visual Studio Extensions

For an enhanced development experience, consider installing the following extensions:

- **ReSharper** - A productivity extension for .NET developers.
- **CodeMaid** - A tool to clean up and simplify code.
- **Visual Studio Bitbucket Extension** - Integrates Bitbucket with Visual Studio.
- **Color Theme Editor 2017** - Customizes Visual Studio's color theme.

To install these extensions, navigate to `Tools -> Extensions and Updates` within Visual Studio.

## Project Structure

The solution is organized into several projects, each serving a distinct role:

- **RF.Application** - Contains application-level services and business logic.
- **RF.Domain** - Defines domain entities and interfaces.
- **RF.Infrastructure** - Handles data access, configurations, and external integrations.
- **RF.Library** - Provides utility functions and shared components.
- **RF.Services** - Offers additional services supporting the application.
- **RF.WebApi** - Hosts the Web API for external communication.
- **RF.UnitTests** - Contains unit tests to ensure code reliability.

## Getting Started

1. **Clone the Repository**
   ```sh
   git clone https://github.com/ARG-Software/WebApi-CleanCode.git
   ```
2. **Set Up the Database**
   - Ensure PostgreSQL is installed and configured.
   - Update the connection strings in the project settings as needed.
3. **Build the Solution**
   - Open the solution in Visual Studio and build it to restore dependencies.
4. **Run the Application**
   - Start the Web API project to begin interacting with the application.

## Contributing

Contributions are welcome! If you have suggestions or improvements, please open an issue or submit a pull request.

## License

This project is licensed under the MIT License. See the `LICENSE` file for more details.

---

*Note: This README is based on the available information and may need adjustments to align with the project's specifics.*

