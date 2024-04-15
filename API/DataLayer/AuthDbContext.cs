using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.DataLayer
{
    public class AuthDbContext:IdentityDbContext
    {
        public AuthDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId="5ad53095-713b-4511-b3f5-5970502a56af";
            var writerRoleId="f51b390a-b2b7-4873-9dd3-f9aca43c350f";

            //Create Reader and Writer Role
            var roles= new List<IdentityRole>
            {
                new IdentityRole()
                {
                 Id=readerRoleId,
                 Name="Reader",
                 NormalizedName="Reader".ToUpper(),
                 ConcurrencyStamp=readerRoleId
                },
                  new IdentityRole()
                {

                 Id=writerRoleId,
                 Name="Writer",
                 NormalizedName="Writer".ToUpper(),
                 ConcurrencyStamp=writerRoleId
                }
            };
           //seed the roles

            builder.Entity<IdentityRole>().HasData(roles);

            var adminUserId="a51da8ed-7b38-4a02-b3b6-8bba17b1f695";

            //create admin user 
            var admin=new IdentityUser()
            {
                Id=adminUserId,
                UserName="mustafaulas@windowslive.com",
                Email="mustafaulas@windowslive.com",
                NormalizedEmail="mustafaulas@windowslive.com".ToUpper(),
                NormalizedUserName="mustafaulas@windowslive.com".ToUpper()

            };

            admin.PasswordHash=new PasswordHasher<IdentityUser>().HashPassword(admin,"Dogo-12345");
            builder.Entity<IdentityUser>().HasData(admin);

            //Give Roles to admin

            var adminRoles = new List<IdentityUserRole<string>>()
            {
             new()
             {
                UserId=adminUserId,
                RoleId=readerRoleId
             },
             new()
             {
                UserId=adminUserId,
                RoleId=writerRoleId
             }

            };

            builder.Entity<IdentityUserRole<string>>().HasData(adminRoles);

        }
    }
}