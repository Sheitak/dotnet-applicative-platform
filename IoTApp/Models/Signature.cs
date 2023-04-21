using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTApp.Models
{
    internal class Signature
    {
        //public int SignatureID { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsPresent { get; set; }

        //public int StudentID { get; set; }

        //public Student Student { get; set; }
    }
}
