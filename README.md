# Video Game Store

## Entity Framework
This proyect uses the [Identity Framework](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-7.0&tabs=visual-studio) to manage interactions with the database.
To update the datbases, run:
```
dotnet ef database update -c UsersContext 
dotnet ef database update -c GamesDbContext 
```
### Migrations
To create more migrations, use:
```
# For UsersContext
dotnet ef migrations add <migration-name> -o Data\Migrations\Identity -c UsersContext
# For GamesDbContext
dotnet ef migrations add <migration-name> -o Data\Migrations -c GamesDbContext
```

### Install Libman

Verify if Libman is installed

```ps
dotnet tool list --global

Package Id                            Version      Commands
-----------------------------------------------------------
microsoft.web.librarymanager.cli      2.1.175      libman
```

If it's not installed run:

```ps
dotnet tool install --global Microsoft.Web.librarymManager.Cli --version 2.1.175
```

## CSS Libraries

This proyect uses some external css libraries that are not stored on the repo. To use them, do:

### Restore

If *libman.json* is found on the project directory, run:

```ps
libman restore
```

This will restore all *css* libraries to the project.

### Initialize Libman

If *libman.json* is not found, inside the project folder run:

```ps 
libman init -p cdnjs
```

#### Install [Bootstrap](https://getbootstrap.com/docs/5.2/getting-started/introduction)

Install version `5.2.3` of Bootstrap

```ps
libman install bootstrap@5.2.3 -d wwwroot/lib/bootstrap
```

#### Install [Font Awesome](https://fontawesome.com/search?m=free&o=r)

Install Font Awesome version `6.2.1`

```ps
libman install font-awesome@6.2.1 -d wwwroot/lib/font-awesome
```
