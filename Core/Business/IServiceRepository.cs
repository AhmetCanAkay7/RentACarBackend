using Core.Entities;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Business
{
    public interface IServiceRepository<T> where T: class,IEntity,new()
    {
        IDataResult<List<T>> GetAll();

        IDataResult<T> GetById(int entityId);

        IResult Insert(T entity);
        IResult Update(T entity);
        IResult Delete(T entity);
    }
}
