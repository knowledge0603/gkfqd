﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;

using gkfqd.ui;

namespace gkfqd.ui.gk01
{
    public partial class gk015 : WinFormsUI.Docking.DockContent
    {
       #region 变量定义区
        //oracle 连接 操作
    //    public static OleDbconnection conn = new OleDbconnection("Provider=MSDAORA.1;User ID=gkfqd;Password=123456;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.1.103)(PORT = 1521))) (connECT_DATA = (SERVICE_NAME = orcl)))");
        DataSet dataSet = new DataSet();
        DataSet dataSet4 = new DataSet();
        DataSet dataSet1 = new DataSet();
        DataSet dataSet2 = new DataSet();
        DataSet dataSet5 = new DataSet();
        DataSet dataSet6 = new DataSet();
        DataSet dataSet7 = new DataSet();
        DataSet dataSet8 = new DataSet();
        DataSet dataSetOutPut = new DataSet();
        DataSet dataSetTownName = new DataSet();

        StringBuilder sqlQuery = new StringBuilder();
        #endregion

       #region 初始化
         public gk015()
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
            //当用户角色 2 为普通浏览者，浏览用户，只能看不能改删，看到所有用户的记录
            if (login.userRole=="2")
            {
                //删除按钮，不可用
                button2.Enabled = false;
                //修改按钮，不可用
                button3.Enabled = false;
                //添加按钮，不可用
                button4.Enabled = false;
                //上传文档按钮，不可用
                button6.Enabled = false;
                //项目地块录入按钮不可用
                button5.Enabled = false;
             }
            //if(login.userRole=="1")
            //{
            //    button8.Text = "问题报告";
            //}
            //else
            //{
            //    button8.Text = "问题已解决";
            //}
          
        }
        #endregion

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

       #region 查询处理
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
            //普通用户，能看到自己的录入，能删除修改自己的录入
            if(login.userRole=="3"){
                sqlQuery.Append("  AND  YHID='" + login.userId + "'");
            }
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSet5.Clear();
            OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter.Fill(dataSet5);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            dataGridView1.DataSource = dataSet5.Tables[0];
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

        }
        #endregion

       #region 添加记录
        private void button4_Click(object sender, EventArgs e)
        {
            gkfqd.Common.DbUse.GetconnectionStringsConfig();
            gk014 frmgk014 = new gk014();
            frmgk014.FormBorderStyle = FormBorderStyle.FixedSingle;//固定窗体大小不变
            frmgk014.StartPosition = FormStartPosition.CenterScreen;//窗体居中
            frmgk014.ShowDialog();
        }
        #endregion 

       #region 添加地块记录
        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) {
                MessageBox.Show("请点击行头选择数据！");
                return;
            }
            DataGridViewRow dgvr = dataGridView1.CurrentRow;
            //获取项目名称值
            string strXmmc = dgvr.Cells["项目名称"].Value.ToString();
            sqlQuery.Clear();
            sqlQuery.Append("SELECT XMSZX AS 项目所在县");
            sqlQuery.Append(" FROM   FKXM ");
            sqlQuery.Append(" WHERE  XMMC='" + strXmmc + "'");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSetTownName.Clear();
            OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter.Fill(dataSetTownName);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();

            gk011 frmgk011 = new gk011(strXmmc, "复垦", dataSetTownName.Tables[0].Rows[0]["项目所在县"].ToString());
            frmgk011.FormBorderStyle = FormBorderStyle.FixedSingle;//固定窗体大小不变
            frmgk011.StartPosition = FormStartPosition.CenterScreen;//窗体居中

            frmgk011.ShowDialog();
        }
        #endregion

       #region 删除记录
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("请点击行头选择数据！");
                return;
            }
            DialogResult RSS = MessageBox.Show(this, "确定要删除选中行数据,删除项目后，项目对应录入信息也被删除，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            switch (RSS)
            {
                case DialogResult.Yes:
                    DataGridViewRow dgvr = dataGridView1.CurrentRow;
                    //获取项目名称值
                    string strXmmc = dgvr.Cells["项目名称"].Value.ToString();
                    string tableName = gkfqd.Common.DbUse.GetTownCodeByProjectName(strXmmc);

                    //确认该项目下是否录入地块，若录入地块，先删除地块后，再删除项目
                    sqlQuery.Clear();
                    sqlQuery.Append("SELECT FKXMMC FROM  " + tableName);
                    sqlQuery.Append("  WHERE  FKXMMC ='" + strXmmc + "'");
                    gkfqd.Common.DbUse.GetOleDbconnection().Close();
                    gkfqd.Common.DbUse.GetOleDbconnection().Open();
                    dataSet7.Clear();
                    OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
                    MyAdapter.Fill(dataSet7);
                    gkfqd.Common.DbUse.GetOleDbconnection().Close();
                    if (dataSet7.Tables[0].Rows.Count > 0  )
                    {
                        MessageBox.Show("该项目已经录入地块，请确认删除地块后，再删除项目！");
                        return;
                    }
                    //直接删除图形文件数据记录，超图底层再次查询异常
                   /* sqlQuery.Clear();
                    sqlQuery.Append("DELETE FROM GKFQD ");
                    sqlQuery.Append("WHERE  FKXMMC ='" + strXmmc + "'");
                    gkfqd.Common.DbUse.GetOleDbconnection().Close();
                    gkfqd.Common.DbUse.GetOleDbconnection().Open();
                    try
                    {
                        OleDbCommand deleteCommand = new OleDbCommand(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
                        deleteCommand.ExecuteNonQuery();
                        gkfqd.Common.DbUse.GetOleDbconnection().Close();
                        MessageBox.Show("复垦项目对应地块表删除成功！");
                    }
                    catch
                    {
                        MessageBox.Show("复垦项目对应地块表删除失败！");
                    }*/
                    sqlQuery.Clear();
                    sqlQuery.Append("DELETE FROM FKXM ");
                    sqlQuery.Append("WHERE  XMMC ='" + strXmmc+"'");
                    gkfqd.Common.DbUse.conn.Close();
                    gkfqd.Common.DbUse.conn.Open();
                    try
                    {
                        OleDbCommand deleteCommand = new OleDbCommand(sqlQuery.ToString(), gkfqd.Common.DbUse.conn);
                        deleteCommand.ExecuteNonQuery();
                        gkfqd.Common.DbUse.conn.Close();
                        dataGridView1.Rows.Remove(dgvr);
                        MessageBox.Show("复垦项目表删除成功！");
                    }
                    catch
                    {
                        MessageBox.Show("复垦项目表删除失败！");
                    }
                    
                    break;
                case DialogResult.No:
                    break;
            }
        }
        #endregion 

       #region 更新记录
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("请点击行头选择数据！");
                return;
            }
            DataGridViewRow dgvr = dataGridView1.CurrentRow;
            //获取项目名称值
            string strXmmc = dgvr.Cells["项目名称"].Value.ToString();
            sqlQuery.Clear();
            sqlQuery.Append("SELECT  XMSZ AS 项目所在省, ");
            sqlQuery.Append("XMSZS        AS 项目所在市, ");
            sqlQuery.Append("XMSZX        AS 项目所在县, ");
            sqlQuery.Append("XMMC         AS 项目名称, ");
            sqlQuery.Append("ZZSSDW       AS 组织实施单位, ");
            sqlQuery.Append("PZWH         AS 批准文号, ");
            sqlQuery.Append("PZJG         AS 批准机关, ");
            sqlQuery.Append("PZSJ         AS 批准时间, ");
            sqlQuery.Append("JHKSRQ       AS 计划开始日期, ");
            sqlQuery.Append("JHJSRQ       AS 计划结束日期, ");
            sqlQuery.Append("XMSJQYDWGS   AS 项目涉及权益单位个数, ");
            sqlQuery.Append("ZQYJDWSL     AS 征求意见单位数量, ");
            sqlQuery.Append("TYSL         AS 同意数量, ");
            sqlQuery.Append("FKGM         AS 复垦规模, ");
            sqlQuery.Append("FKGDGM       AS 复垦耕地规模, ");
            sqlQuery.Append("FKZJ         AS 土地复垦资金预算, ");
            sqlQuery.Append("CQF          AS 拆迁费, ");
            sqlQuery.Append("QT           AS 其他, ");
            sqlQuery.Append("HJ           AS 合计, ");
            sqlQuery.Append("CZZJ         AS 财政资金, ");
            sqlQuery.Append("SHZJ         AS 社会资金, ");
            sqlQuery.Append("XDZJ         AS 信贷资金, ");
            sqlQuery.Append("QTZJ         AS 其他资金, ");
            sqlQuery.Append("QSXZ         AS 权属性质, ");
            sqlQuery.Append("QSDWMC       AS 权属单位名称, ");
            sqlQuery.Append("QSDWDM       AS 权属单位代码, ");
            sqlQuery.Append("ZLDWM        AS 坐落单位名, ");
            sqlQuery.Append("FKJD         AS 复垦进度, ");
            sqlQuery.Append("YSZT         AS 验收状态, ");
            sqlQuery.Append("ZLDWDM       AS 坐落单位代码, ");
            sqlQuery.Append("FKJXGM       AS 复垦建新规模, ");
            sqlQuery.Append("XMQF         AS 项目区分, ");
            sqlQuery.Append("ZBSY         AS 指标使用, ");
            sqlQuery.Append("ZJAP         AS 资金安排,  ");
            sqlQuery.Append("YSSJ         AS 验收时间,  ");
            sqlQuery.Append("QSTZ         AS 权属调整,  ");
            sqlQuery.Append("KHZT         AS 考核状态,  ");
            sqlQuery.Append("XMGK         AS 项目概况  ");
            sqlQuery.Append("FROM         FKXM  ");
            sqlQuery.Append("WHERE         XMMC='" + strXmmc+"'");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSet6.Clear();
            OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter.Fill(dataSet6);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            //dataSet5.Tables[0].Rows[0]["药物名"].ToString();
            gk014 frmgk014 = new gk014(dataSet6.Tables[0]);
            frmgk014.FormBorderStyle = FormBorderStyle.FixedSingle;//固定窗体大小不变
            frmgk014.StartPosition = FormStartPosition.CenterScreen;//窗体居中
            frmgk014.ShowDialog();
        }
        #endregion

       #region 上传文档
        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("请点击行头选择数据！");
                return;
            }
            DataGridViewRow dgvr = dataGridView1.CurrentRow;
            //获取项目名称值
            string strXmmc = dgvr.Cells["项目名称"].Value.ToString();
            gk017 frmgk017 = new gk017(strXmmc);
            frmgk017.FormBorderStyle = FormBorderStyle.FixedSingle;//固定窗体大小不变
            frmgk017.StartPosition = FormStartPosition.CenterScreen;//窗体居中
            frmgk017.ShowDialog();
           
        }
        #endregion

       #region 导出项目信息 对接国家系统导入项目信息
        private void button7_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "导出项目信息文件(*.xml)|*.xml";
            sfd.FileName = "项目信息导出文件" + DateTime.Now.ToString("yyyyMMddhhmmss");
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                if (dataGridView1.CurrentRow == null) return;
                DataGridViewRow dgvr = dataGridView1.CurrentRow;
                //取得当前行项目名称值
                string strProjectName = dgvr.Cells["项目名称"].Value.ToString();
                sqlQuery.Clear();
                sqlQuery.Append("SELECT  A.XMMC AS 项目名称, ");
                sqlQuery.Append("        A.ZZSSDW AS 组织实施单位, ");
                sqlQuery.Append("        A.JHKSRQ AS 项目计划开始时间, ");
                sqlQuery.Append("        A.JHJSRQ AS 项目计划结束时间, ");
                sqlQuery.Append("        A.XMSJQYDWGS AS 项目实施涉及权益单位个数, ");
                sqlQuery.Append("        A.ZQYJDWSL AS 征求意见权益单位数量, ");
                sqlQuery.Append("        A.TYSL AS 同意数量, ");
                sqlQuery.Append("        A.PZWH AS 批准文号, ");
                sqlQuery.Append("        A.PZJG AS 批准机关, ");
                sqlQuery.Append("        A.PZSJ AS 批准时间, ");
                sqlQuery.Append("        A.CZZJ AS 财政资金, ");
                sqlQuery.Append("        A.SHZJ AS 社会资金, ");
                sqlQuery.Append("        A.XDZJ AS 信贷资金, ");
                sqlQuery.Append("        A.QTZJ AS 其他资金, ");
                sqlQuery.Append("        A.FKZJ AS 土地复垦费, ");
                sqlQuery.Append("        A.CQF AS 拆迁费, ");
                sqlQuery.Append("        A.QT AS 其他费用, ");
                sqlQuery.Append("        B.XZQDM AS 行政区代码 ");
                sqlQuery.Append("FROM         FKXM A ,XZQ B ");
                sqlQuery.Append("WHERE        A.XMSZX = B.XZQM ");
                sqlQuery.Append("             AND   XMMC='" + strProjectName + "'");
                gkfqd.Common.DbUse.GetOleDbconnection().Close();
                gkfqd.Common.DbUse.GetOleDbconnection().Open();
                dataSetOutPut.Clear();
                OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
                MyAdapter.Fill(dataSetOutPut);
                gkfqd.Common.DbUse.GetOleDbconnection().Close();

                sw.WriteLine("<?xml version=\"1.0\" encoding=\"gb2312\"?>");
                sw.WriteLine("<!-- 工矿复垦项目XML格式 -->");
                sw.WriteLine("<CL_PROJECT>");
                //dataSetOutPut.Tables[0].Rows[0]["药物名"].ToString();
                sw.WriteLine("  <PRJNAME name=\"项目名称\">" + dataSetOutPut.Tables[0].Rows[0]["项目名称"].ToString() + "</PRJNAME>");
                sw.WriteLine("  <LOCATION name=\"所在省市县(请填写地区编码)\">" + dataSetOutPut.Tables[0].Rows[0]["行政区代码"].ToString() + "</LOCATION>");
                sw.WriteLine("  <UNITWORK name=\"组织实施单位\">" + dataSetOutPut.Tables[0].Rows[0]["组织实施单位"].ToString() + "</UNITWORK>");
                sw.WriteLine("  <PLANSTARTDATE name=\"项目计划开始时间\"> " + dataSetOutPut.Tables[0].Rows[0]["项目计划开始时间"].ToString() + "</PLANSTARTDATE>");
                sw.WriteLine("  <PLANENDDATE name=\"项目计划结束时间\"> " + dataSetOutPut.Tables[0].Rows[0]["项目计划结束时间"].ToString() + "</PLANENDDATE>");
                sw.WriteLine("  <COVERUNITCOUNT name=\"项目实施涉及权益单位个数\">" + dataSetOutPut.Tables[0].Rows[0]["项目实施涉及权益单位个数"].ToString() + "</COVERUNITCOUNT>");
                sw.WriteLine("  <UNITOPINIONCOUNT name=\"征求意见权益单位数量\">" + dataSetOutPut.Tables[0].Rows[0]["征求意见权益单位数量"].ToString() + "</UNITOPINIONCOUNT>");
                sw.WriteLine("  <UNITAGREECOUNT name=\"同意数量\"> " + dataSetOutPut.Tables[0].Rows[0]["同意数量"].ToString() + "</UNITAGREECOUNT>");
                sw.WriteLine("  <PASSFILENO name=\"批准文号(格式:某国土资函〔2013〕123)\"> " + dataSetOutPut.Tables[0].Rows[0]["批准文号"].ToString() + "</PASSFILENO>");
                sw.WriteLine("  <PASSORG name=\"批准机关\"> " + dataSetOutPut.Tables[0].Rows[0]["批准机关"].ToString() + "</PASSORG>");
                sw.WriteLine("  <PASSDATE name=\"批准时间\"> " + dataSetOutPut.Tables[0].Rows[0]["批准时间"].ToString() + "</PASSDATE>");
                sw.WriteLine("  <CINVERT name=\"财政资金\">" + dataSetOutPut.Tables[0].Rows[0]["财政资金"].ToString() + "</CINVERT>");
                sw.WriteLine("  <SINVERT name=\"社会资金\">" + dataSetOutPut.Tables[0].Rows[0]["社会资金"].ToString() + "</SINVERT>");
                sw.WriteLine("  <XINVERT name=\"信贷资金\">" + dataSetOutPut.Tables[0].Rows[0]["信贷资金"].ToString() + "</XINVERT>");
                sw.WriteLine("  <OINVERT name=\"其他资金\"> " + dataSetOutPut.Tables[0].Rows[0]["其他资金"].ToString() + "</OINVERT>");
                sw.WriteLine("  <LANDRECLFEE name=\"土地复垦费\"> " + dataSetOutPut.Tables[0].Rows[0]["土地复垦费"].ToString() + "</LANDRECLFEE>");
                sw.WriteLine("  <CHAIFEE name=\"拆迁费\"> " + dataSetOutPut.Tables[0].Rows[0]["拆迁费"].ToString() + "</CHAIFEE>");
                sw.WriteLine("  <OTHERFEE name=\"其他费用\"> " + dataSetOutPut.Tables[0].Rows[0]["其他费用"].ToString() + "</OTHERFEE>");
                sw.WriteLine("</CL_PROJECT>");
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
                fs.Close();
                MessageBox.Show("项目信息导出成功！");
            }
        }
        #endregion

       #region 问题报告
        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (login.userRole == "1")
                {
                    if (dataGridView1.CurrentRow == null)
                    {
                        MessageBox.Show("请点击行头选择数据！");
                        return;
                    }
                    DataGridViewRow dgvr = dataGridView1.CurrentRow;
                    //获取项目名称值
                    string strXmmc = dgvr.Cells["项目名称"].Value.ToString();
                    gk07.gk071 frmgk071 = new gk07.gk071(strXmmc);
                    frmgk071.FormBorderStyle = FormBorderStyle.FixedSingle;//固定窗体大小不变
                    frmgk071.StartPosition = FormStartPosition.CenterScreen;//窗体居中
                    frmgk071.ShowDialog();
                }
                else
                {
                    gk07.gk071 frmgk071 = new gk07.gk071();
                    frmgk071.FormBorderStyle = FormBorderStyle.FixedSingle;//固定窗体大小不变
                    frmgk071.StartPosition = FormStartPosition.CenterScreen;//窗体居中
                    frmgk071.ShowDialog();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("复垦项目数据提交处理异常！");
                return;
            }
            
        }
        #endregion


    }
}
