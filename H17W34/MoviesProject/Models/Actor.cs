/*
*  File	    Actor.cs
*  Author	Kevin McDonald, Dundee & Angus College, kevin.mcdonald@dundeeandangus.ac.uk
*  Date     22nd April 2019
*  Purpose	The Model class for our Actor Entity
*  Notes    
*
*       Copyright © Kevin McDonald 2019. All rights reserved.
*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public virtual ICollection<Movie> Movies { get; set; }

        [NotMapped]
        public string Fullname { get { return $"{FName} {LName}";  } }
    }
}
//------------------------------- End of File -------------------------------​