using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
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

        public IResult Delete(User user)
        {
            _userDal.Delete(user);
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

        public IDataResult<User> GetById(int userId)
        {
            return new SuccessDataResult<User>(_userDal.Get(u=> u.Id== userId));
        }

        public IDataResult<User> GetByMail(string email)
        {
            var data = _userDal.Get(u => u.Email==email);
            if (data==null)
            {
                return new ErrorDataResult<User>();
            }
            return new SuccessDataResult<User>(data,Messages<User>.EntityListed);
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }

        public IResult Insert(User user)
        {
            if (DateTime.Now.Hour==21)
            {
                return new ErrorResult(Messages<User>.EntityNotAdded);
            }
            _userDal.Insert(user);
            return new SuccessResult(Messages<User>.EntityAdded);
        }

        public IResult Update(User user)
        {
            _userDal.Update(user);
            return new SuccessResult(Messages<User>.EntityUpdated);
        }
    }
}
