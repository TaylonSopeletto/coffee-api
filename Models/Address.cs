using System;
namespace CoffeeApiV2.Models
{
    public class Address
    {
        public int Id { get; set; }
        public User? User { get; set; }
        public string ?PostalCode { get; set; }
        public string ?Street { get; set; }
        public string ?State { get; set; }

    }
}

