using System;
using CoffeeApiV2.Models;
namespace CoffeeApiV2.DTOs
{
    public class OrderCoffeeDTO
    {
        public int Id { get; set; }
    }

    public class OrderAddressDTO
    {
        public int Id { get; set; }
    }
    public class OrderDTO
    {
        public List<OrderCoffeeDTO> ?Coffees { get; set; }
        public OrderAddressDTO? Address { get; set; }
        public string ?PaymentMethod { get; set; }
    }
}

