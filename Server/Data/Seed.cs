using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Server.Models;

namespace Server.Data
{
    public class Seed
    {
        public static async Task SeedBuiltInData(
            IConfiguration config,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;
            var bFUserName =  config.GetSection("Users:BFUserName").Value;
            var bFPassword =  config.GetSection("Users:BFPassword").Value;
            var gFUserName =  config.GetSection("Users:GFUserName").Value;
            var gFPassword =  config.GetSection("Users:GFPassword").Value;
            var adminPassword =  config.GetSection("Users:adminPassword").Value;

            var roles = new List<IdentityRole>
            {
                new IdentityRole{Name = "BoyFriend"},
                new IdentityRole{Name = "GirlFriend"},
                new IdentityRole{Name = "Admin"},
            };
            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            var bf = new AppUser
            {
                UserName = bFUserName,
            };
            var gf = new AppUser
            {
                UserName = gFUserName,
            };
            var admin = new AppUser
            {
                UserName = "admin",
            };
            var bfResult = await userManager.CreateAsync(bf, bFPassword);
            var gfResult = await userManager.CreateAsync(gf, gFPassword);
            var adminResult = await userManager.CreateAsync(admin, adminPassword);
            if (bfResult.Succeeded && gfResult.Succeeded && adminResult.Succeeded)
            {
                Console.WriteLine("User Initialization Succeeded");
            }
            else
            {
                Console.WriteLine($"Failed to insert user");
            }
            await userManager.AddToRolesAsync(bf, new List<string>{"BoyFriend"});
            await userManager.AddToRolesAsync(gf, new List<string>{"GirlFriend"});
            await userManager.AddToRolesAsync(admin, new List<string>{"Admin"});
        }
    }
}