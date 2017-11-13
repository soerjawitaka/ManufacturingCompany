using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ManufacturingCompany.Models;

namespace ManufacturingCompany.Classes
{
    public class ManageEmployeeModel
    {
        public ApplicationUser Employee { get; set; }

        [Required]
        public string UserID { get; set; }

        [Required]
        [Display(Name = "Role Name")]
        public string RoleToBeAssigned { get; set; }
    }
}