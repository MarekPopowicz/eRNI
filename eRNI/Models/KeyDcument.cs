using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using eRNI.Models.CustomDataAnnotations;

namespace eRNI.Models
{
    [Table("tblKeyDocuments")]
    public class KeyDcument
    {
        [Key]
        [ScaffoldColumn(false)]
        public int keyDocumentID { get; set; }

        [DisplayName("Data dokumentu")]
        [Required(ErrorMessage = "Data wydania dokument jest wymagana.")]
        [CurrentDate(ErrorMessage = "Data dokumentu nie może leżeć w przyszłości.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime keyDocumentDate { get; set; }

        [DisplayName("Znak")]
        [Required(ErrorMessage = "Znak dokumentu jest wymagany")]
        [StringLength(100, ErrorMessage = "Znak dokumentu nie może być dłuższy niż 100 znaków.")]
        public string keyDocumentSign { get; set; }

        [DisplayName("Nazwa")]
        [Required(ErrorMessage = "Nazwa dokumentu jest wymagana")]
        public int KeyDocumentCategoryID { get; set; }
        [ForeignKey("KeyDocumentCategoryID")]
        public virtual KeyDocumentCategory keyDocumentCategory { get; set; }

        [DisplayName("Otrzymano")]
        [Required(ErrorMessage = "Data otrzymania jest wymagana")]
        [CurrentDate(ErrorMessage = "Data otrzymania nie może leżeć w przyszłości.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime keyDocumentObtainment { get; set; }

        [DisplayName("Numer SAP")]
        public int projectID { get; set; }
        [ForeignKey("projectID")]
        public virtual Project project { get; set; }
    }
}