using Core.Business;
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
    public interface ICarService : IServiceRepository<Car>
    {
        IDataResult<List<CarDetailsDto>> GetCarDetailsDtos();
        IDataResult<List<Car>> GetCarsByColorId(int colorId);
        IDataResult<List<Car>> GetCarsByBrandId(int brandId);
       


    }
}
