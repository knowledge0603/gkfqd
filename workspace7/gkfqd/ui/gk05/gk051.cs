using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Windows.Forms.DataVisualization.Charting;

namespace gkfqd.ui.gk05
{
    public partial class gk051 : WinFormsUI.Docking.DockContent
    {

        DataSet dataSet = new DataSet();
        DataSet dataSet4 = new DataSet();
        DataSet dataSet1 = new DataSet();
        DataSet dataSet2 = new DataSet();
        DataSet dataSet5 = new DataSet();
        DataSet dataSetChartUse = new DataSet();
        DataSet dataSet7 = new DataSet();
        StringBuilder sqlQuery = new StringBuilder();

        public gk051()
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

        #region 区县级联选择处理
        public void LoadComboBox()
        {
            sqlQuery.Clear();
            sqlQuery.Append(" SELECT XZQM  FROM  XZQ  ");
            sqlQuery.Append(" WHERE CJ ='1'  ");
            sqlQuery.Append(" ORDER BY  XZQDM  ");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSet.Clear();
            OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter.Fill(dataSet);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();

            comboBox2.DataSource = dataSet.Tables[0];
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
            dataSet4.Clear();

            OleDbDataAdapter MyAdapter2 = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter2.Fill(dataSet4);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            comboBox3.DataSource = dataSet4.Tables[0];
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
            dataSet1.Clear();
            OleDbDataAdapter MyAdapter1 = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter1.Fill(dataSet1);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            //取得包含标识
            String strBhbs = dataSet1.Tables[0].Rows[0][0].ToString();


            sqlQuery.Clear();
            sqlQuery.Append(" SELECT XZQM  FROM  XZQ  ");
            sqlQuery.Append(" WHERE CJ ='2'  ");
            sqlQuery.Append(" AND BHBS ='" + strBhbs + "'");
            sqlQuery.Append(" ORDER BY  XZQDM  ");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSet2.Clear();


            OleDbDataAdapter MyAdapter2 = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter2.Fill(dataSet2);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            comboBox3.DataSource = dataSet2.Tables[0];
            comboBox3.DisplayMember = "XZQM";
            comboBox3.ValueMember = "XZQM";
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            sqlQuery.Clear();
            sqlQuery.Append("SELECT XMMC AS 项目名称, ");
            sqlQuery.Append("       ZZSSDW AS 组织实施单位, ");
            sqlQuery.Append("       PZWH AS 批准文号, ");
            sqlQuery.Append("       PZSJ AS 批准时间, ");
            sqlQuery.Append("       JHJSRQ AS 计划结束日期, ");
            sqlQuery.Append("       ZQYJDWSL AS 征求意见单位数量 ");
            sqlQuery.Append("FROM   FKXM ");
            //项目所在省 
            sqlQuery.Append("WHERE  XMSZ='" + comboBox1.Text + "'");
            if (comboBox2.Text != "全部")
            {
                //项目所在市
                sqlQuery.Append("  AND   XMSZS='" + comboBox2.Text + "'");
            }
            if (comboBox3.Text != "全部")
            {
                //项目所在县
                sqlQuery.Append("  AND   XMSZX='" + comboBox3.Text + "'");
            }
            //项目名称
            if (textBox1.Text != "")
            {
                sqlQuery.Append("  AND   XMMC LIKE '%" + textBox1.Text + "%'");
            }
            //年份  数据库字段对应 计划开始日期
            if (comboBox4.Text != "全部")
            {
                sqlQuery.Append("  AND   to_char(JHKSRQ,'yyyy')='" + comboBox4.Text + "'");

            }
            sqlQuery.Append("  AND   XMQF = '历史项目'");
            
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSet5.Clear();
            OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter.Fill(dataSet5);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            dataGridView1.DataSource = dataSet5.Tables[0];
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

           
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow == null) 
            {
                MessageBox.Show("请选择查询表行头，选择行数据！");
                return; 
            } 
            DataGridViewRow dgvr = dataGridView1.CurrentRow;
            string strProjectName = dgvr.Cells["项目名称"].Value.ToString();//获取smid值
            sqlQuery.Clear();
            sqlQuery.Append("SELECT CZZJ AS 财政资金, ");
            sqlQuery.Append("       SHZJ AS 社会资金, ");
            sqlQuery.Append("       XDZJ AS 信贷资金, ");
            sqlQuery.Append("       QTZJ AS 其他资金 ");
            sqlQuery.Append("FROM   FKXM ");
            sqlQuery.Append("       WHERE  XMMC='" + strProjectName+"'");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSetChartUse.Clear();
            OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter.Fill(dataSetChartUse);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            if (dataSetChartUse.Tables[0].Rows[0][0].ToString() != "")
            {
                double[] yValues = { Convert.ToDouble(dataSetChartUse.Tables[0].Rows[0][0]), Convert.ToDouble(dataSetChartUse.Tables[0].Rows[0][1]), Convert.ToDouble(dataSetChartUse.Tables[0].Rows[0][2]), Convert.ToDouble(dataSetChartUse.Tables[0].Rows[0][3]) };
                string[] xValues = { "财政资金", "社会资金", "信贷资金", "其他资金" };
                // this.chart1.Series[0].Label = "#PERCENT";
                this.chart1.Series[0]["PieLabelStyle"] = "Outside";
                this.chart1.Series[0]["PieLineColor"] = "Black";//连线颜色
                this.chart1.Series[0].Points.DataBindXY(xValues, yValues);
                this.chart2.Series[0].Points.DataBindXY(xValues, yValues);
            }
            else {

                MessageBox.Show("项目信息【财政资金】没有录入！");
                return;
            }
        }

    }
}
