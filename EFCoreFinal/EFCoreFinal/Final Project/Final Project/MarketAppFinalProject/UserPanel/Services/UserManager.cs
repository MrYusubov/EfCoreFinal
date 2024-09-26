using MarketApp.Models;
using MarketApp.Data;
using System.Linq;

public class UserManager
{
    private readonly MarketAppContext db;
    public User? CurrentUser { get; private set; }

    public UserManager(MarketAppContext context)
    {
        db = context;
    }

    public void Register(string name, string surname, string email, string password, DateOnly dateOfBirth)
    {
        var user = new User
        {
            Name = name,
            Surname = surname,
            Email = email,
            Password = password,
            DateOfBirth = dateOfBirth
        };
        db.Users.Add(user);
        db.SaveChanges();
    }

    public bool Login(string email, string password)
    {
        CurrentUser = db.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        return CurrentUser != null;
    }

    public void Logout()
    {
        CurrentUser = null;
    }
}
