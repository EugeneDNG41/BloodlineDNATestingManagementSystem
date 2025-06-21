# BloodlineDNATestingManagementSystem
### Database Migrations (Code First)
To generate the MySQL schema and migrate the database, locate the ConnectionStrings section and update the connection string with your MySQL server credentials:
  ```json
  {
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost; Port=3306; Database=DNATestingDb; Uid=youruid; Pwd=yourpassword; SslMode=Preferred;"
  }
```
This project uses Entity Framework Core with Code First approach. Follow the steps below to manage database schema changes using .NET CLI or Package Manager Console.

#### Using .NET CLI
1. Create a Migration
  ```bash
  dotnet ef migrations add MigrationName
```
2. Apply Migration to Database
  ```bash
  dotnet ef database update
```
3. Remove Last Migration (Optional)
  ```bash
  dotnet ef migrations remove
```

  

#### Using NuGet Package Manager Console
1. Create a Migration
  ```powershell
  Add-Migration MigrationName
```
2. Apply Migration to Database
  ```powershell
  Update-Database
```
3. Remove Last Migration (Optional)
  ```powershell
  Remove-Migration
```
