using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Stock
{
    public class UpdateStockRequestDto
    {
        [Required]
        [MaxLength(255, ErrorMessage = "Symbol must be less than 255 characters")]
        public string  Symbol { get; set; } = string.Empty;
        [Required]
        [MaxLength(255, ErrorMessage = "Company must be less than 255 characters")]
        public string CompanyName { get; set; } = string.Empty;
        [Required]
        [Range(1,10000000000)]
        public decimal Purchase { get; set; }
        [Required]
        [Range(1,10000000000)]
        public decimal LastDiv { get; set; }
        [Required]
        [MaxLength(255, ErrorMessage = "Industry must be less than 255 characters")]
        public string Industry { get; set; } = string.Empty;
        [Required]
        [Range(1,10000000000)]
        public long MarketCap { get; set; }
    }
}