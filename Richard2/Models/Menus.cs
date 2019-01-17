using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Richard2.Models
{
    public class Menus
    {       
        public string ItemName { get; set; }
        public string Description { get; set; }
        public string VegComment { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }
        public string Comment { get; set; }
        public string SizeComment { get; set; }
        public string PriceComment { get; set; }

        public string MenuLink { get; set; }
        public string RestaurantName { get; set; }
        public string MenuType { get; set; }
        public int RestID { get; set; }
        public string UpdateTime { get; set; }       

        public Menus(string line)
        {
            var split = line.Split(';');

            ItemName = split[0]; 
            Description = split[1];
            VegComment = split[2];
            Price = Convert.ToDouble(split[3]);          
            Category = split[5];
            Comment = split[6];
            SizeComment = split[7];
            PriceComment = split[8];

            MenuLink = split[10];
            RestaurantName = split[11];
            MenuType = split[12];
            RestID = Convert.ToInt16(split[13]);
            UpdateTime = split[14];
        }
    }
}
