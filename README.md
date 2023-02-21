﻿# Video Game Store

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