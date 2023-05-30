using Mango.Services.Coupon.Web.Api.Data;
using Mango.Services.Coupon.Web.Api.Microsoft.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure all the requiered services for this application using custom extensions methods.
builder.Services.ConfigureApplication(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
