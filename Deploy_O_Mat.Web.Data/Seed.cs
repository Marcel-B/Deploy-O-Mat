using System;
using System.Collections.Generic;
using System.Linq;
using com.b_velop.Deploy_O_Mat.Web.Data.Context;
using com.b_velop.Deploy_O_Mat.Web.Domain.Models;
using com.b_velop.Deploy_O_Mat.Web.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace com.b_velop.Deploy_O_Mat.Web.Data
{
    public class Seed
    {
        public static void SeedData(
            WebContext dataContext,
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager)
        {
            var roles = new[]
            {
                new AppRole {Id = Guid.NewGuid().ToString(), Name = "Admin"},
                new AppRole {Id = Guid.NewGuid().ToString(), Name = "User"}
            };

            foreach (var role in roles)
            {
                var tmp = roleManager.Roles.FirstOrDefault(r => r.Name == role.Name);
                if (tmp == null)
                    roleManager.CreateAsync(role).Wait();
            }
        }
    }
}