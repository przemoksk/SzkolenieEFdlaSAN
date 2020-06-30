using CarDealerView.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDealerView.ViewModels
{
    public class CarViewModel
    {
        public int ProductionYear { get; set; }
        public int Mileage { get; set; }
        public bool AccidentFree { get; set; }
        public FuelType FuelType { get; set; }
        public double Price { get; set; }

        public string PicturePath { get; set; }

        public IFormFile CarPicture { get; set; }
        public int CarModelId { get; set; }
        
        //public virtual CarModel CarModels { get; set; }
    }
}
