using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;


namespace eRNI.Models
{
    [Table("tblOwnerships")]
    public class Ownership
    {
        [Key]
        public int ownershipID { get; set; }

        public int localizationID { get; set; }
        [ForeignKey("localizationID")]
        public virtual Localization tblLocalizations { get; set; }

        public Nullable<int> ownerID { get; set; }
        [ForeignKey("ownerID")]
        public virtual Owner tblOwners { get; set; }

    }
    
}