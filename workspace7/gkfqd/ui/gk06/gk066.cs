using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gkfqd.ui.gk06
{
    public partial class gk066 : Form
    {
        public  string title = "";
        public  int   FontHeight = 200;
        public  int   FontWidth = 170;
        public double scaleNumerator  = 0;
        public double scaleDenominato = 0;
        public Color selectColor;
        public gk066()
        {
            InitializeComponent();
           //比例尺分子设定为1 为只读
            textBox4.Text = "1";
            textBox4.ReadOnly = true;
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" ){
                MessageBox.Show("请输入标题！");
                return;
            }
            if (textBox2.Text == "" ){
                MessageBox.Show("请输入标题高度！");
                return;
            }
            if (textBox3.Text == "" ){
                MessageBox.Show("请输入标题宽度！");
                return;
            }
            if (textBox4.Text == "" ){
                MessageBox.Show("请输入比例尺！");
                return;
            }
            title = textBox1.Text;
            FontHeight = int.Parse(textBox2.Text);
            FontWidth = int.Parse(textBox3.Text);
            scaleNumerator = int.Parse(textBox4.Text);
            scaleDenominato = int.Parse(textBox5.Text);
            selectColor = this.colorDialog1.Color;
            this.Close();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            gkfqd.Common.Tool.IsNumber(e, sender);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            gkfqd.Common.Tool.IsNumber(e, sender);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            selectColor = this.colorDialog1.Color;
        }

       
    }
}
