using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ManufacturingCompany.Models;

namespace ManufacturingCompany.Classes
{
    [MetadataType(typeof(ProductViewModel_Metadata))]
    public class ProductViewModel : Product
    {
        public static ProductViewModel ToModel(Product p)
        {
            var pModel = new ProductViewModel();
            pModel.Id = p.Id;
            pModel.product_name = p.product_name;
            pModel.product_short_description = p.product_short_description;
            pModel.product_long_description = p.product_long_description;
            pModel.product_note = p.product_note;
            pModel.product_unit_cost = p.product_unit_cost;
            pModel.product_unit_price = p.product_unit_price;
            pModel.product_category_id = p.product_category_id;
            return pModel;
        }

        public static Product ToBase(ProductViewModel p)
        {
            var pModel = new Product();
            pModel.Id = p.Id;
            pModel.product_name = p.product_name;
            pModel.product_short_description = p.product_short_description;
            pModel.product_long_description = p.product_long_description;
            pModel.product_note = p.product_note;
            pModel.product_unit_cost = p.product_unit_cost;
            pModel.product_unit_price = p.product_unit_price;
            pModel.product_category_id = p.product_category_id;
            return pModel;
        }

    }

    public class ProductViewModel_Metadata
    {
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal product_unit_cost { get; set; }
        
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:c}")]
        public decimal product_unit_price { get; set; }
    }
}