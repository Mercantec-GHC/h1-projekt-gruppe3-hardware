using BlazorApp.Data;

public class Seller
{
    public int SellerId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public byte Trusted { get; set; }
    public int ProductId { get; set; }

    // Navigation property til Product
    public Hardware Product { get; set; }
}
