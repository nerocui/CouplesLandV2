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
            var bfFirstName = config.GetSection("Users:BFFirstName").Value;
            var bFLastName =  config.GetSection("Users:BFLastName").Value;
            var bFUserName =  config.GetSection("Users:BFUserName").Value;
            var bFPassword =  config.GetSection("Users:BFPassword").Value;
            var gFFirstName = config.GetSection("Users:GFFirstName").Value;
            var gFLastName =  config.GetSection("Users:GFLastName").Value;
            var gFUserName =  config.GetSection("Users:GFUserName").Value;
            var gFPassword =  config.GetSection("Users:GFPassword").Value;
            
            Console.WriteLine("================");
            Console.Write("Info: ");
            Console.WriteLine(bfFirstName);
            Console.WriteLine(bFLastName);
            Console.WriteLine(bFUserName);
            Console.WriteLine(bFPassword);
            Console.WriteLine(gFFirstName);
            Console.WriteLine(gFLastName);
            Console.WriteLine(gFUserName);
            Console.WriteLine(gFPassword);
            Console.WriteLine("================");
            
            var bfRole = new IdentityRole{Name = "BoyFriend"};
            var gfRole = new IdentityRole{Name = "GirlFriend"};
            var roles = new List<IdentityRole>
            {
                bfRole,
                gfRole,
            };
            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }
            
            var bf = new AppUser
            {
                FirstName = bfFirstName,
                LastName = bFLastName,
                UserName = bFUserName,
            };
            var gf = new AppUser
            {
                FirstName = gFFirstName,
                LastName = gFLastName,
                UserName = gFUserName,
            };
            var bfResult = await userManager.CreateAsync(bf, bFPassword);
            var gfResult = await userManager.CreateAsync(gf, gFPassword);
            if (bfResult.Succeeded)
            {
                Console.WriteLine("Insert boy friend succeed");
                Console.WriteLine($"User ID is: {bf.Id}");
            }
            else
            {
                Console.WriteLine($"Failed to insert user{bfResult.ToString()}");
            }
            await userManager.AddToRolesAsync(bf, new List<string>{"BoyFriend"});
            await userManager.AddToRolesAsync(gf, new List<string>{"GirlFriend"});
        }
    }
}