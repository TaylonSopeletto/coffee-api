using System;
namespace CoffeeApiV2.Models
{
	public class Order
	{
        public int ProductsPrice { get; set; }
        public List<Coffee>? Coffees { get; set; }
        public double TotalPrice { get; set; }
        public User ?User { get; set; }
        public double Tip { get; set; }
        public int Id { get; set; }
        public Address ?Address { get; set; }
        public string ?Status { get; set; }
        public string ?PaymentMethod { get; set; }
    }
}

