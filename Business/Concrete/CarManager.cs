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
                return new ErrorResult(Messages<Car>.MaintenanceTime);
            }
            _carDal.Delete(car);
            return new SuccessResult(Messages<Car>.EntityDeleted);
        }

        public IDataResult<List<Car>> GetAll()
        {
            if (DateTime.Now.Hour ==14)
            {
                return new ErrorDataResult<List<Car>>(_carDal.GetAll(), Messages<Car>.MaintenanceTime);
            }

            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages<Car>.EntityListed);
        }

        public IDataResult<Car> GetById(int carId)
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<Car>(_carDal.Get(c => c.Id == carId), Messages<Car>.EntityNotListed);
            }
            return new SuccessDataResult<Car>(_carDal.Get(c=>c.Id==carId), Messages<Car>.EntityListed);
        }

        public IDataResult<List<CarDetailsDto>> GetCarDetailsDtos()
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<CarDetailsDto>>(Messages<Car>.EntityNotListed);
            }
            return new SuccessDataResult<List<CarDetailsDto>>(_carDal.GetCarDetailsDtos(), Messages<Car>.EntityListed);
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int brandId)
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<Car>>(Messages<Car>.EntityNotListed);
            }
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c=>c.BrandId==brandId), Messages<Car>.EntityListed);
        }

        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            if (DateTime.Now.Hour==22)
            {
                return new ErrorDataResult<List<Car>>(Messages<Car>.EntityNotListed);
            }
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c=>c.ColorId==colorId), Messages<Car>.EntityListed);
        }


        public IResult Insert(Car car)
        {
            if (car.Description.Length<3) 
            {
                return new ErrorResult(Messages<Car>.EntityNotAdded);
            }
            _carDal.Insert(car);
            return new SuccessResult(Messages<Car>.EntityAdded);
        }

        public IResult Update(Car car)
        {
            if (DateTime.Now.Hour>12)
            {
                return new ErrorResult(Messages<Car>.EntityNotUpdated);
            }
            _carDal.Update(car);
            return new SuccessResult(Messages<Car>.EntityUpdated);
        }
    }
}
