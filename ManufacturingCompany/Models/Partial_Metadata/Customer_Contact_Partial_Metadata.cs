using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ManufacturingCompany.Models
{
    [MetadataType(typeof(Customer_Contact_Partial_Metadata))]
    public partial class Customer_Contact { }

    public class Customer_Contact_Partial_Metadata
    {
        [Required]
        [Display(Name = "First Name")]
        public string first_name { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string last_name { get; set; }
        
        [Display(Name = "Office Phone")]
        public string work_phone { get; set; }
        
        [Display(Name = "Mobile Phone")]
        public string mobile_phone { get; set; }
        
        [Display(Name = "Fax")]
        public string fax { get; set; }
        
        [Display(Name = "Email")]
        public string contact_email { get; set; }
    }
}