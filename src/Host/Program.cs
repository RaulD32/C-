using ApplicationCore;
using Infraestructure;

var builder = WebApplication.CreateBuilder(args);

// Habilitar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
            .AllowAnyOrigin()   // Permite cualquier origen
            .AllowAnyMethod()   // Permite cualquier m�todo (GET, POST, etc.)
            .AllowAnyHeader()); // Permite cualquier encabezado
});

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();
builder.Services.AddApplicationCore();
builder.Services.AddInfraestructure(builder.Configuration);

var app = builder.Build();

// Inicializar base de datos (seg�n tu configuraci�n)
await app.Services.InitializeDatabasesAsync();

// Aplica la pol�tica de CORS antes de los middlewares personalizados
app.UseCors("AllowAll");

app.UseInfraestructure();
app.Run();
