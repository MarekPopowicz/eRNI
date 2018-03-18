using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace eRNI.Models
{
    [Table("tblDeviceCategories")]
    public class DeviceCategory
    {
        [Key]
        [ScaffoldColumn(false)]
        public int deviceCategoryID { get; set; }

        [DisplayName("Urządzenie")]
        [Required(ErrorMessage = "Nazwa kategorii jest wymagana.")]
        [StringLength(255, ErrorMessage = "Nazwa kategorii urządzenia nie może być dłuższy niż 255 znaków.")]
        public string deviceCategoryName { get; set; }

        public virtual ICollection<Device> deviceCollection { get; set; }
    }
}