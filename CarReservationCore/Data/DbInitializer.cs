using CarReservationCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace CarReservationCore.Data
{
    public static class DbInitializer
    {
        public static void SeedData(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    context.Database.Migrate(); // automatycznie robi migracje

                    // Dodaj role
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    SeedRoles(roleManager).Wait();

                    // Dodaj użytkowników
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    SeedUsers(userManager).Wait();

                    // Dodaj przykładowe dane (samochody, itp.)
                    SeedCarsAndCustomers(context);
                }
                catch (Exception ex)
                {
                    // Logowanie błędów
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }
            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }
        }

        private static async Task SeedUsers(UserManager<ApplicationUser> userManager)
        {
            // Admin
            var adminEmail = "admin@demo.pl";
            if (await userManager.FindByEmailAsync(adminEmail) == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail
                };
                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Zwykły użytkownik
            var userEmail = "user@demo.pl";
            if (await userManager.FindByEmailAsync(userEmail) == null)
            {
                var normalUser = new ApplicationUser
                {
                    UserName = userEmail,
                    Email = userEmail
                };
                var result = await userManager.CreateAsync(normalUser, "User123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(normalUser, "User");
                }
            }
        }

        private static void SeedCarsAndCustomers(ApplicationDbContext context)
        {
            // Przykładowe dane
            if (!context.Cars.Any())
            {
                context.Cars.AddRange(
                    new Car { Brand = "Toyota", Model = "Corolla", Year = 2020 },
                    new Car { Brand = "Ford", Model = "Focus", Year = 2019 }
                );
                context.SaveChanges();
            }
            if (!context.Customers.Any())
            {
                context.Customers.AddRange(
                    new Customer { FirstName = "Jan", LastName = "Kowalski", Email = "jan.kowalski@demo.pl" },
                    new Customer { FirstName = "Anna", LastName = "Nowak", Email = "anna.nowak@demo.pl" }
                );
                context.SaveChanges();
            }
        }
    }
}
