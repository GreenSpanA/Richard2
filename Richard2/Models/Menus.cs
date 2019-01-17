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
            Category = split[4];
            Comment = split[4];
            SizeComment = split[5];
            PriceComment = split[6];

            MenuLink = split[9];
            RestaurantName = split[10];
            MenuType = split[11];
            RestID = Convert.ToInt16(split[12]);
            UpdateTime = split[13];
        }
    }
}
