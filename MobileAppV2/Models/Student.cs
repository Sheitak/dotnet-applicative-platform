﻿namespace MobileAppV2.Models
{
    internal class Student
    {
        public int StudentID { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public bool? IsActive { get; set; }

        public string MacAdress { get; set; }

        public override string ToString()
        {
            return $"StudentID: {StudentID}, Firstname: {Firstname}, Lastname: {Lastname}, IsActive: {IsActive}, MacAdress: {MacAdress}";
        }
    }
}
