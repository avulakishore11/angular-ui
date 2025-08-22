using DotnetApi.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Read connection string from configuration or environment variables. If none is
// provided, fall back to a sensible default that connects to the mysql service
// defined in the Kubernetes manifests. Note that the environment variable
// syntax uses double underscores to represent nested keys (e.g.
// ConnectionStrings__DefaultConnection).
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
    "server=mysql;port=3306;database=itemsdb;user=root;password=secret";

// Register the DbContext using MySQL provider. ServerVersion.AutoDetect will
// automatically detect the correct server version at runtime.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Enable controllers (MVC) for attribute‑routed controllers.
builder.Services.AddControllers();

// Allow cross origin calls from any origin. In production you may want to
// restrict this.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Register Swagger/OpenAPI services. This is optional but helps visualize
// available endpoints during development.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Automatically create the database if it does not exist. In a real project you
// might prefer migrations instead of EnsureCreated().
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

// Enable middleware. When running behind a reverse proxy like an ingress
// controller, you may need to configure forwarded headers.
app.UseCors("AllowAll");

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();