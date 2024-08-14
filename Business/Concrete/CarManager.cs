using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
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

        public IResult Delete(Car car)
        {
            if (DateTime.Now.Hour==22)
            {
                return new ErrorResult(Messages.MaintenanceTime);
            }
            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);
        }

        public IDataResult<List<Car>> GetAll()
        {
            if (DateTime.Now.Hour ==14)
            {
                return new ErrorDataResult<List<Car>>(_carDal.GetAll(), Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<Car>>(_carDal.GetAll(),Messages.CarListed);
        }

        public IDataResult<Car> GetById(int carId)
        {
            if (false)
            {
                return new ErrorDataResult<Car>(_carDal.Get(c => c.Id == carId), Messages.CarNotListed);
            }
            return new SuccessDataResult<Car>(_carDal.Get(c=>c.Id==carId), Messages.CarListed);
        }

        public IDataResult<List<CarDetailsDto>> GetCarDetailsDtos()
        {
            if (false)
            {
                return new ErrorDataResult<List<CarDetailsDto>>(Messages.CarNotListed);
            }
            return new SuccessDataResult<List<CarDetailsDto>>(_carDal.GetCarDetailsDtos(),Messages.CarListed);
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int brandId)
        {
            if (false)
            {
                return new ErrorDataResult<List<Car>>(Messages.CarNotListed);
            }
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c=>c.BrandId==brandId),Messages.CarListed);
        }

        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            if (false)
            {
                return new ErrorDataResult<List<Car>>(Messages.CarNotListed);
            }
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c=>c.ColorId==colorId),Messages.CarListed);
        }


        public IResult Insert(Car car)
        {
            if (car.Description.Length<3) 
            {
                return new ErrorResult(Messages.CarNotAdded);
            }
            _carDal.Insert(car);
            return new SuccessResult(Messages.CarAdded);
        }

        public IResult Update(Car car)
        {
            if (DateTime.Now.Hour>12)
            {
                return new ErrorResult(Messages.CarNotUpdated);
            }
            _carDal.Update(car);
            return new SuccessResult(Messages.CarUpdated);
        }
    }
}
