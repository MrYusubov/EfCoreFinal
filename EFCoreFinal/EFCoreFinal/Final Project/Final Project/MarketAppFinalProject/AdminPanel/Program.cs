using AdminPanel.Services;
using MarketApp.Data;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            AdminLogin();
            using (var db = new MarketAppContext())
            {
                var categoryManager = new CategoryManager(db);
                var productManager = new ProductManager(db);

                AdminMainPage(categoryManager, productManager);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Critical Error: {ex.Message}");
        }
    }
    public static void AdminLogin()
    {
    AdminLogin:
        Console.Clear();
        Console.WriteLine("*********Admin Sign In*********");
        Console.Write("Email: ");
        var email = Console.ReadLine();
        Console.Write("Password: ");
        var password = Console.ReadLine();

        if (email == "admin" && password == "admin")
        {
            Console.WriteLine("Admin login successful!");
        }
        else
        {
            Console.WriteLine("Invalid email or password");
            Thread.Sleep(2000);
            goto AdminLogin;
        }
    }

    public static void AdminMainPage(CategoryManager categoryManager, ProductManager productManager)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("1. Add Category");
            Console.WriteLine("2. Add Product");
            Console.WriteLine("3. Update Product Detail");
            Console.WriteLine("4. Update Stocks");
            Console.WriteLine("5. View Products by Category");
            Console.WriteLine("6. Delete Product");
            Console.WriteLine("7. Delete Category");
            Console.WriteLine("8. Exit");

            Console.Write("Enter your choice: ");
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    AddCategory(categoryManager);
                    break;
                case "2":
                    AddProduct(productManager,categoryManager);
                    break;
                case "3":
                    UpdateProduct(productManager);
                    break;
                case "4":
                    UpdateStock(productManager);
                    break;
                case "5":
                    ViewProductsByCategory(productManager,categoryManager);
                    break;
                case "6":
                    DeleteProduct(productManager);
                    break;
                case "7":
                    DeleteCategory(categoryManager);
                    break;
                case "8":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    public static void AddCategory(CategoryManager categoryManager)
    {
        Console.Clear();
        Console.Write("Enter category name: ");
        var name = Console.ReadLine();
        categoryManager.AddCategory(name!);
        Console.WriteLine("Category added successfully!");
    }

    public static void AddProduct(ProductManager productManager,CategoryManager categoryManager)
    {
        Console.Clear();
        Console.Write("Enter product name: ");
        var name = Console.ReadLine();
        Console.Write("Enter product price: ");
        var price = double.Parse(Console.ReadLine()!);
        Console.Write("Enter product description: ");
        var description = Console.ReadLine();
        Console.Write("Enter the Stock: ");
        var stock = int.Parse(Console.ReadLine()!);
        var categories = categoryManager.GetCategories();
        foreach (var category in categories)
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine($"Category Id: {category.CategoryId}");
            Console.WriteLine($"Category Name: {category.CategoryName}");
            Console.WriteLine("------------------------------");
        }
        Console.Write("Enter the Category Id: ");
        var categoryId = int.Parse(Console.ReadLine()!);

        productManager.AddProduct(name!, price, description!, categoryId, stock);
        Console.WriteLine("Product added successfully!");
    }

    public static void UpdateProduct(ProductManager productManager)
    {
        Console.Clear();
        var products = productManager.GetProducts();
        foreach (var product in products)
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine($"Product Id: {product.ProductId}");
            Console.WriteLine($"Product Name: {product.ProductName}");
            Console.WriteLine($"Price: {product.Price}");
            Console.WriteLine($"Stock: {product.Stock}");
            Console.WriteLine($"Description: {product.Description}");
            Console.WriteLine("------------------------------");
        }
        Console.Write("Enter product ID to update: ");
        var productId = int.Parse(Console.ReadLine()!);
        Console.Write("Enter new product name: ");
        var name = Console.ReadLine();
        Console.Write("Enter new product price: ");
        var price = double.Parse(Console.ReadLine()!);
        Console.Write("Enter new product description: ");
        var description = Console.ReadLine();
        Console.Write("Enter new stock amount: ");
        var stock = int.Parse(Console.ReadLine()!);

        productManager.UpdateProduct(productId, name!, price, description!, stock);
        Console.WriteLine("Product updated successfully!");
    }

    public static void UpdateStock(ProductManager productManager)
    {
        Console.Clear();
        var products = productManager.GetProducts();
        foreach (var product in products)
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine($"Product Id: {product.ProductId}");
            Console.WriteLine($"Product Name: {product.ProductName}");
            Console.WriteLine($"Price: {product.Price}");
            Console.WriteLine($"Stock: {product.Stock}");
            Console.WriteLine($"Description: {product.Description}");
            Console.WriteLine("------------------------------");
        }
        Console.Write("Enter product ID to update stock: ");
        var productId = int.Parse(Console.ReadLine()!);
        Console.Write("Enter new stock amount: ");
        var stock = int.Parse(Console.ReadLine()!);

        productManager.UpdateStock(productId, stock);
        Console.WriteLine("Product stock updated successfully!");
    }

    public static void ViewProductsByCategory(ProductManager productManager,CategoryManager categoryManager)
    {
        Console.Clear();
        var categories = categoryManager.GetCategories();
        foreach (var category in categories)
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine($"Category Id: {category.CategoryId}");
            Console.WriteLine($"Category Name: {category.CategoryName}");
            Console.WriteLine("------------------------------");
        }
        Console.Write("Enter category ID: ");
        var categoryId = int.Parse(Console.ReadLine()!);
        var products = productManager.GetProductsByCategory(categoryId);

        foreach (var product in products)
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine($"Product Id: {product.ProductId}");
            Console.WriteLine($"Product Name: {product.ProductName}");
            Console.WriteLine($"Price: {product.Price}");
            Console.WriteLine($"Stock: {product.Stock}");
            Console.WriteLine($"Description: {product.Description}");
            Console.WriteLine("------------------------------");
        }
        Console.ReadKey();
    }

    public static void DeleteProduct(ProductManager productManager)
    {
        Console.Clear();
        var products=productManager.GetProducts();
        foreach (var product in products)
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine($"Product Id: {product.ProductId}");
            Console.WriteLine($"Product Name: {product.ProductName}");
            Console.WriteLine($"Price: {product.Price}");
            Console.WriteLine($"Stock: {product.Stock}");
            Console.WriteLine($"Description: {product.Description}");
            Console.WriteLine("------------------------------");
        }
        Console.Write("Enter product ID to delete: ");
        var productId = int.Parse(Console.ReadLine()!);
        productManager.DeleteProduct(productId);
        Console.WriteLine("Product deleted successfully!");
    }

    public static void DeleteCategory(CategoryManager categoryManager)
    {
        Console.Clear();
        var categories=categoryManager.GetCategories();
        foreach (var category in categories)
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine($"Category Id: {category.CategoryId}");
            Console.WriteLine($"Category Name: {category.CategoryName}");
            Console.WriteLine("------------------------------");
        }
        Console.Write("Enter category ID to delete: ");
        var categoryId = int.Parse(Console.ReadLine()!);
        categoryManager.DeleteCategory(categoryId);
        Console.WriteLine("Category deleted successfully!");
    }
}


