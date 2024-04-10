using Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Infrastructure
{
    public class BackContextSeedData
    {
        public static async Task SeedAsync(BackContext context, ILogger logger, UserManager<User> userManager)
        {
            try
            {
                if (!userManager.Users.Any())
                {
                    var student = new User
                    {
                        UserName = "mindspring",
                        Email = "mindspring@test.com"
                    };

                    await userManager.CreateAsync(student, "Password@123");
                    await userManager.AddToRoleAsync(student, "Manager");

                    var instructor = new User
                    {
                        UserName = "admin",
                        Email = "admin@test.com"
                    };

                    await userManager.CreateAsync(instructor, "Password@123");
                    await userManager.AddToRolesAsync(instructor, new[] { "Student", "Admin" });
                }
                if (!context.TblResponses.Any())
                {
                    var funData = File.ReadAllText("../Infrastructure/Seed/funData.json");
                    var fun = JsonSerializer.Deserialize<List<TblResponse>>(funData);

                    foreach (var item in fun)
                    {
                        context.TblResponses.Add(item);
                    }

                    await context.SaveChangesAsync();

                }
            }
            catch (Exception ex)

            {
                logger.LogError(ex.Message);
            }

        }
    }
}
