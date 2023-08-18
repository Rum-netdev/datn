using EcommercialWebApp.Core.Enums;
using EcommercialWebApp.Data.Models.Commons;
using Microsoft.AspNetCore.Identity;
namespace EcommercialWebApp.Data.Helpers
{
    public static class Seed
    {
        public static async Task SeedData(ApplicationDbContext db)
        {
            await SeedRoles(db);
            await SeedUsers(db);
        }

        private static async Task SeedRoles(ApplicationDbContext db)
        {
            var roles = Enum.GetNames(typeof(RoleEnums));
            foreach (var role in roles)
            {
                if (db.Roles.Where(t => t.Name.Equals(role.ToString())).FirstOrDefault() == null)
                {
                    db.Roles.Add(new ApplicationRole
                    {
                        Name = role.ToString(),
                        ConcurrencyStamp = Guid.NewGuid().ToString()
                    });
                }
            }

            await db.SaveChangesAsync();
        }

        private static async Task SeedUsers(ApplicationDbContext db)
        {
            if (!db.Users.Any(t => t.UserName.Equals("admin")))
            {
                ApplicationUser admin = new ApplicationUser()
                {
                    UserName = "admin",
                    Email = "jinkey.coredev@gmail.com",
                    PhoneNumber = "0795671811",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    FirstName = "Quang",
                    LastName = "Ngo Viet",
                    IdentificationNumber = "191916414"
                };
                var hasher = new PasswordHasher<ApplicationUser>();
                admin.PasswordHash = hasher.HashPassword(null, "@Admin123");

                db.Users.Add(admin);
                await db.SaveChangesAsync();
            }
        }

    }
}
