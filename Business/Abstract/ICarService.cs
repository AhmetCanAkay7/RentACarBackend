using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICarService
    {

        IDataResult<List<Car>> GetAll();
        IDataResult<List<CarDetailsDto>> GetCarDetailsDtos();
        IDataResult<List<Car>> GetCarsByColorId(int colorId);
        IDataResult<List<Car>> GetCarsByBrandId(int brandId);
        IDataResult<Car> GetById(int carId);
        IResult Insert(Car car);
        IResult Update(Car car);
        IResult Delete(Car car);


    }
}
