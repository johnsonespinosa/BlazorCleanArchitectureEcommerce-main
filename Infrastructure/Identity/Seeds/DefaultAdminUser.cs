using Domain.Enums;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Seeds
{
    public static class DefaultAdminUser
    {
        private const string AdminUserName = "admin";
        private const string AdminEmail = "admin@gmail.com";
        private const string AdminPassword = "Admin@123";
        private const string AdminPhoneNumber = "54541079@123";

        public static async Task SeedAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Verificar si el usuario ya existe
            var existingUser = await userManager.FindByNameAsync(AdminUserName);
            if (existingUser == null)
            {
                var user = new User
                {
                    UserName = AdminUserName,
                    Email = AdminEmail,
                    EmailConfirmed = true,
                    PhoneNumber = AdminPhoneNumber,
                    PhoneNumberConfirmed = true,
                };

                // Intentar crear el usuario
                var result = await userManager.CreateAsync(user, AdminPassword);
                if (result.Succeeded)
                {
                    // Asignar los roles al usuario
                    await userManager.AddToRoleAsync(user, Roles.Administrator.ToString());
                    await userManager.AddToRoleAsync(user, Roles.Customer.ToString());
                }
                else
                {
                    // Manejo de errores: puedes registrar los errores o lanzar excepciones según sea necesario
                    throw new Exception($"No se pudo crear el usuario administrador: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }
    }
}
