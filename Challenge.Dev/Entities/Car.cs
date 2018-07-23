using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Challenge.Dev.Models
{
    public class Car
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Brand is required")]
        [StringLength(20, ErrorMessage = "Brand may not be longer than 20 characters")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Model is required")]
        [StringLength(20, ErrorMessage = "Model may not be longer than 20 characters")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Plate Number is required")]
        [StringLength(10, ErrorMessage = "Plate Number may not be longer than 10 characters")]
        public string PlateNumber { get; set; }

        [ForeignKey("User")]
        public long IdUser { get; set; }
        public virtual User User { get; set; }
    }
}
