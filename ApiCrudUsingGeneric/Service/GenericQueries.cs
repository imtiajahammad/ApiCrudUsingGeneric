using ApiCrudUsingGeneric.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCrudUsingGeneric.Service
{
    public class GenericQueries<T> : IGenericQueries<T> where T : class
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public GenericQueries(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public T GetItembyId(int? Id)
        {
            var item =_applicationDbContext.Set<T>().Find(Id);
            if (item is null)
            {
                return null;
            }
            return item;
        }

        public IQueryable<T> GetItems()
        {
            //List<T> items = _applicationDbContext.Set<T>().AsQueryable();
            IQueryable<T> items = _applicationDbContext.Set<T>().AsQueryable();

            if (items is null)
            {
                return null;
            }
            return (IQueryable<T>)items;
        }
    }
}
