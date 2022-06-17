using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAssignment.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public int Quantiy { get; set; }
        public decimal Price { get; set; }
    }
}