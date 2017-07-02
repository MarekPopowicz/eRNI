using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

using System.Linq;
using System.Web;

namespace eRNI.Models
{
    [Table("tblRegulations")]
    public class Regulation
    {
        [Key]
        [ScaffoldColumn(false)]
        public int regulationID { get; set; }

        [DisplayName("Informacja dodatkowa")]
        public string regulationAdditionalInfo { get; set; }

        [DisplayName("Element PSP")]
        [Required(ErrorMessage = "Element PSP w SAP jest wymagany.")]
        [StringLength(10, ErrorMessage = "Długość elementu PSP nie może przekraczać 10 znaków.")]
        public string regulationSapElement { get; set; }

        [DisplayName("Koszt regulacji")]
        public Nullable<decimal> regulationCost { get; set; }

        public int deviceID { get; set; }
        [ForeignKey("deviceID")]
        public virtual Device device { get; set; }

        public int regulationCategoryID { get; set; }
        [ForeignKey("regulationCategoryID")]
        public virtual ReguationCategory reguationCategory { get; set; }

        public virtual ICollection<RegulationDocument> regulationDocumentCollection { get; set; }
    }
}