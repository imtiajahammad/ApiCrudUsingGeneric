using ApiCrudUsingGeneric.IService;
using ApiCrudUsingGeneric.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCrudUsingGeneric.Service
{
    public class MoviesCommand : IMoviesCommand
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public MoviesCommand(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public void Add(Movie movies)
        {
            _applicationDbContext.Movies.Add(movies);
        }

        public void Update(Movie movies)
        {
            _applicationDbContext.Entry(movies).State = EntityState.Modified;
        }

        public void Delete(Movie movies)
        {
            _applicationDbContext.Entry(movies).State = EntityState.Deleted;
        }
    }
}
