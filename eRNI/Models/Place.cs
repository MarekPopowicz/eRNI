using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace eRNI.Models
{
    [Table("tblPlaces")]
    public class Place
    {
        [Key]
        [ScaffoldColumn(false)]
        public int placeID { get; set; }

        [DisplayName("Miejscowość")]
        [Required(ErrorMessage = "Nazwa miejscowości/gminy/powiatu jest wymagana")]
        [StringLength(255, ErrorMessage = "Nazwa miejscowości/gminy/powiatu nie może być dłuższa niż 255 znaków.")]
        public string placeName { get; set; }

        public virtual ICollection<Localization> LocalizationCollection { get; set; }
    }
}
    
