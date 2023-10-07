using System;
using CoffeeApiV2.Models;
namespace CoffeeApiV2.DTOs
{
    public class AddressDTO
    {
        public int Id { get; set; }
        public string? PostalCode { get; set; }
        public string? Street { get; set; }
        public string? State { get; set; }
    }
}

