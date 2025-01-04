using Application;
using Infrastructure;
using Infrastructure.Data.Contexts;
using Infrastructure.Data.Seeds;
using Infrastructure.Identity.Models;
using Infrastructure.Identity.Seeds;
using Microsoft.AspNetCore.Identity;
using Server;
using Server.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

// Agrega servicios para usar el formato Problem Details
services.AddProblemDetails();
services.AddApplicationServices();
services.AddInfrastructureServices(configuration);
services.AddServerServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

// Convierte excepciones no manejadas en respuestas Problem Details
app.UseExceptionHandler();

// Devuelve la respuesta Problem Details para respuestas no exitosas
app.UseStatusCodePages();

app.UseHttpsRedirection();

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

// Inicializar roles y usuario administrador
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();


        // Llamar a los métodos de inicialización
        await DefaultRole.SeedAsync(roleManager);
        await DefaultAdminUser.SeedAsync(userManager, roleManager);
        await DefaultCategories.SeedCategories(context);
    }
    catch (Exception ex)
    {
        // Manejo de errores, puedes registrar el error aquí
        Console.WriteLine($"Ocurrió un error durante la inicialización: {ex.Message}");
    }
}

app.Run();

public partial class Program { }
