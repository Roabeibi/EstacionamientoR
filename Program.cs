using EstacionamientoR.Data;
using EstacionamientoR.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Configuración MongoDB
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

var mongoSettings = builder.Configuration
    .GetSection("MongoDbSettings")
    .Get<MongoDbSettings>();

builder.Services.AddSingleton(mongoSettings);
builder.Services.AddSingleton<MongoDbContext>();

// Services
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<VehiculoService>();
builder.Services.AddScoped<PagoService>();
builder.Services.AddScoped<ReservaService>();
builder.Services.AddScoped<ReporteService>();
builder.Services.AddScoped<ComentarioService>();

// Razor Pages
builder.Services.AddRazorPages();

// Autenticación por cookies
builder.Services.AddAuthentication(
    CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/Login";
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Middleware
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();