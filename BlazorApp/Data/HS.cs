using BlazorApp.Data;

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
