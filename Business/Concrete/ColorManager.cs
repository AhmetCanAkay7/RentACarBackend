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
                return new ErrorResult("Color could not be deleted");
            }
            _colorDal.Delete(color);
            return new SuccessResult("Color is deleted!");
        }

        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll(), "Colors are listed.");
        }

        public IDataResult<Color> GetById(int colorId)
        {
            return new SuccessDataResult<Color>(_colorDal.Get(c => c.ColorId == colorId), "This is the desired color.");
        }

        public IResult Insert(Color color)
        {
            if (color.ColorName.Length > 3 && color.ColorName.EndsWith('a'))
            {
                return new ErrorResult("Color could not be inserted.");
            }
            _colorDal.Insert(color);
            return new SuccessResult("Color is inserted.");
        }

        public IResult Update(Color color)
        {
            if (DateTime.Now.Hour != 22)
            {
                _colorDal.Update(color);
                return new SuccessResult("Color was updated.");
            }
            return new ErrorResult(Messages.MaintenanceTime);
        }
    }
}
