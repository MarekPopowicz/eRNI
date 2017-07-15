using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using eRNI.Models.CustomDataAnnotations;

namespace eRNI.Models
{

    [Table("tblProjects")]
    public class Project
    {
        [Key]
        [DisplayName("Teczka")]
        public int projectID { get; set; }

        [DisplayName("Uwagi")]
        [DataType(DataType.MultilineText)]
        public string projectAdditionalInfo { get; set; }

        [DisplayName("Wpływ")]
        [Required(ErrorMessage = "Data wpływu jest wymagana")]
        [CurrentDate(ErrorMessage = "Data wpływu nie może leżeć w przyszłości.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime projectInflow { get; set; }

        [DisplayName("Status")]
        [Required(ErrorMessage = "Status projektu jest wymagany")]
        public Status? projectStatus { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Czynność")]
        public Nullable<DateTime> projectLastActivity { get; set; }

        [DisplayName("Prowadzący")]
        [Required(ErrorMessage = "Nazwa prowadząego regulację jest wymagana")]
        [StringLength(80, ErrorMessage = "Nazwa prowadzącego nie może być dłuższa niż 80 znaków.")]
        public string projectLeader { get; set; }

        [DisplayName("Inżynier")]
        [Required(ErrorMessage = "Nazwa Inżyniera projektu jest wymagana")]
        [StringLength(80, ErrorMessage = "Nazwa Inżyniera projektu nie może być dłuższa niż 80 znaków.")]
        public string projectManager { get; set; }

        [DisplayName("Nr SAP")]
        [Required(ErrorMessage = "Numer SAP jest wymagany")]
        [StringLength(20, ErrorMessage = "Nr SAP nie może być dłuższy niż 20 znaków.")]
        public string projectSapNo { get; set; }

        [DisplayName("Zadanie")]
        [Required(ErrorMessage = "Nazwa zadania jest wymagana")]
        [StringLength(400, ErrorMessage = "Nazwa zadania nie może być dłuższa niż 400 znaków.")]
        [DataType(DataType.MultilineText)]
        public string projectTask { get; set; }

        [DisplayName("Priorytet")]
        public bool projectPriority { get; set; }

        [DisplayName("Kategoria")]
        [Required(ErrorMessage = "Nazwa kategorii projektu jest wymagana")]
        public int projectCategoryID { get; set; }
        [ForeignKey("projectCategoryID")]
        public virtual ProjectCategory projectCategory { get; set; }

        public virtual ICollection<Activity> actionCollection { get; set; }
        public virtual ICollection<Invoice> invoiceCollection { get; set; }
        public virtual ICollection<Localization> localizationCollection { get; set; }
        public virtual ICollection<PropertyDocument> propertyDocumentCollection { get; set; }
    }
}