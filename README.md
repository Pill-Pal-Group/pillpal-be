# PillPal - Backend WebAPI

<a name="top">

<div align="left">
    <a href="./LICENSE"><img src="https://img.shields.io/badge/License-GPL-yellow.svg" alt="gpl-license"/></a>
</div>

<div align="left">
    <a href="https://dotnet.microsoft.com"><img src="https://img.shields.io/badge/.NET%208.0-512BD4?style=flat&logo=dotnet&logoColor=white" alt="dotnet"/></a>
    <a href="https://www.nuget.org/"><img src="https://img.shields.io/badge/NuGet-004880?style=flat&logo=nuget&logoColor=white" alt="nuget"/></a>
    <a href="https://www.microsoft.com/sql-server"><img src="https://img.shields.io/badge/SQL_Server-CC2927?style=flat&logo=microsoft%20sql%20server" alt="sqlserver"/></a>
    <a href="https://redis.io/"><img src="https://img.shields.io/badge/Redis-%23DD0031.svg?&style=flat&logo=redis&logoColor=white" alt="redis"/></a>
    <a href="https://cloud.google.com/"><img src="https://img.shields.io/badge/Google_Cloud-4285F4?style=flat&logo=google-cloud&logoColor=white" alt="google-cloud"/></a>
    <a href="https://firebase.google.com/"><img src="https://img.shields.io/badge/Firebase-ffca28?style=flat&logo=firebase&logoColor=black" alt="firebase"/></a>
    <a href="https://sonarcloud.io/"><img src="https://img.shields.io/badge/SonarCloud-F3702A?style=flat&logo=sonarcloud&logoColor=white" alt="sonar"/></a>
    <a href="https://swagger.io/"><img src="https://img.shields.io/badge/Swagger-85EA2D?style=flat&logo=swagger&logoColor=black" alt="swagger"/></a>
    <a href="https://www.openapis.org/"><img src="https://img.shields.io/badge/OpenAPI-6BA539?style=flat&logo=openapiinitiative&logoColor=white" alt="openapi"/></a>
    <a href="https://jwt.io/"><img src="https://img.shields.io/badge/JSON%20Web%20Tokens-000000?style=flat&logo=jsonwebtokens&logoColor=white" alt="jwt"/></a>
</div>

<details>
  <summary>Click to expand</summary>

- [Introduction](#introduction)
- [System Overview](#system-overview)
- [Installation](#installation)
- [Contributors](#contributors)

</details>

## Introduction

Backend API for PillPal project. Built with the latest .NET 8.0 with Entity Framework Core. The source code is structured following the Clean Architecture with the Repository Pattern and was meticulously well-documented with Swagger and OpenAPI.

<p align="right"><a href="#top">[back to top]</a></p>

## System Overview

<div align="center">
    <img src="./res/be-system.png" alt="system-design"/>
    <p>Backend Architecture Design</p>
</div>

<div align="center">
    <img src="./res/be-package.png" alt="package-design"/>
    <p>Backend Package Design</p>
</div>

<div align="center">
    <img src="./res/logical-erd.png" alt="logical-db"/>
    <p>Logical Database Design</p>
</div>

<p align="right"><a href="#top">[back to top]</a></p>

## Installation

Make sure you have the **.NET 8.0 SDK** installed on your machine. If not, you can download it [here](https://dotnet.microsoft.com/download/dotnet/8.0).

1. Clone the repository

```console
git clone https://github.com/Pill-Pal-Group/pillpal-be.git
```

2. Navigate to the project directory

```console
cd pillpal-be/src
```

3. Set the environment variables in the `appsettings.json` file

Configure environment variables in the `appsettings.json` file following the template provided in `appsettings.json.example`.
Both file are located in the `src/PillPal.WebApi` directory and should look like below:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "FirebaseSettings": {
    "ProjectId": "ProjectId",
    "ServiceKey": "ServiceKey"
  },
  "JwtSettings": {
    "SecretKey": "SecretKey",
    "Issuer": "Issuer",
    "Audience": "Audience",
    "Expires": 60,
    "RefreshExpires": 43200
  },
  "CacheSettings": {
    "SlidingExpiration": 5,
    "AbsoluteExpiration": 60
  },
  "ConnectionStrings": {
    "REDIS": "REDIS_CONNECTION_STRING",
    "PILLPAL_DB": "PILLPAL_DB_CONNECTION_STRING",
    "HANGFIRE_DB": "HANGFIRE_DB_CONNECTION_STRING"
  },
  "Hangfire": {
    "User": "User",
    "Pass": "Pass"
  },
  "ZaloPay": {
    "AppId": "AppId",
    "AppUser": "AppUser",
    "Key1": "Key1",
    "Key2": "Key2",
    "PaymentUrl": "PaymentUrl",
    "ReturnUrl": "ReturnUrl",
    "IpnUrl": "IpnUrl"
  }
}
```

4. Migrate the database

```console
dotnet ef database update --project PillPal.Infrastructure --startup-project PillPal.WebApi
```

5. Run the project

For straightforward execution, run the following command:

```console
dotnet run --project PillPal.WebApi
```

Or for more flexible options while running the project, you can use the following command:

```console
dotnet watch run --project PillPal.WebApi
```

<p align="right"><a href="#top">[back to top]</a></p>

## Contributors

<a href="https://github.com/Pill-Pal-Group/pillpal-be/graphs/contributors">
  <img src="https://contrib.rocks/image?repo=Pill-Pal-Group/pillpal-be" alt="contributor"/>
</a>

<p align="right"><a href="#top">[back to top]</a></p>
