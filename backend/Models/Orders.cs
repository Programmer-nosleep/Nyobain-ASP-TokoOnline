using System;

namespace backend.Models
{
    public class Orders
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string orderNo { get; set; }
        public decimal orderTotal { get; set; }
        public string orderStatus { get; set; }
    }
}
