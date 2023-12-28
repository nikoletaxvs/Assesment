##Assessment 

## Table of Contents

- [Introduction](#introduction)
- [Installation](#installation)

## Introduction

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

## Endpoints

##### 1. FindSecondLargest

Endpoint: POST /FindSecondLargest

###### Input:

    A JSON body of RequestObj.

###### Output:

    The second largest integer of the array gets returned.

###### Response:

    Status 200: Success. Returns the second largest integer.
    Status 400: Bad Request. The given array should have at least two integers.
    Status 500: Internal Server Error.

###### Example:
```
json

POST /FindSecondLargest

{
  "RequestArrayObj": [4, 7, 1, 9, 3]
}
```
###### Response:
```
json

{
  "secondLargest": 7
}
```

##### 2. GetCountries

Endpoint: GET /GetCountries

###### Output:

    IEnumerable<Country> with common name, capital, and borders as its fields, retrieved from the third-party API, cache, or the database.

###### Response:

    Status 200: Success. Returns the list of countries.
    Status 500: Internal Server Error.

##### 3. DeleteCountriesUtility

Endpoint: GET /DeleteAllCountriesUtility

###### Description:
This utility endpoint deletes all countries and their borders.

###### Response:

    Status 200: Success. Deleted all countries and their borders.
    Status 400: Bad Request. There was some issue.
    Status 500: Internal Server Error.