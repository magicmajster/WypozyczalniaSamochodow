using CarReservationCore.Data;
using CarReservationCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1) Dodaj usługi EF Core (z łańcuchem połączenia)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2) Dodaj Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{

    // Możesz skonfigurować polityki haseł, lockout itp.
    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// 3) Dodaj MVC (lub addControllersWithViews)
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 4) Obsługa wyjątków
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// 5) Routing i Identity
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// 6) Seeding (tworzenie bazy + dodanie domyślnych danych)
DbInitializer.SeedData(app);

// 7) Definicja routingu MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

// 8) Uruchom
app.Run();
