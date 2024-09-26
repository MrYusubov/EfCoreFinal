using MarketApp.Data;
using MarketApp.Models;
using Microsoft.EntityFrameworkCore;

namespace AdminPanel.Services
{
    public class CategoryManager
    {
        private readonly MarketAppContext db;

        public CategoryManager(MarketAppContext context)
        {
            db = context;
        }

        public void AddCategory(string name)
        {
            var category = new Category
            {
                CategoryName = name
            };
            db.Categories.Add(category);
            db.SaveChanges();
        }

        public List<Category> GetCategories()
        {
            return db.Categories.ToList();
        }

        public void DeleteCategory(int categoryId)
        {
            var category = db.Categories.Find(categoryId);
            if (category != null)
            {
                db.Categories.Remove(category);
                db.SaveChanges();
            }
        }

        public void UpdateCategory(int categoryId, string newName)
        {
            var category = db.Categories.Find(categoryId);
            if (category != null)
            {
                category.CategoryName = newName;
                db.SaveChanges();
            }
        }
    }
}
