﻿using BlazorApp.Data;

namespace ClassLibrary1
{
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
                Trusted = 1,
                Product = new Hardware
                {
                    Name = "Gigabyte GeForce RTX 4090 GAMING OC 24G Grafikkort - 24GB GDDR6X - NVIDIA RTX 4090 - PCI Express 4.0 x16",
                    Description = "New 4090 graphicscard. Not used, and works really well",
                    Price = 17999.95m
                }
            };

            Seller seller2 = new Seller
            {
                Name = "Bob",
                PhoneNumber = "+45 22222222",
                Email = "bob@example.com",
                Trusted = 0,
                Product = new Hardware
                {
                    Name = "Cool Hardware",
                    Description = "A cool hardware product",
                    Price = 149.99m
                }
            };

            // Tilføj sælger til markedspladsen
            Marketplace marketplace = new Marketplace();
            marketplace.AddSeller(seller1);
            marketplace.AddSeller(seller2);

            // Vis alle sælgere
            marketplace.DisplayAllSellers();

            // Vis kun trusted sælgere
            marketplace.DisplayTrustedSellers();

            // Vis sælgere sorteret efter pris
            marketplace.DisplaySellersByPrice();
        }
    }
}