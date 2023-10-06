using System;
namespace CoffeeApiV2.Models
{
	public class Rating
	{
        public int Id { get; set; }
        public string Comment { get; set; } = string.Empty;
        public int Star { get; set; }
        public User ?User { get; set; }
        
    }
}

