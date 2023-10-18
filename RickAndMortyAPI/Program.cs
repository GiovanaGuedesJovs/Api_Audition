using Microsoft.EntityFrameworkCore;
using RickAndMortyAPI.Configuration;
using RickAndMortyAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Configuration setup
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddEnvironmentVariables();


builder.Services.RegisterServices(builder.Configuration);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

IMvcBuilder mvcBuilder = builder.Services.AddControllers();


// Application setup
var app = builder.Build();

// Development-specific middleware setup
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Standard middleware setup
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Application execution
app.Run();
