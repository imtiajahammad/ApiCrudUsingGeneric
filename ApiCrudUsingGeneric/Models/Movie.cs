using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCrudUsingGeneric.Models
{
    [Table("Movies")]
    public class Movie
    {
        [Key]
        public int MoviesID { get; set; }
        public string Name { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
        public string Rating { get; set; }
        public string MovieLength { get; set; }
    }
}
