using ApiCrudUsingGeneric.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCrudUsingGeneric.IService
{
    public interface IMoviesQueries
    {
        Movie GetMoviesbyId(int? moviesId);
        IQueryable<Movie> GetMovies();
    }
}
