using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassLibrary1
{
    public class HS
    {
        public string Type { get; set; } = string.Empty;
        public string? Name { get; set; }
        public string? AttackSurface { get; set; }
        public string? State { get; set; }
        public double Version { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }

    public class Seller
    {
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public bool Trusted { get; set; }
        public HS Product { get; set; } = new HS();
    }

    public class Marketplace
    {
        private List<Seller> sellers = new List<Seller>();
        //Tilføj en sælger, så personen kan sælge produkter 
        public void AddSeller(Seller seller)
        {
            sellers.Add(seller);
        }
        //Viser alle sælgere 
        public void DisplayAllSellers()
        {
            foreach (var seller in sellers)
            {
                Console.WriteLine($"Seller: {seller.Name}");
                Console.WriteLine($"Product: {seller.Product.Name}");
                Console.WriteLine($"Price: {seller.Product.Price:C}");
                Console.WriteLine("--------");
            }
        }
        //Viser alle trusted sælgere. (Sælgere med bedste rating)
        public void DisplayTrustedSellers()
        {
            var trustedSellers = sellers.Where(seller => seller.Trusted);

            Console.WriteLine("Trusted Sellers:");
            foreach (var seller in trustedSellers)
            {
                Console.WriteLine($"Seller: {seller.Name}");
                Console.WriteLine($"Product: {seller.Product.Name}");
                Console.WriteLine($"Price: {seller.Product.Price:C}");
                Console.WriteLine("--------");
            }
        }
        //Viser sælgere og deres produkter i forhold til deres pris 
        public void DisplaySellersByPrice()
        {
            var sellersByPrice = sellers.OrderBy(seller => seller.Product.Price);

            Console.WriteLine("Sellers Sorted by Price:");
            foreach (var seller in sellersByPrice)
            {
                Console.WriteLine($"Seller: {seller.Name}");
                Console.WriteLine($"Product: {seller.Product.Name}");
                Console.WriteLine($"Price: {seller.Product.Price:C}");
                Console.WriteLine("--------");
            }
        }
        //Giver brugeren mulighed for at søge efter produkter. 
        public void SearchProduct(string productName)
        {
            var matchingSellers = sellers.Where(seller => seller.Product.Name?.Contains(productName, StringComparison.OrdinalIgnoreCase) == true);

            Console.WriteLine($"Search Results for '{productName}':");
            foreach (var seller in matchingSellers)
            {
                Console.WriteLine($"Seller: {seller.Name}");
                Console.WriteLine($"Product: {seller.Product.Name}");
                Console.WriteLine($"Price: {seller.Product.Price:C}");
                Console.WriteLine("--------");
            }
        }
        //Mulighed for at tilføje et produkt/annonce 
        public void CreateAd(string sellerName, string phoneNumber, string email, string productName, decimal price)
        {
            Seller newSeller = new Seller
            {
                Name = sellerName,
                PhoneNumber = phoneNumber,
                Email = email,
                Trusted = false,
                Product = new HS
                {
                    Type = "Hardware", // Assuming it's always hardware for simplicity
                    Name = productName,
                    Price = price
                }
            };

            sellers.Add(newSeller);
            Console.WriteLine($"Ad created for '{productName}' by '{sellerName}'.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Eksempel på sælgerkontaktoplysninger og produkt
            Seller potentialSeller = new Seller
            {
                Name = "Ben Dover",
                PhoneNumber = "+45 42126969",
                Email = "ben.dover@example.com",
                Trusted = false,
                Product = new HS
                {
                    Type = "Hardware",
                    Name = "Spy ETH",
                    AttackSurface = "Network",
                    State = "New",
                    Version = 2.0,
                    Description = "Backdoor Ethernet Cable",
                    Price = 499.99m
                }
            };

            // Tilføj sælger til markedspladsen
            Marketplace marketplace = new Marketplace();
            marketplace.AddSeller(potentialSeller);

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
        }
    }
}
