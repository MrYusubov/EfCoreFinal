using UserPanel.Services;
using MarketApp.Data;
using MarketApp.Models;
using Microsoft.EntityFrameworkCore;
using System;

public class Program
{
    public static void Main(string[] args)
    {
        using (var db = new MarketAppContext())
        {
            var userManager = new UserManager(db);
            var categoryManager = new CategoryManager(db);
            var productManager = new ProductManager(db);
            var cartManager = new CartManager(db);

            MainPage(userManager, categoryManager, productManager, cartManager);
        }
    }

    public static void MainPage(UserManager userManager, CategoryManager categoryManager, ProductManager productManager, CartManager cartManager)
    {
        Console.Clear();
        Console.WriteLine("********* Welcome to User Panel *********");
        Console.WriteLine("1. Register");
        Console.WriteLine("2. Login");
        Console.Write("Enter your choice: ");
        var choice = Console.ReadLine();
        if (choice == "1")
        {
            Register(userManager);
        }
        else if (choice == "2")
        {
            Login(userManager);
        }

        if (userManager.CurrentUser != null)
        {
            UserPage(userManager, categoryManager, productManager, cartManager);
        }
    }

    public static void Register(UserManager userManager)
    {
        Console.Clear();
        Console.WriteLine("********* Register *********");
        Console.Write("Enter Name: ");
        var name = Console.ReadLine();
        Console.Write("Enter Surname: ");
        var surname = Console.ReadLine();
        Console.Write("Enter Email: ");
        var email = Console.ReadLine();
        Console.Write("Enter Password: ");
        var password = Console.ReadLine();
        Console.Write("Enter Date of Birth (dd.MM.yyyy): ");
        var dateOfBirth = DateOnly.Parse(Console.ReadLine()!);

        userManager.Register(name!, surname!, email!, password!, dateOfBirth);
        Console.WriteLine("User registered successfully!");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    public static void Login(UserManager userManager)
    {
        Console.Clear();
        Console.WriteLine("********* Login *********");
        Console.Write("Enter Email: ");
        var email = Console.ReadLine();
        Console.Write("Enter Password: ");
        var password = Console.ReadLine();

        if (userManager.Login(email!, password!))
        {
            Console.WriteLine("Login successful!");
        }
        else
        {
            Console.WriteLine("Invalid credentials! Press any key to retry.");
            Console.ReadKey();
            Login(userManager);
        }
    }

    public static void UserPage(UserManager userManager, CategoryManager categoryManager, ProductManager productManager, CartManager cartManager)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("********* User Menu *********");
            Console.WriteLine("1. View Categories");
            Console.WriteLine("2. View Cart");
            Console.WriteLine("3. View Profile");
            Console.WriteLine("4. Logout");
            Console.Write("Enter your choice: ");
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ViewCategories(categoryManager, productManager, cartManager, userManager);
                    break;
                case "2":
                    ViewCart(cartManager, userManager.CurrentUser!,productManager);
                    break;
                case "3":
                    ViewProfile(userManager);
                    break;
                case "4":
                    userManager.Logout();
                    MainPage(userManager, categoryManager, productManager, cartManager);
                    break;
                default:
                    Console.WriteLine("Invalid choice! Press any key to retry.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    public static void ViewCategories(CategoryManager categoryManager, ProductManager productManager, CartManager cartManager, UserManager userManager)
    {
        Console.Clear();
        Console.WriteLine("********* Categories *********");
        var categories = categoryManager.GetCategories();
        foreach (var category in categories)
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine($"Category Id: {category.CategoryId}");
            Console.WriteLine($"Category Name: {category.CategoryName}");
            Console.WriteLine("------------------------------");
        }

        Console.Write("Select a category by ID: ");
        var categoryId = int.Parse(Console.ReadLine()!);
        var selectedCategory = categoryManager.GetCategoryById(categoryId);

        if (selectedCategory != null)
        {
            Console.Clear();
            var products = productManager.GetProductsByCategory(selectedCategory.CategoryId);
            foreach (var product in products)
            {
                if (product.Stock != 0)
                {
                    Console.WriteLine("------------------------------");
                    Console.WriteLine($"Product Id: {product.ProductId}");
                    Console.WriteLine($"Product Name: {product.ProductName}");
                    Console.WriteLine($"Price: {product.Price}");
                    Console.WriteLine($"Stock: {product.Stock}");
                    Console.WriteLine($"Description: {product.Description}");
                    Console.WriteLine("------------------------------");
                }
            }
            Console.Write("Select a product by ID to add to cart: ");
            var productId = int.Parse(Console.ReadLine()!);
            var selectedProduct = productManager.GetProductById(productId);

            Console.Write("Enter quantity: ");
            var quantity = int.Parse(Console.ReadLine()!);

            cartManager.AddToCart(selectedProduct!, quantity,userManager.CurrentUser!);
            Console.WriteLine("Product added to cart! Press any key to return...");
            Console.ReadKey();
        }
        else
        {
            Console.WriteLine("Category not found! Press any key to return...");
            Console.ReadKey();
        }
    }

    public static void ViewCart(CartManager cartManager, User user,ProductManager productManager)
    {
        Console.Clear();
        Console.WriteLine("********* Cart *********");
        var cartItems = cartManager.GetCartItems(user);
        foreach (var item in cartItems)
        {
            Console.WriteLine($"Product: {item.Product.ProductName}");
            Console.WriteLine($"Quantity: {item.Quantity}");
        }

        double total = cartItems.Sum(ci => ci.Product.Price * ci.Quantity);
        Console.WriteLine($"Total: {total}");
        Console.Write("Do you want to checkout? (y/n): ");
        var choice = Console.ReadLine();
        if (choice!.ToLower().Trim() == "y")
        {
            Console.Write("Enter payment amount: ");
            var payment = double.Parse(Console.ReadLine()!);
            var change = cartManager.Checkout(user, payment);
            Console.WriteLine($"Checkout successful! Change: {change}");
            var products = productManager.GetProducts();
            foreach (var item in cartItems)
            {
                productManager.UpdateStock(item.ProdId, item.Quantity);
            }
        }
        else
        {
            Console.WriteLine("Checkout canceled.");
        }

        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
    }

    public static void ViewProfile(UserManager userManager)
    {
        var user = userManager.CurrentUser;
        Console.Clear();
        Console.WriteLine("********* Profile *********");
        Console.WriteLine($"Name: {user!.Name}");
        Console.WriteLine($"Surname: {user.Surname}");
        Console.WriteLine($"Email: {user.Email}");
        Console.WriteLine($"Date of Birth: {user.DateOfBirth}");
        Console.WriteLine("Press any key to return...");
        Console.ReadKey();
    }
}
