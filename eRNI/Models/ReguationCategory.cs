using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace eRNI.Models
{
    [Table("tblRegulationCategories")]
    public class ReguationCategory
    {
        [Key]
        [ScaffoldColumn(false)]
        public int regulationCategoryID { get; set; }

        [DisplayName("Regulacja")]
        [Required(ErrorMessage = "Nazwa kategorii regulacji jest wymagana.")]
        [StringLength(80, ErrorMessage = "Nazwa kategorii regulacji nie może być dłuższa niż 80 znaków.")]
        public string regulationCategoryName { get; set; }

        public virtual ICollection<Regulation> regulationCollection { get; set; }
    }
}