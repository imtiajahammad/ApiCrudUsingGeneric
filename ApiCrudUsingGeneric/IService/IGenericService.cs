using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCrudUsingGeneric.IService
{
    public interface IGenericService<T>
    {
        List<T> GetAll();

        T GetById(int Id);

        List<T> Insert(T item);

        List<T> Delete(int id);

        List<T> Update(T item);
    }
}
