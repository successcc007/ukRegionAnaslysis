using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Cells;
using Aspose;
using System.Reflection;

namespace regionAnalysis
{
    public class ReadExcel
    {
        Workbook wk;
        Worksheet sht;
        int rowCount;
        int cellCount;
        public int RowCount
        {
            get { return rowCount; }
        }
        public int CellCount
        {
            get { return cellCount; }
        }
        public ReadExcel(string filePath)
        {          
            wk = new Workbook(filePath);
        }
        public void SetPageNum(int pageNum)
        {
            sht = wk.Worksheets[pageNum];
            Cells cells = sht.Cells;
            rowCount = cells.MaxDataRow + 1;
            cellCount = cells.MaxDataColumn + 1;
        }
        /// <summary>
        /// 获取第几行几列单元格内容
        /// </summary>
        /// <param name="rowNum"></param>
        /// <param name="columnNum"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        public string Read(int rowNum, int columnNum)
        {
            Cells cells = sht.Cells;
            string v = cells[rowNum, columnNum].Value.ToString();
            return v;
        }
    }
    //public string ReadAll(int pageNum = 0)
    //{
    //    Worksheet sht = wk.Worksheets[pageNum];//查看文档的sheet0内容  
    //    Cells cells = sht.Cells;//获取sheet0的所有单元格             
    //    int rowCount = cells.MaxDataRow + 1;//当Excel没有一行数据时，读取到的cells.MaxDataRow=-1，当有一行数据时cells.MaxDataRow=0     MaxDataRow：包含数据的单元格的最大行索引  
    //    int cellCount = cells.MaxDataColumn + 1;//当Excel没有一行数据时，读取到的cells.MaxDataRow=-1，当有一行数据时cells.MaxDataRow=0     MaxDataRow：包含数据的单元格的最大列索引             
    //    string contant = cells[rowNum, columnNum].Value.ToString();//获取第j行k列单元格的内容  
    //    return contant;
    //}

    public class WriteExcel
    {
        Workbook wk;
        public WriteExcel()
        {         
            wk = new Workbook();
            License l = new License();
            var filePath = AppDomain.CurrentDomain.BaseDirectory;
            l.SetLicense(filePath+"/License.lic");
        }
        /// <summary>
        /// 写入第几行几列单元格内容
        /// </summary>
        /// <param name="rowNum"></param>
        /// <param name="columnNum"></param>
        /// <param name="value"></param>
        /// <param name="pageNum"></param>
        public void Write(int rowNum, int columnNum, string value, int pageNum = 0)
        {
            Worksheet sheet = wk.Worksheets[pageNum];
            Cells cells = sheet.Cells;
            cells[rowNum, columnNum].PutValue(value);
        }
        /// <summary>
        /// 保存表格
        /// </summary>
        /// <param name="filePath"></param>
        public void Save(string filePath)
        {
            wk.Save(filePath);
        }

      
    }

}
