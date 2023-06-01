# Mango web .NET8

In this repository you will find a shopping cart web site using a ```microservice``` structure.

This repository was initially created using ```.NET 8 Preview```, if you see this repo and still does not been deployed ```.NET 8```, remember to install the preview using the following link.

https://dotnet.microsoft.com/en-us/download/dotnet/8.0

Make sure that if you are working in a project like this and already exists a new preview version, you should have the same preview version both the project and ```NuGet``` packages.

## Use Entity Framework Core for SQL

Remember that each ```microservice``` needs them own ```database```, for that reason if you downloaded this repository, remember to access to each project using the ```terminal``` and execute the needs commands to use ```Entity Framework Core``` and create both databases and tables that you will need.

- add-migration ```{ Migration name }```
- update-database

If you see an error refer about ```globalization-invariant mode```, you only need to modify the ```InvariantGlobalization``` tag to ```false``` in the project file.

## Project structure

To manage in a better way our project, each microservice has a different port that can help us to identify them.

The main project (```web application```) does not need to specify a unique port.

### Auth API
This project was modified to use ```7002``` port for the ```https``` profile in the ```launchSettings.json``` file.

Something that is important to say is that when you configure your ```Auth API``` with the library ```Microsoft.AspNetCore.Identity.EntityFrameworkCore``` you can execute the following commands in the ```Package Manager Console``` to see in your ```database``` the default information of ```users``` and ```roles``` for the identity.

- add-migration addIdentityTables
- update-database

### Coupon API
This project was modified to use ```7001``` port for the ```https``` profile in the ```launchSettings.json``` file.

### Product API
This project was modified to use ```7000``` port for the ```https``` profile in the ```launchSettings.json``` file.

### Shopping Cart API
This project was modified to use ```7003``` port for the ```https``` profile in the ```launchSettings.json``` file.

### Order API
This project was modified to use ```7004``` port for the ```https``` profile in the ```launchSettings.json``` file.
