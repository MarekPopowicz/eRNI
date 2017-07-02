using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace eRNI.Models
{
    [Table("tblProjectCategories")]
    public class ProjectCategory
    {
        [Key]
        [ScaffoldColumn(false)]
        public int projectCategoryID { get; set; }

        [DisplayName("Kategoria")]
        [Required(ErrorMessage = "Nazwa kategorii projektu jest wymagana")]
        [StringLength(80, ErrorMessage = "Nazwa kategorii projektu nie może być dłuższa niż 80 znaków.")]
        public string projectCategryName { get; set; }

        public virtual ICollection<Project> projectCollection { get; set; }
    }
}