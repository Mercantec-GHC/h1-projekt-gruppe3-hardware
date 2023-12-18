using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorApp.Data
{
    public class Marketplace
    {
        public List<Seller> sellers = new List<Seller>();

        public Seller potentialSeller = new Seller();

        // Tilføj en sælger, så personen kan sælge produkter 
        public void AddSeller(Seller seller)
        {
            sellers.Add(seller);
        }

        // Viser alle sælgere 
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

        // Viser alle trusted sælgere. (Sælgere med bedste rating)
        public void DisplayTrustedSellers()
        {
            var trustedSellers = sellers.Where(seller => seller.Trusted == 1);

            Console.WriteLine("Trusted Sellers:");
            foreach (var seller in trustedSellers)
            {
                Console.WriteLine($"Seller: {seller.Name}");
                Console.WriteLine($"Product: {seller.Product.Name}");
                Console.WriteLine($"Price: {seller.Product.Price:C}");
                Console.WriteLine("--------");
            }
        }

        // Viser sælgere og deres produkter i forhold til deres pris 
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

        // Giver brugeren mulighed for at søge efter produkter. 
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

        // Mulighed for at tilføje et produkt/annonce 
        public void CreateAd(string email, string productName, decimal price)
        {
            Seller newSeller = new Seller
            {
                Email = email,
                Trusted = 0,
                Product = new Hardware
                {
                    Name = productName,
                    Price = price
                }
            };

            sellers.Add(newSeller);
        }
    }

    // Define your Seller and Hardware classes here (assuming they are not provided in this snippet).
}
