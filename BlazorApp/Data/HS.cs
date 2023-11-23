using System;
using System.Collections.Generic;
using System.Linq;
using BlazorApp.Data;

namespace ClassLibrary1
{
    //public class HS
    //{
    //    public string Type { get; set; } = string.Empty;
    //    public string? Name { get; set; }
    //    public string? AttackSurface { get; set; }
    //    public string? State { get; set; }
    //    public double Version { get; set; }
    //    public string? Description { get; set; }
    //    public decimal Price { get; set; }
    //}

    //public class Seller
    //{
    //    public string? Name { get; set; }
    //    public string? PhoneNumber { get; set; }
    //    public string? Email { get; set; }
    //    public bool Trusted { get; set; }
    //    public HS Product { get; set; } = new HS();
    //}

    //public class ShoppingCartItem
    //{
    //    public Seller Seller { get; set; }
    //    public HS Product { get; set; }
    //}

    //public class Marketplace
    //{
    //    private List<Seller> sellers = new List<Seller>();
    //    private List<ShoppingCartItem> shoppingCart = new List<ShoppingCartItem>();

    //    public Seller potentialSeller = new Seller();

    //    //Tilføj en sælger, så personen kan sælge produkter 
    //    public void AddSeller(Seller seller)
    //    {
    //        sellers.Add(seller);
    //    }
    //    //Viser alle sælgere 
    //    public void DisplayAllSellers()
    //    {
    //        foreach (var seller in sellers)
    //        {
    //            Console.WriteLine($"Seller: {seller.Name}");
    //            Console.WriteLine($"Product: {seller.Product.Name}");
    //            Console.WriteLine($"Price: {seller.Product.Price:C}");
    //            Console.WriteLine("--------");
    //        }
    //    }

    //    //Viser alle trusted sælgere. (Sælgere med bedste rating)
    //    public void DisplayTrustedSellers()
    //    {
    //        var trustedSellers = sellers.Where(seller => seller.Trusted);

    //        Console.WriteLine("Trusted Sellers:");
    //        foreach (var seller in trustedSellers)
    //        {
    //            Console.WriteLine($"Seller: {seller.Name}");
    //            Console.WriteLine($"Product: {seller.Product.Name}");
    //            Console.WriteLine($"Price: {seller.Product.Price:C}");
    //            Console.WriteLine("--------");
    //        }
    //    }

    //    //Viser sælgere og deres produkter i forhold til deres pris 
    //    public void DisplaySellersByPrice()
    //    {
    //        var sellersByPrice = sellers.OrderBy(seller => seller.Product.Price);

    //        Console.WriteLine("Sellers Sorted by Price:");
    //        foreach (var seller in sellersByPrice)
    //        {
    //            Console.WriteLine($"Seller: {seller.Name}");
    //            Console.WriteLine($"Product: {seller.Product.Name}");
    //            Console.WriteLine($"Price: {seller.Product.Price:C}");
    //            Console.WriteLine("--------");
    //        }
    //    }

    //    //Giver brugeren mulighed for at søge efter produkter. 
    //    public void SearchProduct(string productName)
    //    {
    //        var matchingSellers = sellers.Where(seller => seller.Product.Name?.Contains(productName, StringComparison.OrdinalIgnoreCase) == true);

    //        Console.WriteLine($"Search Results for '{productName}':");
    //        foreach (var seller in matchingSellers)
    //        {
    //            Console.WriteLine($"Seller: {seller.Name}");
    //            Console.WriteLine($"Product: {seller.Product.Name}");
    //            Console.WriteLine($"Price: {seller.Product.Price:C}");
    //            Console.WriteLine("--------");
    //        }
    //    }

    //    //Mulighed for at tilføje et produkt/annonce 
    //    public void CreateAd(string sellerName, string phoneNumber, string email, string productName, decimal price)
    //    {
    //        Seller newSeller = new Seller
    //        {
    //            Name = sellerName,
    //            PhoneNumber = phoneNumber,
    //            Email = email,
    //            Trusted = false,
    //            Product = new Hardware
    //            {
    //                Type = "Hardware", // Sat til kun hardware, for at gøre det nemmere. Kan ændres senere, hvis software også skal tilføjes
    //                Name = productName,
    //                Price = price
    //            }
    //        };

    //        //Giver en besked om at annoncen er oprettet med produktnavn og sælgerens navn
    //        sellers.Add(newSeller);
    //        Console.WriteLine($"Ad created for '{productName}' by '{sellerName}'.");
    //    }

    //    //Tilføj til kurven 
    //    public void AddToCart(Seller seller)
    //    {
    //        shoppingCart.Add(new ShoppingCartItem
    //        {
    //            Seller = seller,
    //            Product = seller.Product
    //        });

    //        Console.WriteLine($"Product '{seller.Product.Name}' added to the shopping cart.");
    //    }

    //    //Vis kurven (For at vise hvad du har planer om at købe)
    //    public void DisplayShoppingCart()
    //    {
    //        Console.WriteLine("Shopping Cart:");

    //        foreach (var item in shoppingCart)
    //        {
    //            Console.WriteLine($"Seller: {item.Seller.Name}");
    //            Console.WriteLine($"Product: {item.Product.Name}");
    //            Console.WriteLine($"Price: {item.Product.Price:C}");
    //            Console.WriteLine("--------");
    //        }
    //    }
    //}

    class Program
    {
        static void Main(string[] args)
        {
            // Eksempel på sælger med produkt 
            Seller seller1 = new Seller
            {
                Name = "Alice",
                PhoneNumber = "+45 11111111",
                Email = "alice@example.com",
                Trusted = true,
                Product = new Hardware
                {
                    Type = "Hardware",
                    Name = "Gigabyte GeForce RTX 4090 GAMING OC 24G Grafikkort - 24GB GDDR6X - NVIDIA RTX 4090 - PCI Express 4.0 x16",
                    AttackSurface = "None",
                    State = "New",
                    Version = 1.0,
                    Description = "New 4090 graphicscard. Not used, and works really well",
                    Price = 17999.95m
                }
            };

            Seller seller2 = new Seller
            {
                Name = "Bob",
                PhoneNumber = "+45 22222222",
                Email = "bob@example.com",
                Trusted = false,
                Product = new Hardware
                {
                    Type = "Hardware",
                    Name = "Cool Hardware",
                    AttackSurface = "Peripheral",
                    State = "Used",
                    Version = 3.0,
                    Description = "A cool hardware product",
                    Price = 149.99m
                }
            };

            // Tilføj sælger til markedspladsen
            Marketplace marketplace = new Marketplace();
            marketplace.AddSeller(seller1);

            // Vis alle sælgere
            marketplace.DisplayAllSellers();

            // Vis kun trusted sælgere
            marketplace.DisplayTrustedSellers();

            // Vis sælgere sorteret efter pris
            marketplace.DisplaySellersByPrice();

            // Søg efter et produkt
            marketplace.SearchProduct("Spy ETH");

            // Opret en annonce
            marketplace.CreateAd("John Doe", "+45 12345678", "john.doe@example.com", "Gaming Mouse", 29.99m);
                
            // Vis alle sælgere igen, inklusive den nye annonce
            marketplace.DisplayAllSellers();

            // Tilføj et produkt til indkøbskurven
            marketplace.AddToCart(seller1);

            // Vis indkøbskurven
            marketplace.DisplayShoppingCart();
        }
    }
}
