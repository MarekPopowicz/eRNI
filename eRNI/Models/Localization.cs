using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace eRNI.Models
{
    [Table("tblLocalizations")]
    public class Localization
    {
        [Key]
        [ScaffoldColumn(false)]
        public int localizationID { get; set; }

        [DisplayName("Nr KW")]
        [Required(ErrorMessage = "Nr Księgi Wieczystej jest wymagany")]
        [StringLength(50, ErrorMessage = "Nr Księgi Wieczystej nie może być dłuższy niż 50 znaków.")]
        public string localizationLandRegister { get; set; }

        [DisplayName("AM")]
        [Required(ErrorMessage = "Arkusz mapy jest wymagany")]
        [StringLength(10, ErrorMessage = "Numer arkusza mapy nie może być dłuższy niż 10 znaków.")]
        public string localizationMapNo { get; set; }

        [DisplayName("Nr dz.")]
        [Required(ErrorMessage = "Nr działki jest wymagany")]
        [StringLength(9, ErrorMessage = "Numer działki nie może być dłuższy niż 9 znaków.")]
        public string localizationPlotNo { get; set; }

        [DisplayName("Obręb")]
        [Required(ErrorMessage = "Nazwa obrębu jest wymagana")]
        [StringLength(80, ErrorMessage = "Nazwa obrębu nie może być dłuższa niż 80 znaków.")]
        public string localizationPrecinct { get; set; }

        [DisplayName("Status")]
        [Required(ErrorMessage = "Status regulcji jest wymagany")]
        public Status localizationRegulationStatus { get; set; }

        [DisplayName("Rejon ulic")]
        [StringLength(255, ErrorMessage = "Oznaczenie ulic nie może przekraczać 255 znaków.")]
        public string localizationStreets { get; set; }

        [DisplayName("Region")]
        [Required(ErrorMessage = "Region jest wymagany")]
        [StringLength(100, ErrorMessage = "Nazwa regionu nie może przekraczać 100 znaków.")]
        public string localizationRegion { get; set; }

        [DisplayName("Miejscowość")]
        [Required(ErrorMessage = "Miejscowość jest wymagana")]
        public int placeID { get; set; }
        [ForeignKey("placeID")]
        public virtual Place place { get; set; }

        [DisplayName("Nr Sap")]
        public int projectID { get; set; }
        [ForeignKey("projectID")]
        public virtual Project project { get; set; }

        public virtual ICollection<Device> deviceColection { get; set; }
        public virtual ICollection<Ownership> ownershipcCollection { get; set; }
    }
}