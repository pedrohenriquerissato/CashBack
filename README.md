<h1 align="center"> CashBack API </h1> <br>
<p align="center">
  <a href="https://i.ibb.co/QQXv6Xs/swagger.png">
    <img alt="A white screen web page with consumable api endpoints" title="CashBack API" src="https://i.ibb.co/QQXv6Xs/swagger.png" width="450">
  </a>
</p>

<p align="center">
  An API for check your purchases and get your cashback!
</p>

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [TODO](#todo)
- [Feedback](#feedback)
- [Build Process](#build-process)
- [Acknowledgments](#acknowledgments)

## Introduction

[![Build Status](https://travis-ci.org/pedrohenriquerissato/CashBack.svg?branch=master)](https://travis-ci.org/pedrohenriquerissato/CashBack)
[![Coverage](https://coveralls.io/repos/github/pedrohenriquerissato/CashBack/badge.svg?branch=master)](https://coveralls.io/github/pedrohenriquerissato/CashBack?branch=master)
[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg?style=flat-square)](http://makeapullrequest.com)

## Features

Implemented stuff:

- Register a new Retailer
- Login with a new Retailer account
- Register a new purchase
- Retrieve all purchases with cashback percents / values
- Retrieve cashback total

Extra implemented stuff:

- JWT Token Authentication
- Unit Tests - with xUnit
- Logging Interface
- Github Action to test and build. [Check it out!](https://github.com/pedrohenriquerissato/CashBack/pull/1)
- Swagger API Documentation
- Retailer & Purchase CRUD (not all endpoints are externally available)
- Domain Driven Design - DDD
- Implemented Travis CI
- Implemented Coverall

## TODO

- Implement integration tests
- Implement pagination on GetAllWithPurchase endpoint
- Implement pagination on GetAllWithRetailerAsync endpoint
- Implement database log save instead of a file
- Move log implementation to service
- Implement retailer's documentId validation on controller

<h3 align="center"> CashBack Purchases </h3> <br>
<p align="center">
  <img src = "https://i.ibb.co/C9YkpnS/img1.png" width=700>
</p>

<h3 align="center"> External API CashBack Total </h3> <br>
<p align="center">
  <img src = "https://i.ibb.co/9pTKrKM/img2.png" width=700>
</p>

## Feedback

Feel free to send me feedback on [Email](mailto:pedro_giberti@hotmail.com) or [file an issue](https://github.com/gitpoint/git-point/issues/new). Feature requests are always welcome.

## Build Process

- Install [.NET Core 3.1](https://dotnet.microsoft.com/download) version 3.1.8
- Install [Docker](https://www.docker.com/get-started)
- Install [dotnet ef tools](https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet#installing-the-tools)

- Install & Run SQL Server:
  `docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=yourStrong(!)Password' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest`

- Run on project:
  `dotnet ef database update InitialCreate`

- Then finally run:
  `dotnet build`
  `dotnet run`

## Acknowledgments

- GitPoint for borrowing me their README :) - [Check](https://github.com/gitpoint/git-point#readme)
- Thanks for the opportunity.
