using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ManufacturingCompany.Models
{
    [MetadataType(typeof(EmployeeRoleViewModel_Partial_Metadata))]
    public partial class EmployeeRoleViewModel { }

    public class EmployeeRoleViewModel_Partial_Metadata
    {
        [Key]
        public string UserID { get; set; }

        [Required]
        public string Role { get; set; }
    }
}