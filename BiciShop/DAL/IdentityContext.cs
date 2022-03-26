using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiciShop.Models
{
    public class IdentityContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public IdentityContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            string userId = "8e445865-a24d-4543-a6c6-9443d048cdb9";
            string roleId = "2c5e174e-3b0e-446f-86af-483d56fd7210";

            
                IdentityRole headRole = new IdentityRole()
            {
                Id = roleId,
                Name = "head",
                NormalizedName = "HEAD"
            };
            IdentityRole adminRole = new IdentityRole()
            {
                Id = "2",
                Name = "admin",
                NormalizedName = "ADMIN"
            };
            IdentityRole userRole = new IdentityRole()
            {
                Id = "3",
                Name = "user",
                NormalizedName = "USER"
            };
           
            builder.Entity<IdentityRole>().HasData(headRole, adminRole, userRole);


            var hasher = new PasswordHasher<IdentityUser>();

            string email = "admin@gmail.com";

            builder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = userId, 
                    Email = email,
                    NormalizedEmail = email.ToUpper(),
                    UserName = email,
                    NormalizedUserName = email.ToUpper(),
                    PasswordHash = hasher.HashPassword(null, "84l56SCp"),
                }
            );
            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = roleId,
                    UserId = userId
                });
            base.OnModelCreating(builder);
        }
    }
}
