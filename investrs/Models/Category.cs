using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace investrs.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        [Range(0,100)]
        public int Number { get; set; }

    }
}
