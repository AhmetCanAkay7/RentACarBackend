using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.InMemoryCarDal
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars=new List<Car>();

        public InMemoryCarDal()
        {
        _cars.Add(new Car {Description="Audi A3", BrandId=1, ColorId=1, DailyPrice=3000000, ModelYear=2015, Id=1});  
        _cars.Add(new Car {Description="Audi A5", BrandId=1, ColorId=2, DailyPrice= 5000000, ModelYear=2018, Id=2}); 
        _cars.Add(new Car {Description="BMW i7", BrandId=2, ColorId=1, DailyPrice= 10000000, ModelYear=2019, Id=3}); 
        _cars.Add(new Car {Description="BMW iX", BrandId=2, ColorId=2, DailyPrice= 15000000, ModelYear=2023, Id=4}); 
        _cars.Add(new Car {Description="Mercedes", BrandId=3, ColorId=1, DailyPrice= 4000000, ModelYear=2016, Id=5}); 

        }
        public void Add(Car car)
        {
            foreach (var c in _cars)
            {
                if (c.Id == car.Id)
                {
                    Console.WriteLine("Araç sistemde ekli.");
                    return;
                }
            }
            _cars.Add(car);
        }

        public void Delete(Car car)
        {
            Car carToDelete = _cars.SingleOrDefault(c => c.Id == car.Id);
            _cars.Remove(carToDelete);
        }

        public Car Get(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Car GetById(int id)
        {
            return _cars.SingleOrDefault(c => c.Id == id);


        }

        public void Update(Car car)
        {

            Car carToUpdate;
            carToUpdate = _cars.SingleOrDefault(c => c.Id==car.Id);
            int i = _cars.IndexOf(carToUpdate);
            carToUpdate.Description = car.Description;
            carToUpdate.DailyPrice = car.DailyPrice;
            carToUpdate.ModelYear = car.ModelYear;
            carToUpdate.ColorId = car.ColorId;
            carToUpdate.BrandId = car.BrandId;
            _cars[i] = carToUpdate;

        }
    }
}
