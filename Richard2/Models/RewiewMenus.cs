using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Richard2.Models
{
    public class RewiewMenus
    {
        public string ItemName { get; set; }
        public string Description { get; set; }
        public string VegComment { get; set; }
        public string Price { get; set; }
        public string Category { get; set; }
       

        public RewiewMenus(string line)
        {
            var split = line.Split(';');

            ItemName = split[0];
            Description = split[1];
            VegComment = split[2];
            Price = split[3];
            Category = split[4];            
        }
    }
}
