using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarImageDal : EfEntityRepositoryBase<CarImage, CarsDbContext>, ICarImageDal
    {
        public List<CarImageDetailsDto> GetCarImageDetailsDto()
        {
            using (CarsDbContext context = new CarsDbContext())
            {
                var result = from car in context.Cars
                             join carImage in context.CarImages
                             on car.Id equals carImage.CarId
                             select new CarImageDetailsDto
                             {
                                 CarId = carImage.CarId,
                                 Description=car.Description,
                                 brandId=car.BrandId,
                                 ImageId = carImage.ImageId,
                                 ImagePath=carImage.ImagePath
                             };

                return result.ToList();

            }
        }
    }
}
