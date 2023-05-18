using AutoMapper;
using Mango.Product.Web.Api.AutoMapper;
using Mango.Product.Web.Api.DbContexts;
using Mango.Product.Web.Api.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Agregamos uso de Autenticación tipo Bearer por medio del proyecto Service.Identity.
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://localhost:7107/"; // URL de proyecto Mango.Service.Identity.
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
        };
    });

// Ahora agregaremos una autorización (scope) por medio de la aplicación "mango"
// que definimos en la clase SD de Service.Identity.
// Actualmente no agregamos un Authorization pero esta es una forma muy elegante de hacerlo (estando en línea con clase SD).
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("ApiScope", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "mango");
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mango.Services.ProductAPI", Version = "v1" });
    c.EnableAnnotations();
    // Agregamos definición de seguridad para el uso de la api desde Swagger.
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"Enter 'Bearer' [space] and your token",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    // Agregamos requerimientos de seguridad para swagger.
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

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

// Es necesario para usar el Token de Service.Identity.
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
