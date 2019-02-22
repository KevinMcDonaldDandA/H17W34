/*
*  File	    MovieDirectors.cs
*  Author	Kevin McDonald, Dundee & Angus College, kevin.mcdonald@dundeeandangus.ac.uk
*  Date     22nd February 2019
*  Purpose	The ViewModel class for our Movie Entity
*  Notes    This ViewModel acts as a Data Transfer Object to show both Movie and Directors information in a single View
*
*       Copyright © Kevin McDonald 2019. All rights reserved.
*/

using MoviesProject.Models;
using System.Collections.Generic;

namespace MoviesProject.ViewModels
{
    public class MovieDirectors
    {
        public Movie Movie { get; set; }
        public List<Director> Directors { get; set; }
    }
}
//------------------------------- End of File -------------------------------​