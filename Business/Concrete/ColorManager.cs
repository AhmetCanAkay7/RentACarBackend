using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ColorManager : IColorService
    {
        IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }


        public IResult Delete(Color color)
        {
            if (color.ColorName.Length < 3)
            {
                return new ErrorResult(Messages<Color>.EntityNotDeleted);
            }
            _colorDal.Delete(color);
            return new SuccessResult(Messages<Color>.EntityDeleted);
        }

        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll(), Messages<Color>.EntityListed);
        }

        public IDataResult<Color> GetById(int colorId)
        {
            return new SuccessDataResult<Color>(_colorDal.Get(c => c.ColorId == colorId), Messages<Color>.EntityListed);
        }

        public IResult Insert(Color color)
        {
            if (color.ColorName.Length > 3 && color.ColorName.EndsWith('a'))
            {
                return new ErrorResult(Messages<Color>.EntityNotAdded);
            }
            _colorDal.Insert(color);
            return new SuccessResult(Messages<Color>.EntityAdded);
        }

        public IResult Update(Color color)
        {
            if (DateTime.Now.Hour != 22)
            {
                _colorDal.Update(color);
                return new SuccessResult(Messages<Color>.EntityUpdated);
            }
            return new ErrorResult(Messages<Color>.MaintenanceTime);
        }
    }
}
