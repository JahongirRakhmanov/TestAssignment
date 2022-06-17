using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestAssignment.Models
{
    public class ProductAudit
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public string Action { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}