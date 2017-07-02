using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace eRNI.Models
{
    [Table("tblRegulationDocuments")]
    public class RegulationDocument
    {
        [Key]
        [ScaffoldColumn(false)]
        public int regulationDocumentID { get; set; }

        [DisplayName("Data dokumentu")]
        [Required(ErrorMessage = "Data wydania dokumentu jest wymagana.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime regulationDocumentDate { get; set; }

        [DisplayName("Znak dokumentu")]
        [Required(ErrorMessage = "Znak dokumentu jest wymagany.")]
        [StringLength(100, ErrorMessage = "Znak dokumentu nie może być dłuższy niż 100 znaków.")]
        public string regulationDocumentSignature { get; set; }

        [DisplayName("Wystawca dokumentu")]
        [Required(ErrorMessage = "Nazwa Wystawcy dokumentu jest wymagana.")]
        [StringLength(100, ErrorMessage = "Nazwa Wystawcy dokumentu nie może być dłuższa niż 100 znaków.")]
        public string regulationDocumentSource { get; set; }

        [DisplayName("Typ dokumentu")]
        [Required(ErrorMessage = "Typ dokumentu jest wymagany.")]
        [StringLength(50, ErrorMessage = "Nazwa typu dokumentu nie może być dłuższa niż 50 znaków.")]
        public string regulationDocumentType { get; set; }

        [DisplayName("Link")]
        [StringLength(400, ErrorMessage = "Ścieżka do miejsca przechowywania dokumentu nie może być dłuższa niż 400 znaków.")]
        public string regulationDocumentLink { get; set; }

        public int regulationID { get; set; }
        [ForeignKey("regulationID")]
        public virtual Regulation regulation { get; set; }
    }
}