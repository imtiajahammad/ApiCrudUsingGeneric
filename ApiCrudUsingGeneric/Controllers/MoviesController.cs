using ApiCrudUsingGeneric.IService;
using ApiCrudUsingGeneric.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCrudUsingGeneric.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IUnitOfWorkEntityFramework _unitOfWorkEntityFramework;
        private readonly IMoviesQueries _iMoviesQueries;

        public MoviesController(
            IUnitOfWorkEntityFramework unitOfWorkEntityFramework
            ,IMoviesQueries moviesQueries
            )
        {
            _unitOfWorkEntityFramework = unitOfWorkEntityFramework;
            _iMoviesQueries = moviesQueries;
        }

        [HttpPost]
        public IActionResult CreateMovies(MoviesCreateRequest moviesRequest)
        {
            Movie movies = new Movie()
            {
                MoviesID = 0,
                Name = moviesRequest.Name,
                Director = moviesRequest.Director,
                Genre = moviesRequest.Genre,
                MovieLength = moviesRequest.MovieLength,
                Rating = moviesRequest.Rating,
                ReleaseYear = moviesRequest.ReleaseYear,
                Title = moviesRequest.Title
            };

            _unitOfWorkEntityFramework.MoviesCommand.Add(movies);
            var result = _unitOfWorkEntityFramework.Commit();

            if (result)
            {
                return StatusCode(StatusCodes.Status200OK, "added");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetMovie(int? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Not Found");

            }
            var movieTb = _iMoviesQueries.GetMoviesbyId(id);
            if (movieTb == null)
            {
                return NotFound();
            }

            return Ok(movieTb);
        }

        [HttpDelete]
        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = _iMoviesQueries.GetMoviesbyId(id);
            if (movie == null)
            {
                return NotFound();
            }

            _unitOfWorkEntityFramework.MoviesCommand.Delete(movie);
            var result = _unitOfWorkEntityFramework.Commit();

            if (result)
            {
                return StatusCode(StatusCodes.Status200OK, "Movies Deleted Successfully !");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
        }

        [HttpPut]
        public IActionResult UpdateMovie(MoviesCreateRequest moviesRequest, int? id)
        {
            var movie = _iMoviesQueries.GetMoviesbyId(id);

            movie.Director = moviesRequest.Director;
            movie.Genre = moviesRequest.Genre;
            movie.MovieLength = moviesRequest.MovieLength;
            movie.Rating = moviesRequest.Rating;
            movie.ReleaseYear = moviesRequest.ReleaseYear;
            movie.Title = moviesRequest.Title;

            _unitOfWorkEntityFramework.MoviesCommand.Update(movie);
            var result = _unitOfWorkEntityFramework.Commit();

            if (result)
            {
                return StatusCode(StatusCodes.Status200OK, "Movies Updated Successfully !");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }

        }

        /*[HttpGet]
        [Route("GetAllMovie")]
        public IEnumerable<Movie> GetAllMovies([FromQuery] PagingParameter pagingParameter)
        {
            var source = _iMoviesQueries.GetMovies();
            // Get's No of Rows Count   
            int count = source.Count();

            // Parameter is passed from Query string if it is null then it default Value will be pageNumber:1  
            int currentPage = pagingParameter.PageNumber;

            // Parameter is passed from Query string if it is null then it default Value will be pageSize:20  
            int pageSize = pagingParameter.PageSize;

            // Display TotalCount to Records to User  
            int totalCount = count;

            // Calculating Totalpage by Dividing (No of Records / Pagesize)  
            int totalPages = (int)Math.Ceiling(count / (double)pageSize);

            // Returns List of Customer after applying Paging   
            var items = source.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();

            // if CurrentPage is greater than 1 means it has previousPage  
            var previousPage = currentPage > 1 ? "Yes" : "No";

            // if TotalPages is greater than CurrentPage means it has nextPage  
            var nextPage = currentPage < totalPages ? "Yes" : "No";

            // Object which we are going to send in header   
            var paginationMetadata = new PaginationMetadata
            {
                TotalCount = totalCount,
                PageSize = pageSize,
                CurrentPage = currentPage,
                TotalPages = totalPages,
                PreviousPage = previousPage,
                NextPage = nextPage
            };

            HttpContext.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
            // Returing List of Customers Collections  
            return items;
        }*/
    }
}