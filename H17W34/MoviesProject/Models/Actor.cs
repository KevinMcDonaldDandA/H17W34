using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MoviesProject.Models
{
    public class Actor
    {
        public int ID { get; set; }
        [Display(Name = "Firstname")]
        public string FName { get; set; }
        [Display(Name = "Surname")]
        public string LName { get; set; }
        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DoB { get; set; }

        public string Gender { get; set; }

        [NotMapped]
        public string Fullname { get { return $"{FName} {LName}";  } }

        public virtual ICollection<Movie> Movies { get; set; }

        public Actor()
        {
            Movies = new HashSet<Movie>();
        }
    }
}