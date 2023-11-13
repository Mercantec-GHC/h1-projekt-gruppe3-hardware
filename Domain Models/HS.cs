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

        // Sælgerens produktinformation
        public HS product { get; set; } = new HS();
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
                product = new HS
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
        }
    }
}
