using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eRNI.ViewModels
{
    public class UserViewModel
    {
        [ScaffoldColumn(false)]
        [Key]
        public string UserId { get; set; }
        [ScaffoldColumn(false)]
        public List<SelectListItem> rolesList  { get; set; }

        [Required]
        [DisplayName("Rola")]
        public string RoleId { get; set; }

        [Required]
        [DisplayName("Użytkownik")]
        public string UserName { get; set; }

        [Required]
        [DisplayName("Nr. telefonu")]
        [StringLength(15, ErrorMessage = "{0} musi zawierać co najmniej następującą liczbę znaków: {2}.", MinimumLength = 15)]
        [RegularExpression(@"^\(0[1-9][1-9]\) \d{3}-\d{2}-\d{2}$", ErrorMessage = "Nr tel. jest niezgodny ze wzorcem (099) 999-99-99.")]
        public string PhoneNumber { get; set; }

        [DisplayName("e-mail")]
        [Required]
        [EmailAddress]
        [StringLength(100, ErrorMessage = "E-mail nie może być dłuższy niż 100 znaków.")]
        [RegularExpression(@"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,4})$", ErrorMessage = "Adres e-mail jest niepoprawny.")]
        public string Email { get; set; }  
     }
}