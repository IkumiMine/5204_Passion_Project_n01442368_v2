using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _5204_Passion_Project_n01442368_v2.Models.ViewModels
{
    public class ListViewModel
    {
        public PhotoDto Photo { get; set; }
        public IEnumerable<PhotoDto> AllPhotos { get; set; }
        public FilmDto Film { get; set; }
        public IEnumerable<FilmDto> AllFilms { get; set; }
    }
}