using ApiCrudUsingGeneric.IService;
using ApiCrudUsingGeneric.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCrudUsingGeneric.Service
{
    public class MoviesQueries: IMoviesQueries
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public MoviesQueries(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public Movie GetMoviesbyId(int? moviesId)
        {
            var movie = _applicationDbContext.Movies
                .FirstOrDefault(m => m.MoviesID == moviesId);

            return movie;
        }


        public IQueryable<Movie> GetMovies()
        {
            var source = (from movies in _applicationDbContext.Movies.
                    OrderBy(a => a.MoviesID)
                          select movies).AsQueryable();

            return source;
        }
    }
}
