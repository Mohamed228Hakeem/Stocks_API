using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Stock
{
    public class CreateStockRequest
    {
        //we add the data validation in the DTO not in the Model

        [Required]
        [MaxLength(255, ErrorMessage = "Symbol must be less than 255 characters")]
        public string  Symbol { get; set; } = string.Empty;


        [Required]
        [MaxLength(255, ErrorMessage = "Company Name must be less than 255 characters")]
        public string CompanyName { get; set; } = string.Empty;
        
        [Required]
        [Range(1,10000000000)]
        public decimal Purchase { get; set; }

        [Required]
        [Range(1,10000000000)]
        public decimal LastDiv { get; set; }

        [Required]
        [MaxLength(10000,ErrorMessage ="This Doesnt Exist My kind Sir !")]
        public string Industry { get; set; } = string.Empty;
        [Required]
        [Range(1,100000000000)]
        public long MarketCap { get; set; }
    }
}