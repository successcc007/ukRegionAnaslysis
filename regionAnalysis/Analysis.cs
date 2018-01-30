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
        List<Model> modelList = new List<Model>();
     
        /// <summary>
        /// 得到公司列表
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
                if (companyDict.Keys.Contains(companyName))
                {
                    companyDict[companyName].SetEmployeesNum(year, 1);
                    companyDict[companyName].SetRegionEmployeesNum(province, city, year, 1, 1);
                }
                else
                {
                    Company com = new Company(companyName);
                    com.SetEmployeesNum(year, 1);
                    com.SetRegionEmployeesNum(province, city, year, 1, 1);
                    companyDict.Add(companyName, com);
                }          
            }
        }

        public void SetModel()
        {
            #region
            //foreach (var e in employeesList)
            //{
            //    if (companyYear.Keys.Contains(e.Company))
            //    {
            //        var d1 = companyYear[e.Company];
            //        if (d1.Keys.Contains(e.WorkYear))
            //        {
            //            var d2 = d1[e.WorkYear];
            //            if (d2.Keys.Contains(e.Province))
            //            {
            //                var d3 = d2[e.Province];
            //                if (d3.Keys.Contains(e.City))
            //                {
            //                    var cn = d3[e.City];
            //                    companyYear[e.Company][e.WorkYear][e.Province][e.City] = cn + 1;
            //                }
            //                else
            //                {
            //                    companyYear[e.Company][e.WorkYear][e.Province].Add(e.City, 1);
            //                }

            //            }
            //            else
            //            {
            //                cityNum.Add(e.City, 1);
            //                companyYear[e.Company][e.WorkYear].Add(e.Province, cityNum);
            //            }
            //        }
            //        else
            //        {
            //            cityNum.Add(e.City, 1);
            //            provinceCity.Add(e.Province, cityNum);
            //            companyYear[e.Company].Add(e.WorkYear, provinceCity);
            //        }
            //    }
            //    else
            //    {
            //        cityNum.Add(e.City, 1);
            //        provinceCity.Add(e.Province, cityNum);
            //        yearProvince.Add(e.WorkYear, provinceCity);
            //        companyYear.Add(e.Company, yearProvince);
            //    }

            //}
            #endregion
            for (var i = 0; i < companyDict.Count; i++)
            {
                var celement = companyDict.ElementAt(i);
                var ckey = celement.Key;
                var cvalue = celement.Value;
                var yearList = cvalue.GetYearList();
                foreach (var y in yearList)
                {
                    var proList = cvalue.GetProList(y);
                    foreach (var p in proList)
                    {
                        var cList = cvalue.GetCityList(y, p);
                        foreach (var c in cList)
                        {
                            Model m = new Model();
                            m.company = ckey;
                            m.year = y;
                            var allNum = cvalue.GetNumByYear(y);
                            m.allNum = allNum.ToString();
                            m.province = p;
                            var pNum = cvalue.GetProvinceEmployeesNum(p, y);
                            m.provNum = pNum.ToString();  
                            m.provProportion = (((double)pNum / allNum)).ToString("0.000000");
                            m.city = c;
                            var cNum = cvalue.GetCityEmployeesNum(p, c, y);
                            m.cityNum = cNum.ToString();
                            m.cityProportion = ((double)cNum / allNum).ToString("0.000000");
                            modelList.Add(m);
                        }
                    }
                }
            }    
        }

        public bool Save(string filePath)
        {
            WriteExcel ew = new WriteExcel();
            ew.Write(0, 0, "公司名称");
            ew.Write(0, 1, "年代");
            ew.Write(0, 2, "总人数");
            ew.Write(0, 3, "省");
            ew.Write(0, 4, "人数");
            ew.Write(0, 5, "比例");
            ew.Write(0, 6, "市");
            ew.Write(0, 7, "人数");
            ew.Write(0, 8, "比例");
            int i = 1;
            foreach (var m in modelList)
            {
                ew.Write(i, 0, m.company);
                ew.Write(i, 1, m.year);
                ew.Write(i, 2, m.allNum);
                ew.Write(i, 3, m.province);
                ew.Write(i, 4, m.provNum);
                ew.Write(i, 5, m.provProportion);
                ew.Write(i, 6, m.city);
                ew.Write(i, 7, m.cityNum);
                ew.Write(i, 8, m.cityProportion);
                i++;
            }
            ew.Save(filePath);
            return true;
        }


    }
    class Model
    {
        public string company;
        public string year;
        public string allNum;
        public string province;
        public string provNum;
        public string provProportion;
        public string city;
        public string cityNum;
        public string cityProportion;
    }
    class YearCompany
    {
        string year;
        string company;
    }
}
