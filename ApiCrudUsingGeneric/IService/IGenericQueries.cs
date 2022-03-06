using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCrudUsingGeneric.IService
{
    public interface IGenericQueries<T>
    {
        T GetItembyId(int? Id);
        IQueryable<T> GetItems();
    }
}
