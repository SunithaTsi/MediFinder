# Hosting MediFinder in IIS

MediFinder is an ASP.NET Core MVC app. IIS hosts it through the ASP.NET Core Module, which starts the app process when requests arrive.

## 1. Prerequisites

- IIS enabled on Windows.
- ASP.NET Core Hosting Bundle installed.
- .NET 8 runtime installed.
- SQL Server available to the IIS app pool.

The current app uses SQL Server LocalDB:

```text
Server=(localdb)\mssqllocaldb;Database=MediFinderDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True
```

LocalDB is convenient for development, but IIS usually runs under an application pool identity. For a durable IIS setup, use SQL Server Express, SQL Server Developer, or a full SQL Server instance and update `appsettings.json`.

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

If IIS shows a database or startup error, the most likely cause is LocalDB access. Change `DefaultConnection` to a SQL Server instance that the IIS app pool identity can access.
