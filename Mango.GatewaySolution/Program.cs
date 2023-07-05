using Mango.GatewaySolution.Microsoft.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureApplication(builder.Configuration);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

await app.RunApplicationAsync();
