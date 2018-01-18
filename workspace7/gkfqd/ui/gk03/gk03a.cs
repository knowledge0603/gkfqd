using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace gkfqd.ui.gk03
{
    public partial class gk03a : WinFormsUI.Docking.DockContent
    {

        #region 变量定义区
        //oracle 连接 操作
        //    public static OleDbconnection conn = new OleDbconnection("Provider=MSDAORA.1;User ID=gkfqd;Password=123456;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.1.103)(PORT = 1521))) (connECT_DATA = (SERVICE_NAME = orcl)))");
        DataSet dataSetTownName = new DataSet();
        DataSet dataSetCountyName = new DataSet();
        DataSet dataSetSelect = new DataSet();
        DataSet dataSetChange = new DataSet();
        DataSet dataSet = new DataSet();
        StringBuilder sqlQuery = new StringBuilder();
        #endregion
        public gk03a()
        {
            InitializeComponent();
            //默认选内蒙古自治区
            comboBox1.SelectedIndex = 0;
            //默认年份为 全部
            comboBox4.SelectedIndex = 0;
            //区县comboBox列表加载
            LoadComboBox();
            //不显示dataGridView1最后一行
            dataGridView1.AllowUserToAddRows = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text=="") 
            {
                MessageBox.Show("请输入建新规模面积!");
                return;
            }
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append(" SELECT      XMMC AS 所属复垦项目名称, ");
            sqlQuery.Append(" PZGM AS 实际批准建新规模, ");
            sqlQuery.Append("       PZWH AS 批准文号, ");
            sqlQuery.Append("       JXZYGDMJ AS 建新占用耕地面积, ");
            sqlQuery.Append("       FHGH AS  符合土地利用规划, ");
            sqlQuery.Append("       JXWJMC AS 建新文件名称 ");
            sqlQuery.Append("FROM      JXXM ");
            sqlQuery.Append("WHERE   PZGM < " +textBox1.Text);
            if (comboBox2.Text != "全部")
            {
                sqlQuery.Append(" AND  XMSZS = '" + comboBox2.Text + "'");
            }
            if (comboBox3.Text != "全部")
            {
                sqlQuery.Append(" AND  XMSZX = '" + comboBox3.Text + "'");
            }
            if (comboBox4.Text != "全部")
            {
                sqlQuery.Append(" AND  to_char(PZSJ,'yyyy')='" + comboBox4.Text + "'");
            }
            if (textBox6.Text != "")
            {
                sqlQuery.Append(" AND  XMMC  like '%" + textBox6.Text + "%'");
            }
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSet.Clear();
            OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter.Fill(dataSet);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            dataGridView1.DataSource = dataSet.Tables[0];
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            gkfqd.Common.Tool.IsNumber(e,sender);
        }


        #region 区县级联选择处理
        public void LoadComboBox()
        {
            sqlQuery.Clear();
            sqlQuery.Append(" SELECT XZQM  FROM  XZQ  ");
            sqlQuery.Append(" WHERE CJ ='1'  ");
            sqlQuery.Append(" ORDER BY  XZQDM  ");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSetTownName.Clear();
            OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter.Fill(dataSetTownName);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();

            comboBox2.DataSource = dataSetTownName.Tables[0];
            comboBox2.DisplayMember = "XZQM";
            comboBox2.ValueMember = "XZQM";

            sqlQuery.Clear();
            sqlQuery.Append(" SELECT XZQM  FROM  XZQ  ");
            sqlQuery.Append(" WHERE CJ ='2'  ");
            //默认加载全部  BHBS = 0000
            sqlQuery.Append(" AND BHBS ='0000'");
            sqlQuery.Append(" ORDER BY  XZQDM  ");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSetCountyName.Clear();

            OleDbDataAdapter MyAdapter2 = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter2.Fill(dataSetCountyName);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            comboBox3.DataSource = dataSetCountyName.Tables[0];
            comboBox3.DisplayMember = "XZQM";
            comboBox3.ValueMember = "XZQM";
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            sqlQuery.Clear();
            sqlQuery.Append(" SELECT BHBS  FROM  XZQ  ");
            sqlQuery.Append(" WHERE XZQM  = '" + comboBox2.SelectedValue + "'");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSetSelect.Clear();
            OleDbDataAdapter MyAdapter1 = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter1.Fill(dataSetSelect);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            //取得包含标识
            String strBhbs = dataSetSelect.Tables[0].Rows[0][0].ToString();

            sqlQuery.Clear();
            sqlQuery.Append(" SELECT XZQM  FROM  XZQ  ");
            sqlQuery.Append(" WHERE CJ ='2'  ");
            sqlQuery.Append(" AND BHBS ='" + strBhbs + "'");
            sqlQuery.Append(" ORDER BY  XZQDM  ");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSetChange.Clear();

            OleDbDataAdapter MyAdapter2 = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter2.Fill(dataSetChange);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            comboBox3.DataSource = dataSetChange.Tables[0];
            comboBox3.DisplayMember = "XZQM";
            comboBox3.ValueMember = "XZQM";
        }
        #endregion

    }
}
