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
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult Delete(User entity)
        {
            _userDal.Delete(entity);
            return new SuccessResult(Messages<User>.EntityDeleted);
        }

        public IDataResult<List<User>> GetAll()
        {
           if (_userDal == null)
            {
                return new ErrorDataResult<List<User>>(Messages<User>.EntityNotListed);
            }
            else
            {
                return new SuccessDataResult<List<User>>(_userDal.GetAll(),Messages<User>.EntityListed);
            }
        }

        public IDataResult<User> GetById(int entityId)
        {
            return new SuccessDataResult<User>(_userDal.Get(u=> u.UserId==entityId));
        }

        public IResult Insert(User entity)
        {
            if (DateTime.Now.Hour==21)
            {
                return new ErrorResult(Messages<User>.EntityNotAdded);
            }
            _userDal.Insert(entity);
            return new SuccessResult(Messages<User>.EntityAdded);
        }

        public IResult Update(User entity)
        {
            _userDal.Update(entity);
            return new SuccessResult(Messages<User>.EntityUpdated);
        }
    }
}
