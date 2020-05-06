using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Domain.Models;
using com.b_velop.Deploy_O_Mat.Web.Identity.Models;
using com.b_velop.Deploy_O_Mat.Web.Persistence.Context;
using Microsoft.AspNetCore.Identity;

namespace com.b_velop.Deploy_O_Mat.Web.Persistence
{
    public class Seed
    {
        static string basePath = Path.Combine(AppContext.BaseDirectory, "Values");

        public static List<T> GetSeedData<T>(string fileName)
        {
            var path = Path.Combine(basePath, fileName);
            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<T>>(json);
        }

        public static async Task SeedData(
            WebContext dataContext,
            UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager)
        {
            var images = GetSeedData<DockerImage>("Images.json");
            foreach (var dockerImage in images)
            {
                var tmp = dataContext.DockerImages.Find(dockerImage.Id);
                if (tmp == null)
                    dataContext.DockerImages.Add(dockerImage);
            }

            var badges = GetSeedData<Badge>("Badges.json");
            foreach (var badge in badges)
            {
                var tmp = dataContext.Badges.Find(badge.Id);
                if (tmp == null)
                    dataContext.Badges.Add(badge);
            }

            var dockerStacks = GetSeedData<DockerStack>("Stacks.json");
            foreach (var dockerStack in dockerStacks)
            {
                var tmp = dataContext.DockerStacks.Find(dockerStack.Id);
                if (tmp == null)
                    dataContext.DockerStacks.Add(dockerStack);
            }

            var dockerServices = GetSeedData<DockerService>("Services.json");
            foreach (var dockerService in dockerServices)
            {
                var tmp = dataContext.DockerServices.Find(dockerService.Id);
                if (tmp == null)
                    dataContext.DockerServices.Add(dockerService);
            }

            var dockerStackImages = GetSeedData<DockerStackImage>("StackImages.json");
            foreach (var dockerStackImage in dockerStackImages)
            {
                var tmp = dataContext.DockerStackImages.Find(dockerStackImage.DockerImageId, dockerStackImage.DockerStackId);
                if (tmp == null)
                    dataContext.DockerStackImages.Add(dockerStackImage);
            }

            var dockerActiveServices = GetSeedData<DockerActiveService>("ActiveServices.json");
            foreach (var dockerActiveService in dockerActiveServices)
            {
                var tmp = await dataContext.DockerActiveServices.FindAsync(dockerActiveService.Id);
                if (tmp == null)
                    dataContext.DockerActiveServices.Add(dockerActiveService);
            }
        
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

            // var adminUser = new AppUser
            // {
            //     Id = Guid.NewGuid().ToString(),
            //     Email = "marcel.benders@icloud.com",
            //     UserName = "Admin",
            //     DisplayName = "Admin",
            // };
            
            // var result =  userManager.CreateAsync(adminUser, "").Result;
            // if (result.Succeeded)
            // {
            //     var admin = await  userManager.FindByEmailAsync("marcel.benders@icloud.com");
            //     var iResult = await userManager.AddToRolesAsync(admin, new[] {"Admin", "User"});
            // }
            //
            //
            // var marcel = await  userManager.FindByIdAsync("62b617ee-a29b-4557-ae57-a64c8e423527");
            // await userManager.AddToRolesAsync(marcel, new[] {"User"});
            //
            // var mila = await userManager.FindByIdAsync("af9c87e5-e09a-4dfe-9016-f45655089aca");
            // await userManager.AddToRoleAsync(mila, "User");
            
            dataContext.SaveChanges();
        }
    }
}