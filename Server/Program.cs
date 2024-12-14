using Application;
using Infrastructure;
using Infrastructure.Identity.Models;
using Infrastructure.Identity.Seeds;
using Microsoft.AspNetCore.Identity;
using Server;
using Server.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

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

app.UseExceptionHandler(options => { });

app.UseHttpsRedirection();

app.UseAuthorization();

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

        // Llamar a los métodos de inicialización
        await DefaultRole.SeedAsync(roleManager);
        await DefaultAdminUser.SeedAsync(userManager, roleManager);
    }
    catch (Exception ex)
    {
        // Manejo de errores, puedes registrar el error aquí
        Console.WriteLine($"Ocurrió un error durante la inicialización: {ex.Message}");
    }
}

app.Run();

public partial class Program { }
