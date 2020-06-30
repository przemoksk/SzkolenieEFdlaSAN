using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace CarDealerView.Models
{
    public class DbSeeder
    {
        internal static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new CarDealerDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<CarDealerDbContext>>()
                ))
            {
                if (context.CarModels.Any())
                {
                    return;
                }

                context.CarModels.AddRange(
                        new CarModel
                        {
                            Manufacturer = "Opel",
                            Model = "Meriva"
                        },
                        new CarModel
                        {
                            Manufacturer = "Ford",
                            Model = "Mondeo"
                        },
                        new CarModel
                        {
                            Manufacturer = "Ford",
                            Model = "Galaxy"
                        }
                    );
                context.SaveChanges();

                CarModel meriva = context.CarModels.ToList<CarModel>()[0];
                context.Cars.AddRange(
                    new Car()
                    {
                        AccidentFree = true,
                        ProductionYear = 2017,
                        FuelType = FuelType.Petrol,
                        Price = 35000,
                        Mileage = 30000,
                        CarModels = meriva
                    },
                    new Car()
                    {
                        AccidentFree = true,
                        ProductionYear = 2010,
                        FuelType = FuelType.Diesel,
                        Price = 15000,
                        Mileage = 199050,
                        CarModels = meriva
                    });
                context.SaveChanges();
            }
        }
    }
}