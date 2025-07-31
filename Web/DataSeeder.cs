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
            string adminPassword = "123";
            await CreateUserAsync(userManager, adminEmail, adminPassword, "Admin");
            string managerEmail = "manager@manager.com";
            string managerPassword = "123";
            await CreateUserAsync(userManager, managerEmail, managerPassword, "Manager");
            string staffEmail = "staff@staff.com";
            string staffPassword = "123";
            await CreateUserAsync(userManager, staffEmail, staffPassword, "Staff");
            string customerEmail = "customer@customer.com";
            string customerPassword = "123";
            await CreateUserAsync(userManager, customerEmail, customerPassword, "Customer");

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
                    EmailConfirmed = true,
                    DateOfBirth = DateTime.UtcNow.AddYears(-18),
                    Gender = "Male",
                    PhoneNumber = "1234567890", // Default phone number, can be changed later
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
            else
            {
                // If user already exists, ensure they are in the correct role
                if (!await userManager.IsInRoleAsync(user, role))
                {
                    var result = await userManager.AddToRoleAsync(user, role);
                    if (!result.Succeeded)
                    {
                        throw new Exception($"Failed to add {role} role to existing user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                    }
                }
            }
        }
    }
}
