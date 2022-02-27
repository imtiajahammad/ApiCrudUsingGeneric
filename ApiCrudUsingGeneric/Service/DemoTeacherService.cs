using ApiCrudUsingGeneric.IService;
using ApiCrudUsingGeneric.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCrudUsingGeneric.Service
{
    public class DemoTeacherService : IGenericService<Teacher>
    {
        List<Teacher> _teachers = new List<Teacher>();
        public DemoTeacherService()
        {
            for (int i = 1; i <= 9; i++)
            {
                _teachers.Add(
                    new Teacher
                    {
                        TeacherId = i,
                        Name = "Tch" + i + 1,
                        Subject = "Sub" + i
                    }
                    );
            }
        }
        public List<Teacher> Delete(int id)
        {
            _teachers.RemoveAll(x => x.TeacherId == id);
            return _teachers;
        }

        public List<Teacher> GetAll()
        {
            return _teachers;
        }

        public Teacher GetById(int Id)
        {
            return _teachers.Where(x => x.TeacherId == Id).SingleOrDefault();
        }

        public List<Teacher> Insert(Teacher item)
        {
            _teachers.Add(item);
            return _teachers;
        }
    }
}
