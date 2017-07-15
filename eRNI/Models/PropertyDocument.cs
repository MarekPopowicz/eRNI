using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace eRNI.Models
{
    [Table("tblPropertyDocuments")]
    public class PropertyDocument
    {
        [Key]
        [ScaffoldColumn(false)]
        public int propertyDocumentID { get; set; }

        [DataType(DataType.MultilineText)]
        [DisplayName("Informacja dodatkowa")]
        public string propertyDocumentAdditionalInfo { get; set; }

        [DisplayName("Numer w SAP")]
        [Required(ErrorMessage = "Numer dokumentu w SAP jest wymagany.")]
        [StringLength(15, ErrorMessage = "Numer SAP nie przekraczać 15 znaków.")]
        public string propertyDocumentSapRegisterNo { get; set; }

        [DisplayName("Data rejestracji w SAP")]
        [Required(ErrorMessage = "Data rejestracjji w SAP jest wymagana.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime propertyDocumentSapRegistrationDate { get; set; }

        [DisplayName("Typ dokumentu")]
        [Required(ErrorMessage = "Typ dokumentu jest wymagany.")]
        [StringLength(80, ErrorMessage = "Nazwa typu dokumentu nie może być dłuższa niż 80 znaków.")]
        public string propertyDocumentType { get; set; }

        [DisplayName("Numer SAP")]
        public Nullable<int> projectID { get; set; }
        [ForeignKey("projectID")]
        public virtual Project project { get; set; }
    }
}