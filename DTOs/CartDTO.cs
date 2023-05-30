using System;
using CoffeeApiV2.Models;
namespace CoffeeApiV2.DTOs
{
	public class CartDTO
	{
        public List<Coffee> ?Coffees { get; set; }
        public double TotalPrice { get; set; }
        public int ProductsPrice { get; set; }
        public double Tip { get; set; }
    }
}

