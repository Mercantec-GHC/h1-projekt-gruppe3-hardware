using BlazorApp.Data;

public class Seller
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public bool Trusted { get; set; }
    public int ProductId { get; set; }

    // Navigation property til Product
    public Hardware Product { get; set; }
}
