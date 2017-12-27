using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace investrs.Models
{
    public class ItemPriceHistory
    {
        [Key]
        public int ItemPriceHistoryID { get; set; }
        [ForeignKey("Item")]
        public int ItemID { get; set; }
        public DateTime Date { get; set; }
        public int Price { get; set; } // GE Market price "daily"
        public int AveragePrice { get; set; } // Provided calculated trend price "average

        public virtual Item Item { get; set; }

    }
}
