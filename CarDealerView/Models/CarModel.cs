using CarDealerView.Resources;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace CarDealerView.Models
{
    [Table("CarModels")]
    public class CarModel

    {
        [Key]
        public int CarModelId { get; set; }
        [Required(ErrorMessageResourceName ="Required", ErrorMessageResourceType =
            typeof(CustomErrorMessages))]
        [StringLength(20, ErrorMessageResourceName = "ManufacturerNameTooLong",
            ErrorMessageResourceType = typeof(CustomErrorMessages))]
        public string Manufacturer { get; set; }
        [Required]
        public string Model { get; set; }
        
        [NotMapped]
        public string FullModelName { get { return Manufacturer + " " + Model; } }
    }
}
