using System;

namespace backend.Models
{
    public class Cart
    {
        public int id { get; set; }
        public int userId { get; set; }
        public decimal unitPrice { get; set; }
        public decimal discount { get; set; }
        public int quantity { get; set; }
        public decimal totalPrice { get; set; }
    }
}
