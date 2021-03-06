using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientNamespace
{
    public class ClientPayment
    {
        public int ClientId { get; set; }
        public decimal Value { get; set; }
        public string AccountNumberFrom { get; set; }
        public string AccountNumberTo { get; set; }
        public string Currency { get; set; }
    }
}
