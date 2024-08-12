using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        { 
            _carDal = carDal;
            
        }

        public void Insert(Car car)
        {
            if(car.Description.Length >= 2 && car.DailyPrice > 0)
            {
                _carDal.Insert(car);
            }
            else
            {
                throw new Exception("Araba ismi minimum 2 karakter olmalıdır ve günlük fiyatı 0'dan büyük olmalıdır.");
            }
           
        }

        public void Delete(Car car)
        {
           _carDal.Delete(car);
        }

        public List<Car> GetAll()
        {
            return _carDal.GetAll();
        }

        public Car GetById(int carId)
        {
            return _carDal.Get(c=> c.Id == carId);
        }

        public List<Car> GetCarsByBrandId(int brandId)
        {
            return _carDal.GetAll(c => c.BrandId == brandId);
        }

        public List<Car> GetCarsByColorId(int colorId)
        {
            return _carDal.GetAll(c => c.ColorId == colorId);
        }

        public void Update(Car car)
        {
            _carDal.Update(car);
        }

        public List<CarDetailsDto> GetCarDetailsDtos()
        {
           return _carDal.GetCarDetailsDtos();
        }
    }
}
