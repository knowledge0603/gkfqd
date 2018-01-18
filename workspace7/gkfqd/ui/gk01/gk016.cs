using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using SuperMap.Data;
using SuperMap.Mapping;
using System.Diagnostics;
using SuperMap.UI;
using System.IO;
using gkfqd.Common;

namespace gkfqd.ui.gk01
{
    public partial class gk016 : WinFormsUI.Docking.DockContent
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
        DataSet dataSet8 = new DataSet();
        DataSet dataSetOutPut = new DataSet();
        DataSet dataSetOutPutZb = new DataSet();
        DataSet dataSetTownName = new DataSet();
        DataSet dataSetTownName1 = new DataSet();
        DataSet dataSetTownName2 = new DataSet();
        private Datasource importDatasource;
        //工矿废弃地临时图层DatasetVector
        DatasetVector importResultShp = null;
        #endregion

        #region 初始化
        public gk016()
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
            dataGridView2.AllowUserToAddRows = false;
            MapShow("T150102_2017");
            //当用户角色 2 为普通浏览者，浏览用户，只能看不能改删，看到所有用户的记录
            if (login.userRole == "2")
            {
                //添加地块按钮，不可用
                button2.Enabled = false;
                //修改地块按钮，不可用
                button3.Enabled = false;
                //图层删除操作不可用
                layersTree1.Enabled = false;
            }
        }
        #endregion 

        #region 项目查询
        private void button1_Click(object sender, EventArgs e)
        {
            sqlQuery.Clear();
            sqlQuery.Append("SELECT A.XMMC AS 项目名称, ");
            sqlQuery.Append("       A.ZZSSDW AS 组织实施单位, ");
            sqlQuery.Append("       A.PZWH AS 批准文号, ");
            sqlQuery.Append("       A.PZSJ AS 批准时间, ");
            sqlQuery.Append("       A.JHJSRQ AS 计划结束日期, ");
            sqlQuery.Append("       A.ZQYJDWSL AS 征求意见单位数量, ");
            sqlQuery.Append("       A.XMSZX AS 项目所在区县， ");
            sqlQuery.Append("       B.XZQDM AS 行政区代码 ");
            sqlQuery.Append(" FROM   FKXM  A, XZQ  B");
            sqlQuery.Append(" WHERE    A.XMSZX=B.XZQM");
            //项目所在省 
            sqlQuery.Append("  AND  A.XMSZ='" + comboBox1.Text + "'");
            if (comboBox2.Text != "全部")
            {
                //项目所在市
                sqlQuery.Append("  AND   A.XMSZS='" + comboBox2.Text + "'");
            }
            if (comboBox3.Text != "全部")
            {
                //项目所在县
                sqlQuery.Append("  AND   A.XMSZX='" + comboBox3.Text + "'");
            }
            //项目名称
            if (textBox1.Text != "")
            {
                sqlQuery.Append("  AND   A.XMMC LIKE '%" + textBox1.Text + "%'");
            }
            //年份  数据库字段对应 计划开始日期
            if (comboBox4.Text != "全部")
            {
                sqlQuery.Append("  AND   to_char(A.JHKSRQ,'yyyy')='" + comboBox4.Text + "'");
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

        #region 区县 级联选择
        private void comboBox2_SelectedIndexChanged_1(object sender, EventArgs e)
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

        #region 项目表 dataGridView1_CellClick
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dgvr = dataGridView1.CurrentRow;
            string strProjectName = dgvr.Cells["项目名称"].Value.ToString();//获取smid值
            string tableName = gkfqd.Common.DbUse.GetTownCodeByProjectName(strProjectName);

            sqlQuery.Clear();
            sqlQuery.Append("SELECT SMID AS 地块编号, ");
            sqlQuery.Append("       DKBH AS 地块逻辑编号, ");
            sqlQuery.Append("       FKXMMC AS 项目名称, ");
            sqlQuery.Append("       DKWZ AS 地块位置, ");
            sqlQuery.Append("       DKZGYMJ AS 地块中的国有面积, ");
            sqlQuery.Append("       DKZJTMJ AS  地块中的集体面积, ");
            sqlQuery.Append("       TYBZQK AS 土源保障情况 ");
            sqlQuery.Append("FROM  " + tableName);
            sqlQuery.Append(" WHERE FKXMMC= '" + strProjectName+"'");

            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSet6.Clear();
            OleDbDataAdapter MyAdapter1 = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter1.Fill(dataSet6);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            dataGridView2.DataSource = dataSet6.Tables[0];
            MapShow(tableName);
        }
        #endregion

        #region 显示地图
        public void MapShow(string  mapLayerName)
        {
            workspace1 = new SuperMap.Data.Workspace();
            workspace1.Open(gkfqd.Common.Tool.GetConnectionInfo());
            importDatasource = workspace1.Datasources[gkfqd.Common.Tool.GetWorkspaceDataDatasources()];
            importResultShp = importDatasource.Datasets[mapLayerName] as DatasetVector;
            mapControl1.Map.Workspace = workspace1;
            mapControl1.Map.Layers.Clear();
            mapControl1.Map.Layers.Add(importResultShp, true);
            mapControl1.Map.ViewEntire();
            mapControl1.Map.Refresh();
            layersTree1.Map = mapControl1.Map;
            layersTree1.Icons =  TreeIconTypes.Editable;

        }
        #endregion

        #region 地块 dataGridView  CellMouseClick
        private void dataGridView2_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView2.CurrentRow == null) return;
            DataGridViewRow dgvr = dataGridView2.CurrentRow;

            string strName = dgvr.Cells["地块编号"].Value.ToString();//获取smid值
            // 构造一个查询参数对象，查询选中的记录
            QueryParameter para = new QueryParameter();
            para.AttributeFilter = "SMID =" + strName;
            para.CursorType = CursorType.Dynamic;

            Recordset recordset = null;
            try
            {
                recordset = importResultShp.Query(para);
                Selection selection = mapControl1.Map.Layers[0].Selection;
                selection.FromRecordset(recordset);
                mapControl1.Map.EnsureVisible(recordset.GetGeometry(), 0.8);
                mapControl1.Map.Refresh();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
            finally
            {
                recordset.Dispose();
            }
        }
        #endregion

        #region 删除地块
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;
            DataGridViewRow dgvr = dataGridView2.CurrentRow;
            string strName = dgvr.Cells["地块编号"].Value.ToString();//获取smid值
            // 构造一个查询参数对象，查询选中的记录
            QueryParameter para = new QueryParameter();
            para.AttributeFilter = "SMID =" + strName;
            para.CursorType = CursorType.Dynamic;
            Recordset recordset = null;
            try
            {
                recordset = importResultShp.Query(para);
                Selection selection = mapControl1.Map.Layers[0].Selection;
                selection.FromRecordset(recordset);
                selection.Remove(System.Int32.Parse(strName));
                mapControl1.Map.Refresh();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
            finally
            {
                recordset.Dispose();
            }
        }
        #endregion

        #region 删除地块操作
        // 修改 Delete 键的响应内容为自定义的执行内容
        private void ChangeInteraction()
        {
            layersTree1.Interactions[InteractionType.KeyDelete] = new KeyEventHandler(workspaceTreeKeyDownCallBack);
        }

        // 自定义的 Delete 键操作的执行内容
        private void workspaceTreeKeyDownCallBack(object sender, KeyEventArgs e)
        {

            DialogResult RSS = MessageBox.Show(this, "确定要删除选中行数据？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            switch (RSS)
            {
                case DialogResult.Yes:
                    MessageBox.Show("已删除地块！");
                    break;
                case DialogResult.No:
                    break;
            }
        }

        private void gk016_KeyDown(object sender, KeyEventArgs e)
        {
            string G_str_Mode = "";
         //   string G_str_text = e.KeyCode + ":" + e.Modifiers + ":" + e.KeyData + ":" + "(" + e.KeyValue + ")";
          //捕捉del删除键46
            Keys key = e.KeyCode;
          
            switch (key)
            {
               
                case Keys.Delete:
                    ChangeInteraction();
                    break;
            }
        }

        private void gk016_KeyPress(object sender, KeyPressEventArgs e)
        {
            char Key_Char = e.KeyChar;//判斷按鍵的 Keychar 
        
            if(Key_Char.ToString()=="46"){
                ChangeInteraction();

            }
        }
        #endregion

        #region 添加地块信息
        private void button2_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("请点击行头选择数据！");
                return;
            }
            DataGridViewRow dgvr = dataGridView1.CurrentRow;
            //获取项目名称值
            string strXmmc = dgvr.Cells["项目名称"].Value.ToString();
            //----------------
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
            //----------------
            gk011 frmgk011 = new gk011(strXmmc,"复垦", dataSetTownName.Tables[0].Rows[0]["项目所在县"].ToString());
            frmgk011.FormBorderStyle = FormBorderStyle.FixedSingle;//固定窗体大小不变
            frmgk011.StartPosition = FormStartPosition.CenterScreen;//窗体居中

            frmgk011.ShowDialog();
        }
        #endregion

        #region 修改地块信息
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow == null)
            {
                MessageBox.Show("请点击地块表行头选择数据！");
                return;
            }
            DataGridViewRow dgvr = dataGridView2.CurrentRow;
            string strDkbh = dgvr.Cells["地块逻辑编号"].Value.ToString();
            string strProjectName = dgvr.Cells["项目名称"].Value.ToString();
            string tableName = gkfqd.Common.DbUse.GetTownCodeByProjectName(strProjectName);
            //获取项目名称值
            sqlQuery.Clear();
            sqlQuery.Append("SELECT  DKBH   AS 地块编号, ");
            sqlQuery.Append("DKZGYMJ        AS 地块中的国有面积, ");
            sqlQuery.Append("DKWZ           AS 地块位置, ");
            sqlQuery.Append("DKZJTMJ        AS 地块中的集体面积, ");
            sqlQuery.Append("TYBZQK         AS 土源保障情况, ");
            sqlQuery.Append("XZDXPD         AS 现状地形坡度, ");
            sqlQuery.Append("SYBZQK         AS 水源保障情况, ");
            sqlQuery.Append("YQFKGDDJ       AS 预期复垦耕地等级, ");
            sqlQuery.Append("YQDXPD         AS 预期地形坡度, ");
            sqlQuery.Append("QTGHMC         AS 其他规划名称, ");
            sqlQuery.Append("SFFH           AS 是否符合, ");
            sqlQuery.Append("SFFHTDLYZTGH   AS 是否符合土地利用总体规划, ");
            sqlQuery.Append("XZYWWRZK       AS 现状有无污染状况, ");
            sqlQuery.Append("XZWRZK         AS 现状污染状况, ");
            sqlQuery.Append("XZYWDZZHYH     AS 现状有无地质灾害隐患, ");
            sqlQuery.Append("JTYSYD         AS 交通运输用地, ");
            sqlQuery.Append("GYYD           AS 工业用地, ");
            sqlQuery.Append("QTJSYD         AS 其他建设用地, ");
            sqlQuery.Append("CKYD           AS 采矿用地, ");
            sqlQuery.Append("SYJSLSSYD      AS 水域及水利设施用地, ");
            sqlQuery.Append("XZXJ           AS 现状小计, ");
            sqlQuery.Append("YXTCHD         AS 有效土层厚度, ");
            sqlQuery.Append("YQYWWRZK       AS 预期有无污染状况, ");
            sqlQuery.Append("YQWRZK         AS 预期污染状况, ");
            sqlQuery.Append("YQYWDZZHYH     AS 预期有无地质灾害隐患, ");
            sqlQuery.Append("NCDL           AS 农村道路, ");
            sqlQuery.Append("GD             AS 耕地, ");
            sqlQuery.Append("YD             AS 园地, ");
            sqlQuery.Append("KTSM           AS 坑塘水面, ");
            sqlQuery.Append("LD             AS 林地, ");
            sqlQuery.Append("CD             AS 草地, ");
            sqlQuery.Append("QTNYD          AS 其他农用地, ");
            sqlQuery.Append("GQ             AS 沟渠, ");
            sqlQuery.Append("YQXJ           AS 预期小计,  ");
            sqlQuery.Append("JSYDHF         AS 建设用地合法性,  ");
            sqlQuery.Append("FKYWR          AS 复垦义务人情况,  ");
            sqlQuery.Append("DKMC           AS 地块名称,  ");
            sqlQuery.Append("DKMJ           AS 地块面积,  ");
            sqlQuery.Append("ZBX            AS 坐标系,  ");
            sqlQuery.Append("JZDS           AS 界址点数,  ");
            sqlQuery.Append("JDFD           AS 几度分带,  ");
            sqlQuery.Append("JD             AS 精度,  ");
            sqlQuery.Append("JLDW           AS 计量单位,  ");
            sqlQuery.Append("JLTCSX         AS 记录图形属性,  ");
            sqlQuery.Append("TYLX           AS 投影类型 ");
            //动态加载表名 
            sqlQuery.Append(" FROM     " + tableName);
            sqlQuery.Append(" WHERE         FKXMMC='" + strProjectName + "'AND DKBH='" + strDkbh + "'");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSet8.Clear();
            OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter.Fill(dataSet8);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();

            //----------------
            sqlQuery.Clear();
            sqlQuery.Append("SELECT XMSZX AS 项目所在县");
            sqlQuery.Append(" FROM   FKXM ");
            sqlQuery.Append(" WHERE  XMMC='" + strProjectName + "'");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSetTownName1.Clear();
            OleDbDataAdapter MyAdapter1 = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter1.Fill(dataSetTownName1);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            //----------------
            gk013 frmgk013 = new gk013(importDatasource, dgvr, strProjectName, "已录入", dataSet8.Tables[0], dataSetTownName1.Tables[0].Rows[0]["项目所在县"].ToString());
            frmgk013.Owner = this;
            frmgk013.FormBorderStyle = FormBorderStyle.FixedSingle;//固定窗体大小不变
            frmgk013.StartPosition = FormStartPosition.CenterScreen;//窗体居中
            frmgk013.ShowDialog();
        }
        #endregion

        #region 地图操作
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            mapControl1.Action = SuperMap.UI.Action.Select;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            mapControl1.Action = SuperMap.UI.Action.Pan;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            mapControl1.Action = SuperMap.UI.Action.ZoomIn;
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            mapControl1.Action = SuperMap.UI.Action.ZoomOut;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            mapControl1.Action = SuperMap.UI.Action.ZoomFree;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            mapControl1.Map.ViewEntire();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            mapControl1.Map.Refresh();
        }
        #endregion

        #region 导出地块信息
        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "导出地块信息文件(*.xml)|*.xml";
            sfd.FileName = "地块信息导出文件" + DateTime.Now.ToString("yyyyMMddhhmmss");
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                if (dataGridView2.CurrentRow == null) {
                    MessageBox.Show("请点击地块表行头，选择地块信息！");
                    return;
                }
                DataGridViewRow dgvr = dataGridView2.CurrentRow;
                //取得当前行项目名称值
                string strProjectName = dgvr.Cells["项目名称"].Value.ToString();
                //取得当前行地块编号该编号为用户输入的
                string strDkbh = dgvr.Cells["地块逻辑编号"].Value.ToString();
                string tableName = gkfqd.Common.DbUse.GetTownCodeByProjectName(strProjectName);
                sqlQuery.Clear();
                sqlQuery.Append("SELECT  DKBH AS 地块编号, ");
                sqlQuery.Append("        DKWZ AS 地块位置, ");
                sqlQuery.Append("        DKZGYMJ AS 地块中的国有面积, ");
                sqlQuery.Append("        DKZJTMJ AS 地块中的集体面积, ");
                sqlQuery.Append("        XZDXPD AS 地形坡度, ");
                sqlQuery.Append("        TYBZQK AS 土源保障情况, ");
                sqlQuery.Append("        SYBZQK AS 水源保障情况, ");
                sqlQuery.Append("        XZYWDZZHYH AS 有无地质灾害隐患, ");
                sqlQuery.Append("        XZYWWRZK AS 有无污染状况, ");
                sqlQuery.Append("        XZWRZK AS 污染状况, ");
                sqlQuery.Append("        SFFHTDLYZTGH AS 是否符合土地利用总体规划, ");
                sqlQuery.Append("        SFFH AS 是否符合, ");
                sqlQuery.Append("        QTGHMC AS 其他规划名称, ");
                sqlQuery.Append("        CKYD AS 采矿用地, ");
                sqlQuery.Append("        GYYD AS 工业用地, ");
                sqlQuery.Append("        JTYSYD AS 交通运输用地, ");
                sqlQuery.Append("        SYJSLSSYD AS 水域及水利设施用地,  ");
                sqlQuery.Append("        QTJSYD  AS  其它建设用地, ");
                sqlQuery.Append("        GD AS 预期耕地面积, ");
                sqlQuery.Append("        YD AS 预期园地面积, ");
                sqlQuery.Append("        LD    AS    预期林地面积, ");
                sqlQuery.Append("        CD    AS    预期草地面积, ");
                sqlQuery.Append("        NCDL  AS    预期农村道路, ");
                sqlQuery.Append("        KTSM  AS    预期坑塘水面, ");
                sqlQuery.Append("        GQ    AS    预期沟渠, ");
                sqlQuery.Append("        QTNYD  AS   预期其他农用地面积, ");
                sqlQuery.Append("        YQDXPD AS 预期地形坡度, ");
                sqlQuery.Append("        YXTCHD AS  预期有效土层厚度, ");
                sqlQuery.Append("        YQYWDZZHYH AS 预期有无地质灾害隐患, ");
                sqlQuery.Append("        YQYWWRZK AS  预期有无污染状况, ");
                sqlQuery.Append("        YQFKGDDJ AS  预期耕地等级, ");
                sqlQuery.Append("        YQWRZK AS  预期没有达标填写污染状况,");
                sqlQuery.Append("        JSYDHF AS  建设用地合法性,");
                sqlQuery.Append("        FKYWR AS  复垦义务人情况,");
                sqlQuery.Append("        DKMC AS  地块名称,");
                sqlQuery.Append("        DKMJ AS  地块面积,");
                sqlQuery.Append("        ZBX AS  坐标系,");
                sqlQuery.Append("        JZDS AS  界址点数,");
                sqlQuery.Append("        JDFD AS  几度分带,");
                sqlQuery.Append("        JD AS  精度,");
                sqlQuery.Append("        JLDW AS  计量单位,");
                sqlQuery.Append("        JLTCSX AS  记录图形属性,");
                sqlQuery.Append("        TYLX AS  投影类型");
                sqlQuery.Append(" FROM " + tableName);
                sqlQuery.Append(" WHERE   FKXMMC = '" + strProjectName + "'");
                sqlQuery.Append(" AND    DKBH = '" + strDkbh + "'");
                gkfqd.Common.DbUse.GetOleDbconnection().Close();
                gkfqd.Common.DbUse.GetOleDbconnection().Open();
                dataSetOutPut.Clear();
                OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
                MyAdapter.Fill(dataSetOutPut);
                gkfqd.Common.DbUse.GetOleDbconnection().Close();

                sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                sw.WriteLine("<!-- 工矿复垦项目导入地块XML格式 -->");
                sw.WriteLine("<CL_PROJECT_BLOCK>");
                sw.WriteLine("  <RECLBLOCKNO name=\"地块编号\">" + dataSetOutPut.Tables[0].Rows[0]["地块编号"].ToString() + "</RECLBLOCKNO>");
                sw.WriteLine("  <LconnAME name=\"位置行政村\">" + dataSetOutPut.Tables[0].Rows[0]["地块位置"].ToString() + "</LconnAME>");
                sw.WriteLine("  <BLOCKAREAG name=\"地块中的国有面积\"> " + dataSetOutPut.Tables[0].Rows[0]["地块中的国有面积"].ToString() + "</BLOCKAREAG>");
                sw.WriteLine("  <BLOCKAREAJ name=\"地块中的集体面积\">" + dataSetOutPut.Tables[0].Rows[0]["地块中的集体面积"].ToString() + "</BLOCKAREAJ>");
                sw.WriteLine("  <!-- “建设用地合法性”填写规则：0表示“空”，1表示“1987年1月《土地管理法》颁布实施前形成”，2表示“1987年1月至1999年1月《土地管理法》修订实施前形成”，3表示“1999年1月至2011年《土地复垦条例》颁布实施前形成”-->");
                string strJsyd = "";
                if (dataSetOutPut.Tables[0].Rows[0]["建设用地合法性"].ToString() == "1987年1月《土地管理法》颁布实施前形成")
                {
                    strJsyd = "1";
                }
                if (dataSetOutPut.Tables[0].Rows[0]["建设用地合法性"].ToString() == "1987年1月至1999年1月《土地管理法》修订实施前形成")
                {
                    strJsyd = "2";
                }
                if (dataSetOutPut.Tables[0].Rows[0]["建设用地合法性"].ToString() == "1999年1月至2011年《土地复垦条例》颁布实施前形成")
                {
                    strJsyd = "3";
                }
                sw.WriteLine("  <BUILDLEGALNO name=\"建设用地合法性\">" + strJsyd + "</BUILDLEGALNO>");
                sw.WriteLine("  <!--“复垦义务人情况”填写规则：0表示“空”，1表示“复垦义务人灭失”，2表示“1988年《土地复垦规定》颁布实施以前损毁土地”，3表示“政策原因无法追溯义务人”-->");
                string strFkyw = "";
                if (dataSetOutPut.Tables[0].Rows[0]["复垦义务人情况"].ToString() == "复垦义务人灭失")
                {
                    strFkyw = "1";
                }
                if (dataSetOutPut.Tables[0].Rows[0]["复垦义务人情况"].ToString() == "1988年《土地复垦规定》颁布实施以前损毁土地")
                {
                    strFkyw = "2";
                }
                if (dataSetOutPut.Tables[0].Rows[0]["复垦义务人情况"].ToString() == "政策原因无法追溯义务人")
                {
                    strFkyw = "3";
                }
                sw.WriteLine(" <RELCPERSON name=\"复垦义务人情况\">"+strFkyw+"</RELCPERSON>");
                string strTdbzqk = "";
                if (dataSetOutPut.Tables[0].Rows[0]["土源保障情况"].ToString() == "")
                {
                    strTdbzqk = "0";
                }
                if (dataSetOutPut.Tables[0].Rows[0]["土源保障情况"].ToString() == "良好")
                {
                    strTdbzqk = "1";
                }
                if (dataSetOutPut.Tables[0].Rows[0]["土源保障情况"].ToString() == "一般")
                {
                    strTdbzqk = "2";
                }
                if (dataSetOutPut.Tables[0].Rows[0]["土源保障情况"].ToString() == "较差")
                {
                    strTdbzqk = "3";
                }
                sw.WriteLine("  <LANDSTUATION name=\"土源保障情况\">"+strTdbzqk+"</LANDSTUATION>");
                sw.WriteLine("  <!--“水源保障情况” 0表示“空”，1表示“良好”，2表示“一般”，3表示“较差”-->");
                string strSybzqk = "";
                if (dataSetOutPut.Tables[0].Rows[0]["水源保障情况"].ToString() == "")
                {
                    strSybzqk = "0";
                }
                if (dataSetOutPut.Tables[0].Rows[0]["水源保障情况"].ToString() == "良好")
                {
                    strSybzqk = "1";
                }
                if (dataSetOutPut.Tables[0].Rows[0]["水源保障情况"].ToString() == "一般")
                {
                    strSybzqk = "2";
                }
                if (dataSetOutPut.Tables[0].Rows[0]["水源保障情况"].ToString() == "较差")
                {
                    strSybzqk = "3";
                }
                sw.WriteLine("  <WATERSTUATION name=\"水源保障情况\">" + strSybzqk + "</WATERSTUATION>");
                string strYwyh = "";
                if (dataSetOutPut.Tables[0].Rows[0]["有无地质灾害隐患"].ToString() == "无")
                {
                    strYwyh = "0";
                }
                if (dataSetOutPut.Tables[0].Rows[0]["有无地质灾害隐患"].ToString() == "有")
                {
                    strYwyh = "1";
                }
                sw.WriteLine("  <ISLANDHURT nmae=\"有无地质灾害隐患\">" + strYwyh + "</ISLANDHURT>");
                string strYwwr = "";
                if (dataSetOutPut.Tables[0].Rows[0]["有无污染状况"].ToString() == "无")
                {
                    strYwwr = "0";
                }
                if (dataSetOutPut.Tables[0].Rows[0]["有无污染状况"].ToString() == "有")
                {
                    strYwwr = "1";
                }
                sw.WriteLine("  <ISPOLUSTUATION name=\"有无污染状况\">" + strYwwr + "</ISPOLUSTUATION>");
                sw.WriteLine("  <POLUSTUATION name=\"污染状况\">" + dataSetOutPut.Tables[0].Rows[0]["污染状况"].ToString() + "</POLUSTUATION>");
                string strFh = "";
                if (dataSetOutPut.Tables[0].Rows[0]["是否符合土地利用总体规划"].ToString() == "否")
                {
                    strFh = "0";
                }
                if (dataSetOutPut.Tables[0].Rows[0]["是否符合土地利用总体规划"].ToString() == "是")
                {
                    strFh = "1";
                }
                sw.WriteLine("  <ISLANDUSEPLAN name=\"是否符合土地利用总体规划\">" + strFh + "</ISLANDUSEPLAN>");
                string strSfh = "";
                if (dataSetOutPut.Tables[0].Rows[0]["是否符合"].ToString() == "否")
                {
                    strSfh = "0";
                }
                if (dataSetOutPut.Tables[0].Rows[0]["是否符合"].ToString() == "是")
                {
                    strSfh = "1";
                }
                sw.WriteLine("  <ISOTHERPLAN name=\"是否符合\">" + strSfh + "</ISOTHERPLAN>");
                sw.WriteLine("  <OTHERPLANNAME name=\"其他规划名称\">" + dataSetOutPut.Tables[0].Rows[0]["其他规划名称"].ToString() + "</OTHERPLANNAME>");
                sw.WriteLine("  <CLANDAREA name=\"采矿用地\">"+ dataSetOutPut.Tables[0].Rows[0]["采矿用地"].ToString()+"</CLANDAREA>");
                sw.WriteLine("  <GLANDAREA nmae=\"工业用地\">" + dataSetOutPut.Tables[0].Rows[0]["工业用地"].ToString() + "</GLANDAREA>");
                sw.WriteLine("  <JLANDAREA name=\"交通运输用地\">" + dataSetOutPut.Tables[0].Rows[0]["交通运输用地"].ToString() + "</JLANDAREA>");
                sw.WriteLine("  <SLANDAREA name=\"水域及水利设施用地\">" + dataSetOutPut.Tables[0].Rows[0]["水域及水利设施用地"].ToString() + "</SLANDAREA>");
                sw.WriteLine("  <OLANDAREA name=\"其它建设用地\">" + dataSetOutPut.Tables[0].Rows[0]["其它建设用地"].ToString() + "</OLANDAREA>");
                sw.WriteLine("  <EXGLANDAREA name=\"预期耕地面积\">" + dataSetOutPut.Tables[0].Rows[0]["预期耕地面积"].ToString() + "</EXGLANDAREA>");
                sw.WriteLine("  <EXYLANDAREA name=\"预期园地面积\">" + dataSetOutPut.Tables[0].Rows[0]["预期园地面积"].ToString() + "</EXYLANDAREA>");
                sw.WriteLine("  <EXLLANDAREA name=\"预期林地面积\">" + dataSetOutPut.Tables[0].Rows[0]["预期林地面积"].ToString() + "</EXLLANDAREA>");
                sw.WriteLine("  <EXCLANDAREA name=\"预期草地面积\">" + dataSetOutPut.Tables[0].Rows[0]["预期草地面积"].ToString() + "</EXCLANDAREA>");
                sw.WriteLine("  <EXDLANDAREA name=\"预期农村道路\">" + dataSetOutPut.Tables[0].Rows[0]["预期农村道路"].ToString() + "</EXDLANDAREA>");
                sw.WriteLine("  <EXKLANDAREA nmae=\"预期坑塘水面\">" + dataSetOutPut.Tables[0].Rows[0]["预期坑塘水面"].ToString() + "</EXKLANDAREA>");
                sw.WriteLine("  <EXQLANDAREA name=\"预期沟渠\">" + dataSetOutPut.Tables[0].Rows[0]["预期沟渠"].ToString() + "</EXQLANDAREA>");
                sw.WriteLine("  <EXOLANDAREA name=\"预期其他农用地面积\">" + dataSetOutPut.Tables[0].Rows[0]["预期其他农用地面积"].ToString() + "</EXOLANDAREA>");
                sw.WriteLine("  <!--“预期地形坡度”填写规则：请选择填入0，0—5度，5—15度，15—25度，25度以上，中的一个。-->");
                sw.WriteLine("  <EXLANDSLIP name=\"预期地形坡度\">" + dataSetOutPut.Tables[0].Rows[0]["预期地形坡度"].ToString() + "</EXLANDSLIP>");
                sw.WriteLine("  <EXLANDINCL name=\"预期有效土层厚度\">" + dataSetOutPut.Tables[0].Rows[0]["预期有效土层厚度"].ToString() + "</EXLANDINCL>");
                string strZhyh = "";
                if (dataSetOutPut.Tables[0].Rows[0]["预期有无地质灾害隐患"].ToString() == "无")
                {
                    strZhyh = "0";
                }
                if (dataSetOutPut.Tables[0].Rows[0]["预期有无地质灾害隐患"].ToString() == "有")
                {
                    strZhyh = "1";
                }
                sw.WriteLine("  <EXISLANDHURT name=\"预期有无地质灾害隐患\">" + strZhyh + "</EXISLANDHURT>");
                string strYwwrzk = "";
                if (dataSetOutPut.Tables[0].Rows[0]["预期有无污染状况"].ToString() == "无")
                {
                    strYwwrzk = "0";
                }
                if (dataSetOutPut.Tables[0].Rows[0]["预期有无污染状况"].ToString() == "有")
                {
                    strYwwrzk = "1";
                }
                sw.WriteLine("  <EXISPOLUSTUATION name=\"预期有无污染状况\">" + strYwwrzk + "</EXISPOLUSTUATION>");
                sw.WriteLine("  <EXPOLUSTUATION name=\"预期没有达标填写污染状况\">" + dataSetOutPut.Tables[0].Rows[0]["预期没有达标填写污染状况"].ToString() + "</EXPOLUSTUATION>");
                sw.WriteLine("  <EXGLANDRECLLEVEL name=\"预期耕地等级\">" + dataSetOutPut.Tables[0].Rows[0]["预期耕地等级"].ToString() + "</EXGLANDRECLLEVEL>");
                sw.WriteLine("  <CUTNU name=\"几度分带\">" + dataSetOutPut.Tables[0].Rows[0]["几度分带"].ToString() + "</CUTNU>");
                sw.WriteLine("  <MEASUREMENT name=\"计量单位\">" + dataSetOutPut.Tables[0].Rows[0]["计量单位"].ToString() + "</MEASUREMENT>");
                sw.WriteLine("  <PRECISION name=\"精度\">" + dataSetOutPut.Tables[0].Rows[0]["精度"].ToString() + "</PRECISION>");
                sw.WriteLine("  <DROPNU name=\"界址点数\">" + dataSetOutPut.Tables[0].Rows[0]["界址点数"].ToString() + "</DROPNU>");
                sw.WriteLine("  <LANDNAME name=\"地块名称\">" + dataSetOutPut.Tables[0].Rows[0]["地块名称"].ToString() + "</LANDNAME>");
                sw.WriteLine("  <DRAWINGPROPERTY name=\"记录图形属性\">" + dataSetOutPut.Tables[0].Rows[0]["记录图形属性"].ToString() + "</DRAWINGPROPERTY>");
                sw.WriteLine("  <SHADOWTYPE name=\"投影类型\">" + dataSetOutPut.Tables[0].Rows[0]["投影类型"].ToString() + "</SHADOWTYPE>");
                sw.WriteLine("  <COORSYSTEM name=\"坐标系\">" + dataSetOutPut.Tables[0].Rows[0]["坐标系"].ToString() + "</COORSYSTEM>");
                sw.WriteLine("  <LANDAREA name=\"地块面积\">" + dataSetOutPut.Tables[0].Rows[0]["地块面积"].ToString() + "</LANDAREA>");
                sw.Write("  <param ZB=\"@1$");
                DataGridViewRow dgvr1 = dataGridView2.CurrentRow;
                string strSmid = dgvr1.Cells["地块编号"].Value.ToString();//获取smid值
                // 构造一个查询参数对象，查询选中的记录
                QueryParameter para = new QueryParameter();
                para.AttributeFilter = "SMID =" + strSmid;
                para.CursorType = CursorType.Dynamic;

                DatasetVector importResultShp = null;
                SuperMap.Data.Workspace workspace1;
                workspace1 = new SuperMap.Data.Workspace();
                workspace1.Open(gkfqd.Common.Tool.GetConnectionInfo());
                importDatasource = workspace1.Datasources[gkfqd.Common.Tool.GetWorkspaceDataDatasources()];
                importResultShp = importDatasource.Datasets[tableName] as DatasetVector;
                Recordset recordset = null;
                try
                {
                    recordset = importResultShp.Query(para);
                    while (!recordset.IsEOF)
                    {
                        Geometry geometry = recordset.GetGeometry();
                        GeoRegion region = geometry as GeoRegion;
                        for (int i = 0; i < region.PartCount; i++)
                        {
                            Point2Ds point2ds = region[i];
                            for (int k = 0; k < point2ds.Count; k++)
                            {
                                Point2D point2d = point2ds[k];
                                sw.Write((k + 1).ToString() + "," + point2d.X.ToString() + "," + point2d.Y.ToString() + ";");
                            }
                        }
                        recordset.MoveNext();
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);
                }
                finally
                {
                    recordset.Dispose();
                }
                sw.WriteLine(" \" />");
                sw.WriteLine("</CL_PROJECT_BLOCK>");
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
                fs.Close();
                MessageBox.Show("项目信息导出成功！");
            }
        }
        #endregion

        private void button5_Click(object sender, EventArgs e)
        {
           
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "导出txt坐标文件(*.txt)|*.txt";
            sfd.FileName = "坐标导出文件" + DateTime.Now.ToString("yyyyMMddhhmmss");

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                DataGridViewRow dgvr1 = dataGridView2.CurrentRow;
                //取得当前行项目名称值
                string strProjectName = dgvr1.Cells["项目名称"].Value.ToString();
                //取得当前行地块编号该编号为用户输入的
                string strDkbh = dgvr1.Cells["地块逻辑编号"].Value.ToString();
                string tableName = gkfqd.Common.DbUse.GetTownCodeByProjectName(strProjectName);

                FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);

                if (dataGridView2.CurrentRow == null) return;
                DataGridViewRow dgvr = dataGridView2.CurrentRow;


                string strSmid = dgvr.Cells["地块编号"].Value.ToString();//获取smid值
                // 构造一个查询参数对象，查询选中的记录
                QueryParameter para = new QueryParameter();
                para.AttributeFilter = "SMID =" + strSmid;
                para.CursorType = CursorType.Dynamic;

                DatasetVector importResultShp = null;
                SuperMap.Data.Workspace workspace1;
                workspace1 = new SuperMap.Data.Workspace();
            
                workspace1.Open(gkfqd.Common.Tool.GetConnectionInfo());
                importDatasource = workspace1.Datasources[gkfqd.Common.Tool.GetWorkspaceDataDatasources()];
                importResultShp = importDatasource.Datasets[tableName] as DatasetVector;
                Recordset recordset = null;
                //取系统时间
                DateTime dt= DateTime.Now;

                if (dataGridView2.CurrentRow == null)
                {
                    MessageBox.Show("请点击地块表行头，选择地块信息！");
                    return;
                }
               
                sqlQuery.Clear();
                sqlQuery.Append("SELECT  DKMC AS 地块名称, ");
                sqlQuery.Append("        DKMJ AS 地块面积, ");
                sqlQuery.Append("        ZBX AS 坐标系, ");
                sqlQuery.Append("        JZDS AS 界址点数, ");
                sqlQuery.Append("        JDFD AS 几度分带, ");
                sqlQuery.Append("        JD AS 精度, ");
                sqlQuery.Append("        JLDW AS 计量单位, ");
                sqlQuery.Append("        JLTCSX AS 记录图形属性, ");
                sqlQuery.Append("        TYLX AS 投影类型 ");
                sqlQuery.Append(" FROM  " + tableName);
                sqlQuery.Append("  WHERE   FKXMMC = '" + strProjectName + "'");
                sqlQuery.Append(" AND    DKBH = '" + strDkbh + "'");
                gkfqd.Common.DbUse.GetOleDbconnection().Close();
                gkfqd.Common.DbUse.GetOleDbconnection().Open();
                dataSetOutPutZb.Clear();
                OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
                MyAdapter.Fill(dataSetOutPutZb);
                gkfqd.Common.DbUse.GetOleDbconnection().Close();
                
                sw.WriteLine("[属性描述]"); 
                sw.WriteLine("格式版本号=1．01版本"); 
                sw.WriteLine("数据产生单位=国土资源部"); 
                sw.WriteLine("数据产生日期="+dt.ToLongDateString().ToString());
                sw.WriteLine("坐标系=" + dataSetOutPutZb.Tables[0].Rows[0]["坐标系"].ToString());
                sw.WriteLine("几度分带=" + dataSetOutPutZb.Tables[0].Rows[0]["几度分带"].ToString());
                sw.WriteLine("投影类型=" + dataSetOutPutZb.Tables[0].Rows[0]["投影类型"].ToString());
                sw.WriteLine("计量单位="+dataSetOutPutZb.Tables[0].Rows[0]["计量单位"].ToString());
                sw.WriteLine("带号=" + dataSetOutPutZb.Tables[0].Rows[0]["几度分带"].ToString());
                sw.WriteLine("精度=" + dataSetOutPutZb.Tables[0].Rows[0]["精度"].ToString());
                sw.WriteLine("转换参数=0,0,0,0,0,0,0");
                sw.WriteLine("[地块坐标]");
                //保留该行记录导出结果，界址点数,地块面积,地块编号,地块名称,记录图形属性(点、线、面),图幅号,地块用途,地类编码,@
                //以上记录信息如界址点数、地块面积、地块用途、地类编码需进一步编码取得
                //sw.WriteLine("9,0.018,2003-10,双桥乡地块1,面,I－50-77-（22）,公共基础设施,,@");
                try
                {
                    recordset = importResultShp.Query(para);
                    while (!recordset.IsEOF)
                    {
                        Geometry geometry = recordset.GetGeometry();
                        GeoRegion region = geometry as GeoRegion;
                        for (int i = 0; i < region.PartCount; i++)
                        {
                            Point2Ds point2ds = region[i];
                            for (int k = 0; k < point2ds.Count; k++)
                            {
                                Point2D point2d = point2ds[k];
                                //点号,地块圈号,X坐标,Y坐标
                                sw.WriteLine((k + 1).ToString() +","+ (i + 1).ToString() + "," + point2d.X.ToString() + "," + point2d.Y.ToString());
                            }
                        }
                        recordset.MoveNext();
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.Message);
                }
                finally
                {
                    recordset.Dispose();
                }
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
                fs.Close();
                MessageBox.Show("属性坐标信息导出成功！");
            }
        }

    }
}
