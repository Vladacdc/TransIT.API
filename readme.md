# TransITAPI
[![Build status](https://ci.appveyor.com/api/projects/status/ijoktib4wnjmm9no?svg=true)](https://ci.appveyor.com/project/Vladacdc/transit-api)

This project was built on .NetCore 2.2 using AspNetCore, EntityFrameworkCore, AutoMapper.
This repository contains server-side application, client-side is stored [here](https://github.com/Vladacdc/TransIT.Client). 

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.
See deployment for notes on how to deploy the project on a live system.

1) Clone repository;
2) Checkout dev branch;
3) Open Package Manager Console(ALT+T+N+O);
4) Choose TransIT.DAL as default project
[Screenshot](./images/PackageManagerConsole.jpg)
5) Enter command "add-migration TransITDB";
6) Choose TransIT profile
[Screenshot](./images/TransITProfile.jpg)
7) Start Project (F5);

## Running the tests

To Run test choose Test->Run->All Tests or use Ctr + R, A Hotkey
[Screenshot](./images/RunTests.jpg)

## Deployment

Choose TransIT.API project and click right mouse button, then choose publish.

## Built With

* [AutoMapper](https://automapper.org/) - to map DTOs and Entities between BLL an DALl
* [Microsoft.AspNetCore](https://asp.net/) - the highest layer is built on it, Asp provides routing and dependency injection;
* [FluentValidation](https://fluentvalidation.net/) - for validation of entites values;
* [Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) - Swagger tooling for API's built with ASP.NET Core.
Generate beautiful API documentation, including a UI to explore and test operations, directly from your routes, controllers and models;
* [Microsoft.EntityFrameworkCore](https://docs.microsoft.com/en-us/ef/core/) - to work with database;
In this project was used database first to code first approach;
* [Microsoft.AspNet.Identity](https://docs.microsoft.com/en-us/aspnet/identity/overview/getting-started/introduction-to-aspnet-identity) - to control users and their roles;
