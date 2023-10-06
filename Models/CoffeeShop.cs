using System;
namespace CoffeeApiV2.Models
{
	public class CoffeeShop
	{
        public int Id { get; set; }
        public string? Name { get; set; }
        public float Rating { get; set; }
        public string? City { get; set; }
        public List<Rating>? Ratings { get; set; }
    }
}

