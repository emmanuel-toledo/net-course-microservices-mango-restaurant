using Mango.Service.Identity;
using Mango.Service.Identity.DbContexts;
using Mango.Service.Identity.Initializer;
using Mango.Service.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Agregamos el uso de la base de datos.
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Agregamos el uso del identity en nuestro proyecto,
// pero aún no lo configuramos el uso de identity server en nuestro proyecto.
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

// Agregamos el uso de Identity Server en nuestro proyecto.
var identityServer = builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
    options.EmitStaticAudienceClaim = true; // Esto solo es para ambiente de desarrollo.
})
    .AddInMemoryIdentityResources(SD.IdentityResources) // Agregamos el uso de Resources que definimos en clase SD.
    .AddInMemoryApiScopes(SD.ApiScopes) // Agregamos el uso de Scopes que definimos en clase SD.
    .AddInMemoryClients(SD.Clients) // Agregamos el uso de Clients que definimos en clase SD.
    .AddAspNetIdentity<ApplicationUser>(); // Definimos el uso de nuestro modelo "ApplicationUser" para usar en el identity.

identityServer.AddDeveloperSigningCredential(); // Configuramos uso de credenciales para ambiente de desarrollo.

// Agregamos el servicio IDbInitializer.
builder.Services.AddScoped<IDbInitializer, DbInitializer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer(); // Agregamos el uso de Identity Server en el PipeLine.

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Ejecutamos la función "initialize" recién modificada para crear los roles y usuarios admin y customer.
using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;
    var dbInitializer = services.GetRequiredService<IDbInitializer>();
    dbInitializer.Initialize();
}

app.Run();