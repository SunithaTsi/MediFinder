# Hosting MediFinder in IIS

MediFinder is an ASP.NET Core MVC app. IIS hosts it through the ASP.NET Core Module, which starts the app process when requests arrive.

## 1. Prerequisites

- IIS enabled on Windows.
- ASP.NET Core Hosting Bundle installed.
- .NET 8 runtime installed.
- SQL Server available to the IIS app pool.

The app is configured to use the local default SQL Server instance:

```text
Server=localhost;Database=MediFinderDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True
```

When hosted in IIS, the app runs under the `IIS APPPOOL\MediFinder` identity by default. That identity needs access to the `MediFinderDb` database.

## 2. Publish

From the project folder:

```powershell
dotnet publish .\MediFinder.csproj -c Release -o .\publish\iis
```

## 3. Configure IIS

Open PowerShell as Administrator and run:

```powershell
Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass
.\deployment\IIS-Setup.ps1
```

The default site URL is:

```text
http://localhost:8085
```

To use another port:

```powershell
.\deployment\IIS-Setup.ps1 -Port 8090
```

## 4. IIS Manager Checks

- Application pool name: `MediFinder`
- .NET CLR version: `No Managed Code`
- Managed pipeline mode: `Integrated`
- Site physical path: `publish\iis`
- Site binding: `http`, port `8085`

## 5. Database Note

If IIS shows a database or startup error, grant database access to the app pool identity:

```sql
CREATE LOGIN [IIS APPPOOL\MediFinder] FROM WINDOWS;
USE [MediFinderDb];
CREATE USER [IIS APPPOOL\MediFinder] FOR LOGIN [IIS APPPOOL\MediFinder];
ALTER ROLE db_owner ADD MEMBER [IIS APPPOOL\MediFinder];
```
