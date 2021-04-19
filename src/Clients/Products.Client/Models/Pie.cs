﻿namespace Products.Client.Models
{
    public class Pie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public byte[] Image { get; set; }
        public int TypeId { get; set; }
    }
}
