using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace eRNI.Models
{
    [Table("tblStreets")]
    public class Street
    {
        [Key]
        [ScaffoldColumn(false)]
        public int streetID { get; set; }

        [DisplayName("Nazwa ulicy")]
        [Required(ErrorMessage = "Nazwa ulicy jest wymagana.")]
        [StringLength(80, ErrorMessage = "Nazwa ulicy nie może być dłuższa niż 80 znaków.")]
        public string streetName { get; set; }

        public virtual ICollection<Owner> ownerCollection { get; set; }
    }
}