/*
*  File	    Movie.cs
*  Author	Kevin McDonald, Dundee & Angus College, kevin.mcdonald@dundeeandangus.ac.uk
*  Date     22nd February 2019
*  Purpose	The Model class for our Movie Entity
*  Notes    
*
*       Copyright © Kevin McDonald 2019. All rights reserved.
*/

using System.ComponentModel.DataAnnotations;

namespace MoviesProject.Models
{
    public class Movie
    {
        public int ID { get; set; }
        [Required]
        [StringLength(250)]
        public string Title { get; set; }
        public string Summary { get; set; }
        [Display(Name ="User Rating")]
        public double UserRating { get; set; }
        public string ImagePath { get; set; }

        //  Director FK
        public int DirectorId { get; set; }

        //  Navigation Property
        public Director Director { get; set; }

    }
}
//------------------------------- End of File -------------------------------​