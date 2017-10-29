using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ManufacturingCompany.Models;

namespace ManufacturingCompany.Classes
{
    public class PaycheckViewModel : Paycheck
    {
        public enum PaycheckMode
        {
            Check,
            Deposit
        }
        public PaycheckMode ModeOfPaycheck
        {
            get
            {
                this.ModeOfPaycheck = (PaycheckMode)Enum.Parse(typeof(PaycheckMode), payment_type);
                return this.ModeOfPaycheck;
            }
            set
            {
                this.ModeOfPaycheck = value;
                this.payment_type = this.ModeOfPaycheck.ToString();
            }
        }
    }
}