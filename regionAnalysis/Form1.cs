using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace regionAnalysis
{
    public partial class Form1 : Form
    {
        private string openFileName = null;
        private string saveFileName = null;
        bool over = false;
        System.Timers.Timer t;
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
                LableOpenFileName.Location = new Point(LableOpenFileName.Location.X - 80, LableOpenFileName.Location.Y);
                LableOpenFileName.Text = file;
                openFileName = file;               
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
            string name = @"\result.xlsx";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string foldPath = dialog.SelectedPath+name;
                LableSaveFileName.Location = new Point(LableSaveFileName.Location.X - 80, LableSaveFileName.Location.Y);
                LableSaveFileName.Text = foldPath;
                saveFileName = foldPath;              
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label3.Text = "分析计算中";
            Thread th = new Thread(start);
            th.Start();          
        }
        public void start()
        {
            Analysis a = new Analysis();
            a.GetList(openFileName);
            a.SetModel();
            if (a.Save(saveFileName))
            {
                SetTB();
            }
        }

        /// <summary>
        /// 定义委托事件
        /// </summary>
        private delegate void SetTBMethodInvok();
        private void SetTB()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SetTBMethodInvok(SetTB));
            }
            else
            {
                label3.Text = "完成";
            }
        }


    }
}
