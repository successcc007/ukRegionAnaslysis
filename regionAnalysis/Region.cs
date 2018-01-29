using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace regionAnalysis
{
    class Region
    {
        string province;
        string city;
        string employeesNumByProv;
        string employeesNumByCity;
        
        public string Province
        {
            get { return this.province; }
            set { this.province = value; }
        }
        public string City
        {
            get { return this.city; }
            set { this.city = value; }
        }
        public string EmployeesNumByProv
        {
            get { return this.city; }
            set { this.city = value; }
        }
        public string EmployeesNumByCity
        {
            get { return this.city; }
            set { this.city = value; }
        }
    }
}
