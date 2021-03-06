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
    [Table("tblInvoices")]
    public class Invoice
    {
        [Key]
        [ScaffoldColumn(false)]
        public int invoiceID { get; set; }

        [DisplayName("Informacja dodatkowa")]
        [DataType(DataType.MultilineText)]
        public string invoiceAdditionalInfo { get; set; }

        [DisplayName("Data wystawienia")]
        [Required(ErrorMessage = "Data wystawienia faktury jest wymagana.")]
        [CurrentDate(ErrorMessage = "Data wystawienia faktury nie może leżeć w przyszłości.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime invoiceIssueDate { get; set; }

        [DisplayName("Kwota netto")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Column(TypeName = "money")]
        [Required(ErrorMessage = "Kwota netto jest wymagana.")]
        public decimal invoiceNettoValue { get; set; }

        [DisplayName("VAT")]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Stawka opodatkowania jest wymagana.")]
        public decimal invoiceTax { get; set; }

        [DisplayName("Numer")]
        [Required(ErrorMessage = "Nr faktury jest wymagany.")]
        [StringLength(50, ErrorMessage = "Numer faktury nie może być dłuższy niż 50 znaków.")]
        public string invoiceNo { get; set; }

        [DisplayName("Nr rej. SAP")]
        [Required(ErrorMessage = "Nr SAP jest wymagany")]
        [StringLength(20, ErrorMessage = "Numer SAP nie może być dłuższy niż 20 znaków.")]
        public string invoiceSapRegisterNo { get; set; }

        [DisplayName("Data likwidacji")]
        [Required(ErrorMessage = "Data likwidacji w SAP jest wymagana.")]
        [CurrentDate(ErrorMessage = "Data likwidacji nie może leżeć w przyszłości.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<DateTime> invoiceSapRegistrationDate { get; set; }

        [DisplayName("Wystawca")]
        [Required(ErrorMessage = "Wystawca faktry jest wymagany")]
        [StringLength(255, ErrorMessage = "Nazwa wystawcy faktury nie może być dłuższa niż 255 znaków.")]
        public string invoiceSellerName { get; set; }

        [DisplayName("Tytuł")]
        [Required(ErrorMessage = "Tytuł faktury jest wymagany.")]
        [StringLength(255, ErrorMessage = "Tytuł faktury nie może być dłuższy niż 255 znaków.")]
        public string invoiceTitle { get; set; }

        [DisplayName("Numer SAP")]
        public int projectID { get; set; }
        [ForeignKey("projectID")]
        public virtual Project projects { get; set; }
    }
}