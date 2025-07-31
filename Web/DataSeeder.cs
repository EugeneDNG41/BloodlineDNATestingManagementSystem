using Data;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static System.Formats.Asn1.AsnWriter;

namespace Web
{
    public static class DataSeeder
    {
        public static async Task<IServiceCollection> InitializeAsync(this IServiceCollection service)
        {
            var serviceProvider = service.BuildServiceProvider();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var db = serviceProvider.GetRequiredService<AppDbContext>();
            if (!await db.Database.CanConnectAsync() || (await db.Database.GetPendingMigrationsAsync()).Any())
                return service;
            // Define roles
            string[] roles = { "Admin", "Manager", "Staff", "Customer" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // Create admin user if it doesn't exist
            string adminEmail = "admin@admin.com";
            string adminPassword = "Admin@123";
            await CreateUserAsync(userManager, adminEmail, adminPassword, "Admin");
            string managerEmail = "manager@manager.com";
            string managerPassword = "Manager@123";
            await CreateUserAsync(userManager, managerEmail, managerPassword, "Manager");
            string staffEmail = "staff@staff.com";
            string staffPassword = "Staff@123";
            await CreateUserAsync(userManager, staffEmail, staffPassword, "Staff");

            return service;
        }

        private static async Task CreateUserAsync(UserManager<User> userManager, string email, string password, string role)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FullName = email.Split('@')[0],
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
                else
                {
                    throw new Exception($"Failed to create {role} user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
        }
    }
}
