using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Richard2.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public int CompanyID { get; set; }

        public Employee(string line)
        {
            var split = line.Split(';');
            EmployeeID = Convert.ToInt32(split[0]);
            Name = split[1];
            Position = split[2];
            CompanyID = Convert.ToInt32(split[3]);
        }
    }
}
