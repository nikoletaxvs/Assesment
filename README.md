# Project Title

Brief description of your project.

## Table of Contents

- [Project Title](#project-title)
- [Description](#description)
- [Installation](#installation)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## Description

Provide a more detailed overview of your project. What is it about? What problem does it solve?

## Installation

5 step install and project setup.
###### Step 1. Clone the Repository
```bash
# Example installation steps
git clone https://github.com/nikoletaxvs/Assessment
cd Assessment
```
###### Step2 .Install necessary NuGet packages
```bash
#Restore NuGet Packages
dotnet restore

```
###### Step 3.Create a database migration
```bash
dotnet ef migrations add InitialCreate -c ApplicationDbContext

```
###### Step 4.Setup Redis with Docker
```bash
#If redis is not pulled yet, open powershell as an administrator and run, otherwise skip this step
docker pull redis

#Then run
docker run -p 6379:6379 --name my-redis -d redis
```

###### Step 5.Build and Run the API
```bash
dotnet build
dotnet run
```
