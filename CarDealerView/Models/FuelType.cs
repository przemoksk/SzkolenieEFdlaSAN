using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarDealerView.Models
{
    public enum FuelType
    {
        Petrol,
        [Display(Name ="Opis diesla")]
        Diesel,
        LPG,
        Hybrid,
        Electric
    }
}
