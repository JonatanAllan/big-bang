# Enterprise Template

## Requirements
- .NET 8
- Docker

## Application Architecture - Overview
- Presentation: applications to export and process business rules:
    - Enterprise.Template.WebApi: makes business rules available through a Web API.
    - Enterprise.Template.Consumer: performs the processing of messages present in a RabbitMQ queue.
    - Enterprise.Template.Worker: performs batch processing of business rules.

- Application: contains the implementation of the application's business rule (Use Case):
    - Enterprise.Template.Application: responsible for orchestrating the processing of the business rule and its external dependencies.
 
- Domain: contains the domain entities and interfaces required to run the application:
    - Enterprise.Template.Domain: contains items that represent the domain to be implemented by the application: entities, interfaces, enums, value objects, etc.

- Infrastructure: contains integration with external entities, such as databases, message queues, APIs, etc.:
    - Enterprise.Template.Core: contains the common implementation used in the Infrastructure layer.
    - Enterprise.Template.Data: contains the implementation of data persistence.
    - Enterprise.Template.IoC: registers the services used by the system - Dependency Injection.

- Tests: contains applications' unit tests and integration tests: 
    - Enterprise.Template.Tests: performs unit tests and integration tests.

## Template Setup

1. After downloading the repository, go to the `src` folder and execute the commands below:

- `dotnet clean`
- `dotnet new install .`

A new template called `enterprise-webapi` will be made available in the dotnet CLI template list.
Run the command below to list the available templates and check if the `enterprise-webapi` template has been installed:

- `dotnet new --list`

2. With the template installed on the machine, enter the commands below to create a new solution:

`dotnet new enterprise-webapi -n <project name> -o <project name>`

When the solution is created, it will contain all the layers presented previously.

## Remove template

3. If you want to remove the template from the local machine, enter the commands below:

- `dotnet new uninstall .`

Run the command below to list the available templates and check if the `enterprise-webapi` template has been removed:

- `dotnet new --list`

## Next Steps

- Split presentation layer items into different .NET templates:
    - enterprise-webapi: creates a new Web API
    - enterprise-consumer: creates a new Consumer worker.
    - enterprise-job: creates a new Worker.

- Define the Build and Deployment process (Automation)
