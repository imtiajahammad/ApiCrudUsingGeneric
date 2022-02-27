using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCrudUsingGeneric.Models
{
    [Table("Teachers")]
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
