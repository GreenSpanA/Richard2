using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Richard2.Models
{
    public class Company
    {        

        public int ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Town { get; set; }

        public Company (string line) {
            var split = line.Split(';');
            ID = Convert.ToInt32(split[0]);
            Name = split[1];
            Address = split[2];
            Town = split[3];
        }        
    }
}
