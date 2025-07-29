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

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new User
                {
                    FullName = "Admin",
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
                else
                {
                    throw new Exception("Failed to create admin user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }            
            }

            string staffEmail = "staff@staff.com";
            string staffPassword = "Staff@123";

            var staffUser = await userManager.FindByEmailAsync(staffEmail);
            if (staffUser == null)
            {
                staffUser = new User
                {
                    FullName = "Staff",
                    UserName = staffEmail,
                    Email = staffEmail,
                    EmailConfirmed = true
                };

                var result1 = await userManager.CreateAsync(staffUser, staffPassword);
                if (result1.Succeeded)
                {
                    await userManager.AddToRoleAsync(staffUser, "Staff");
                }
                else
                {
                    throw new Exception("Failed to create staff user: " + string.Join(", ", result1.Errors.Select(e => e.Description)));
                }
            }

            string customerEmail = "customer@customer.com";
            string customerPassword = "customer@123";

            var customerUser = await userManager.FindByEmailAsync(customerEmail);
            if (customerUser == null)
            {
                customerUser = new User
                {
                    FullName = "Customer",
                    UserName = customerEmail,
                    Email = customerEmail,
                    EmailConfirmed = true
                };

                var result1 = await userManager.CreateAsync(customerUser, customerPassword);
                if (result1.Succeeded)
                {
                    await userManager.AddToRoleAsync(customerUser, "Customer");
                }
                else
                {
                    throw new Exception("Failed to create staff user: " + string.Join(", ", result1.Errors.Select(e => e.Description)));
                }
            }



            return service;
        }
    }
}
