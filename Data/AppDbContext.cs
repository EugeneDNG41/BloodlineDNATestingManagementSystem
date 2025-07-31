using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Service> Services { get; set; }
        public new DbSet<User> Users { get; set; }
        private string GetConnectionString()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            return configuration.GetConnectionString("MySQLConnection");
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //=> optionsBuilder.UseSqlServer(GetConnectionString());
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) optionsBuilder.UseMySql(GetConnectionString(), ServerVersion.Parse("8.0.37-mysql"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Cấu hình Identity Roles
            modelBuilder.Entity<IdentityRole>().HasData
            (
                new IdentityRole { Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Name = "Manager", NormalizedName = "MANAGER" },
                new IdentityRole { Name = "Staff", NormalizedName = "STAFF" },
                new IdentityRole { Name = "Customer", NormalizedName = "CUSTOMER" }
            );

            // Cấu hình dữ liệu mẫu cho Service
            modelBuilder.Entity<Service>().HasData
            (
                new Service { Id = 1, ServiceName = "Basic Ancestry DNA", Description = "Discover your ethnic background and find DNA matches with our basic ancestry test. Get insights into your family history and genetic heritage.", Price = 99.99m, Duration = "2-3 weeks" },
                new Service { Id = 2, ServiceName = "Advanced Bloodline Analysis", Description = "Comprehensive bloodline analysis including detailed family tree construction and advanced genetic markers.", Price = 299.99m, Duration = "4-6 weeks" },
                new Service { Id = 3, ServiceName = "Paternity Testing", Description = "Accurate paternity testing with 99.9% accuracy. Confidential and legally admissible results.", Price = 199.99m, Duration = "1-2 weeks" },
                new Service { Id = 4, ServiceName = "Health + Ancestry", Description = "Complete package including ancestry information plus health predispositions and carrier status reports.", Price = 499.99m, Duration = "3-4 weeks" }
            );
        }      
    }
}
