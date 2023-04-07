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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
