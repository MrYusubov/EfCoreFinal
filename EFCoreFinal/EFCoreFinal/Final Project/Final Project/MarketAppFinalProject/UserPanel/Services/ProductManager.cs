using MarketApp.Models;
using MarketApp.Data;

namespace UserPanel.Services
{
    public class ProductManager
    {
        private readonly MarketAppContext db;

        public ProductManager(MarketAppContext context)
        {
            db = context;
        }

        public List<Product> GetProductsByCategory(int categoryId)
        {
            return db.Products.Where(p => p.CategId == categoryId && p.Stock > 0).ToList();
        }
        public List<Product> GetProducts()
        {
            return db.Products.ToList();
        }

        public Product? GetProductById(int productId)
        {
            return db.Products.Find(productId);
        }

        public void UpdateStock(int productId, int newStock)
        {
            var product = db.Products.Find(productId);
            if (product != null)
            {
                product.Stock = product.Stock-newStock;
                db.SaveChanges();
            }
        }
    }
}
