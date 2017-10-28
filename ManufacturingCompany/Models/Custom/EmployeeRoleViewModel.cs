using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ManufacturingCompany.Models
{
    public class EmployeeRoleViewModel
    {
        [Key]
        public string UserID { get; set; }

        [Required]
        public string Role { get; set; }
    }
}