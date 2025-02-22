# ToDoApp

A simple To-Do List API built with .NET Core and ABP Framework.

## Features
- CRUD operations for to-do items.
- Caching for improved performance.
- Swagger documentation.
- Global Exception handling.
- Docker support for containerization.

## Installation
1. Clone the repository:
   ```bash
        git clone https://[github.com/waheb-benzaid/ToDoApp.git]

2. Navigate to the project directory:
   ```bash
        cd ToDoApp
   
3. Restore dependencies:
   ```bash
        dotnet restore
4. Run the application:
   ```bash
        dotnet run --project ToDoApp.HttpApi.Host

## Usage

1. API Endpoints

       GET /api/todo: Retrieve all to-do items.

       GET /api/todo/{id}: Retrieve a to-do item by ID.

       POST /api/todo: Create a new to-do item.

       PUT /api/todo/{id}: Update an existing to-do item.

       DELETE /api/todo/{id}: Delete a to-do item.
   
2.  Add a .gitignore File

   A .gitignore file prevents unnecessary files (e.g., bin, obj, .vs, node_modules) from being tracked by Git. This keeps your repository clean and reduces its size.



### Pre-requirements

* [.NET 9.0+ SDK](https://dotnet.microsoft.com/download/dotnet)
* [Node v20.11+](https://nodejs.org/en)

### Configurations

The solution comes with a default configuration that works out of the box. However, you may consider to change the following configuration before running your solution:


### Before running the application

#### Generating a Signing Certificate

In the production environment, you need to use a production signing certificate. ABP Framework sets up signing and encryption certificates in your application and expects an `openiddict.pfx` file in your application.

This certificate is already generated by ABP CLI, so most of the time you don't need to generate it yourself. However, if you need to generate a certificate, you can use the following command:

```bash
dotnet dev-certs https -v -ep openiddict.pfx -p 94103f56-545f-4c24-9186-8fc241d74cc0
```

> `94103f56-545f-4c24-9186-8fc241d74cc0` is the password of the certificate, you can change it to any password you want.

It is recommended to use **two** RSA certificates, distinct from the certificate(s) used for HTTPS: one for encryption, one for signing.

For more information, please refer to: https://documentation.openiddict.com/configuration/encryption-and-signing-credentials.html#registering-a-certificate-recommended-for-production-ready-scenarios

> Also, see the [Configuring OpenIddict](https://abp.io/docs/latest/deployment/configuring-openiddict#production-environment) documentation for more information.

#### Install Client-Side Libraries

Run the following command in the directory of your final application:

```bash
abp install-libs
```

> This command installs all NPM packages for MVC/Razor Pages and Blazor Server UIs and this command is already run by the ABP CLI, so most of the time you don't need to run this command manually.

#### Create the Database

Run `ToDoApp.DbMigrator` to create the initial database. This should be done in the first run. It is also needed if a new database migration is added to the solution later.

### Solution structure

This is a layered monolith application that consists of the following applications:

* `ToDoApp.DbMigrator`: A console application which applies the migrations and also seeds the initial data. It is useful on development as well as on production environment.

### Deploying the application

Deploying an ABP application is not different than deploying any .NET or ASP.NET Core application. However, there are some topics that you should care about when you are deploying your applications. You can check ABP's [Deployment documentation](https://abp.io/docs/latest/deployment) before deploying your application.

### Additional resources

You can see the following resources to learn more about your solution and the ABP Framework:

* [Web Application Development Tutorial](https://abp.io/docs/latest/tutorials/book-store/part-01?UI=Blazor&DB=EF)
* [Application Startup Template Structure](https://abp.io/docs/latest/solution-templates/layered-web-application)
