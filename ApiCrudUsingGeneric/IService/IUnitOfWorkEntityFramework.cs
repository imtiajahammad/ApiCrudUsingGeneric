using ApiCrudUsingGeneric.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCrudUsingGeneric.IService
{
    public interface IUnitOfWorkEntityFramework
    {
        IMoviesCommand MoviesCommand { get; }
        IGenericCommand<Employee> EmployeesCommand { get; }
        bool Commit();
        void Dispose();
    }
}
