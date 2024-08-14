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

            CarManager carManager = new CarManager(new EfCarDal());
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            ColorManager colorManager = new ColorManager(new EfColorDal());
            //CarUpdateTest(carManager);
            //BrandUpdateTest(brandManager);
            //ColorUpdateTest(colorManager);

            //GetCarsTest(carManager);

            //Getting error here because of business layer rules.
            //InsertCarTest(carManager);

            //CarDtoTest(carManager);

            var result2 = carManager.GetAll();
            if (result2.IsSuccess)
            {
                foreach (var car in result2.Data)
                {
                    Console.WriteLine(car.Description);
                }
            }
            else
            {
                Console.WriteLine(result2.Message);
            }

            Console.ReadLine();
        }

        private static void CarDtoTest(CarManager carManager)
        {
            var result1 = carManager.GetCarDetailsDtos();
            foreach (var carDto in result1.Data)
            {
                Console.WriteLine(carDto.CarName + " : " + carDto.BrandName + " : " + carDto.ColorName + " : " + carDto.DailyPrice);
            }
        }

        private static void ColorUpdateTest(ColorManager colorManager)
        {
            Color colorToUpdate = colorManager.GetById(5).Data;
            colorToUpdate.ColorName = "pink";
            colorManager.Update(colorToUpdate);
        }

        private static void BrandUpdateTest(BrandManager brandManager)
        {
            Brand brandToUpdate = brandManager.GetById(5).Data;
            brandToUpdate.BrandName = "Ford";
            brandManager.Update(brandToUpdate);
        }

        private static void CarUpdateTest(CarManager carManager)
        {
            Car carToUpdate = carManager.GetById(1).Data;
            carToUpdate.BrandId = 1;
            carToUpdate.ColorId = 1;
            carToUpdate.ModelYear = 2023;
            carToUpdate.DailyPrice = 1000;
            carToUpdate.Description = "BMW-White";
            carManager.Update(carToUpdate);
        }

        private static void InsertCarTest(CarManager carManager)
        {
            carManager.Insert(new Car { BrandId = 1, ColorId = 1, DailyPrice = 100, Description = "a", ModelYear = 2020 });
        }

        private static void GetCarsTest(CarManager carManager)
        {
            foreach (var car in carManager.GetCarsByBrandId(1).Data)
            {
                System.Console.WriteLine(car.Description);
            }
        }
    }
}