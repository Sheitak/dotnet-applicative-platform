﻿namespace WebAPI.Models
{
    public class Promotion
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Group>? Groups { get; set; }
    }
}
