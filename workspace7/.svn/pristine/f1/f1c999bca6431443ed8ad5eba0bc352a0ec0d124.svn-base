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
    public partial class gk033 : WinFormsUI.Docking.DockContent
    {
        #region 变量定义区
        //oracle 连接 操作
        //    public static OleDbconnection conn = new OleDbconnection("Provider=MSDAORA.1;User ID=gkfqd;Password=123456;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.1.103)(PORT = 1521))) (connECT_DATA = (SERVICE_NAME = orcl)))");
      
    
        DataSet dataSetTownName = new DataSet();
        DataSet dataSetCountyName = new DataSet();
        DataSet dataSetSelect = new DataSet();
        DataSet dataSetChange= new DataSet();

        StringBuilder sqlQuery = new StringBuilder();
        #endregion


        DataSet dataSet = new DataSet();

        public gk033()
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
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append(" SELECT  XMMC   AS  项目名称, ");
            sqlQuery.Append("         FKJD   AS  复垦进度, ");
            sqlQuery.Append("         PZWH   AS  批准文号, ");
            sqlQuery.Append("         ZZSSDW AS  组织实施单位, ");
            sqlQuery.Append("         PZJG   AS  批准机关, ");
            sqlQuery.Append("         PZSJ   AS  批准时间 ");
            sqlQuery.Append(" FROM    FKXM ");
            sqlQuery.Append(" WHERE   1=1  ");
            if(checkBox1.Checked == true|| checkBox2.Checked == true||checkBox4.Checked == true){
                sqlQuery.Append(" AND  FKJD IN ( ''");
                //复垦区分
                if (checkBox1.Checked == true)
                {
                    sqlQuery.Append(",'已复垦'");
                }
                //复垦区分
                if (checkBox2.Checked == true)
                {
                    sqlQuery.Append(",'未复垦'");
                }
                //复垦区分
                if (checkBox4.Checked == true)
                {
                    sqlQuery.Append(",'复垦中'");
                }
                sqlQuery.Append(" ,'') ");
            }
        
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
