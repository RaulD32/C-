using ApplicationCore;
using Infraestructure;

var builder = WebApplication.CreateBuilder(args);

// Habilitar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
            .AllowAnyOrigin()   // Permite cualquier origen
            .AllowAnyMethod()   // Permite cualquier método (GET, POST, etc.)
            .AllowAnyHeader()); // Permite cualquier encabezado
});

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();
builder.Services.AddApplicationCore();
builder.Services.AddInfraestructure(builder.Configuration);

var app = builder.Build();

// Inicializar base de datos (según tu configuración)
await app.Services.InitializeDatabasesAsync();

// Aplica la política de CORS antes de los middlewares personalizados
app.UseCors("AllowAll");

app.UseInfraestructure();
app.Run();
