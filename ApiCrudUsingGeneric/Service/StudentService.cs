using ApiCrudUsingGeneric.IService;
using ApiCrudUsingGeneric.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCrudUsingGeneric.Service
{
    public class StudentService : IGenericService<Student>
    {
        private readonly ApplicationDbContext _dbContext;
        public StudentService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public List<Student> Delete(int id)
        {
            Student std = _dbContext.Students.FirstOrDefault(x => x.StudentId == id);
            _dbContext.Students.Remove(std);
            _dbContext.SaveChanges();
            return _dbContext.Students.ToList();
        }

        public List<Student> GetAll()
        {
            return _dbContext.Students.ToList();
        }

        public Student GetById(int id)
        {
            Student std = _dbContext.Students.FirstOrDefault(x => x.StudentId == id);
            return std;
        }

        public List<Student> Insert(Student item)
        {
            _dbContext.Students.Add(item);
            _dbContext.SaveChanges();
            return _dbContext.Students.ToList();
        }
    }
}
