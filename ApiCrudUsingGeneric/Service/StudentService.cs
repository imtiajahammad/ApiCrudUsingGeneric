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
        List<Student> _students = new List<Student>();

        public StudentService()
        {
            for(int i=1; i<=9; i++)
            {
                _students.Add(
                    new Student
                    {
                        StudentId = i,
                        Name = "Stu" + i + 1,
                        Roll = "100" + i
                    }
                    );
            }
        }

        public List<Student> Delete(int id)
        {
            _students.RemoveAll(x => x.StudentId == id);
            return _students;
        }

        public List<Student> GetAll()
        {
            return _students;
        }

        public Student GetById(int Id)
        {
            return _students.Where(x => x.StudentId == Id).SingleOrDefault();
        }

        public List<Student> Insert(Student item)
        {
            _students.Add(item);
            return _students;
        }
    }
}
