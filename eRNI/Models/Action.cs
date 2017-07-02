using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace eRNI.Models
{
    [Table("tblActions")]
    public class Action
    {
        [Key]
        [ScaffoldColumn(false)]
        public int actionID { get; set; }

        [DisplayName("Data czynności")]
        [Required(ErrorMessage = "Data czynności jest wymagana.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime actionDate { get; set; }

        [DisplayName("Opis czynności")]
        [Required(ErrorMessage = "Opis czynności jest wymagany")]
        [StringLength(500, ErrorMessage = "Opis czynności nie może być dłuższy niż 500 znaków.")]
        public string actionDescription { get; set; }

        public int projectID { get; set; }
        [ForeignKey("projectID")]
        public virtual Project project { get; set; }
    }
}