namespace BlazorApp.Data
{
    public class Marketplace
    {
        private List<Seller> sellers = new List<Seller>();
        private List<ShoppingCartItem> shoppingCart = new List<ShoppingCartItem>();

        public Seller potentialSeller = new Seller();

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
                Product = new Hardware
                {
                    Type = "Hardware", // Sat til kun hardware, for at gøre det nemmere. Kan ændres senere, hvis software også skal tilføjes
                    Name = productName,
                    Price = price
                }
            };

            //Giver en besked om at annoncen er oprettet med produktnavn og sælgerens navn
            sellers.Add(newSeller);
            Console.WriteLine($"Ad created for '{productName}' by '{sellerName}'.");
        }

        //Tilføj til kurven 
        public void AddToCart(Seller seller)
        {
            shoppingCart.Add(new ShoppingCartItem
            {
                Seller = seller,
                Product = seller.Product
            });

            Console.WriteLine($"Product '{seller.Product.Name}' added to the shopping cart.");
        }

        //Vis kurven (For at vise hvad du har planer om at købe)
        public void DisplayShoppingCart()
        {
            Console.WriteLine("Shopping Cart:");

            foreach (var item in shoppingCart)
            {
                Console.WriteLine($"Seller: {item.Seller.Name}");
                Console.WriteLine($"Product: {item.Product.Name}");
                Console.WriteLine($"Price: {item.Product.Price:C}");
                Console.WriteLine("--------");
            }
        }
    }
}
