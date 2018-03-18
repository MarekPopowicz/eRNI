using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace eRNI.Models
{
    [Table("tblOwners")]
    public class Owner
    {
        [Key]
        [ScaffoldColumn(false)]
        public int ownerID { get; set; }

        [DisplayName("Informacje dodatkowe")]
        [DataType(DataType.MultilineText)]
        public string ownerAdditionalInfo { get; set; }

        [DisplayName("Nazwa")]
        [Required(ErrorMessage = "Nazwa właściciela jest wymagana")]
        [StringLength(255, ErrorMessage = "Nazwa Właściciela nie może być dłuższa niż 255 znaków.")]
        public string ownerName { get; set; }

        [DisplayName("Nr bud.")]
        [StringLength(10, ErrorMessage = "Numer budynku nie może być dłuższy niż 10 znaków.")]
        [Required(ErrorMessage = "Nr budynku jest wymagany")]
        public string ownerHouseNo { get; set; }

        [DisplayName("Nr lok.")]
        [StringLength(10, ErrorMessage = "Numer mieszkania nie może być dłuższy niż 10 znaków.")]
        public string ownerAptNo { get; set; }

        [DisplayName("Tel. stac.")]
        [StringLength(20, ErrorMessage = "Numer telefonu nie może być dłuższy niż 20 znaków.")]
        [RegularExpression(@"^\(0[1-9][1-9]\) \d{3}-\d{2}-\d{2}$", ErrorMessage = "Nr tel. stacjonarnego jest niepoprawny.")]
        public string ownerPhone { get; set; }

        [DisplayName("Tel. kom.")]
        [StringLength(20, ErrorMessage = "Numer telefonu komórkowego nie może być dłuższy niż 20 znaków.")]
        [RegularExpression(@"^\d{3}-\d{3}-\d{3}$", ErrorMessage = "Nr tel. komórkowego jest niepoprawny.")]
        public string ownerCellPhone { get; set; }

        [DisplayName("Poczta")]
        [Required(ErrorMessage = "Poczta jest wymagana")]
        [StringLength(80, ErrorMessage = "Nazwa poczty nie może być dłuższa niż 80 znaków.")]
        public string ownerPostOffice { get; set; }

        [DisplayName("Kod")]
        [Required(ErrorMessage = "Kod pocztowy jest wymagany")]
        [StringLength(6, ErrorMessage = "Kod pocztowy nie może być dłuższy niż 6 znaków.")]
        [RegularExpression(@"^[0-9]{2}\-[0-9]{3}$", ErrorMessage = "Wprowadzony kod jest niepoprawny.")]
        public string ownerZipCode { get; set; }

        [DisplayName("e-mail")]
        [StringLength(100, ErrorMessage = "E-mail nie może być dłuższy niż 100 znaków.")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$", ErrorMessage = "Adres e-mail jest niepoprawny.")]
        public string ownerEmail { get; set; }

        [DisplayName("Miejscowość")]
        [StringLength(80, ErrorMessage = "Nazwa miejscowości nie może być dłuższa niż 80 znaków.")]
        [Required(ErrorMessage = "Nazwa miejscowości jest wymagana")]
        public string ownerCity { get; set; }

        [DisplayName("Ulica")]
        public Nullable<int> streetID { get; set; }
        [ForeignKey("streetID")]
        public virtual Street street { get; set; }

        public virtual ICollection<Ownership> ownershipCollection { get; set; }
    }
}