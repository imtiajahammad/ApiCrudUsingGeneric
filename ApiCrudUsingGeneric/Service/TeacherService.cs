using ApiCrudUsingGeneric.IService;
using ApiCrudUsingGeneric.Models;
using Microsoft.EntityFrameworkCore;
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
        public List<Teacher> Update(Teacher item)
        {
            /*Teacher std = _dbContext.Teachers.FirstOrDefault(x => x.TeacherId == item.TeacherId);
            if (std is null)
            {
                return _dbContext.Teachers.ToList();
            }
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return _dbContext.Teachers.ToList();*/
            Teacher std = _dbContext.Teachers.FirstOrDefault(x => x.TeacherId == item.TeacherId);
            if (std is null)
            {
                return _dbContext.Teachers.ToList();
            }
            std.Name = item.Name;
            std.Subject = item.Subject;
            std.Message = item.Message;
            _dbContext.Entry(std).State = EntityState.Modified;
            _dbContext.SaveChanges();
            return _dbContext.Teachers.ToList(); 

        }
    }
}
