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
        List<Model> modelList = new List<Model>();
        //市和人数
        Dictionary<string, int> cityNum = new Dictionary<string, int>();
        //省和市
        Dictionary<string, Dictionary<string, int>> provinceCity = new Dictionary<string, Dictionary<string, int>>();
        //年代和省
        Dictionary<string, Dictionary<string, Dictionary<string, int>>> yearProvince = new Dictionary<string, Dictionary<string, Dictionary<string, int>>>();
        //公司和年代
        Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, int>>>> companyYear = new Dictionary<string, Dictionary<string, Dictionary<string, Dictionary<string, int>>>>();
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
                var employeesName = excel.Read(i, 2);
                var province = excel.Read(i, 3);
                var city = excel.Read(i, 4);
                //if (companyDict.Keys.Contains(companyName))
                //{
                //    companyDict[companyName].SetEmployeesNum(year, 1);
                //}
                //else
                //{
                //    Company com = new Company(companyName);
                //    com.SetEmployeesNum(year, 1);
                //    companyDict.Add(companyName, com);
                //}
                Employees em = new Employees(companyName, year, employeesName);
                em.Province = province;
                em.City = city;
                employeesList.Add(em);
            }
        }

        public void SetModel()
        {
            foreach(var e in employeesList)
            {                
                if (companyYear.Keys.Contains(e.Company))
                {
                    var d1 = companyYear[e.Company];
                    if (d1.Keys.Contains(e.WorkYear))
                    {
                        var d2 = d1[e.WorkYear];
                        if (d2.Keys.Contains(e.Province)){
                            var d3 = d2[e.Province];
                            if (d3.Keys.Contains(e.City))
                            {
                                var cn = d3[e.City];
                                companyYear[e.Company][e.WorkYear][e.Province][e.City] = cn + 1;
                            }
                            else
                            {
                                companyYear[e.Company][e.WorkYear][e.Province].Add(e.City, 1);
                            }

                        }
                        else
                        {
                            cityNum.Add(e.City, 1);
                            companyYear[e.Company][e.WorkYear].Add(e.Province, cityNum);
                        }
                    }
                    else
                    {
                        cityNum.Add(e.City, 1);
                        provinceCity.Add(e.Province, cityNum);
                        companyYear[e.Company].Add(e.WorkYear, provinceCity);
                    }
                }
                else
                {
                    cityNum.Add(e.City, 1);
                    provinceCity.Add(e.Province, cityNum);
                    yearProvince.Add(e.WorkYear, provinceCity);
                    companyYear.Add(e.Company, yearProvince);
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
    class YearCompany
    {
        string year;
        string company;
    }
}
