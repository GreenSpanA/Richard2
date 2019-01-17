using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Richard2.Models
{
    public class Restaurants
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        //public double Lat { get; set; }
        //public double Lng { get; set; }
        public string Neighborhood { get; set; }
 
        public Restaurants (string line)
        {
            var split = line.Split(';');
            ID = Convert.ToInt32(split[0]);
            Name = split[2];
            Website = split[3];
            //Lat = Convert.ToDouble(split[4]);
            //Lng = Convert.ToDouble(split[5]);
            Neighborhood = split[6];

        }
    }
}
