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
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }
        public IResult Delete(Customer entity)
        {
            _customerDal.Delete(entity);
           return new SuccessResult(Messages<Customer>.EntityDeleted);
        }

        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll(),Messages<Customer>.EntityListed);
        }

        public IDataResult<Customer> GetById(int entityId)
        {
            return new SuccessDataResult<Customer>(_customerDal.Get(c=> c.CustomerId==entityId),Messages<Customer>.EntityListed);
        }

        public IResult Insert(Customer entity)
        {
            _customerDal.Insert(entity);
            return new SuccessResult(Messages<Customer>.EntityAdded);
        }

        public IResult Update(Customer entity)
        {
            _customerDal.Update(entity);
            return new SuccessResult(Messages<Customer>.EntityUpdated);
        }
    }
}
