using ApiCrudUsingGeneric.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCrudUsingGeneric.IService
{
    public interface IMoviesCommand
    {
        void Add(Movie movies);
        void Update(Movie movies);
        void Delete(Movie movies);
    }
}
