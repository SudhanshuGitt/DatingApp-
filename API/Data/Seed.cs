using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        // as we returing void wen have not given task any type parameter as we returning void 
        public static async Task SeedUsers(DataContext context)
        {
            // check if our users table contains any users
            if(await context.Users.AnyAsync())
            {
                return; 
            }
            // if we dont have some user in our database
            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            // we need to desearailze the data inside the json file 
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            foreach(var user in users)
            {
                using var hmac = new HMACSHA512();
                user.UserName = user.UserName.ToLower();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("welcome@123"));
                user.PasswordSalt = hmac.Key;
                context.Users.Add(user);
            }

            await context.SaveChangesAsync();
        }
    }
}