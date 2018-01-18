using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.OracleClient;
using gkfqd.ui.gk01;

namespace gkfqd
{
    public partial class login : DevExpress.XtraEditors.XtraForm
    {
        StringBuilder sqlQuery = new StringBuilder();
        DataSet dataSet = new DataSet();
        public static string userId = "";
        public static string userRole = "";
       
        public login()
        {
            InitializeComponent();
           
            this.MaximizeBox = false;//隐藏最大化
            this.MinimizeBox = false;//隐藏最小化
            this.FormBorderStyle = FormBorderStyle.FixedSingle;//固定窗体大小不变
            this.StartPosition = FormStartPosition.CenterScreen;//窗体居中
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        
          //  OleDbConnection conn = new OleDbConnection("Provider=MSDAORA.1;User ID=gkfqd;Password=123456;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.1.103)(PORT = 1521))) (CONNECT_DATA = (SERVICE_NAME = orcl)))");
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            sqlQuery.Clear();
            sqlQuery.Append("SELECT USERID,USERROLE  FROM  LOGIN ");
            sqlQuery.Append(" WHERE USERID= '"+textBox2.Text+"'");
            sqlQuery.Append("   AND   PASSWORD ='" + textBox1.Text+"'");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSet.Clear();
            OleDbDataAdapter MyAdapter2 = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter2.Fill(dataSet);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                userId = dataSet.Tables[0].Rows[0]["USERID"].ToString();
                userRole = dataSet.Tables[0].Rows[0]["USERROLE"].ToString();
                // MessageBox.Show("登陆成功");
                //显示主界面
                Menu menu = new Menu(userId, userRole);
                menu.StartPosition = FormStartPosition.CenterScreen;//窗体居中
                menu.WindowState = FormWindowState.Maximized;

                menu.Show();
                this.Hide();
              

            }
            else 
            {
                MessageBox.Show("用户名或者密码错误");
                return;
            
            }
           
        }
        //密码隐藏
        private void login_Load(object sender, EventArgs e)
        {
            textBox1.UseSystemPasswordChar = true;
        }
        //回车登录
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                button1_Click(sender, e);
            }
        }

        private void checkBox1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //1、取得用户名
            //2、取得密码
            //3、写文件
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
           /* gk01c frmgk01c = new gk01c();
            frmgk01c.FormBorderStyle = FormBorderStyle.FixedSingle;//固定窗体大小不变
            frmgk01c.StartPosition = FormStartPosition.CenterScreen;//窗体居中
            frmgk01c.ShowDialog();*/
        }
    }
}
