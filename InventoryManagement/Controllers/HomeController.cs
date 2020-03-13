using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InventoryManagement.Models;

namespace InventoryManagement.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Choose()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Inventory()
        {
            InventoryWrapper wrapper = new InventoryWrapper();
            wrapper.LoadProducts();
            return View(wrapper);
        }

        [HttpGet]
        public ActionResult AddInventory()
        {
            InventoryViewModel vm = new InventoryViewModel();
            return View(vm);
        }

        [HttpPost]
        public ActionResult AddInventory(InventoryViewModel vm)
        {
            try
            {
                IMDAL.AddUpdate<Product>(new Product()
                {
                    Price = vm.Price,
                    ProductName = vm.ProductName,
                    Quantity = vm.Quantity,
                    Remarks = vm.Remarks,
                    VendorName = vm.VendorName
                });
            }
            catch (Exception e)
            {

            }

            return RedirectToAction("Inventory");
        }

        [HttpGet]
        public ActionResult CreateSale()
        {
            SalesWrapper sale = new SalesWrapper();
            sale.Sales = new List<SaleViewModel>();
            sale.Sales.Add(new SaleViewModel() { });
            ViewBag.Counter = 1;
            return View(sale);
        }

        [HttpPost]
        public ActionResult AddMore(int divId)
        {
            ViewBag.Counter = divId;
            return PartialView("SaleItem", new SaleViewModel());
        }

        [HttpGet]
        public ActionResult Sale()
        {
            SaleViewModel vm = new SaleViewModel();
            return View(vm);
        }

        [HttpPost]
        public ActionResult CalculateAmount(long productId, long quantity)
        {
            decimal amount = 0;
            try
            {
                amount = IMDAL.FindAmount(productId, quantity);
            }
            catch (Exception e)
            {
                amount = 0;
            }
            return Content(amount.ToString());
        }

        [HttpPost]
        public ActionResult Sale(SaleViewModel vm)
        {
            try
            {
                vm.InvoiceNumber = IMDAL.GenerateInvoiceNumber();
                IMDAL.AddUpdate<Sale>(vm.ToEntity());
            }
            catch (Exception e)
            {

            }
            return RedirectToAction("Sale");
        }

        public ActionResult Report()
        {
            SalesWrapper wrapper = new SalesWrapper();
            wrapper.LoadSales();
            return View(wrapper);
        }
    }
}