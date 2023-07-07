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

### Mango Web

This project is the frontend of the application, contains different libraries like ```Datatables```, ```Bootstrap```, etc.
- https://datatables.net/

Inside the project we will see an example of ```Custom data annotations``` for ```ProductDto``` class.

### Auth API
This project was modified to use ```7002``` port for the ```https``` profile in the ```launchSettings.json``` file.

Something that is important to say is that when you configure your ```Auth API``` with the library ```Microsoft.AspNetCore.Identity.EntityFrameworkCore``` you can execute the following commands in the ```Package Manager Console``` to see in your ```database``` the default information of ```users``` and ```roles``` for the identity.

- add-migration addIdentityTables
- update-database

Inside this project we have an implementation of ```IMessageBus``` service to send a new ```queue``` request to ```Azure Service Bus```.

### Coupon API
This project was modified to use ```7001``` port for the ```https``` profile in the ```launchSettings.json``` file.

### Product API
This project was modified to use ```7000``` port for the ```https``` profile in the ```launchSettings.json``` file.

### Shopping Cart API
This project was modified to use ```7003``` port for the ```https``` profile in the ```launchSettings.json``` file.

### Order API
This project was modified to use ```7004``` port for the ```https``` profile in the ```launchSettings.json``` file.

### Email API

This project is the one who manage any call to ```Azure Service Bus``` for ```emailshoppingcart``` and ```registeruser``` ```queues```.

You can test the use of this project starting everything but not this project, after that send some emails from the app and then, start
your application with all the projects and see how ```Email API``` manage all the ```Queues``` from ```Azure Service Bus```.

### Rewards API

This project is the one who manage any call to ```Azure Service Bus``` for ```ordercreated``` topic with  ```OrderCreatedEmail``` and ```OrderCreatedRewardsUpdate``` ```subcriptions```.

You can test the use of this project starting everything but not this project, after that send some emails from the app and then, start
your application with all the projects and see how ```Rewards API``` manage all the ```Subcriptions``` from ```Azure Service Bus```.

### Gateway

To create a ```Gateway``` project we selected the ```ASP.NET Core Empty``` project because we will create all the architecture from 0.

This project was modified to use ```7777``` port for the ```https``` profile in the ```launchSettings.json``` file.

## Additional knowledge

### Azure Service Bus

```Azure Service Bus``` is a resource that can help us to manage ```async tasks```, this is similar to ```Apache Kafka```. You sent a request
and ```Azure``` manage the same request. This service evalue the service that needs to call and once that is up and ready, sent the request, it is like a middleware.

Usually this resource store a request for ```14 days```, but this value can be modified.

In the solution we have an integration folder where we have a project with name ```Mango.Integration.MessageBus```. This project can help 
us to connect to an azure resource named ```Service Bus```.

We create this resource with a ```free azure account``` and the ```Service Bus``` is with ```pricing tier``` of ```Standar``` type. Remember that 
a ```Queue``` and ```Topic``` in this kind of resource must be unique.

Any ```Azure Resource``` have a cost for this project, for that reason we use a ```free azure account```.

To get the ```Connection String``` from ```Azure Service Bus``` resource you have to go to ```Shared access policies```
section and get the ```Primary Connection String``` from ```root``` Policy. Also you can create a new one.

A ```Queue``` follow the structure of ```First in, First Out (FIFO)```. With ```Topics and Subcriptions``` is different, because you can send multiples messages that can be
executed at the same time, a ```Topic``` don't need to execute one message and then the another.

- ```Queue = One sender, one receiver ```
- ```Top``````ics = One sender, multiple receivers ```

A ```Topic``` can have multiples ```subcriptions```. A ```subcription``` inside a ```Topic``` is like a sample ```Queue``` inside of the ```Topic```.


### Stripe - Payment integration

We will use ```stripe``` for payments integration in the application (```https://stripe.com/en-mx```). You will need to create a new Developer 
account for work with this platform.

The following links can help us to understand the integration with ```Stripe.Net``` library in ```C# .NET```.
- https://stripe.com/docs/checkout/quickstart?lang=dotnet
- https://stripe.com/docs/api/checkout/sessions/create

When we want to apply a coupon code using ```stripe```, we need to create a coupon in ```stripe platform```. To create one coupon click in 
the following link https://dashboard.stripe.com/test/coupons.

To know more about the topic you can see the following doc (https://stripe.com/docs/api/coupons/create).

We also can know what is the current status of a payment intent, follow the link and see more about it (https://stripe.com/docs/api/errors#errors-payment_intent-status)

### Architecture - Use of Gateway

We can have our ```Web App``` project connected to all our ```microservices```. But this is not the best approach. Currentyl we only have one application, the ```Web App```, but
imagine that we need to add a new client, in this case a ```mobile app```, you will have to modify all the ```microservice``` to accept the requests from the ```web app and mobile app```.

In this example we should add a ```Gateway```, this is a new ```layer``` in our architecture and programming, because a ```gateway``` works as a ```unified point of entry```, where all our request
will be send to the ```gateway```, and this one will connect and redirect each request to the requiered ```microservice```.

In this case we will use an ```Open Source Gateway``` called ```OCELOT```, of course, we can also use the ```microsoft azure gateway```, but in this case we will use ```OCELOT```.

Some of the ventages that we have when use a ```Gateway``` are:
- Single point of entry to set of services.
- Easier security management.
- Suppot to larger application.

The only service that will not be managed by the ```Gateway``` in this project will be the ```Auth API```, this one will be isolated to the rest. The ```Gateway``` will receive the ```Bearer Token``` from the ```Authentication```.

OCELOT documentation in https://ocelot.readthedocs.io/en/latest/introduction/gettingstarted.html

Example of OCELOT configuration.

```
"Routes": [
    // The request is taken from the upstream and redirected to the downstream.
    // This configuration is for all the services.
    {
      "DownstreamPathTemplate": "/api/product", // microservice endpoint
      "DownstreamScheme": "https", // connection protocol.
      "DownstreamHostAndPorts": [
        {
          "Host": "localhost", // domain or host of microservice.
          "Port": 7000 // port of the microservice
        }
      ],
      "UpstreamPathTemplate": "/api/product", // path of the service in OCELOT, make sure that the request in you app will be correct.
      "UpstreamHttpMethod": [ "Get", "Post", "Put" ] // protocol type for the service.
    }
  ],
  // Global configuration for OCELOT.
  "GlobalConfiguration": {
    "BaseUrl": "https://localhost:7777" // URL in our architecture.
  }
```

You need to be carefully about what url you are trying to reach out, let's see an exmaple. 

If you define a request in your ```Web App``` for ```ProductAPI``` and the url is like ```https://localhost:7777/api/product/```, but you defined in your ```OCELOT Routes```  the following route value
```"UpstreamPathTemplate": "/api/product"```, the request will return a ```not found``` error.

To solve it, you need to match the request url in your ```web app``` and the ```OCELOT .json configuration```.

An additional step is to validate the authentication if you are using ```Bearer``` token like in this project. Make sure that the ```Issuer```, ```Secret``` and ```Client``` are the same.

### Azure Deploy

To deploy the project in azure you need to do different steps.

a)  Make sure that you have already created and configured in your project the use of ```Azure Service Bus``` with ```Queues``` and ```Topics```, only if you want to use them, if not, make sure
    that you will need to change the current project code to don't connect to ```Azure Service Bus``` and you won't need the ```Integration``` project. Also, you will need to modify the ```web api```
    projects where you manage all the calls to ```Azure Service Bus``` for ```Queues``` and ```Topics```. If you use ```Azure Service Bus```, make sure that you have configured a ```Resource group``` 
  a in Azure for this project, this can be named like ```mango-web-net-8```.

b) Create each database for your ```web apis```, go to azure portal and create ```SQL databases```, you can use the same name that you define in your ```appsettings.json``` files for each api.
    - NET8_Mango_Auth
    - NET8_Mango_Coupon
    - NET8_Mango_Email
    - NET8_Mango_Order
    - NET8_Mango_Product
    - NET8_Mango_Reward
    - NET8_Mango_ShoppingCart
   
    Make sure that, when you create your databases, create or use an existent Server to have them. You could use the name of ```net8mangomicroservices``` for the server.
    Also, select the authentication method of SQL Authentication, for the user and password you could use something las the following:
    
    - User: sqladmin
    - Password: MangoDbAdmin1234!

    Once you create the server, make sure to configure each of this properties in yours db as you see here.
    
    For ```Workload environment``` select ```Development``` (or Production if you want but it is more expensive). For the pricing (```Compute + storage```), 
    you can select ```Basic (for less demanding workloads)```. The estimated cost is about ```4.90 USD```.

    - Connectivity method = public
    - Allow Azure services and resources to access this server = yes
    - Add current client IP address = yes
     
    Once that you have all this configuration you can create the SQL Server for your databases.

c) For all the database make sure to select the same SQL Server of step ```b)```. Please, make sure that for the pricing (```Compute + storage```) in the databases, 
   you select ```Basic (for less demanding workloads)```. The estimated cost is about ```4.90 USD```.
   
   Also, in the networking tab make sure to add your current api address.

Once that you have created all the databases, you need to configure the access to each one of them in our project, as we do for our local databases, for that you will use the ```appsettings.Production.json``` file.

To publish all your api projects, you will need do the following.

a) Login with your free azure account in your visual studio.
b) Right click in your api project for example ```Mango.Services.Auth.Web.Api``` and clic on ```publish```.
c) Click on add a new profile to publish and select ```Azure``` option.
d) Select ```Azure App Service (Windows)``` option.
e) After that make sure that you select your free azure account in the top and right of the window.
f) Click on create new app service (or use one if you created that before)
   - Set the name of ```MangoServicesAuthWebApi```.
   - Choose the free subscription.
   - Select the resource group that we create before (```mango-web-net-8```) or create new one.
   - Create a new ```Hosting Plan```.
g) In the ```Hosting Plan``` window.
   - Set the name of ```MangoServicesAuthWebApiPlan```.
   - Location set the default value.
   - Size set the ```Free``` option (or another if you want to pay).
   - Click ```OK``` button.
h) Click on ```Create``` button.
i) Click on ```Next``` button.
j) Click on ```Skip this step``` because we don't need any ```API manager``` for now.
k) Click on ```Finish``` button.

Once it finished, in the main page of ```Publish```, set the following configuration.

a) In the ```Deployment Mode``` choose the ```Self-contained``` (```Independiente``` in spanish) mode.
b) Click on ```Publish``` button.

After that, you need to configure an environment variable in the created ```App Service```, go to it in Azure, then in the left menu select 
```Configuration``` option, in ```Application settings``` select ```New application setting```. Then set the following name and value.
- Name = ASPNETCORE_ENVIRONMENT
- Value = Production
 
Then save the ```App Service``` and you will see how it works successfully connected to production configuration (using ```appsettings.Production.json``` file configuration).
If you don't see that is running successfully, then go ```Overview``` page and click on ```Restart```.

To configure the ```Azure Service Bus``` for our ```Reward API``` and ```Email API```, you will need to change the service plan from free to another one.
Follow the following steps.
- Inside the ```App Service```, on left menu, go to ```Scale up (App Service Plan) -> Select Basic B1``` and save.
- Inside the ```App Service```, on left menu, go to ```Configuration -> General Settings```, in ```Always on``` set value of ```on```.

If you don't do this configuration, the web apis wont be able to detect when a new request is inside the ```Azure Service Bus``` as ```Queue``` or ```Topic``` because they only will be
activaded when a user access to it.

The next step is to modify the url's for each microservice in ```appsettings.Production.json``` files. Replace each https://localhost for the new url that you have by each ```Azure App Service```. 
Make sure that you publish all the project after this update.

Now it is time to publish our ```Web App``` project, and for that you will need to do the same that we saw before, create a new file named ```appsettings.Production.json``` and set all the required configuration
and set the microservices url from the ```Azure App Service```.

You can test the application until here.

Then, you will need to publish the ```Gateway App```, for that, as we do for ```Web App```, following the same steps, and once it finished, remember to update in the 
```appsettings.Production.json``` file each url that is not the ```OCELOT Gateway``` url in both ```Microservices``` and ```Web App``` projects.

After that, create a file named ```ocelot.Production.json``` to store the configuration of OCELOT, that in this case the only thing that you will need to change is the 
```BaseUrl``` for the url that you have in the ```App Service``` in Azure for the ```Gateway``` project. Also, in the ```DownstreamHostAndPorts``` remove the ```Port``` property
and set in ```Host``` the domain of each ```Microservice```, then publish the project.

Example: 

```
"Routes": [
    {
      "DownstreamPathTemplate": "/api/product",
      "DownstreamScheme": "https",
      "DownstreamHostAndPorts": [
        {
          "Host": "mangoservicescouponwebapi.azurewebsites.net",
        }
      ],
      "UpstreamPathTemplate": "/api/product",
      "UpstreamHttpMethod": [ "Get", "Post", "Put" ]
    }
  ],
  "GlobalConfiguration": {
    "BaseUrl": "https://mangoservicesgateway.azurewebsites.net"
  }
```

Remember to consider the following once that the project is published.
- Make sure that you have the coupons created in ```Stripe``` when you test the production app.
- Check if the ```Azure Service Bus``` is working as expected saving the data in production databases, this for ```Email API``` and ```Reward API```.

### Azure Deploy Error

If you publish and don't see the app is running and show an error ```500:30```, make sure that the .NET version in the project
is present in the ```App service``` you just created. For .NET 8 preview I did't find a solution because it is as a preview version, hopefully you will see it enable in the section 
```Configuration -> General Settings -> .NET Version``` inside the ```App Service``` in ```Azure```.
- https://learn.microsoft.com/en-us/answers/questions/1303909/when-will-net-8-be-available-on-azure-app-services

To simulate the production environment you can to go to ```lauchSettings.json``` file, and inside the ```https``` profile, modifiy the environment from ```Development``` to ```Production```.



