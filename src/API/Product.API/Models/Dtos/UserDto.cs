﻿namespace Product.API.Models.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }

        public string Token { get; set; }
    }
}
