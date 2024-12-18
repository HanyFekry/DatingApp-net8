using DatingApi.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace DatingApi.Data
{
    public static class DataSeedExtensions
    {
        public async static Task ApplyMigrations(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            if (context == null) return;
            if (context.Database.GetPendingMigrations().Any())
            {
                await context.Database.MigrateAsync();
            }

        }

        public async static Task SeedUsers(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            if (context == null) return;
            if (context.Users.Any()) return;
            var file = await File.ReadAllTextAsync("Data/UserSeedData.json");
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var users = JsonSerializer.Deserialize<List<AppUser>>(file, options);
            if (users == null) return;
            var hmac = new HMACSHA512();
            for (int i = 0; i < users.Count; i++)
            {
                AppUser? user = users[i];
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));
                user.PasswordSalt = hmac.Key;
                user.UserName = user.UserName.ToLower();
                await context.Users.AddAsync(user);
            }
            await context.SaveChangesAsync();
        }
    }
}
