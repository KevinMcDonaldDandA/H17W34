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
using System.Linq;
using System.Web.Mvc;

namespace MoviesProject.ViewModels
{
    public class CreateMovieVM
    {
        public Movie Movie { get; set; }
        public List<Director> Directors { get; set; }

        public IEnumerable<SelectListItem> AllActors { get; set; }

        private List<int> _selectedActors;
        public List<int> SelectedActors
        {
            get
            {
                if (_selectedActors == null)
                {
                    _selectedActors = Movie.Actors.Select(m => m.ID).ToList();
                }
                return _selectedActors;
            }
            set { _selectedActors = value; }
        }
    }
}
//------------------------------- End of File -------------------------------​