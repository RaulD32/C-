using ApplicationCore;
using Infraestructure;

var builder = WebApplication.CreateBuilder(args);

// Configuraci�n de servicios
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
});

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers();
builder.Services.AddApplicationCore();
builder.Services.AddInfraestructure(builder.Configuration); // Aseg�rate de llamar a este m�todo

var app = builder.Build();

// Inicializar base de datos
await app.Services.InitializeDatabasesAsync();

app.UseCors("AllowAll");
app.UseRouting();
app.MapControllers();
app.UseInfraestructure();

app.Run();
