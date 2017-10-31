using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ManufacturingCompany.Models
{
    [MetadataType(typeof(Customer_Partial_Metadata))]
    public partial class Customer { }

    public class Customer_Partial_Metadata
    {
        [Required]
        [Display(Name = "Company Name")]
        public string customer_company_name { get; set; }

        [Required]
        [Display(Name = "Company Address")]
        public string customer_address1 { get; set; }
        
        [Display(Name = "")]
        public string customer_address2 { get; set; }

        [Required]
        [Display(Name = "City")]
        public string customer_city { get; set; }

        [Required]
        [Display(Name = "State")]
        public string customer_state { get; set; }

        [Required]
        [Display(Name = "Zip Code")]
        public string customer_zip { get; set; }

        [Required]
        [Display(Name = "Country")]
        public string customer_country { get; set; }
        
        [Display(Name = "Phone Number")]
        public string customer_phone { get; set; }
        
        [Display(Name = "Company Website")]
        public string customer_website { get; set; }

    }
}