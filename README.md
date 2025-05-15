# HomeVital Harmony - Backend 

## Description
This project is part of a final project in computer science at Reykjavík University.

This is a API for a web application and an App that is used to monitor the health of elderly patients living at home and them to use a phone application to input their measurements. 

The API is built using .NET Core and Entity Framework Core, and it uses a Postgres Server database to store data. The API is designed to be used with a web application and an App, but it can also be used with other applications that support RESTful APIs.

### Group Members
Aron Ingi Jónsson   |  aronj22@ru.is | Guarantor for API

Jakub Ingvar Pitak  |   jakub22@ru.is

Sindri Guðmundsson  |   sindrig23@ru.is

Þórir Gunnar Valgeirsson    |   thorirv21@ru.is 

## Getting started
### Prerequisites
- Visual Studio 2022 or preferred IDE
- .NET Core 8.0
- Entity Framework Core
- Pgadmin 
- Postgres Server

### Installation and Setup
1. Clone the repository
2. Open the solution in Visual Studio or your preferred IDE
3. open the terminal in the API folder 
```sh
cd .\HomeVital.API\
```
4. Run the following command to install the required packages
```sh
dotnet restore
dotnet build
```
5. Open appsettings.json and set up your database connection string
```json
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=HomeVital;User Id=sa;Password=yourpassword;"
  }
```
6. Open the terminal in the Repository folder
```sh
cd .\HomeVital.Repository\
```
7. Run the following command to create the database
```sh
dotnet ef database update
```
8. Open the terminal in the API folder
```sh
cd .\HomeVital.API\
```
9. Run the following command to run the project
```sh
dotnet run
```
10. Open your web browser and navigate to http://localhost:5118/ or http://localhost:5118/swagger to see the API documentation. (Under LaunchSettings.json you can change the port if you want to run it on a different port.)

11. You can use Postman or any other API testing tool to test the API endpoints.

12. You can use the SQL Server Management Studio or Pgadmin to connect to the database and see the data that is being stored in the database.



## Migrations and appsetting.json
  ### Appsettings
  Appsettings is now gitignored so you can set up your own local database if you want to test something out before it reaches DEV.
  To change the appsettings on DEV/MAIN you need to:

  ~

  1. Run   
  
  ```sh
  git update-index --no-assume-unchanged HomeVital.API/appsettings.json  
  ```

  2. Change the appsettings.json file 
  
  3. Comment appsetting.json out in the gitignore
  
  4. commit and push and merge to dev.

  5. Uncomment appsettings.json in the gitignore file

  6. Run 
  ```sh
  git update-index --assume-unchanged HomeVital.API/appsettings.json
  ``` 
  to stop tracking again.

  ### Migrations
To add migrations to the database you need to have the following:

Navigate to the Repository folder
  ```sh
  cd .\HomeVital.Repository\
  ```

  Run the command
  ```sh
  dotnet ef migrations add <Your-migration-name>
  ```
  If you want to add the migrations to your database run this command
  ```sh
  dotnet ef database update 
  ```


Migrations for DEV and Main branches that runs on Azure are updated automatically. 


## Testing The API with Dotnet
### Integration Testing
To run the integration tests, you need to navigate to the Tests folder and make sure you have the appsettings.json file set up correctly.
```sh 
cd .\HomeVital.Tests\
```
Then run the following command:
```sh
.\RunTests.ps1
```
This will run all the tests in the project and output the results to the console.

The tests are located in the HomeVital.Tests project and are divided into different classes based on the functionality being tested. Each test class contains multiple test methods that test different aspects of the API.


If you run into any trouble, please contact us and let us know of the trouble.