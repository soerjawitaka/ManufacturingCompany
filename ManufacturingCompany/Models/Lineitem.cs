using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManufacturingCompany.Models
{
    public class Lineitem
    {
        public int product_inventory_id { get; set; }
        public int lineitem_unit_quantity { get; set; }
    }
}