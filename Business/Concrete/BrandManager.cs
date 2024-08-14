using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {

        IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }
        public IResult Delete(Brand brand)
        {
           if(brand.BrandName.Length < 3)
            {
                return new ErrorResult("Brand could not be deleted");
            }
           _brandDal.Delete(brand);
            return new SuccessResult("Brand is deleted!");
        }

        public IDataResult<List<Brand>> GetAll()
        {
           return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(),"Brands are listed.");
        }

        public IDataResult<Brand> GetById(int brandId)
        {
            return new SuccessDataResult<Brand>(_brandDal.Get(b=>b.BrandId==brandId),"This is the brand.");
        }

        public IResult Insert(Brand brand)
        {
            if(brand.BrandName.Length>3 && brand.BrandName.EndsWith('a'))
            {
            _brandDal.Insert(brand);
                return new SuccessResult("Brand was inserted.");
            }
            return new ErrorResult("Brand could not be inserted.");
        }

        public IResult Update(Brand brand)
        {
            if(DateTime.Now.Hour != 22)
            {
                _brandDal.Update(brand);
                return new SuccessResult("Brand was updated.");
            }
            return new ErrorResult(Messages.MaintenanceTime);
            
        }
    }
}
