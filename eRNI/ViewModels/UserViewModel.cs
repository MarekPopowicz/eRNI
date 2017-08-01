using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eRNI.ViewModels
{
    public class UserViewModel
    {
        [ScaffoldColumn(false)]
        [Key]
        public string UserId { get; set; }
        [ScaffoldColumn(false)]
        public string RoleId { get; set; }

        [DisplayName("Rola")]
        public string RoleName { get; set; }
        [DisplayName("Użytkownik")]
        public string UserName { get; set; }
        [DisplayName("Nr. telefonu")]
        public string PhoneNumber { get; set; }
        [DisplayName("E-mail")]
        public string Email { get; set; }  
     }
}