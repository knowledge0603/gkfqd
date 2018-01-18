using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;

namespace gkfqd.ui.gk01
{
    public partial class gk01a : WinFormsUI.Docking.DockContent
    {
        #region 变量区
        StringBuilder sqlQuery = new StringBuilder();
        DataSet dataSet = new DataSet();
        DataSet dataSet4 = new DataSet();
        DataSet dataSet1 = new DataSet();
        DataSet dataSet2 = new DataSet();
        DataSet dataSet5 = new DataSet();
        DataSet dataSet6 = new DataSet();
        DataSet dataSet7 = new DataSet();
        DataSet dataSetOutPut = new DataSet();
        DataSet dataSetTownName = new DataSet();
        #endregion

        #region 初始化
        public gk01a()
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
            if (login.userRole == "2")
            {
                //上传文档按钮，不可用
                button6.Enabled = false;
                //建新项目地块录入地块按钮，不可用
                button5.Enabled = false;
                //补耕地块录入按钮，不可用
                button8.Enabled = false;
                //删除按钮，不可用
                button2.Enabled = false;
                //修改按钮，不可用
                button3.Enabled = false;
                //添加按钮，不可用
                button4.Enabled = false;
            }
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
        #endregion

        #region 市下拉列表点击 选择 
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

        #region  查询建新项目
        private void button1_Click(object sender, EventArgs e)
        {
            sqlQuery.Clear();
            sqlQuery.Append("SELECT XMMC AS 所属复垦项目名称, ");
            sqlQuery.Append("       PZWH AS 批准文号, ");
            sqlQuery.Append("       JXWJMC AS 建新文件名称, ");
            sqlQuery.Append("       PZSJ AS 实际批准建新规模批准时间, ");
            sqlQuery.Append("       PZGM AS 实际批准建新规模, ");
            sqlQuery.Append("       JXZYGDMJ AS 建新占用耕地面积 ");
            sqlQuery.Append("FROM   JXXM ");
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
                sqlQuery.Append("  AND   to_char(PZSJ,'yyyy')='" + comboBox4.Text + "'");
            }
            //普通用户，能看到自己的录入，能删除修改自己的录入
            if (login.userRole == "3")
            {
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

        #region 添加 建新项目 文档
        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("请点击行头选择数据！");
                return;
            }
            DataGridViewRow dgvr = dataGridView1.CurrentRow;
            //获取项目名称值
            string strXmmc = dgvr.Cells["所属复垦项目名称"].Value.ToString();
            gk017 frmgk017 = new gk017(strXmmc);
            frmgk017.FormBorderStyle = FormBorderStyle.FixedSingle;//固定窗体大小不变
            frmgk017.StartPosition = FormStartPosition.CenterScreen;//窗体居中
            frmgk017.ShowDialog();
        }
        #endregion

        #region 建新地块录入
        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("请点击行头选择数据！");
                return;
            }
            DataGridViewRow dgvr = dataGridView1.CurrentRow;
            //获取项目名称值
            string strXmmc = dgvr.Cells["所属复垦项目名称"].Value.ToString();
            //----------------
            sqlQuery.Clear();
            sqlQuery.Append("SELECT XMSZX AS 项目所在县");
            sqlQuery.Append(" FROM   JXXM ");
            sqlQuery.Append(" WHERE  XMMC='" + strXmmc + "'");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSetTownName.Clear();
            OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter.Fill(dataSetTownName);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            //----------------
            gk011 frmgk011 = new gk011(strXmmc, "建新", dataSetTownName.Tables[0].Rows[0]["项目所在县"].ToString());
            frmgk011.FormBorderStyle = FormBorderStyle.FixedSingle;//固定窗体大小不变
            frmgk011.StartPosition = FormStartPosition.CenterScreen;//窗体居中

            frmgk011.ShowDialog();
        }
        #endregion

        #region 建新项目删除
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("请点击行头选择数据！");
                return;
            }
            DialogResult RSS = MessageBox.Show(this, "确定要删除选中行数据，删除项目后，项目对应信息也被删除，是否继续？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            switch (RSS)
            {
                case DialogResult.Yes:
                    DataGridViewRow dgvr = dataGridView1.CurrentRow;
                    //获取项目名称值
                    string strXmmc = dgvr.Cells["所属复垦项目名称"].Value.ToString();
                    //确认该项目下是否录入地块，若录入地块，先删除地块后，再删除项目
                    sqlQuery.Clear();
                    sqlQuery.Append("SELECT FKXMMC FROM " + gkfqd.Common.DbUse.GetTownCodeByProjectNameJx(strXmmc));
                    sqlQuery.Append(" WHERE  FKXMMC ='" + strXmmc + "'");
                    gkfqd.Common.DbUse.GetOleDbconnection().Close();
                    gkfqd.Common.DbUse.GetOleDbconnection().Open();
                    dataSet7.Clear();
                    OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
                    MyAdapter.Fill(dataSet7);
                    gkfqd.Common.DbUse.GetOleDbconnection().Close();
                    if (dataSet7.Tables[0].Rows.Count > 0)
                    {
                        MessageBox.Show("该项目已经录入地块，请确认删除地块后，再删除项目！");
                        return;
                    }
                    //地图信息 不能用 DELETE sql 语句删除 删除后查询异常
                  /*sqlQuery.Clear();
                    sqlQuery.Append("DELETE FROM JXTC ");
                    sqlQuery.Append("WHERE  FKXMMC ='" + strXmmc + "'");
                    gkfqd.Common.DbUse.GetOleDbconnection().Close();
                    gkfqd.Common.DbUse.GetOleDbconnection().Open();
                    try
                    {
                        OleDbCommand deleteCommand = new OleDbCommand(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
                        deleteCommand.ExecuteNonQuery();
                        gkfqd.Common.DbUse.GetOleDbconnection().Close();
                        dataGridView1.Rows.Remove(dgvr);
                        MessageBox.Show("建新地块删除成功！");
                    }
                    catch
                    {
                        MessageBox.Show("建新地块删除失败！");
                    }*/
                    sqlQuery.Clear();
                    sqlQuery.Append("DELETE FROM JXXM ");
                    sqlQuery.Append("WHERE  XMMC ='" + strXmmc + "'");
                    gkfqd.Common.DbUse.conn.Close();
                    gkfqd.Common.DbUse.conn.Open();
                    try
                    {
                        OleDbCommand deleteCommand = new OleDbCommand(sqlQuery.ToString(), gkfqd.Common.DbUse.conn);
                        deleteCommand.ExecuteNonQuery();
                        gkfqd.Common.DbUse.conn.Close();
                        dataGridView1.Rows.Remove(dgvr);
                        MessageBox.Show("建新项目删除成功！");
                    }
                    catch
                    {
                        MessageBox.Show("建新项目删除失败！");
                    }
                    break;
                case DialogResult.No:
                    break;
            }
        }
        #endregion

        #region 建新项目修改
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("请点击行头选择数据！");
                return;
            }
            DataGridViewRow dgvr = dataGridView1.CurrentRow;
            //获取项目名称值
            string strXmmc = dgvr.Cells["所属复垦项目名称"].Value.ToString();
            string strJx = dgvr.Cells["建新文件名称"].Value.ToString();
            sqlQuery.Clear();
            sqlQuery.Append("SELECT  XMSZ           AS    项目所在省,");
            sqlQuery.Append("        XMSZS          AS    项目所在市,");
            sqlQuery.Append("        XMSZX          AS    项目所在县,");
            sqlQuery.Append("        XMMC           AS    所属复垦项目名称,");
            sqlQuery.Append("        PZWH           AS    批准文号,");
            sqlQuery.Append("        JXWJMC         AS    建新文件名称,");
            sqlQuery.Append("        FHGH           AS    符合土地利用规划,");
            sqlQuery.Append("        PZSJ           AS    实际批准建新规模批准时间,");
            sqlQuery.Append("        PZGM           AS    实际批准建新规模,");
            sqlQuery.Append("        JXZYGDMJ       AS    建新占用耕地面积,");
            sqlQuery.Append("        ZSZMJ          AS    征收总面积,");
            sqlQuery.Append("        ZSZFY          AS    征收总费用,");
            sqlQuery.Append("        AZZRK          AS    安置总人口,");
            sqlQuery.Append("        LDLRK          AS    劳动力人口,");
            sqlQuery.Append("        ZYZMJ          AS    占用总面积,");
            sqlQuery.Append("        ZYGDMJ         AS    占用耕地面积,");
            sqlQuery.Append("        ZYGDZL         AS    占用耕地质量,");
            sqlQuery.Append("        NCAZZF         AS    农村安置住房,");
            sqlQuery.Append("        FNYFZYD        AS    非农业发展用地,");
            sqlQuery.Append("        PTSS           AS    农村占地,");
            sqlQuery.Append("        QTJSYDNC       AS    其他建设用地农村,");
            sqlQuery.Append("        SFYD           AS    商服用地,");
            sqlQuery.Append("        GKCC           AS    工矿仓储,");
            sqlQuery.Append("        ZZYD           AS    住宅用地,");
            sqlQuery.Append("        QTJSYDCZ       AS    其他建设用地城镇,");
            sqlQuery.Append("        PCH            AS    批次号,");
            sqlQuery.Append("        INSERT_TIME    AS    记录插入时间 ");
            sqlQuery.Append("FROM         JXXM  ");
            sqlQuery.Append("WHERE         XMMC='" + strXmmc + "'");
            sqlQuery.Append("  AND         JXWJMC='" + strJx + "'");
            
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSet6.Clear();
            OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter.Fill(dataSet6);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            //dataSet5.Tables[0].Rows[0]["药物名"].ToString();
            gk018 frmgk018 = new gk018(dataSet6.Tables[0]);
            frmgk018.FormBorderStyle = FormBorderStyle.FixedSingle;//固定窗体大小不变
            frmgk018.StartPosition = FormStartPosition.CenterScreen;//窗体居中
            frmgk018.ShowDialog();
        }
        #endregion 

        #region  建新项目添加弹出窗口
        private void button4_Click(object sender, EventArgs e)
        {
            gk018 frmgk018 = new gk018();
            frmgk018.FormBorderStyle = FormBorderStyle.FixedSingle;//固定窗体大小不变
            frmgk018.StartPosition = FormStartPosition.CenterScreen;//窗体居中
            frmgk018.ShowDialog();
        }
        #endregion

        #region 导出建新项目信息
        private void button7_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "导出建新项目信息文件(*.xml)|*.xml";
            sfd.FileName = "建新项目信息导出文件" + DateTime.Now.ToString("yyyyMMddhhmmss");
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                if (dataGridView1.CurrentRow == null) return;
                DataGridViewRow dgvr = dataGridView1.CurrentRow;
                //取得当前行项目名称值
                string strProjectName = dgvr.Cells["所属复垦项目名称"].Value.ToString();
                string strProjectNameJx = dgvr.Cells["建新文件名称"].Value.ToString();
                sqlQuery.Clear();
                sqlQuery.Append("SELECT  B.XZQDM AS 所在地编号, ");
                sqlQuery.Append("        A.PZSJ AS 实际批准建新规模时间, ");
                sqlQuery.Append("        A.PZWH AS 实际批准建新规模文号, ");
                sqlQuery.Append("        A.JXWJMC AS 实际批准建新规模文件名称, ");
                sqlQuery.Append("        C.FKXMBH AS 所属复垦项目编号, ");
                sqlQuery.Append("        A.XMMC AS 所属复垦项目名称, ");
                sqlQuery.Append("        A.PZGM AS 实际批准建新规模, ");
                sqlQuery.Append("        A.JXZYGDMJ AS 其中建新可占用耕地面积, ");
                sqlQuery.Append("        A.FHGH AS 符合总体规划, ");
                sqlQuery.Append("        A.ZYZMJ AS 占用总面积, ");
                sqlQuery.Append("        A.ZYGDMJ AS 占用耕地面积, ");
                sqlQuery.Append("        A.ZYGDZL AS 占用耕地质量, ");
                sqlQuery.Append("        A.NCAZZF AS 农民安置住房, ");
                sqlQuery.Append("        A.PTSS AS 农村基础设施和公共服务配套设施, ");
                sqlQuery.Append("        A.FNYFZYD AS 非农产业发展用地, ");
                sqlQuery.Append("        A.QTJSYDNC AS 其他建设用地农村, ");
                sqlQuery.Append("        A.SFYD AS 商服用地, ");
                sqlQuery.Append("        A.ZZYD AS 住宅用地,");
                sqlQuery.Append("        A.GKCC AS 工矿用地,");
                sqlQuery.Append("        A.QTJSYDCZ AS 其他建设用地城镇,");
                sqlQuery.Append("        A.ZSZMJ  AS 征收总面积,");
                sqlQuery.Append("        A.ZSZFY  AS 征收总费用,");
                sqlQuery.Append("        A.AZZRK   AS 安置总人口,");
                sqlQuery.Append("        A.LDLRK  AS 劳动力人口");
                sqlQuery.Append("  FROM         JXXM A ,XZQ B, FKXM C ");
                sqlQuery.Append("WHERE        A.XMSZX = B.XZQM ");
                sqlQuery.Append("  AND        A.XMMC = C.XMMC ");
                sqlQuery.Append("             AND   A.XMMC='" + strProjectName + "'");
                sqlQuery.Append("             AND   A.JXWJMC='" + strProjectNameJx + "'");
                gkfqd.Common.DbUse.GetOleDbconnection().Close();
                gkfqd.Common.DbUse.GetOleDbconnection().Open();
                dataSetOutPut.Clear();
                OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
                MyAdapter.Fill(dataSetOutPut);
                gkfqd.Common.DbUse.GetOleDbconnection().Close();

                sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                sw.WriteLine("<!-- 工矿建新项目XML格式 -->");
                sw.WriteLine("<CL_BUILD_NEW>");
                sw.WriteLine("  <ADDRNO name=\"所在地编号\">" + dataSetOutPut.Tables[0].Rows[0]["所在地编号"].ToString() + "</ADDRNO>");
                sw.WriteLine("  <BPASSDATE name=\"实际批准建新规模时间\">" + dataSetOutPut.Tables[0].Rows[0]["实际批准建新规模时间"].ToString() + " </BPASSDATE>");
                sw.WriteLine("  <BPASSFILENO name=\"实际批准建新规模文号\">" + dataSetOutPut.Tables[0].Rows[0]["实际批准建新规模文号"].ToString() + "</BPASSFILENO>");
                sw.WriteLine("  <BPASSFILENAME name=\"实际批准建新规模文件名称\"> " + dataSetOutPut.Tables[0].Rows[0]["实际批准建新规模文件名称"].ToString() + "</BPASSFILENAME>");
                sw.WriteLine("  <PRJNO name=\"所属复垦项目编号\"> " + dataSetOutPut.Tables[0].Rows[0]["所属复垦项目编号"].ToString() + "</PRJNO>");
                sw.WriteLine("  <PRJNAME name=\"所属复垦项目名称\"> " + dataSetOutPut.Tables[0].Rows[0]["所属复垦项目名称"].ToString() + "</PRJNAME>");
                sw.WriteLine("  <BPASSAREA name=\"实际批准建新规模\"> " + dataSetOutPut.Tables[0].Rows[0]["实际批准建新规模"].ToString() + "</BPASSAREA>");
                sw.WriteLine("  <BPASSAREAG name=\"其中建新可占用耕地面积\"> " + dataSetOutPut.Tables[0].Rows[0]["其中建新可占用耕地面积"].ToString() + "</BPASSAREAG>");
                sw.WriteLine("  <ISFITPLAN name=\"符合总体规划\"> " + dataSetOutPut.Tables[0].Rows[0]["符合总体规划"].ToString() + "</ISFITPLAN>");
                sw.WriteLine("  <BUSEALLAREA name=\"占用总面积\"> " + dataSetOutPut.Tables[0].Rows[0]["占用总面积"].ToString() + "</BUSEALLAREA>");
                sw.WriteLine("  <BUSEGAREA name=\"占用耕地面积\"> " + dataSetOutPut.Tables[0].Rows[0]["占用耕地面积"].ToString() + "</BUSEGAREA>");
                sw.WriteLine("  <BUSEGLANDLEVEL name=\"占用耕地质量\"> " + dataSetOutPut.Tables[0].Rows[0]["占用耕地质量"].ToString() + "</BUSEGLANDLEVEL>");
                sw.WriteLine("  <NBHOUSELANDAREA name=\"农民安置住房\"> " + dataSetOutPut.Tables[0].Rows[0]["农民安置住房"].ToString() + "</NBHOUSELANDAREA>");
                sw.WriteLine("  <NBFACLANDAREA name=\"农村基础设施和公共服务配套设施\"> " + dataSetOutPut.Tables[0].Rows[0]["农村基础设施和公共服务配套设施"].ToString() + "</NBFACLANDAREA>");
                sw.WriteLine("  <NBDEVELOPMENTLANDAREA name=\"非农产业发展用地\"> " + dataSetOutPut.Tables[0].Rows[0]["非农产业发展用地"].ToString() + "</NBDEVELOPMENTLANDAREA>");
                sw.WriteLine("  <NBOBLANDAREA name=\"其他建设用地\"> " + dataSetOutPut.Tables[0].Rows[0]["其他建设用地农村"].ToString() + "</NBOBLANDAREA>");
                sw.WriteLine("  <CBSLANDAREA name=\"商服用地\"> " + dataSetOutPut.Tables[0].Rows[0]["商服用地"].ToString() + "</CBSLANDAREA>");
                sw.WriteLine("  <CBZLANDAREA name=\"住宅用地\"> " + dataSetOutPut.Tables[0].Rows[0]["住宅用地"].ToString() + "</CBZLANDAREA>");
                sw.WriteLine("  <CBKLANDAREA name=\"工矿用地\"> " + dataSetOutPut.Tables[0].Rows[0]["工矿用地"].ToString() + "</CBKLANDAREA>");
                sw.WriteLine("  <CBOBLANDAREA name=\"其他建设用地\"> " + dataSetOutPut.Tables[0].Rows[0]["其他建设用地城镇"].ToString() + "</CBOBLANDAREA>");
                sw.WriteLine("  <COLLECTIONLANDAREA name=\"征收总面积\"> " + dataSetOutPut.Tables[0].Rows[0]["征收总面积"].ToString() + "</COLLECTIONLANDAREA>");
                sw.WriteLine("  <COLLECTIONCOST name=\"征收总费用\"> " + dataSetOutPut.Tables[0].Rows[0]["征收总费用"].ToString() + "</COLLECTIONCOST>");
                sw.WriteLine("  <PLACEMENTPEOPLENU name=\"安置总人口\"> " + dataSetOutPut.Tables[0].Rows[0]["安置总人口"].ToString() + "</PLACEMENTPEOPLENU>");
                sw.WriteLine("  <PLACEMENTLPEOPLENU name=\"劳动力人口\"> " + dataSetOutPut.Tables[0].Rows[0]["劳动力人口"].ToString() + "</PLACEMENTLPEOPLENU>");
                sw.WriteLine("</CL_BUILD_NEW>");   
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
                fs.Close();
                MessageBox.Show("建新项目信息导出成功！");
            }
        }
        #endregion

        #region 补充耕地地块录入
        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("请点击行头选择数据！");
                return;
            }
            DataGridViewRow dgvr = dataGridView1.CurrentRow;
            //获取项目名称值
            string strXmmc = dgvr.Cells["所属复垦项目名称"].Value.ToString();
            //----------------
            sqlQuery.Clear();
            sqlQuery.Append("SELECT XMSZX AS 项目所在县");
            sqlQuery.Append(" FROM   JXXM ");
            sqlQuery.Append(" WHERE  XMMC='" + strXmmc + "'");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSetTownName.Clear();
            OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter.Fill(dataSetTownName);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            //----------------
            gk011 frmgk011 = new gk011(strXmmc, "补耕", dataSetTownName.Tables[0].Rows[0]["项目所在县"].ToString());
            frmgk011.FormBorderStyle = FormBorderStyle.FixedSingle;//固定窗体大小不变
            frmgk011.StartPosition = FormStartPosition.CenterScreen;//窗体居中

            frmgk011.ShowDialog();
        }
        #endregion
    }
}
