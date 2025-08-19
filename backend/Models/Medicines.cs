using System;
using System.Collections.Generic;
using System.Linq;

namespace backend.Models
{
    public class Medicines
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string manufaturer { get; set; }
        public decimal unitPrice { get; set; }
        public decimal discount { get; set; }
        public DateTime expDate { get; set; }
        public string imgURL { get; set; }

    }
}
