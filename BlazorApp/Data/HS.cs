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
                Email = "alice@example.com",
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
                Email = "bob@example.com",
                Product = new Hardware
                {
                    Name = "Cool Hardware",
                    Description = "A cool hardware product",
                    Price = 149.99m
                }
            };
        }
    }
}