# Infrastructure

Add migration

```
dotnet ef migrations add MigrationName --context databasecontext -p ..\Infrastructure\Infrastructure.csproj -s .\Api.csproj -o Data/Migrations
```
