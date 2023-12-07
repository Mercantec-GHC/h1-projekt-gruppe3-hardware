using BlazorApp.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace BlazorApp.Data
{
    public class Hardware
    {
        public int HardwareId { get; set; }
        public string Type { get; set; } = string.Empty;
        public string? Name { get; set; }
        public string? AttackSurface { get; set; }
        public string? State { get; set; }
        public double Version { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int SellerId { get; set; }
    }
}



