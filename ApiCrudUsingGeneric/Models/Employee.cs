using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCrudUsingGeneric.Models
{
    [Table("Employees")]
    public class Employee
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Designation { get; set; }
        public int Age { get; set; }
        [NotMapped]
        public IEnumerable<Link> Links { get; set; }
    }
}
