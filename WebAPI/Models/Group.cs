﻿namespace WebAPI.Models
{
    public class Group
    {
        public int GroupID { get; set; }

        public string Name { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
