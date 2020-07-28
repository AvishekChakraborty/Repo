using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.eShopWeb.Infrastructure.Data
{
    public class CatalogContextSeed
    {
        public static async Task SeedAsync(CatalogContext catalogContext,
            ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;
            try
            {
                // TODO: Only run this if using a real database
                catalogContext.Database.Migrate();
                if (!await catalogContext.CatalogBrands.AnyAsync())
                {
                    await catalogContext.CatalogBrands.AddRangeAsync(
                        GetPreconfiguredCatalogBrands());

                    await catalogContext.SaveChangesAsync();
                }

                if (!await catalogContext.CatalogTypes.AnyAsync())
                {
                    await catalogContext.CatalogTypes.AddRangeAsync(
                        GetPreconfiguredCatalogTypes());

                    await catalogContext.SaveChangesAsync();
                }

                if (!await catalogContext.CatalogItems.AnyAsync())
                {
                    await catalogContext.CatalogItems.AddRangeAsync(
                        GetPreconfiguredItems());

                    await catalogContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<CatalogContextSeed>();
                    log.LogError(ex.Message);
                    await SeedAsync(catalogContext, loggerFactory, retryForAvailability);
                }
                throw;
            }
        }

        static IEnumerable<CatalogBrand> GetPreconfiguredCatalogBrands()
        {
            return new List<CatalogBrand>()
            {
                new CatalogBrand("Car"),
                new CatalogBrand("Planes"),
                new CatalogBrand("Trucks"),
                new CatalogBrand("Boats"),
                new CatalogBrand("Rockets")
            };
        }

        static IEnumerable<CatalogType> GetPreconfiguredCatalogTypes()
        {
            return new List<CatalogType>()
            {
                new CatalogType("Basic"),
                new CatalogType("Standard"),
                new CatalogType("Premium"),
                new CatalogType("Luxury")
            };
        }

        static IEnumerable<CatalogItem> GetPreconfiguredItems()
        {
            return new List<CatalogItem>()
            {
                new CatalogItem(2,1, "Convertible Car", "This convertible car is fast! The engine is powered by a neutrino based battery (not included).Power it up and let it go!", 22.50,  "http://catalogbaseurltobereplaced/images/products/carconvert.png"),
                new CatalogItem(1,1, "Old-time Car", "There''s nothing old about this toy car, except it''s looks. Compatible with other old toy cars.", 15.95, "http://catalogbaseurltobereplaced/images/products/carearly.png"),
                new CatalogItem(2,1, "Fast Car", "Yes this car is fast, but it also floats in water.", 32.99,  "http://catalogbaseurltobereplaced/images/products/carfast.png"),
                new CatalogItem(2,1, "Super Fast Car", "Use this super fast car to entertain guests. Lights and doors work!", 8.95, "http://catalogbaseurltobereplaced/images/products/carfaster.png"),
                new CatalogItem(3,1, "Old Style Racer", "This old style racer can fly (with user assistance). Gravity controls flight duration.No batteries required.", 34.95, "http://catalogbaseurltobereplaced/images/products/carracer.png"),
                new CatalogItem(2,2, "Ace Plane", "Authentic airplane toy. Features realistic color and details.", 95, "http://catalogbaseurltobereplaced/images/products/carconvert.png"),
                new CatalogItem(2,2, "Glider", "This fun glider is made from real balsa wood. Some assembly required.",  4.95, "http://catalogbaseurltobereplaced/images/products/carearly.png"),
                new CatalogItem(2,2, "Paper Plane", "This paper plane is like no other paper plane. Some folding required.", 2.95, "http://catalogbaseurltobereplaced/images/products/carfast.png"),
                new CatalogItem(1,2, "Propeller Plane", "Rubber band powered plane features two wheels.", 32.95, "http://catalogbaseurltobereplaced/images/products/carfaster.png"),
                new CatalogItem(3,3, "Early Truck", "This toy truck has a real gas powered engine. Requires regular tune ups.", 15, "http://catalogbaseurltobereplaced/images/products/carracer.png"),
                new CatalogItem(3,3, "Fire Truck", "You will have endless fun with this one quarter sized fire truck.", 26, "http://catalogbaseurltobereplaced/images/products/carconvert.png"),
                new CatalogItem(2,3, "Big Truck", "This fun toy truck can be used to tow other trucks that are not as big.", 29, "http://catalogbaseurltobereplaced/images/products/carfaster.png"),
                new CatalogItem(2,4, "Big Ship", "Is it a boat or a ship. Let this floating vehicle decide by using its artifically intelligent computer brain!", 95, "http://catalogbaseurltobereplaced/images/products/carfaster.png"),
                new CatalogItem(2,4, "Sail Boat", "Put this fun toy sail boat in the water and let it go!", 4.95, "http://catalogbaseurltobereplaced/images/products/carfaster.png"),
                new CatalogItem(2,5, "Rocket", "This fun rocket will travel up to a height of 200 feet.", 122.95, "http://catalogbaseurltobereplaced/images/products/carfaster.png"),
            };
        }
    }
}
