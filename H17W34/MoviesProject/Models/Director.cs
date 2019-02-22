/*
*  File	    Director.cs
*  Author	Kevin McDonald, Dundee & Angus College, kevin.mcdonald@dundeeandangus.ac.uk
*  Date     22nd February 2019
*  Purpose	The Model class for our Director Entity
*  Notes    
*
*       Copyright © Kevin McDonald 2019. All rights reserved.
*/

using System.ComponentModel.DataAnnotations;

namespace MoviesProject.Models
{
    public class Director
    {
        public int ID { get; set; }

        [Required(ErrorMessage ="A Firstname is Required for a Director")]
        [StringLength(25)]
        [Display(Name ="Firstname")]
        public string FName { get; set; }

        [Required(ErrorMessage = "A Surname is Required for a Director")]
        [StringLength(50)]
        [Display(Name = "Surname")]
        public string LName { get; set; }
    }
}
//------------------------------- End of File -------------------------------​