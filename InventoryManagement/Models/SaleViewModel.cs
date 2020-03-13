using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using System.Web.Mvc;

namespace InventoryManagement.Models
{
    public class SaleViewModel : Sale
    {
        public SaleViewModel()
        {

        }

        public SaleViewModel(Sale dbSale)
        {
            this.Id = dbSale.Id;
            this.Amount = dbSale.Amount;
            this.InvoiceNumber = dbSale.InvoiceNumber;
            this.ProductId = dbSale.ProductId;
            this.ProductName = IMDAL.FindProductName(this.ProductId.GetValueOrDefault());
            this.Quantity = dbSale.Quantity;
            this.SoldBy = dbSale.SoldBy;
            this.SoldOn = dbSale.SoldOn.GetValueOrDefault().ToLocalTime();
            this.SoldTo = dbSale.SoldTo;
        }

        public System.Collections.IEnumerable LoadProducts()
        {
            List<SelectListItem> ddl = new List<SelectListItem>();

            foreach (var item in IMDAL.GetAllProducts())
            {
                ddl.Add(new SelectListItem() { Text = item.ProductName, Value = item.Id.ToString() });
            }

            return ddl;
        }

        internal Sale ToEntity()
        {
            return new Sale()
            {
                Amount = this.Amount,
                Id = this.Id,
                InvoiceNumber = this.InvoiceNumber,
                ProductId = this.ProductId,
                Quantity = this.Quantity,
                SoldBy = "Simran",
                SoldOn = DateTime.UtcNow,
                SoldTo = "Customer ABC"
            };
        }

        public string ProductName { get; set; }
    }


    public class SalesWrapper
    {
        public List<SaleViewModel> Sales { get; set; }

        public decimal Amount { get; set; }

        public SalesWrapper()
        {

        }

        internal void LoadSales()
        {
            this.Sales = new List<SaleViewModel>();

            foreach (var item in IMDAL.GetSales())
            {
                this.Sales.Add(new SaleViewModel(item));
            }
        }
    }
}