using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eRNI.Models
{
    public class RoleViewModel
    {
        [ScaffoldColumn(false)]
        public string Id { get; set; }
        [DisplayName("Rola")]
        public string Name { get; set; }

        public RoleViewModel() { }

        public RoleViewModel(ApplicationRole role)
        {
            Id = role.Id;
            Name = role.Name;
        }
    }
}