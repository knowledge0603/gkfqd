using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Text;
using System.Collections;

namespace gkfqd.ui.gk01
{
    public partial class gk01f : Form
    {
        #region 变量区

        SaveFileDialog objFile = new SaveFileDialog();
        StreamWriter myStream;
        public string[] FontSizeName = { "初号", "小初", "一号", "小一", "二号", "小二", "三号", "小三", "四号", "小四", "五号", "小五", "六号", "小六", "七号", "八号", "8", "9", "10", "12", "14", "16", "18", "20", "22", "24", "26", "28", "36", "48", "72" };
        public float[] FontSize = { 42, 36, 26, 24, 22, 18, 16, 15, 14, 12, 10.5F, 9, 7.5F, 6.5F, 5.5F, 5, 8, 9, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
        //定义字号数组
        
        #endregion

        #region 初始化
        public gk01f()
        {
            InitializeComponent();
            System.Drawing.Text.InstalledFontCollection fonts = new System.Drawing.Text.InstalledFontCollection();
            foreach (string name in FontSizeName)
            {
                this.toolStripComboBox2.Items.Add(name);
            }
            //自动获取系统字体
            for (int i = 0; i < FontFamily.Families.Length; i++)
            {
                toolStripComboBox1.Items.Add(FontFamily.Families[i].Name);
            }
            toolStripComboBox1.SelectedIndex = 124;
            toolStripComboBox2.SelectedIndex = 10;

        }
        
        #endregion

        #region 保存报告内容并写入报告
        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            objFile.Filter = "TXT文本(.txt)|*.txt|Excel2003文件(.xls)|*.xls|PDF文件(.pdf)|*.pdf|Word2003文件(.doc)|*.doc|Excel2007文件(.xlsx)|*.xlsx|Word2007文件(.docx)|*.docx|其他文件(.*)|*.*";
            //设置默认文件类型显示顺序 
            objFile.FilterIndex = 1;
            //保存对话框是否记忆上次打开的目录 
            objFile.RestoreDirectory = true; 
            //点了保存按钮进入 
            if (objFile.ShowDialog() == DialogResult.OK)
            {
                myStream = new StreamWriter(objFile.FileName);
                myStream.WriteLine(textBox1.Text); //写入
                myStream.Close();//关闭流
                
                MessageBox.Show("保存完成");
            }
            
        }
        #endregion

        #region 工具栏
        // 鼠标放到按键字体变红
        private void toolStripLabel1_MouseEnter(object sender, EventArgs e)
        {
            toolStripLabel1.ForeColor = Color.Red;
        }
        // 鼠标离开按键字体变黑
        private void toolStripLabel1_MouseLeave(object sender, EventArgs e)
        {
            toolStripLabel1.ForeColor = Color.Black;
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Font = new Font(toolStripComboBox1.Text, textBox1.Font.Size, textBox1.Font.Style);
        }
        private void toolStripComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Font = new Font(textBox1.Font.FontFamily, FontSize[this.toolStripComboBox2.SelectedIndex], textBox1.Font.Style);
        }
        #endregion
    }
}
