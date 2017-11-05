using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ManufacturingCompany.Models;

namespace ManufacturingCompany.Classes
{
    public class PaycheckModeModel : Paycheck
    {
        public enum PaycheckMode
        {
            Check,
            Deposit
        }

        [Required]
        [Display(Name = "Payment Type")]
        public PaycheckMode ModeOfPaycheck { get; set; }

        public void SetPaymentType()
        {
            this.payment_type = this.ModeOfPaycheck.ToString();
        }

        public void SetTypeEnum()
        {
            this.ModeOfPaycheck = (PaycheckMode)Enum.Parse(typeof(PaycheckMode), this.payment_type);
        }

        public static PaycheckModeModel ToModel(Paycheck p)
        {
            PaycheckModeModel newPaycheck = new PaycheckModeModel();
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

        public static Paycheck ToBase(PaycheckModeModel model)
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
        
        public void SetPayroll(int payrollID)
        {
            var payroll = new BusinessEntities().Payrolls.Find(payrollID);
            this.Payroll = payroll;
            this.payroll_id = payrollID;
            this.payment_amount = payroll.grand_total;
        }
    }
}