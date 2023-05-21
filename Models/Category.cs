using System;
using CoffeeApiV2.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CoffeeApiV2.Models
{
	public class Category
	{
        public int Id { get; set; }
        public string ?Name { get; set; }
        [JsonIgnore]
        public List<Coffee> ?Coffees { get; set; }

    }
}

