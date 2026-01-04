using Microsoft.EntityFrameworkCore;
using MzadService.Data.Enums;
using MzadService.Entities;
using MzadService.Infrastructure;
using System.Threading.Tasks;

namespace MzadService.Data.DataSeeding
{
    public static class DbInitializer
    {
        public static async Task InitializeDbAsync(WebApplication application)
        {
            try
            {
                using var scope = application.Services.CreateScope();
                var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
                await Initialize(context);
            }
            catch (Exception ex)
            {
                // Log the exception (you can use any logging framework you prefer)
                Console.WriteLine($"An error occurred while initializing the database: {ex.Message}");
            }

        }
        public static async Task Initialize(ApplicationDbContext context)
        {
            await context.Database.MigrateAsync();
            // Look for any mzads.
            try
            {
                
                var data = await context.Mzads.AnyAsync();
                if (data)
                {
                    return;   // DB has been seeded
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error with seeding the data ...{ex.Message}");
            }
            
            var mzads = new Mzad[]
            {
                new Mzad
                {
                    Seller = "Ahmed Ali",
                    ReservePrice = 1500,
                    CreatedAt = DateTime.UtcNow.AddDays(-10),
                    MzadEnd = DateTime.UtcNow.AddDays(-3),
                    Status = Status.Finished,
                    Horse = new Horse
                    {
                        Name = "Storm",
                        Color = "Brown",
                        Breed = "Arabian",
                        YearOfBirth = 2016,
                        Father = "Desert Wind",
                        Mother = "Sahara",
                        ImageUrl = "http://example.com/storm.jpg"
                    },
                    SoldAmount = 1800,
                    Winner = "Mohamed Hassan",
                    CurrentHighTender = 566
                },

                new Mzad
                {
                    Seller = "Youssef Kamal",
                    ReservePrice = 2500,
                    CreatedAt = DateTime.UtcNow.AddDays(-5),
                    MzadEnd = DateTime.UtcNow.AddDays(2),
                    Status = Status.Live,
                    Horse = new Horse
                    {
                        Name = "Shadow",
                        Color = "Black",
                        Breed = "Thoroughbred",
                        YearOfBirth = 2018,
                        Father = "Night King",
                        Mother = "Moon Light",
                        ImageUrl = "http://example.com/shadow.jpg"
                    },
                    SoldAmount = null,
                    Winner = null,
                    CurrentHighTender = 4000
                },

                new Mzad
                {
                    Seller = "Khaled Omar",
                    ReservePrice = 900,
                    CreatedAt = DateTime.UtcNow.AddDays(-2),
                    MzadEnd = DateTime.UtcNow.AddDays(5),
                    Status = Status.ReserveNotMet,
                    Horse = new Horse
                    {
                        Name = "Blaze",
                        Color = "Chestnut",
                        Breed = "Quarter Horse",
                        YearOfBirth = 2017,
                        Father = "Fire Star",
                        Mother = "Ruby",
                        ImageUrl = "http://example.com/blaze.jpg"
                    },
                    SoldAmount = null,
                    Winner = null,
                    CurrentHighTender = 450
                },

                new Mzad
                {
                    Seller = "Mahmoud Salah",
                    ReservePrice = 3200,
                    CreatedAt = DateTime.UtcNow.AddDays(-15),
                    MzadEnd = DateTime.UtcNow.AddDays(-1),
                    Status = Status.Finished,
                    Horse = new Horse
                    {
                        Name = "Falcon",
                        Color = "Gray",
                        Breed = "Arabian",
                        YearOfBirth = 2014,
                        Father = "Silver Wing",
                        Mother = "Cloud",
                        ImageUrl = "http://example.com/falcon.jpg"
                    },
                    SoldAmount = 3500,
                    Winner = "Omar Nabil",
                    CurrentHighTender = 900
                }
            };
            foreach (Mzad m in mzads)
            {
                await context.Mzads.AddAsync(m);
            }
            try
            {
                await context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error with seeding the data ...{ex.Message}");
            }
            
        }
    }
}
