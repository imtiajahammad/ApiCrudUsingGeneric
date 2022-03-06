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
    public class EmployeeController : ControllerBase
    {
        private readonly IUnitOfWorkEntityFramework _unitOfWorkEntityFramework;
        private readonly IGenericQueries<Employee> _employeeQueries;
        public EmployeeController(
            IUnitOfWorkEntityFramework unitOfWorkEntityFramework
            , IGenericQueries<Employee> genericQueries
            )
        {
            this._unitOfWorkEntityFramework = unitOfWorkEntityFramework;
            this._employeeQueries = genericQueries;
        }

        [HttpPost]
        public IActionResult Post(Employee obj)
        {
            Employee emp = new Employee()
            {
                Name = obj.Name,
                Designation =obj.Designation,
                Age =obj.Age
            };

            _unitOfWorkEntityFramework.EmployeesCommand.Add(emp);
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
        public IActionResult Get(int? id)
        {
            if (id == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Not Found");

            }
            var movieTb = _employeeQueries.GetItembyId(id);
            if (movieTb == null)
            {
                return NotFound();
            }

            return Ok(movieTb);
        }

        [HttpDelete]
        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emp = _employeeQueries.GetItembyId(id);
            if (emp == null)
            {
                return NotFound();
            }

            _unitOfWorkEntityFramework.EmployeesCommand.Delete(emp);
            var result = _unitOfWorkEntityFramework.Commit();

            if (result)
            {
                return StatusCode(StatusCodes.Status200OK, "Employee Deleted Successfully !");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }
        }

        [HttpPut]
        public IActionResult Update(Employee emp)
        { 
            var employee = _employeeQueries.GetItembyId(emp.Id);

            employee.Name = emp.Name;
            employee.Designation = emp.Designation;
            employee.Age = emp.Age;
            _unitOfWorkEntityFramework.EmployeesCommand.Update(employee);
            var result = _unitOfWorkEntityFramework.Commit();

            if (result)
            {
                return StatusCode(StatusCodes.Status200OK, "Employee Updated Successfully !");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Something Went Wrong");
            }

        }


        [HttpGet]
        public IActionResult Get()
        {
            IQueryable<Employee> movieTb = _employeeQueries.GetItems();
            if (movieTb == null)
            {
                return NotFound();
            }

            return Ok(movieTb);
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
