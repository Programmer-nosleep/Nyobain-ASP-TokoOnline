using System;

namespace backend.Models
{
    public class OrderItems
    {
        public int id { get; set; }
        public int orderId { get; set; }
        public int medicineId { get; set; }
        public decimal unitPrice { get; set; }
        public int quantity { get; set; }
        public decimal totalPrice { get; set; }
    }
}
