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
        List<ModelCeo> ceoModelList = new List<ModelCeo>();

        /// <summary>
        /// 得到公司列表
        /// </summary>
        /// <param name="filePath"></param>
        public void GetList(string filePath)
        {
            ReadExcel excel = new ReadExcel(filePath);
            //董事会表
            excel.SetPageNum(0);
            int rowNum = excel.RowCount;
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
            //seo表
            excel.SetPageNum(1);
            int rowNumSheet2 = excel.RowCount;
            for (int i = 1; i < rowNumSheet2; i++)
            {
                var companyName = excel.Read(i, 0);
                var year = excel.Read(i, 1);
                var ceoName = excel.Read(i, 2);
                var province = excel.Read(i, 3);
                var city = excel.Read(i, 4);
                if (companyDict.Keys.Contains(companyName))
                {
                    companyDict[companyName].SetCeoInfo(province, city, year, ceoName);
                }
                else
                {
                    Company com = new Company(companyName);
                    //com.SetEmployeesNum(year, 1);
                    //com.SetRegionEmployeesNum(province, city, year, 1, 1);
                    com.SetCeoInfo(province, city, year, ceoName);
                    companyDict.Add(companyName, com);
                }

            }

        }



        public void SetModel()
        {
            //董事会表和ceo表        
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
                            ModelCeo mc = new ModelCeo();
                            m.company = ckey;
                            mc.company = ckey;
                            m.year = y;
                            mc.year = y;
                            var allNum = cvalue.GetNumByYear(y);
                            m.allNum = allNum.ToString();
                            m.province = p;
                            mc.province = p;
                            var pNum = cvalue.GetProvinceEmployeesNum(p, y);
                            m.provNum = pNum.ToString();
                            m.provProportion = (((double)pNum / allNum)).ToString("0.000000");
                            m.city = c;
                            mc.city = c;
                            var cNum = cvalue.GetCityEmployeesNum(p, c, y);
                            m.cityNum = cNum.ToString();
                            m.cityProportion = ((double)cNum / allNum).ToString("0.000000");
                            mc.inPro = "0";
                            mc.inCity = "0";
                            var clist = cvalue.GetCeoList(y);
                            if (clist != null)
                            {
                                foreach (var ceo in clist)
                                {
                                    if (ceo.province == p)
                                    {
                                        mc.inPro = "1";
                                    }
                                    if (ceo.city == c)
                                    {
                                        mc.inCity = "1";
                                    }
                                }
                            }                            
                            ceoModelList.Add(mc);
                            modelList.Add(m);
                        }
                    }
                }
            }
        }

        public bool Save(string filePath)
        {
            WriteExcel ew = new WriteExcel();
            ew.SetPageNum(0);
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
            ew.SetPageNum(1);
            ew.Write(0, 0, "公司名称");
            ew.Write(0, 1, "年代");
            ew.Write(0, 2, "省");
            ew.Write(0, 3, "是否有CEO");
            ew.Write(0, 4, "市");
            ew.Write(0, 5, "是否有CEO");
            int j = 1;
            foreach (var mc in ceoModelList)
            {
                ew.Write(j, 0, mc.company);
                ew.Write(j, 1, mc.year);
                ew.Write(j, 2, mc.province);
                ew.Write(j, 3, mc.inPro);
                ew.Write(j, 4, mc.city);
                ew.Write(j, 5, mc.inCity);
                j++;
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
    class ModelCeo
    {
        public string company;
        public string year;
        public string province;
        public string inPro;
        public string city;
        public string inCity;
    }

}
