using Mango.Services.Coupon.Web.Api.Data;
using Mango.Services.Coupon.Web.Api.Microsoft.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configure all the requiered services for this application using custom extensions methods.
builder.Services.ConfigureApplication(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    // Configure swagger to accept authentication token.
    option.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string as following: `Bearer { Generated access token }`",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            new string[]{ }
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

// Apply any pending migration to the database.
ApplyMigration();

app.Run();

// Function to apply the pendings migrations in the database (it is like execute update-databse command).
void ApplyMigration()
{
    using (var scope = app.Services.CreateScope())
    {
        // Get "AppDbContext" service.
        var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        // Validate if are pending migrations in the database.
        if(_db.Database.GetPendingMigrations().Count() > 0)
        {
            // Apply the pendings migrations in the database (it is like execute update-database command).
            _db.Database.Migrate();
        }
    }
}
