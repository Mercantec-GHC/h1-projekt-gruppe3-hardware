namespace BlazorApp.Data
{
    public class Hardware
    {
        public string Type { get; set; } = string.Empty;
        public string? Name { get; set; }
        public string? AttackSurface { get; set; }
        public string? State { get; set; }
        public double Version { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}



