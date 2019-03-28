using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DriverRegistration.Domain.Entities
{
    public class Car
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Plate { get; set; }
    }
}
