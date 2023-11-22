namespace BlazorApp.Data
{
    public class Seller
    {
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public bool Trusted { get; set; }
        public Hardware Product { get; set; } = new Hardware();
    }
}
