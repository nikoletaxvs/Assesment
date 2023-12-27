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

Include instructions on how to install and set up your project. Be sure to list any dependencies and provide clear steps.
### Clone the Repository
```bash
# Example installation steps
git clone https://github.com/nikoletaxvs/Assessment
cd Assessment
```
### Install necessary NuGet packages
```bash
#Restore NuGet Packages
dotnet restore

```
### Create a database migration
```bash
dotnet ef migrations add InitialCreate -c ApplicationDbContext

```
### Setup Redis with Docker
```bash
#If redis is not pulled yet, open powershell as an administrator and run, otherwise skip this step
docker pull redis

#Then run
docker run -p 6379:6379 --name my-redis -d redis
```

### Build and Run the API
```bash
dotnet build
dotnet run
```
