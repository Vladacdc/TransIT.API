# TransITAPI
[![Build status](https://ci.appveyor.com/api/projects/status/ijoktib4wnjmm9no?svg=true)](https://ci.appveyor.com/project/Vladacdc/transit-api)

This project was built on .NetCore 2.2 using AspNetCore, EntityFrameworkCore, AutoMapper.
This repository contains server-side application, client-side is stored [here](https://github.com/Vladacdc/TransIT.Client). 

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.
See deployment for notes on how to deploy the project on a live system.

### Prerequisites

What things you need to install the software and how to install them

```
[Visual Studio IDE Community 2019](https://visualstudio.microsoft.com/) or similar

[.Net Core 2.2 SDK](https://dotnet.microsoft.com/download/dotnet-core/2.2)
```

### Installing

1) Install Visual Studio IDE with ASP.NET, be sure to check out ASP.NET and web development
[Screenshot](./images/PackageManagerConsole.jpg);
2) Intsall .NetCore 2.2 SDK;

### Build project
1) Clone repository;
2) Checkout dev branch;
3) Open Package Manager Console(ALT+T+N+O);
4) Choose TransIT.API as default project
[Screenshot](./images/PackageManagerConsole.jpg);
5) Enter command "add-migration TransITDB";
6) Choose TransIT profile
[Screenshot](./images/TransITProfile.jpg);
7) Start Project (F5);
8) Do not close created chrome tab with swagger or console application until you want server to stop;

#### Possible Errors and fixing
<details close>
<summary>The current .NET SDK does not support targeting .NET Core 2.2. Either target .NET Core 2.1 or lower, or use a version of the .NET SDK that supports .NET Core 2.2.</summary>
Install <a href="https://dotnet.microsoft.com/download/dotnet-core/2.2">.Net Core 2.2 SDK</a>;
</details>
<details close>
<summary>System.AggregateException at app.SeedEssentialAsync</summary>
You didn't create any migration for db, repeat 4th and 5th step of Getting Started
</details>
<details close>
<summary>System.AggregateException at any part of the program</summary>
Probably, You made changes to entities that need recreation of database.
To recreate database delete Migrations folder and database (CTRL+\, CTRL+S [Screenshot](./images/DeleteDB.jpg).)
Also this exception can occur if you violate database restrictions,
make sure that you understand requirements right and are using UnitOfWork properly.
</details>

## Running the tests

To Run the test choose Test->Run->All Tests or use CTRL + R, A Hotkey
[Screenshot](./images/RunTests.jpg).

## Development

Development server is accessible via [https://localhost:8080](https://localhost:8080) or [http://localhost:5000](http://localhost:5000).
Intial users that are seeded after database is created can be found in [TransIT.API/appsettings.json](TransIT.API/appsettings.json);

## Deployment

Choose TransIT.API project and click right mouse button, then choose publish.

## Built With

* [AutoMapper](https://automapper.org/) - to map DTOs and Entities between BLL an DAL;
* [Microsoft.AspNetCore](https://asp.net/) - the highest layer is built on it, Asp provides routing and dependency injection;
* [FluentValidation](https://fluentvalidation.net/) - for validation of entites values;
* [Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) - Swagger tooling for API's built with ASP.NET Core.
Generate beautiful API documentation, including a UI to explore and test operations, directly from your routes, controllers and models;
* [Microsoft.EntityFrameworkCore](https://docs.microsoft.com/en-us/ef/core/) - to work with database, using code first with existing database approach;
* [Microsoft.AspNet.Identity](https://docs.microsoft.com/en-us/aspnet/identity/overview/getting-started/introduction-to-aspnet-identity) - to control users and their roles;
