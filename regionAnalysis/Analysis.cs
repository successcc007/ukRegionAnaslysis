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
                var name = excel.Read(i, 0);
                var year = excel.Read(i, 1);

                if (companyDict.Keys.Contains(name))
                {                   
                    companyDict[name].SetEmployeesNum(year, 1);
                }
                else
                {
                    Company com = new Company(name);
                    com.SetEmployeesNum(year, 1);
                    companyDict.Add(name, com);
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
