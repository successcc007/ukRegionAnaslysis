using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace regionAnalysis
{
    public partial class Form1 : Form
    {
        private string openFileName = null;
        private string saveFileName = null;
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 选择分析文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "所有文件(*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string file = fileDialog.FileName;
                LableOpenFileName.Text = file;
                openFileName = file;
                OperateExcel.Read(file);
            }
        }
        /// <summary>
        /// 保存文件位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";
            string name = @"\123.xlsx";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string foldPath = dialog.SelectedPath+name;
                LableSaveFileName.Text = foldPath;
                saveFileName = foldPath;
                OperateExcel.Write(foldPath);
            }
        }




    }
}
