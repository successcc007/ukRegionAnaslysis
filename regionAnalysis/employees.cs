using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace regionAnalysis
{
  
    class Employees
    {
        /// <summary>
        /// 姓名
        /// </summary>
        string name = null;
        /// <summary>
        /// 所属公司
        /// </summary>        
        string company = null;
        /// <summary>
        /// 工作年份
        /// </summary>
        int workYear = 0;
        /// <summary>
        /// 所属省份
        /// </summary>
        string province = null;
        /// <summary>
        /// 所属市
        /// </summary>
        string city = null;
        public Employees(string companyName,int year,string name)
        {
            this.company = companyName;
            this.workYear = year;
            this.name = name;
        }
        public string Province
        {
            set { this.province = value; }
            get { return province; }
        }
        public string City
        {
            set { this.city = value; }
            get { return city; }
        }
        public string Company
        {
            set { this.company = value; }
            get { return company; }
        }
        public int WorkYear
        {
            set { this.workYear = value; }
            get { return workYear; }
        }
        public string Name
        {
            set { this.name = value; }
            get { return name; }
        }


    }
}
