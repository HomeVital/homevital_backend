# HomeVital_Backend

## MEMO
### run .net core project
  1. Navigate to the API folder
  ```sh
  cd .\HomeVital.API\
  ```
  2. Running the project
  ```sh
  dotnet run
  ```

## Migrations and appsetting.json
  ### Appsettings
  Appsettings is now gitignored so you can set up your own local database if you want to test something out before it reaches DEV.
  To change the appsettings on DEV/MAIN you need to:

  Run ```git update-index --no-assume-unchanged HomeVital.API/appsettings.json```
  
  Change the appsettings.json file, commit and push and merge to dev.

  Run ```git update-index --assume-unchanged HomeVital.API/appsettings.json``` to stop tracking again.

  ### Migrations
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
  dotnet ef database update --startup-project ../HomeVital.API
  ```
  or
  ```sh
  dotnet ef database update --project HomeVital.Repositories --startup-project HomeVital.API
  ```

Migrations for DEV and Main branches that runs on Azure are updated automatically.
  