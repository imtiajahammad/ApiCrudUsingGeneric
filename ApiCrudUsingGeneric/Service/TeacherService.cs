using ApiCrudUsingGeneric.IService;
using ApiCrudUsingGeneric.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCrudUsingGeneric.Service
{
    public class TeacherService : IGenericService<Teacher>
    {
        private readonly ApplicationDbContext _dbContext;
        public TeacherService(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public List<Teacher> Delete(int id)
        {
            Teacher std = _dbContext.Teachers.FirstOrDefault(x => x.TeacherId == id);
            _dbContext.Teachers.Remove(std);
            _dbContext.SaveChanges();
            return _dbContext.Teachers.ToList();
        }

        public List<Teacher> GetAll()
        {
            return _dbContext.Teachers.ToList();
        }

        public Teacher GetById(int id)
        {
            Teacher std = _dbContext.Teachers.FirstOrDefault(x => x.TeacherId == id);
            return std;
        }

        public List<Teacher> Insert(Teacher item)
        {

            _dbContext.Teachers.Add(item);
            _dbContext.SaveChanges();
            return _dbContext.Teachers.ToList();
        }
    }
}
