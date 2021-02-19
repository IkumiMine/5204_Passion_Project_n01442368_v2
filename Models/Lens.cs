using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _5204_Passion_Project_n01442368_v2.Models
{
    public class Lens
    {
        [Key]
        public int LensID { get; set; }
        public string BrandName { get; set; }
        public string LensInfo { get; set; }

        public ICollection<Photo> Photos { get; set; }
    }

    //Data Transfer Object 
    public class LensDto
    {
        public int LensID { get; set; }

        [DisplayName("Lens Brand Name")]
        public string BrandName { get; set; }

        [DisplayName("Lens Info")]
        public string LensInfo { get; set; }

    }
}