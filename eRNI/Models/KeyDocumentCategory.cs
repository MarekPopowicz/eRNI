using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace eRNI.Models
{
    [Table("tblKeyDocumentCategories")]
    public class KeyDocumentCategory
    {
        [Key]
        [ScaffoldColumn(false)]
        public int KeyDocumentCategoryID { get; set; }

        [DisplayName("Nazwa dokumentu")]
        [Required(ErrorMessage = "Nazwa dokumentu jest wymagana")]
        [StringLength(150, ErrorMessage = "Nazwa dokumentu nie może być dłuższa niż 150 znaków.")]
        public string keyDocumentName { get; set; }

        public virtual ICollection<KeyDcument> KeyDocumentCollection { get; set; }
    }
}