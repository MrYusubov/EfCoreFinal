using MarketApp.Data;
using MarketApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.Services
{
    public class ProductManager
    {
        private readonly MarketAppContext db;

        public ProductManager(MarketAppContext context)
        {
            db = context;
        }

        public void AddProduct(string name, double price, string description, int categoryId, int stock)
        {
            var product = new Product
            {
                ProductName = name,
                Price = price,
                Description = description,
                CategId = categoryId,
                Stock = stock
            };
            db.Products.Add(product);
            db.SaveChanges();
        }

        public List<Product> GetProductsByCategory(int categoryId)
        {
            return db.Products
                           .Where(p => p.CategId == categoryId && p.Stock > 0)
                           .Include(p => p.Category)
                           .ToList();
        }
        public List<Product> GetProducts()
        {
            return db.Products.ToList();
        }

        public void UpdateProduct(int productId, string name, double price, string description, int stock)
        {
            var product = db.Products.Find(productId);
            if (product != null)
            {
                product.ProductName = name;
                product.Price = price;
                product.Description = description;
                product.Stock = stock;
                db.SaveChanges();
            }
        }

        public void UpdateStock(int productId, int stock)
        {
            var product = db.Products.Find(productId);
            if (product != null)
            {
                product.Stock = stock;
                db.SaveChanges();
            }
        }

        public void DeleteProduct(int productId)
        {
            var product = db.Products.Find(productId);
            if (product != null)
            {
                db.Products.Remove(product);
                db.SaveChanges();
            }
        }
    }
}
