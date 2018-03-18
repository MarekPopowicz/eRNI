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
    [Table("tblRegulationDocumentCharges")]
    public class RegulationDocumentCharge
    {
        [Key]
        [ScaffoldColumn(false)]
        public int regulationDocumentCostID { get; set; }

        [DisplayName("Dokument")]
        [StringLength(100, ErrorMessage = "Nazwa dokumentu nie może być dłuższa niż 50 znaków.")]
        public string regulationDocumentName { get; set; }

        [DisplayName("Data płatności")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> regulationDocumentChargeDate { get; set; }

        [DisplayName("Tytuł płatności")]
        [Required(ErrorMessage = "Tytuł płatności jest wymagany.")]
        [StringLength(100, ErrorMessage = "Tytuł nie może być dłuższy niż 255 znaków.")]
        public string regulationDocumentChargeTitle { get; set; }

        [DisplayName("Odbiorca")]
        [Required(ErrorMessage = "Odbiorca płaności jest wymagany")]
        [StringLength(255, ErrorMessage = "Nazwa odbiorcy nie może być dłuższa niż 255 znaków.")]
        public string regulationDocumentSellerName { get; set; }

        [DisplayName("Kwota netto")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Required(ErrorMessage = "Kwota jest wymagana.")]
        [Column(TypeName = "money")]
        public decimal regulationDocumentFee { get; set; }

        [DisplayName("Podatek")]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Stawka podatkowa jest wymagana.")]
        public decimal regulationDocumentTax { get; set; }

        [DisplayName("Informacja dodatkowa")]
        [DataType(DataType.MultilineText)]
        public string regulationDocumentCostAdditionalInfo { get; set; }

        public int regulationDocumentID { get; set; }
        [ForeignKey("regulationDocumentID")]
        public virtual RegulationDocument regulationDocuments { get; set; }

      
    }
}