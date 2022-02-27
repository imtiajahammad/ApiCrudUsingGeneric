using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCrudUsingGeneric.Models
{
    [Table("Students")]
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Roll { get; set; }
        public string Message { get; set; }
    }
}
