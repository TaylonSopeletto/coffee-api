using System;
using System.ComponentModel.DataAnnotations;
using CoffeeApiV2.Models;

namespace CoffeeApiV2.Models
{
	public class Coffee
	{
        public int Id { get; set; }
        public int Price { get; set; }
        public List<Category> ?Categories { get; set; }
    }
}

