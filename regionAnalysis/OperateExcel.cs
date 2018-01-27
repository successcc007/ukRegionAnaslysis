using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Cells;
using Aspose;

namespace regionAnalysis
{
    public class OperateExcel
    {
        public void Read(string filePath)
        {
            Workbook wk = new Workbook(filePath);
            WorkbookDesigner designer = new WorkbookDesigner(wb); 
         
            Worksheet sht = wk.Worksheets[0];//查看文档的sheet0内容  
            Cells cells = sht.Cells;//获取sheet0的所有单元格  
            if (sht == null)
            {
                return false;
            }
            int rowCount = cells.MaxDataRow + 1;//当Excel没有一行数据时，读取到的cells.MaxDataRow=-1，当有一行数据时cells.MaxDataRow=0     MaxDataRow：包含数据的单元格的最大行索引  

            int cellCount = cells.MaxDataColumn + 1;//当Excel没有一行数据时，读取到的cells.MaxDataRow=-1，当有一行数据时cells.MaxDataRow=0     MaxDataRow：包含数据的单元格的最大列索引  
            string title = cells[j, k].Value.ToString();//获取第j行k列单元格的内容  
        }
    }
}
