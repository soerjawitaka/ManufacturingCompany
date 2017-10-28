using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ManufacturingCompany.Models
{
    [MetadataType(typeof(Timesheet_Partial_Metadata))]
    public partial class Timesheet
    {
        public decimal GetTotalHours()
        {
            return (Convert.ToDecimal(((TimeSpan)punch_out_time - punch_in_time).TotalHours));
        }
    }

    public class Timesheet_Partial_Metadata
    {
        [Required]
        [Display(Name = "Punch In")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh\\:mm}")]
        public System.TimeSpan punch_in_time { get; set; }

        [Required]
        [Display(Name = "Punch Out")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh\\:mm}")]
        public Nullable<System.TimeSpan> punch_out_time { get; set; }

        [Required]
        [Display(Name = "Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime timesheet_date { get; set; }
    }
}