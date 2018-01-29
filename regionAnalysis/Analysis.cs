using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace regionAnalysis
{
    class Analysis
    {
        Dictionary<string, Company> companyDict = new Dictionary<string, Company>();
        List<Employees> employeesList = new List<Employees>();
        /// <summary>
        /// 得到公司列表和人员列表
        /// </summary>
        /// <param name="filePath"></param>
        public void GetList(string filePath)
        {           
            ReadExcel excel = new ReadExcel(filePath);
            excel.SetPageNum(0);
            int rowNum = excel.RowCount;
            int colNum = excel.CellCount;
            for (int i = 1; i < rowNum; i++)
            {
                var companyName = excel.Read(i, 0);
                var year = excel.Read(i, 1);
                var employees = excel.Read(i, 2);
                var province = excel.Read(i, 3);
                var city = excel.Read(i, 4);
                if (companyDict.Keys.Contains(companyName))
                {                   
                    companyDict[companyName].SetEmployeesNum(year, 1);
                }
                else
                {
                    Company com = new Company(companyName);
                    com.SetEmployeesNum(year, 1);
                    companyDict.Add(companyName, com);
                }


            }
        }

    }
    class Model
    {
        string company;
        string year;
        string province;
        string provNum;
        string provProportion;
        string city;
        string cityNum;
        string cityProportion;
    }
}
