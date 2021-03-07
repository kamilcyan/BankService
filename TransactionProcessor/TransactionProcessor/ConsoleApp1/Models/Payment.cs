using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankNamespace.Models
{
    public class Payment
    {
        [PrimaryKey]
        public int id { get; set; }
        public int ClientId { get; set; }
        public decimal Value { get; set; }
        public string AccountNumberFrom { get; set; }
        public string AccountNumberTo { get; set; }
        public string Currency { get; set; }
    }
}
