using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    [Table ("Comments")]
    public class Comment
    {
            public int? StockId { get; set; }
            //navigation property to the stocks table
            public Stocks? Stock { get; set; }

            public int Id { get; set; }

            public string Content { get; set; } = string.Empty;
            public string Title { get; set; } = string.Empty;

            public DateTime CreatedOn { get; set; } = DateTime.Now;

            

    }
}