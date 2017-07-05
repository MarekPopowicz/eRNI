using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace eRNI.Models
{
    [Table("tblDevices")]
    public class Device
    {
        [Key]
        [ScaffoldColumn(false)]
        public int deviceID { get; set; }

       
        [DisplayName("Długość")]
        [Required(ErrorMessage = "Długość jest wymagana")]
        public decimal deviceLenght { get; set; }

       
        [DisplayName("Szerokość")]
        [Required(ErrorMessage = "Szerokość jest wymagana")]
        public decimal deviceWidth { get; set; }

        [DisplayName("Napięcie")]
        [Required(ErrorMessage = "Napięcie jest wymagane")]
         public Voltage? deviceVoltage { get; set; }

        [DisplayName("Urządzenie")]
        public int deviceCategoryID { get; set; }
        [ForeignKey("deviceCategoryID")]
        public virtual DeviceCategory deviceCategory { get; set; }

        [DisplayName("Nr działki")]
        public int localizationID { get; set; }
        [ForeignKey("localizationID")]
        public virtual Localization localization { get; set; }

        public virtual ICollection<Regulation> regulationCollection { get; set; }
    }

    public enum Voltage
    {
        Niskie, Średnie, Wysokie
    }
}