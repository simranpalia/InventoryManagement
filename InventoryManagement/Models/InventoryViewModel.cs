using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace InventoryManagement.Models
{
    public class InventoryViewModel : Product
    {
        public InventoryViewModel()
        {

        }

        public InventoryViewModel(Product dbObj)
        {
            this.Id = dbObj.Id;
            this.Price = dbObj.Price;
            this.ProductName = dbObj.ProductName;
            this.Quantity = dbObj.Quantity;
            this.Remarks = dbObj.Remarks;
            this.VendorName = dbObj.VendorName;

            this.Sold = IMDAL.FindStockLeft(this.Id);

            this.InStock = this.Quantity.GetValueOrDefault() - this.Sold;
        }

        public long InStock { get; set; }

        public long Sold { get; set; }
    }

    public class InventoryWrapper
    {
        public List<InventoryViewModel> Products { get; set; }

        public InventoryWrapper()
        {

        }

        internal void LoadProducts()
        {
            this.Products = new List<InventoryViewModel>();

            foreach (var item in IMDAL.GetAllProducts())
            {
                this.Products.Add(new InventoryViewModel(item));
            }
        }
    }
}