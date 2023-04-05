using AutoMapper;
using Mango.Product.Web.Api.AutoMapper;
using Mango.Product.Web.Api.DbContexts;
using Mango.Product.Web.Api.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración de AutoMapper.
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper(); // MappingConfig es nuestra clase recién generada.
builder.Services.AddSingleton(mapper); // Agrega nuestro objeto mapper como un servicio.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // Toma la configuración desde nuestro assembly.

// Configuramos servicio de Repository.
builder.Services.AddScoped<IProductRepository, ProductRepository>();

// Configuración de uso de base de datos.
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
