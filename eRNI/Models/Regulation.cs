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
        [DataType(DataType.MultilineText)]
        public string regulationAdditionalInfo { get; set; }

        [DisplayName("Element PSP")]
        [Required(ErrorMessage = "Element PSP w SAP jest wymagany.")]
        [StringLength(10, ErrorMessage = "Długość elementu PSP nie może przekraczać 10 znaków.")]
        public string regulationSapElement { get; set; }

        [DisplayName("Koszt regulacji")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Column(TypeName = "money")]
        public Nullable<decimal> regulationCost { get; set; }

        [ScaffoldColumn(false)]
        public int deviceID { get; set; }
        [ForeignKey("deviceID")]
        public virtual Device device { get; set; }

        [DisplayName("Regulacja")]
        public int regulationCategoryID { get; set; }
        [ForeignKey("regulationCategoryID")]
        public virtual ReguationCategory reguationCategory { get; set; }

        public virtual ICollection<RegulationDocument> regulationDocumentCollection { get; set; }
    }
}