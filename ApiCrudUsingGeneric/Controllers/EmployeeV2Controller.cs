using ApiCrudUsingGeneric.IService;
using ApiCrudUsingGeneric.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCrudUsingGeneric.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/Employee")]
    [ControllerName("Employee")]
    public class EmployeeV2Controller : Controller
    {
        private readonly IUnitOfWorkEntityFramework _unitOfWorkEntityFramework;
        private readonly IGenericQueries<Employee> _employeeQueries;
        private LinkGenerator _linkGenerator;
        public EmployeeV2Controller(
            IUnitOfWorkEntityFramework unitOfWorkEntityFramework
            , IGenericQueries<Employee> genericQueries
            , LinkGenerator linkGenerator
            )
        {
            this._unitOfWorkEntityFramework = unitOfWorkEntityFramework;
            this._employeeQueries = genericQueries;
            _linkGenerator = linkGenerator;
        }

        [HttpPost]
        public IActionResult Post(Employee obj)
        {
            Employee emp = new Employee()
            {
                Name = obj.Name,
                Designation = obj.Designation,
                Age = obj.Age
            };
            _unitOfWorkEntityFramework.EmployeesCommand.Add(emp);
            /*object qq = default;
            int i2 = (int)qq;*/
            var result = _unitOfWorkEntityFramework.Commit();
            #region resultIsSuccess-start
            if (result)
            {
                var ownerLinks = CreateLinksForModel(emp.Id, null);
                emp.Links = ownerLinks;
                var ownersWrapper = new LinkCollectionWrapper(emp);
                return Ok(new StatusResult<LinkCollectionWrapper>
                {
                    Result = CreateLinksForController(ownersWrapper),
                    Status = ResponseStatus.FETCHSUCCESS,
                    Message = "Empoyee Created with specified Id"
                });
            }
            #endregion resultIsSuccess-end

            #region resultIsFalse-end
            else
            {
                var ownersWrapper2 = new LinkCollectionWrapper(obj);
                return Ok(new StatusResult<LinkCollectionWrapper>
                {
                    Result = CreateLinksForController(ownersWrapper2),
                    Status = ResponseStatus.COMMITERROR,
                    Message = "Empoyee Creation Failed"
                });
            }
            #endregion resultIsFalse-end
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var movieTb = _employeeQueries.GetItembyId(id);
            #region noContent
            if (movieTb == null)
            {

                var ownersWrapper0 = new LinkCollectionWrapper(movieTb);
                return Ok(new StatusResult<LinkCollectionWrapper>
                {
                    Result = CreateLinksForController(ownersWrapper0),
                    Status = ResponseStatus.NOTFOUND,
                    Message = "No Employee found with specified Id"
                });
            }
            #endregion noContent
            var ownerLinks = CreateLinksForModel(movieTb.Id, null);
            movieTb.Links = ownerLinks;
            var ownersWrapper = new LinkCollectionWrapper(movieTb);
            return Ok(new StatusResult<LinkCollectionWrapper>
            {
                Result = CreateLinksForController(ownersWrapper),
                Status = ResponseStatus.FETCHSUCCESS,
                Message = "Empoyee fetched with specified Id"
            });
        }

        [HttpDelete]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var emp = _employeeQueries.GetItembyId(id);
            if (emp == null)
            {
                var ownersWrapper0 = new LinkCollectionWrapper(id);
                return Ok(new StatusResult<LinkCollectionWrapper>
                {
                    Result = CreateLinksForController(ownersWrapper0),
                    Status = ResponseStatus.NOTFOUND,
                    Message = "No Employee found with specified Id"
                });
            }

            _unitOfWorkEntityFramework.EmployeesCommand.Delete(emp);
            var result = _unitOfWorkEntityFramework.Commit();

            if (result)
            {
                var ownersWrapper = new LinkCollectionWrapper(id);
                return Ok(new StatusResult<LinkCollectionWrapper>
                {
                    Result = CreateLinksForController(ownersWrapper),
                    Status = ResponseStatus.DELETED,
                    Message = "Empoyee Deleted with specified Id"
                });
            }
            else
            {
                var ownersWrapper2 = new LinkCollectionWrapper(id);
                return Ok(new StatusResult<LinkCollectionWrapper>
                {
                    Result = CreateLinksForController(ownersWrapper2),
                    Status = ResponseStatus.COMMITERROR,
                    Message = "Empoyee Delete Failed"
                });
            }
        }

        [HttpPut]
        public IActionResult Update(Employee emp)
        {
            if (emp == null)
            {
                var ownersWrapper0 = new LinkCollectionWrapper(emp);
                return BadRequest(new StatusResult<LinkCollectionWrapper>
                {
                    Result = CreateLinksForController(ownersWrapper0),
                    Status = ResponseStatus.NULLPARAMETER,
                    Message = "Parameter is null"
                });
            }
            var employee = _employeeQueries.GetItembyId(emp.Id);
            if (employee == null)
            {
                var ownersWrapper0 = new LinkCollectionWrapper(emp);
                return Ok(new StatusResult<LinkCollectionWrapper>
                {
                    Result = CreateLinksForController(ownersWrapper0),
                    Status = ResponseStatus.NOTFOUND,
                    Message = "No Employee found with specified Id"
                });
            }
            employee.Name = emp.Name;
            employee.Designation = emp.Designation;
            employee.Age = emp.Age;
            _unitOfWorkEntityFramework.EmployeesCommand.Update(employee);
            var result = _unitOfWorkEntityFramework.Commit();
            result = false;
            if (result)
            {
                var ownerLinks = CreateLinksForModel(employee.Id, null);
                employee.Links = ownerLinks;
                var ownersWrapper0 = new LinkCollectionWrapper(employee);
                return Ok(new StatusResult<LinkCollectionWrapper>
                {
                    Result = CreateLinksForController(ownersWrapper0),
                    Status = ResponseStatus.UPDATED,
                    Message = "Employee updated with specified Id"
                });
            }
            else
            {
                var ownersWrapper0 = new LinkCollectionWrapper(emp);
                return Ok(new StatusResult<LinkCollectionWrapper>
                {
                    Result = CreateLinksForController(ownersWrapper0),
                    Status = ResponseStatus.COMMITERROR,
                    Message = "Employee update failed with specified Id"
                });
            }
        }


        [HttpGet]
        public IActionResult Get()
        {
            IQueryable<Employee> movieTb = _employeeQueries.GetItems();
            if (movieTb == null)
            {
                var ownersWrapper0 = new LinkCollectionWrapper(movieTb);
                return Ok(new StatusResult<LinkCollectionWrapper>
                {
                    Result = CreateLinksForController(ownersWrapper0),
                    Status = ResponseStatus.NOTFOUND,
                    Message = "No Employee found"
                });
            }


            foreach (Employee aa in movieTb)
            {
                var singleModelLinks = CreateLinksForModel(aa.Id, null);
                aa.Links = singleModelLinks;
            }
            var ownersWrapper = new LinkCollectionWrapper(movieTb);
            return Ok(new StatusResult<LinkCollectionWrapper>
            {
                Result = CreateLinksForController(ownersWrapper),
                Status = ResponseStatus.FETCHSUCCESS,
                Message = "Empoyees fetched"
            });
        }


        private IEnumerable<Link> CreateLinksForModel(int id, string fields = "")
        {
            var links = new List<Link>
            {
                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(Get), values: new { id, fields }),
                "GetEmployeeById",
                "GET"),
                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(Update), values: new { id,fields }),
                "UpdateEmployee",
                "PUT"),
                new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(Delete), values: new { id,fields }),
                "DeleteEmployeeById",
                "DELETE")
            };
            return links;
        }
        private LinkCollectionWrapper CreateLinksForController(LinkCollectionWrapper CollectionWrapper)
        {
            CollectionWrapper.Links.Add(new Link("https://localhost:44391/api/Employee",
                    "GetEmployees",
                    "GET"));
            return CollectionWrapper;
        }
    }
}
