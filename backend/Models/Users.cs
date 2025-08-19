using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Users
    {
        public int ID { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public decimal fund { get; set; }
        public string type { get; set; }
        public int status { get; set; }
        public DateTime? createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
    }
}
