using MarketApp.Models;
using MarketApp.Data;
using System.Collections.Generic;
using System.Linq;

public class CartManager
{
    private readonly MarketAppContext db;

    public CartManager(MarketAppContext context)
    {
        db = context;
    }

    public void AddToCart(Product product, int quantity, User user)
    {
        var cart = db.Carts.FirstOrDefault(c => c.UserId == user.UserId);
        if (cart == null)
        {
            cart = new Cart { UserId = user.UserId }; 
            db.Carts.Add(cart);
        }

        var cartItem = cart.Items.FirstOrDefault(i => i.ProdId == product.ProductId);
        if (cartItem == null)
        {
            cart.Items.Add(new CartItem { Product = product, Quantity = quantity, ProdId = product.ProductId });
        }
        else
        {
            cartItem.Quantity += quantity;
        }

        db.SaveChanges();
    }


    public List<CartItem> GetCartItems(User user)
    {
        var cart = db.Carts.FirstOrDefault(c => c.CartId == user.UserId);
        if (cart != null)
        {
            return cart.Items.ToList();
        }
        return new List<CartItem>();
    }

    public double Checkout(User user, double payment)
    {
        var cart = db.Carts.FirstOrDefault(c => c.CartId == user.UserId);
        if (cart == null) throw new InvalidOperationException("Cart not found");

        double total = cart.Items.Sum(i => i.Product.Price * i.Quantity);
        if (payment < total) throw new InvalidOperationException("Insufficient payment");

        db.Carts.Remove(cart);
        db.SaveChanges();

        return payment - total;
    }
}
