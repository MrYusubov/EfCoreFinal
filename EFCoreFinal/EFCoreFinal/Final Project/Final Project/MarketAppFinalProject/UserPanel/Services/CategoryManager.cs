using MarketApp.Models;
using MarketApp.Data;
using Microsoft.EntityFrameworkCore;

namespace UserPanel.Services
{
    public class CategoryManager
    {
        private readonly MarketAppContext db;

        public CategoryManager(MarketAppContext context)
        {
            db = context;
        }

        public List<Category> GetCategories()
        {
            return db.Categories.Include(c => c.Products).ToList();
        }

        public Category? GetCategoryById(int categoryId)
        {
            return db.Categories.Include(c => c.Products).FirstOrDefault(c => c.CategoryId == categoryId);
        }
    }
}
