using System;
using CoffeeApiV2.Models;

namespace CoffeeApiV2.DTOs
{
    public class CoffeeCategoryDTO
    {
        public int Id { get; set; }
    }
    public class EditCoffeeDTO
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public string? Name { get; set; }
        public List<CoffeeCategoryDTO> ?Categories { get; set; }
    }
}

