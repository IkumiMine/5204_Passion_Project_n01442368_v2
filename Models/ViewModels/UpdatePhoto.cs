using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _5204_Passion_Project_n01442368_v2.Models.ViewModels
{
    public class UpdatePhoto
    {
        public PhotoDto Photo { get; set; }
        public IEnumerable<FilmDto> AllFilms { get; set; }
        public IEnumerable<LensDto> AllLenses { get; set; }
    }
}