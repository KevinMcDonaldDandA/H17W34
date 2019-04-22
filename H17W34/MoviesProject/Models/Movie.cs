/*
*  File	    Movie.cs
*  Author	Kevin McDonald, Dundee & Angus College, kevin.mcdonald@dundeeandangus.ac.uk
*  Date     22nd February 2019
*  Purpose	The Model class for our Movie Entity
*  Notes    
*
*       Copyright © Kevin McDonald 2019. All rights reserved.
*/

using System.Collections.Generic;
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
        [Display(Name ="Movie Poster")]
        public string ImagePath { get; set; }

        //  Director FK
        public int DirectorId { get; set; }

        //  Navigation Property
        public Director Director { get; set; }

        public virtual ICollection<Actor> Actors { get; set; }

        public Movie()
        {
            Actors = new List<Actor>();
        }
    }
}
//------------------------------- End of File -------------------------------​