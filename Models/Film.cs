using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _5204_Passion_Project_n01442368_v2.Models
{
    public class Film
    {
        [Key]
        public int FilmID { get; set; }
        public string BrandName { get; set; }
        public string FilmSeries { get; set; }
        public int BoxSpeed { get; set; }

        public ICollection<Photo> Photos { get; set; }
    }

    //Data Transfer Object 
    public class FilmDto
    {
        public int FilmID { get; set; }

        [DisplayName("Film Brand Name")]
        public string BrandName { get; set; }

        [DisplayName("Film Series")]
        public string FilmSeries { get; set; }
     
        [DisplayName("Box Speed")]
        public int BoxSpeed { get; set; }
    }
}
