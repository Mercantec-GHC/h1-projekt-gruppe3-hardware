using BlazorApp.Data;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System;

namespace BlazorApp.Data
{
    public class Hardware
    {
        public int HardwareId { get; set; }
        public string Name { get; set; }
        public string? Condition { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int SellerId { get; set; }
        public string? SellerName { get; set; }
        public string Category { get; set; } 

        public void EnsureNotNullValues()
        {
            Name ??= string.Empty;
            Condition ??= string.Empty;
            Description ??= string.Empty;
            SellerName ??= string.Empty;
            Category ??= string.Empty; 
        }
    }
}

