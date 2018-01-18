using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace gkfqd.ui.gk07
{
    public partial class gk071 : WinFormsUI.Docking.DockContent
    {
        #region 变量区

        //共同用  sqlQuery
        StringBuilder sqlQuery = new StringBuilder();
        DataSet dataSet = new DataSet();
        DataSet dataSet1 = new DataSet();
        DataSet dataSet2 = new DataSet();
        DataSet dataSet3 = new DataSet();

        #endregion

        #region 初始化

        public gk071()
        {
            InitializeComponent();
            //不显示dataGridView1最后一行
            dataGridView1.AllowUserToAddRows = false;
            //自动获取用户
            sqlQuery.Append(" select USERROLE from LOGIN ");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSet1.Clear();
            OleDbDataAdapter MyAdapter1 = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter1.Fill(dataSet1);
            comboBox1.DataSource = dataSet1.Tables[0];
            comboBox1.DisplayMember = "USERROLE";
            comboBox1.ValueMember = "USERROLE";
            if(login.userRole == "1")
            {
                button3.Visible = false;
                groupBox1.Visible = true;
                textBox1.Visible = true;
                label1.Visible = true;
                comboBox1.Visible = true;
                button1.Visible = true;
            }
            else
            {
                textBox2.ReadOnly = true;
                button3.Visible = true;
                textBox1.Visible = true;
                groupBox1.Visible = true;
                label1.Visible = false;
                comboBox1.Visible = false;
                button1.Visible = false;
            }
            //查询问题列表
            query();
        }

        public gk071(string projectId)
        {
            InitializeComponent();
            textBox2.Text = projectId;
            textBox2.ReadOnly = true;
            //不显示dataGridView1最后一行
            dataGridView1.AllowUserToAddRows = false;
            //自动获取用户
            sqlQuery.Append(" select USERROLE from LOGIN ");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSet1.Clear();
            OleDbDataAdapter MyAdapter1 = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter1.Fill(dataSet1);
            comboBox1.DataSource = dataSet1.Tables[0];
            comboBox1.DisplayMember = "USERROLE";
            comboBox1.ValueMember = "USERROLE";
            if (login.userRole == "1")
            {
                button3.Visible = false;
                groupBox1.Visible = true;
                textBox1.Visible = true;
                label1.Visible = true;
                comboBox1.Visible = true;
                button1.Visible = true;
            }
            else
            {
                textBox2.ReadOnly = true;
                button3.Visible = true;
                textBox1.Visible = true;
                groupBox1.Visible = true;
                label1.Visible = false;
                comboBox1.Visible = false;
                button1.Visible = false;
            }
            //查询问题列表
            query();
        }
        
        #endregion

        #region 发送提示

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text=="")
            {
                MessageBox.Show("请输入问题内容！");
                return;
            }
            //Insert into WTBG（字段名即属性）values(你想要插入属性的值)
            if(textBox2.Text == "")
            {
                MessageBox.Show("请输入项目名称");
                return;
            }
            
            sqlQuery.Clear();
            sqlQuery.Append(" INSERT INTO WTBG (WTID, ");
            sqlQuery.Append("XMMC,");
            sqlQuery.Append("WT,");
            sqlQuery.Append("YH");
            sqlQuery.Append(" ) VALUES (  ");
            sqlQuery.Append("wtid.nextval" + ",'");
            sqlQuery.Append(textBox2.Text + "','");
            sqlQuery.Append(textBox1.Text + "','");
            sqlQuery.Append(comboBox1.Text + "'");
            sqlQuery.Append(")");
            gkfqd.Common.DbUse.conn.Close();
            gkfqd.Common.DbUse.conn.Open();
            OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter.Fill(dataSet3);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            MessageBox.Show("已发送");

        }

        #endregion

        #region 查询处理

        private void button2_Click(object sender, EventArgs e)
        {
           
            

        }

        public void query()
        { 
            sqlQuery.Clear();
            sqlQuery.Append("SELECT WTID    AS 编号, ");
            sqlQuery.Append("       XMMC    AS 项目名称, ");
            sqlQuery.Append("       WT      AS 问题, ");
            sqlQuery.Append("       YH      AS 用户ID, ");
            sqlQuery.Append("       WTZT    AS 问题状态, ");
            sqlQuery.Append("       SJ      AS 时间 ");
            sqlQuery.Append("FROM   WTBG  ");
            if (login.userRole!="1")
            {
                sqlQuery.Append(" WHERE   YH = '" + login.userId + "'");
            }
            
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSet.Clear();
            OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter.Fill(dataSet);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            dataGridView1.DataSource = dataSet.Tables[0];
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            for (int rowNumber = 0; rowNumber < dataGridView1.Rows.Count; rowNumber++)
            {
                if (dataGridView1.Rows[rowNumber].Cells[4].Value.Equals("未解决"))
                {
                    dataGridView1.Rows[rowNumber].Cells[4].Style.BackColor = Color.Red;
                }
            }
        }

        #endregion

        #region 设置groupbox颜色
        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics,
                               this.groupBox1.ClientRectangle,
                               Color.DarkRed,         //left
                               1,
                               ButtonBorderStyle.Solid,
                               Color.DarkRed,         //top
                               1,
                               ButtonBorderStyle.Solid,
                               Color.DarkRed,        //right
                               1,
                               ButtonBorderStyle.Solid,
                               Color.DarkRed,        //bottom
                               1,
                               ButtonBorderStyle.Solid);
        }
        #endregion

        #region 向管理员提交
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("请点击行头选择数据！");
                return;
            }
            DataGridViewRow dgvr = dataGridView1.CurrentRow;
            //获取项目名称值
            string strXmmc = dgvr.Cells["编号"].Value.ToString();
            sqlQuery.Clear();
            sqlQuery.Append(" UPDATE   WTBG SET WTZT = '已解决'");
            //sqlQuery.Append(" WHERE  WTID= '10'");
            sqlQuery.Append(" WHERE  WTID='" + strXmmc + "'");
            gkfqd.Common.DbUse.conn.Close();
            gkfqd.Common.DbUse.conn.Open();
            OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter.Fill(dataSet2);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            query();
            for (int rowNumber = 0; rowNumber < dataGridView1.Rows.Count; rowNumber++)
            {
                if (dataGridView1.Rows[rowNumber].Cells[4].Value.Equals("未解决"))
                {
                    dataGridView1.Rows[rowNumber].Cells[4].Style.BackColor = Color.Red;
                }
            }
            MessageBox.Show("已提交");
            
        }
        #endregion

        #region 查看问题
        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            textBox1.ReadOnly = true;
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("请点击行头选择数据！");
                return;
            }
            DataGridViewRow dgvr = dataGridView1.CurrentRow;
            //获取项目名称值
            string strXmwt = dgvr.Cells["问题"].Value.ToString();
            string strXmmc = dgvr.Cells["项目名称"].Value.ToString();
            textBox1.Text = strXmwt;
            textBox2.Text = strXmmc;
        }
        #endregion

    }
}
