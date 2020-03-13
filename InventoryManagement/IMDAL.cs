using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace InventoryManagement
{
    public class IMDAL
    {
        public static InventoryManagementEntities GetContext()
        {
            return new InventoryManagementEntities();
        }

        public static IEnumerable<Product> GetAllProducts()
        {
            using (var db = GetContext())
            {
                return db.Products.ToList();
            }
        }

        public static void AddUpdate<T>(T entity) where T : class
        {
            using (var db = GetContext())
            {
                if (db.Entry(entity).State == EntityState.Detached)
                    db.Set<T>().Add(entity);

                db.SaveChanges();
            }
        }

        internal static long FindStockLeft(long productId)
        {
            using (var db = GetContext())
            {
                if (db.Sales.Any(x => x.ProductId == productId))
                {
                    return db.Sales.Where(x => x.ProductId == productId).Count();
                }
                else
                    return 0;
            }
        }

        internal static decimal FindAmount(long productId, long quantity)
        {
            using (var db = GetContext())
            {
                var dbAProduct = db.Products.Where(x => x.Id == productId).FirstOrDefault();

                return dbAProduct.Price.GetValueOrDefault() * quantity;
            }
        }

        internal static string GenerateInvoiceNumber()
        {
            using (var db = GetContext())
            {
                string invNo = string.Empty;

                invNo = "RCPT";
                long counter = 1;

                while (db.Sales.Any(x => x.InvoiceNumber == (invNo + counter)))
                {
                    counter = counter + 1;
                }

                return invNo + counter;
            }
        }

        internal static IEnumerable<Sale> GetSales()
        {
            using (var db = GetContext())
            {
                return db.Sales.ToList();
            }
        }

        internal static string FindProductName(long pId)
        {
            using (var db = GetContext())
            {
                return db.Products.Where(x => x.Id == pId).Select(x => x.ProductName).FirstOrDefault();
            }
        }
    }
}