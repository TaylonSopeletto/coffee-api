﻿using System;
namespace CoffeeApiV2.DTOs
{
	public class UserDTO
	{
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}

