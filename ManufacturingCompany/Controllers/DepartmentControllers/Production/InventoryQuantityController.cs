using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ManufacturingCompany.Models;

namespace ManufacturingCompany.Controllers.DepartmentControllers.Production
{
    public class InventoryQuantityController : Controller
    {
        BusinessEntities db = new BusinessEntities();

        public InventoryQuantityController()
        {
            ViewBag.ViewHeaderPartial = "_Production";
            ViewBag.ItemTitle = "Inventory Quantity";
        }
        
        // GET: InventoryQuantity
        public ActionResult Index()
        {
            List<string> searchBy = new List<string>();
            searchBy.Add("Name");
            searchBy.Add("Description");
            ViewBag.ErrorString = "";
            ViewBag.SearchBy = new SelectList(searchBy);
            return View(db.Product_Inventory.ToList());
        }

        // POST: InventoryQuantity
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string SearchBy, string inputForUserSearch)
        {
            List<string> searchBy = new List<string>();
            searchBy.Add("Name");
            searchBy.Add("Description");
            ViewBag.ErrorString = "";
            ViewBag.SearchBy = new SelectList(searchBy);

            if (inputForUserSearch != "")
            {
                List<Product_Inventory> productInventories;
                switch (SearchBy)
                {
                    case "Name":
                        productInventories = db.Product_Inventory.Where(u => u.Product.product_name.Contains(inputForUserSearch)).ToList();
                        break;
                    case "Description":
                        productInventories = db.Product_Inventory.Where(u => u.Product.product_short_description.Contains(inputForUserSearch) || u.Product.product_long_description.Contains(inputForUserSearch)).ToList();
                        break;
                    default:
                        productInventories = new List<Product_Inventory>();
                        break;
                }
                if (productInventories.Count > 0)
                {
                    ViewBag.ErrorString = "";
                    return View(productInventories);
                }
                else
                {
                    ViewBag.ErrorString = "Products Not Found";
                    return View();
                }
            }
            else
            {
                ViewBag.ErrorString = "Please enter the search keyword";
                return View();
            }
        }

        // POST: InventoryQuantity/add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(int? id, int? Quantity)
        {
            string message = "";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var inventory = db.Product_Inventory.Find(id);
            if (inventory == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Quantity == null || Quantity == 0)
            {
                message = "Please insert quantity to update " + inventory.Product.product_name.ToString() + "'s Quantity.";
            }

            // if all info are valid
            if (id != null && Quantity != null)
            {
                inventory.unit_quantity += Convert.ToInt32(Quantity);
                db.Entry(inventory).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}