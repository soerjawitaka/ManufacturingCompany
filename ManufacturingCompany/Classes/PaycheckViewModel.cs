using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ManufacturingCompany.Models;

namespace ManufacturingCompany.Classes
{
    [MetadataType(typeof(PaycheckViewModel_Metadata))]
    public class PaycheckViewModel : PaycheckModeModel
    {
        public new static PaycheckViewModel ToModel(Paycheck p)
        {
            PaycheckViewModel newPaycheck = new PaycheckViewModel();
            newPaycheck.Id = p.Id;
            newPaycheck.paycheck_date = p.paycheck_date;
            newPaycheck.SetPayroll(p.payroll_id);
            newPaycheck.payment_type = p.payment_type;
            newPaycheck.check_number = p.check_number;
            newPaycheck.direct_deposit_number = p.direct_deposit_number;
            newPaycheck.Payroll = new BusinessEntities().Payrolls.Find(p.payroll_id);

            newPaycheck.SetTypeEnum();
            return newPaycheck;
        }

        public static Paycheck ToBase(PaycheckViewModel model)
        {
            model.SetPaymentType();

            Paycheck newPaycheck = new Paycheck();
            newPaycheck.Id = model.Id;
            newPaycheck.paycheck_date = model.paycheck_date;
            newPaycheck.payroll_id = model.payroll_id;
            newPaycheck.payment_type = model.payment_type;
            newPaycheck.check_number = model.check_number;
            newPaycheck.direct_deposit_number = model.direct_deposit_number;
            newPaycheck.payment_amount = model.payment_amount;

            return newPaycheck;
        }

    }

    public class PaycheckViewModel_Metadata : Paycheck_Partial_Metadata
    {
        [Required]
        [Display(Name = "Paycheck Amount")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        public decimal payment_amount { get; set; }
    }
}