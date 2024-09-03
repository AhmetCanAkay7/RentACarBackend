using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarImageService 
    {
        IDataResult<List<CarImage>> GetAll();
        IDataResult<CarImage> GetByImageID(int id);

        //IDataResult<List<CarImageDetailDto>> GetCarImagesDetail();
        IDataResult<List<CarImage>> GetImagesByCarId(int carId);
        IResult Insert(IFormFile formFile, CarImage carImage);
        IResult Update(IFormFile formFile, CarImage carImage);
        IResult Delete(CarImage carImage);
    }
}
