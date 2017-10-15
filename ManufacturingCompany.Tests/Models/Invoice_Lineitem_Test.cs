﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ManufacturingCompany.Models;

namespace ManufacturingCompany.Tests.Models
{
    [TestClass]
    public class Invoice_Lineitem_Test
    {
        [TestMethod]
        public void Instantiate()
        {
            Invoice_Lineitem deliveryLI = new Invoice_Lineitem();
            deliveryLI.product_inventory_id = 2;
            Lineitem result1 = deliveryLI as Lineitem;
            //Assert.IsFalse(result == null);
            Assert.AreSame(result1, deliveryLI);
            Assert.IsTrue(result1.product_inventory_id == 2);
        }
    }
}
