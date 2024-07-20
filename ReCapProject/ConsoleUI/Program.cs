using Business.Concrete;
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
            CarManager carManager = new CarManager(inMemoryCarDal);

            foreach (var car in carManager.GetAll())
            {
                Console.WriteLine(car.Description);
            }

            
            Console.WriteLine("-------------------------Hadi bir araç güncelleyelim ---------------------------");
            Car carToUpdate = carManager.GetbyId(2);
            carToUpdate.Description = "Audi A5 Sportback";

            foreach (var car in carManager.GetAll())
            {
                Console.WriteLine(car.Description);
            }



            Console.ReadLine();

            
        }

    }
}