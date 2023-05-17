using Mango.Web.App;
using Mango.Web.App.Services;
using Mango.Web.App.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Agregar uso de HttpClient para interfaz IProductService.
builder.Services.AddHttpClient<IProductService, ProductService>();

// Configuración de variables globales. URL de API de Productos.
SD.ProductAPI = builder.Configuration["ServicesUrls:ProductAPI"]!;

// Configuración de servicio IProductService para ser utilizado en la Inyección de Dependencias de los Controller.
builder.Services.AddScoped<IProductService, ProductService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Agregar uso de Identity Server. Toda la información que se agregue de "AddOpenIdConnect" debe de coincidir con lo definido en la clase SD del proyecto Service.Identity.
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = "Cookies";
    options.DefaultChallengeScheme = "oidc";
})
    .AddCookie("Cookies", c => c.ExpireTimeSpan = TimeSpan.FromMinutes(10))
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = builder.Configuration["ServicesUrls:IdentityAPI"]; // URL de Identity Server.
        options.GetClaimsFromUserInfoEndpoint = true;
        options.ClientId = "mango"; // Este valor del cliente debe de ser exactamente el mismo que definimos en el arreglo de Client en clase SD del proyecto Service.Identity.
        options.ClientSecret = "secret"; // Este valor es el valor de secret del cliente "mango" que definimos en la clase SD del proyecto Service.Identity.
        options.ResponseType = "code"; // Valor "AllowedGrantTypes" del cliente "mango" que definimos en la clase SD del proyecto Service.Identity.
        
        options.TokenValidationParameters.NameClaimType = "name";
        options.TokenValidationParameters.RoleClaimType = "role";

        options.Scope.Add("mango"); // Este valor es el "Scope" que definimos en la clase SD de proyecto Service.Identity.
        options.SaveTokens = true;
    });

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

// Agregamos el uso de una autenticación.
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
