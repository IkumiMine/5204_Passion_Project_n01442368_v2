using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _5204_Passion_Project_n01442368_v2.Models
{
    public class Photo
    {
        [Key]
        public int PhotoID { get; set; }
        public int ISO { get; set; }
        public decimal Aperture { get; set; }
        public string ShutterSpeed { get; set; }
        public int FocalLength_mm { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateTaken { get; set; }

        public bool IsPhoto { get; set; }
        //If there is photo uploaded, the photo file extension will bee recorded
        public string PhotoExtension { get; set; }

        [ForeignKey("Film")]
        public int FilmID { get; set; }
        public virtual Film Film { get; set; }

        [ForeignKey("Lens")]
        public int LensID { get; set; }
        public virtual Lens Lens { get; set; }
    }

    //Data Transfer Object
    public class PhotoDto
    {
        public int PhotoID { get; set; }
        public int ISO { get; set; }
        public decimal Aperture { get; set; }

        [DisplayName("Shutter Speed")]
        public string ShutterSpeed { get; set; }

        [DisplayName("Focal Length(mm)")]
        public int FocalLength_mm { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [Column(TypeName = "DateTime2")]
        [DisplayName("Date Taken")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime DateTaken { get; set; }

        public bool IsPhoto { get; set; }
        public string PhotoExtension { get; set; }

        [DisplayName("Film")]
        public int FilmID { get; set; }
        [DisplayName("Lens")]
        public int LensID { get; set; }
    }
}
