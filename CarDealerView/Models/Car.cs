using CarDealerView.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CarDealerView.Models
{
    [Table("Cars")]
    public class Car
    {
        [Key]
        public int CarId { get; set; }
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType =
            typeof(CustomErrorMessages))]

        public int ProductionYear { get; set; }
        public int Mileage { get; set; }
        public bool AccidentFree { get; set; }
        public FuelType FuelType { get; set; }
        public double Price { get; set; }

        public string PicturePath { get; set; }
        
        [ForeignKey("CarModels")]
        public int CarModelId { get; set; }
        public virtual CarModel CarModels { get; set; }
    }
}
