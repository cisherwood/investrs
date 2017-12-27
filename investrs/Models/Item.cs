using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace investrs.Models
{
    public class Item
    {
        [Key]
        public int ItemID { get; set; }
        [ForeignKey("Category")]
        public int CategoryID { get; set; }

        [MaxLength(500)]
        [Url]
        public string Icon { get; set; }
        [MaxLength(500)]
        [Url]
        public string IconLarge { get; set; }
        [MaxLength(500)]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        public bool IsMembersItem { get; set; }

        public virtual Category Category { get; set; }

        // Example class model

        /*
         * {
                    "item": {
                    "icon": "http://services.runescape.com/m=itemdb_rs/1513605642596_obj_sprite.gif?id=40926",
                    "icon_large": "http://services.runescape.com/m=itemdb_rs/1513605642596_obj_big.gif?id=40926",
                    "id": 40926,
                    "type": "Melee armour - high level",
                    "typeIcon": "http://www.runescape.com/img/categories/Melee armour - high level",
                    "name": "Bandos armour set",
                    "description": "Grand Exchange set containing a Bandos helmet, chestplate, tassets, gloves, boots and warshield.",
                    "current": {
                    "trend": "neutral",
                    "price": "6.4m"
                    },
                    "today": {
                    "trend": "neutral",
                    "price": 0
                    },
                    "members": "true",
                    "day30": {
                    "trend": "positive",
                    "change": "+21.0%"
                    },
                    "day90": {
                    "trend": "negative",
                    "change": "-13.0%"
                    },
                    "day180": {
                    "trend": "negative",
                    "change": "-90.0%"
                    }
                    }
                    }
         
         */

    }
}
