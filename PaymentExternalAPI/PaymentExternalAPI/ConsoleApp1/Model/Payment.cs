using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Model
{
    public class Payment
    {
        public int ClientId { get; set; }
        public decimal Value { get; set; }
        public string AccountNumberFrom { get; set; }
        public string AccountNumberTo { get; set; }
        public string Currency { get; set; }
    }
}
