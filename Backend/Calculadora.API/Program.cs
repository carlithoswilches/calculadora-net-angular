var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers(); // necesario para que la API reconozca los Controllers

builder.Services.AddCors(opciones =>
    opciones.AddDefaultPolicy(p => p.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();

app.UseCors(); // activa la política de CORS definida arriba

app.MapControllers(); // activa el enrutamiento hacia los Controllers (ej: CalculadoraController)

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();