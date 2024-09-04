using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Helpers.FileHelper;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;
        IFileHelper _fileHelper;

        public CarImageManager(ICarImageDal carImageDal,IFileHelper fileHelper)
        {
            _carImageDal = carImageDal;
            _fileHelper = fileHelper;
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(),Messages<CarImage>.EntityListed);
        }

        public IDataResult<CarImage> GetByImageID(int id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(c=>c.ImageId==id));
        }

        public IDataResult<List<CarImage>> GetImagesByCarId(int carId)
        {
            var result = _carImageDal.GetAll(ci => ci.CarId == carId);
            if (result.Count > 0)
            {
                return new SuccessDataResult<List<CarImage>>(result,Messages<CarImage>.EntityListed);
            }
            return new SuccessDataResult<List<CarImage>>(GetDefaultImage(carId).Data);
        }

        public IDataResult<List<CarImageDetailsDto>> GetCarImagesDetail()
        {
            return new SuccessDataResult<List<CarImageDetailsDto>>(_carImageDal.GetCarImageDetailsDto());
        }


        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Insert(IFormFile formFile, CarImage carImage)
        {
            var result = BusinessRules.Run(CheckCarImageLimit(carImage.CarId));
            if(result == null)
            {
                carImage.ImagePath = _fileHelper.Upload(formFile, PathConstant.ImagesPath);
                _carImageDal.Insert(carImage);
                return new SuccessResult(Messages<CarImage>.EntityAdded);
            }
            return result;


        }

        public IResult Delete(CarImage carImage)
        {
            _fileHelper.Delete(PathConstant.ImagesPath + carImage.ImagePath);
            _carImageDal.Delete(carImage);
            return new SuccessResult(Messages<CarImage>.EntityDeleted);
        }

        public IResult Update(IFormFile formFile, CarImage carImage)
        {
            var result = BusinessRules.Run(CheckCarImageLimit(carImage.CarId), DeleteOldImage(carImage.ImageId, carImage.ImagePath));
            if (result != null)
            {
                return result;
            }
            carImage.ImagePath = _fileHelper.Update(formFile, PathConstant.ImagesPath + carImage.ImagePath, PathConstant.ImagesPath);
            _carImageDal.Update(carImage);
            return new SuccessResult(Messages<CarImage>.EntityUpdated);
        }

       

        #region Business Rules

        private IResult CheckCarImageLimit(int carId)
        {
            var result = _carImageDal.GetAll(ci => ci.CarId == carId).Count();
            if (result > 5)
            {
                return new ErrorResult(Messages<CarImage>.CarImagesLimitExceeded);
            }
            return new SuccessResult();
        }

        private IDataResult<List<CarImage>> GetDefaultImage(int carId)
        {
            var listCarImages = new List<CarImage> { new CarImage {CarId=carId,ImagePath="DefaultImage.jpg" } };
            return new SuccessDataResult<List<CarImage>>(listCarImages);
        }

        private IResult DeleteOldImage(int carImageId, string path)
        {
            var result = _carImageDal.Get(ci => ci.ImageId == carImageId).ImagePath;
            if (File.Exists(path + PathConstant.ImagesPath + result))
            {
                File.Delete(path + PathConstant.ImagesPath + result);
            }
            return new SuccessResult();
        }

       
    }
}
        #endregion