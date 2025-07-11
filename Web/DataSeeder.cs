using Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Web
{
    public static class DataSeeder
    {
        public static async Task<IServiceCollection> InitializeAsync(this IServiceCollection service)
        {
            var serviceProvider = service.BuildServiceProvider();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            // Define roles
            string[] roles = { "Admin", "Manager", "Staff", "Customer" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // Create admin user if it doesn't exist
            string adminEmail = "cs.academeet@example.com";
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
            return service;
        }
    }
}
