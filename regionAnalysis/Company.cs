using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace regionAnalysis
{
    class Company
    {
        /// <summary>
        /// 公司姓名
        /// </summary>
        string name = null;
        /// <summary>
        /// 年代和人数字典，key：year，value：num
        /// </summary>
        Dictionary<string, int> NumOfEmployees = new Dictionary<string, int>();

        public Company(string name)
        {
            this.name = name;
        }
        public string Name
        {
            get { return name; }
        }
        /// <summary>
        /// 根据年代获得员工数
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public int GetNumByYear(string year)
        {
            int num = 0;
            if (NumOfEmployees.Keys.Contains(year))
            {
                num = NumOfEmployees[year];
            }
            return num;
        }
        /// <summary>
        /// 增加该年代员工数
        /// </summary>
        /// <param name="year"></param>
        /// <param name="num"></param>
        public void SetEmployeesNum(string year ,int num)
        {
            if (NumOfEmployees.Keys.Contains(year))
            {
                int n = NumOfEmployees[year]+num;
                NumOfEmployees[year] = n;
            }
            else
            {
                NumOfEmployees.Add(year, num);
            }
        }

    }
}
