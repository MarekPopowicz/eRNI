using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace eRNI.Models
{
    [Table("tblDocumentsTemplates")]
    public class DocumentTemplate
    {
        [Key]
        [ScaffoldColumn(false)]
        public int documentTemplateID { get; set; }

        [DisplayName("Nazwa Szablonu")]
        [Required(ErrorMessage = "Nazwa szablonu jest wymagana")]
        [StringLength(255, ErrorMessage = "Nazwa szablonu nie może być dłuższa niż 255 znaków.")]
        public string documentTemplateName { get; set; }
    }
}