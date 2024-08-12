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

        List<Car> GetAll();
        List<CarDetailsDto> GetCarDetailsDtos();
        List<Car> GetCarsByColorId(int colorId);
        List<Car> GetCarsByBrandId(int brandId);
        Car GetById(int carId);
        void Insert(Car car);
        void Update(Car car);
        void Delete(Car car);


    }
}
