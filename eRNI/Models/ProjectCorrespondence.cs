using eRNI.Models.CustomDataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace eRNI.Models
{
    [Table("tblProjectCorrespondence")]
    public class ProjectCorrespondence
    {
        [Key]
        [ScaffoldColumn(false)]
        public int projectCorrespondenceID { get; set; }

        [DisplayName("Kierunek")]
        [Required(ErrorMessage = "Kierunek dokumentu jest wymagany")]
        [StringLength(35, ErrorMessage = "Określenie kierunku nie może być dłuższe niż 35 znaków.")]
        public string projectCorrespondenceDirection { get; set; }

        [DisplayName("Nadawca/Odbiorca")]
        [Required(ErrorMessage = "Nadawca/Odbiorca dokumentu jest wymagany")]
        [StringLength(255, ErrorMessage = "Nadawca/Odbiorca dokumentu nie może być dłuższy niż 255 znaków.")]
        public string projectCorrespondenceSender { get; set; }

        [DisplayName("Szablon")]
        [StringLength(255, ErrorMessage = "Nazwa szablonu dokumentu nie może być dłuższa niż 255 znaków.")]
        public string projectCorrespondenceTemplate { get; set; }

        [DisplayName("Data dokumentu")]
        [Required(ErrorMessage = "Data wydania dokument jest wymagana.")]
        [CurrentDate(ErrorMessage = "Data dokumentu nie może leżeć w przyszłości.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime projectCorrespondenceDate { get; set; }

        [DisplayName("Znak")]
        [Required(ErrorMessage = "Znak dokumentu jest wymagany")]
        [StringLength(100, ErrorMessage = "Znak dokumentu nie może być dłuższy niż 100 znaków.")]
        public string projectCorrespondenceSign { get; set; }

        [DisplayName("Dotyczy")]
        [Required(ErrorMessage = "Temat dokumentu jest wymagany")]
        [DataType(DataType.MultilineText)]
        [StringLength(300, ErrorMessage = "Temat dokumentu nie może być dłuższy niż 300 znaków.")]
        public string projectCorrespondenceSubject { get; set; }
        

        [DisplayName("Wysłano/Otrzymano")]
        [Required(ErrorMessage = "Data otrzymania jest wymagana")]
        [CurrentDate(ErrorMessage = "Data otrzymania nie może leżeć w przyszłości.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime projectCorrespondenceObtainment { get; set; }

        [DisplayName("Numer SAP")]
        public int projectID { get; set; }
        [ForeignKey("projectID")]
        public virtual Project project { get; set; }
    }


}
