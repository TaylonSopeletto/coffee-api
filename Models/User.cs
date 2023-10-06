using System;
using System.Text.Json.Serialization;

namespace CoffeeApiV2.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        [JsonIgnore]
        public string PasswordHash { get; set; } = string.Empty;
    }
}