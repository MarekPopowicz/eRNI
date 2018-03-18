using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace eRNI.Models
{
    [Table("tblAddressBook")]
    public class AddressBook
    {
        [Key]
        [ScaffoldColumn(false)]
        public int addresseeID { get; set; }

        [DisplayName("Nazwa")]
        [Required(ErrorMessage = "Nazwa adresata jest wymagana")]
        [StringLength(255, ErrorMessage = "Nazwa adresata nie może być dłuższa niż 255 znaków.")]
        public string addresseeName { get; set; }

        [DisplayName("Nr bud.")]
        [StringLength(10, ErrorMessage = "Numer budynku nie może być dłuższy niż 10 znaków.")]
        [Required(ErrorMessage = "Nr budynku jest wymagany")]
        public string addresseeHouseNo { get; set; }

        [DisplayName("Nr lok.")]
        [StringLength(10, ErrorMessage = "Numer mieszkania nie może być dłuższy niż 10 znaków.")]
        public string addresseeAptNo { get; set; }

        [DisplayName("Tel. stac.")]
        [StringLength(20, ErrorMessage = "Numer telefonu nie może być dłuższy niż 20 znaków.")]
        [RegularExpression(@"^\(0[1-9][1-9]\) \d{3}-\d{2}-\d{2}$", ErrorMessage = "Nr tel. stacjonarnego jest niepoprawny.")]
        public string addresseePhone { get; set; }

        [DisplayName("Tel. kom.")]
        [StringLength(20, ErrorMessage = "Numer telefonu komórkowego nie może być dłuższy niż 20 znaków.")]
        [RegularExpression(@"^\d{3}-\d{3}-\d{3}$", ErrorMessage = "Nr tel. komórkowego jest niepoprawny.")]
        public string addresseeCellPhone { get; set; }

        [DisplayName("Poczta")]
        [Required(ErrorMessage = "Poczta jest wymagana")]
        [StringLength(80, ErrorMessage = "Nazwa poczty nie może być dłuższa niż 80 znaków.")]
        public string addresseePostOffice { get; set; }

        [DisplayName("Kod")]
        [Required(ErrorMessage = "Kod pocztowy jest wymagany")]
        [StringLength(6, ErrorMessage = "Kod pocztowy nie może być dłuższy niż 6 znaków.")]
        [RegularExpression(@"^[0-9]{2}\-[0-9]{3}$", ErrorMessage = "Wprowadzony kod jest niepoprawny.")]
        public string addresseeZipCode { get; set; }

        [DisplayName("e-mail")]
        [StringLength(100, ErrorMessage = "E-mail nie może być dłuższy niż 100 znaków.")]
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$", ErrorMessage = "Adres e-mail jest niepoprawny.")]
        public string addresseeEmail { get; set; }

        [DisplayName("Miejscowość")]
        [StringLength(80, ErrorMessage = "Nazwa miejscowości nie może być dłuższa niż 80 znaków.")]
        [Required(ErrorMessage = "Nazwa miejscowości jest wymagana")]
        public string addresseeCity { get; set; }

        [DisplayName("Ulica")]
        public Nullable<int> streetID { get; set; }
        [ForeignKey("streetID")]
        public virtual Street street { get; set; }

    }
}
