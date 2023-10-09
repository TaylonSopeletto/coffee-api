using System;
using System.Text.Json.Serialization;
namespace CoffeeApiV2.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string ?PostalCode { get; set; }
        public string ?Street { get; set; }
        public string ?State { get; set; }
        [JsonIgnore]
        public User? User { get; set; }

    }
}

