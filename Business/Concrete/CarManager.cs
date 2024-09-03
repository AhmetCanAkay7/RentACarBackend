using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
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
                return new ErrorDataResult<List<Car>>(Messages<Car>.MaintenanceTime);
            }

            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages<Car>.EntityListed);
        }

        public IDataResult<Car> GetById(int carId)
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<Car>(Messages<Car>.EntityNotListed);
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

        //eklenecek olan car nesnesinin yapısıyla ilgili kurallar validation kodudur.
        [ValidationAspect(typeof(CarValidator))]
        public IResult Insert(Car car)
        {
         var result =BusinessRules.Run(CheckCarNameAlreadyExists(car.Description),CheckCarCountOfBrandError(car.BrandId));
            if (result == null)
            {
                _carDal.Insert(car);
                return new SuccessResult(Messages<Car>.EntityAdded);
            }
            return result;
           
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

        public IResult CheckCarNameAlreadyExists(string carName)
        {
            var result = _carDal.GetAll(c => c.Description.Equals(carName));
            if (result.Any())
            {
                return new ErrorResult(Messages<Car>.CarNameAlreadyExists);
            }
            return new SuccessResult();
        }

        public IResult CheckCarCountOfBrandError(int brandId)
        {
            var result = _carDal.GetAll(c =>c.BrandId==brandId).Count();
            if (result > 10)
            {
                return new ErrorResult(Messages<Car>.CarCountOfBrandError);
            }
            return new SuccessResult();
        }
    }
}
