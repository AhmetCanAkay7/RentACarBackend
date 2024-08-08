using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemoryCarDal;
using Entities.Concrete;
using System.Runtime.CompilerServices;

namespace ConsoleUI
{
    public class Program
    {
        private static void Main(string[] args)
        {
            InMemoryCarDal inMemoryCarDal = new InMemoryCarDal();
            EfCarDal efCarDal = new EfCarDal();
            CarManager carManager = new CarManager(efCarDal);

           foreach (var car in carManager.GetCarsByBrandId(1))
            {
                System.Console.WriteLine(car.Description);
            }

           //Getting error here because of business layer rules.
           carManager.Add(new Car { BrandId = 1, ColorId = 1, DailyPrice = 100, Description = "a", ModelYear = 2020 });



            Console.ReadLine();

            
        }

    }
}