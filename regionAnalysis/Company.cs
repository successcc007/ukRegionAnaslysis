using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace regionAnalysis
{

    public class Company
    {
        /// <summary>
        /// 公司姓名
        /// </summary>
        string name = null;
        /// <summary>
        /// 年代和人数字典，key：year，value：num
        /// </summary>
        Dictionary<string, int> NumOfEmployees = new Dictionary<string, int>();
        /// <summary>
        /// 年代和省市人数字典
        /// </summary>
        Dictionary<string, Dictionary<string, ProvinceInfo>> YearProvince = new Dictionary<string, Dictionary<string, ProvinceInfo>>();        

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
        /// 设置该年代员工数
        /// </summary>
        /// <param name="year"></param>
        /// <param name="num"></param>
        public void SetEmployeesNum(string year, int num)
        {
            if (NumOfEmployees.Keys.Contains(year))
            {
                int n = NumOfEmployees[year] + num;
                NumOfEmployees[year] = n;
            }
            else
            {
                NumOfEmployees.Add(year, num);
            }
        }

        /// <summary>
        /// 设置年代、省、市人数
        /// </summary>
        /// <param name="pro"></param>
        /// <param name="city"></param>
        /// <param name="year"></param>
        /// <param name="numP"></param>
        /// <param name="numC"></param>
        public void SetRegionEmployeesNum(string pro, string city, string year, int numP = 1, int numC = 1)
        {
            if (YearProvince.Keys.Contains(year))
            {
                if (YearProvince[year].Keys.Contains(pro))
                {
                    bool noExist = true;
                    var cl = YearProvince[year][pro].cityList;
                    for (int i = 0; i < cl.Count; i++)
                    {
                        if (cl[i].name == city)
                        {
                            noExist = false;
                            int n = cl[i].num;
                            YearProvince[year][pro].cityList[i].num = n + numC;
                            break;
                        }
                    }
                    if (noExist)
                    {
                        CityInfo c = new CityInfo();
                        c.num = numC;
                        c.name = city;
                        YearProvince[year][pro].cityList.Add(c);
                    }
                    var pn = YearProvince[year][pro].num;
                    YearProvince[year][pro].num = pn + 1;
                }
                else
                {
                    CityInfo c = new CityInfo();
                    c.num = numC;
                    c.name = city;
                    List<CityInfo> cl = new List<CityInfo>();
                    cl.Add(c);
                    ProvinceInfo p = new ProvinceInfo();
                    p.name = pro;
                    p.year = year;
                    p.num = numP;
                    p.cityList = cl;
                    YearProvince[year].Add(pro, p);
                }
            }
            else
            {
                CityInfo c = new CityInfo();
                c.num = numC;
                c.name = city;
                List<CityInfo> cl = new List<CityInfo>();
                cl.Add(c);
                ProvinceInfo p = new ProvinceInfo();
                p.name = pro;
                p.year = year;
                p.num = numP;
                p.cityList = cl;
                var ProvinceNum = new Dictionary<string, ProvinceInfo>();
                ProvinceNum.Add(pro, p);
                YearProvince.Add(year, ProvinceNum);
            }
        }
        /// <summary>
        ///获得省列表
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<string> GetProList(string year)
        {
            List<string> pl = new List<string>();
            foreach (var i in YearProvince[year]?.Values?.ToList())
            {
                pl.Add(i.name);
            }
            return pl;
        }
        /// <summary>
        /// 根据年代,省获得市列表
        /// </summary>
        /// <param name="pro"></param>
        /// <returns></returns>
        public List<string> GetCityList(string year,string pro)
        {
            var cl= YearProvince[year][pro].cityList;
            List<string> cnameL = new List<string>();
            foreach (var i in cl)
            {
                cnameL.Add(i.name);
            }
            return cnameL;
        }
        /// <summary>
        /// 获得年代列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetYearList()
        {
            return YearProvince.Keys.ToList();
        }
        /// <summary>
        /// 取某年某省总人数
        /// </summary>
        /// <param name="pro"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public int GetProvinceEmployeesNum(string pro,string year)
        {
            return YearProvince[year]?[pro]?.num??0;            
        }
        /// <summary>
        /// 取某年某省某市总人数
        /// </summary>
        /// <param name="pro"></param>
        /// <param name="city"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public int GetCityEmployeesNum(string pro,string city,string year)
        {
            foreach (var c in YearProvince[year]?[pro]?.cityList)
            {
                if (c.name == city)
                {
                    return c.num;
                }
            }
            return 0;
        }
    }
    class ProvinceInfo
    {
        public string name;
        public string year;
        public int num;
        public List<CityInfo> cityList;
    }
    class CityInfo
    {
        public string name;
        public int num;
    }


}
