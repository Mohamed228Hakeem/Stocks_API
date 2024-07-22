using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table("Stocks")]
    public class Stocks
    {
        public int Id { get; set; }
        
        public string  Symbol { get; set; } = string.Empty;

        public string CompanyName { get; set; } = string.Empty;
        [Column (TypeName = "decimal(18,2)")]
        public decimal Purchase { get; set; }

        public decimal LastDiv { get; set; }

        public string Industry { get; set; } = string.Empty;

        public long MarketCap { get; set; }
        //this is to represent a one to many relationship as in every stock can contain multiple comments objects
        public List<Comment> comments { get; set; } = new List<Comment>();

        public List<Portfolio> portfolios{get;set;} = new List<Portfolio>();
    }
}