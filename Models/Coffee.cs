using System;
using System.ComponentModel.DataAnnotations;
using CoffeeApiV2.Models;
using System.Text.Json.Serialization;

namespace CoffeeApiV2.Models
{
	public class Coffee
	{
        public int Id { get; set; }
        public int Price { get; set; }
        public string ?Name { get; set; }
        public string ?Description { get; set; }
        public List<Category> ?Categories { get; set; }
        [JsonIgnore]
        public List<Order> ?Orders { get; set; }
        
    }
}

