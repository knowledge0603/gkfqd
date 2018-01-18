using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SuperMap.Data;
using System.Data.OleDb;
using SuperMap.Mapping;
using System.Diagnostics;
using SuperMap.UI;
using System.IO;

namespace gkfqd.ui.gk01
{
    public partial class gk01e : WinFormsUI.Docking.DockContent
    {
        #region  变量定义区域
        //sql语句拼接用字符串
        StringBuilder sqlQuery = new StringBuilder();
        //行政区名称用DataSet
        DataSet dataSetDistrictName = new DataSet();
        //行政区用DataSet
        DataSet dataSetDistrict = new DataSet();
        //行政区标识dataset
        DataSet dataSetIdentification = new DataSet();
        //层级用dataset
        DataSet dataSetHierarchy = new DataSet();
        //项目名称用dataset
        DataSet dataSetProjectName = new DataSet();
        //地块编号用dataset
        DataSet dataSetLandBlockNumber = new DataSet();
        //更新补耕地块用dataSet
        DataSet dataSetLandBlockUpdate = new DataSet();
        //建新地块信息输出xml文件用DataSet
        DataSet dataSetOutPut = new DataSet();
        //建新地块信息输出坐标xml文件用DataSet
        DataSet dataSetOutPutZb = new DataSet();
        //县名用DataSet
        DataSet dataSetTownName = new DataSet();
        //县名用dataset
        DataSet dataSetCountyName = new DataSet();
        private Datasource importDatasource;
        //工矿废弃地临时图层DatasetVector
        DatasetVector importResultShp = null;
        #endregion

        #region 初始化
        public gk01e()
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
            MapShow("JXT150102_2017");

            //当用户角色 2 为普通浏览者，浏览用户，只能看不能改删，看到所有用户的记录
            if (login.userRole == "2")
            {
                //添加按钮，不可用
                button2.Enabled = false;
                //修改按钮，不可用
                button3.Enabled = false;
                //删除图层不可用
                layersTree1.Enabled = false;
            }
        }
        #endregion 

        #region 项目查找
        private void button1_Click(object sender, EventArgs e)
        {
            sqlQuery.Clear();
            sqlQuery.Append("SELECT XMMC AS 项目名称, ");
            sqlQuery.Append("       JXWJMC AS 建新文件名称, ");
            sqlQuery.Append("       PZWH AS 批准文号, ");
            sqlQuery.Append("       PZSJ AS 批准时间, ");
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
            dataSetProjectName.Clear();
            OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter.Fill(dataSetProjectName);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            dataGridView1.DataSource = dataSetProjectName.Tables[0];
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
        #endregion

        #region 建新区地块导入
        private void button2_Click(object sender, EventArgs e)
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

        #region 建新地块信息修改
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView2.CurrentRow == null)
            {
                MessageBox.Show("请点击地块表行头选择数据！");
                return;
            }
            DataGridViewRow dgvr = dataGridView2.CurrentRow;
            string strDkbh = dgvr.Cells["地块逻辑编号"].Value.ToString();//获取smid值
            string strProjectName = dgvr.Cells["项目名称"].Value.ToString();//获取smid值
            string tableName = gkfqd.Common.DbUse.GetTownCodeByProjectNameBg(strProjectName);
            //获取项目名称值
            sqlQuery.Clear();
            sqlQuery.Append("SELECT  DKBH     AS 地块编号, ");
            sqlQuery.Append("  BZ           AS 备注, ");
            sqlQuery.Append("  XMSZXM           AS 项目所在县名称, ");
            sqlQuery.Append("  FKXMMC           AS 所属复垦项目名称");
            sqlQuery.Append(" FROM  " + tableName);
            sqlQuery.Append(" WHERE         FKXMMC='" + strProjectName + "'AND DKBH='" + strDkbh + "'");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSetLandBlockUpdate.Clear();
            OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter.Fill(dataSetLandBlockUpdate);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();

            //----------------
            sqlQuery.Clear();
            sqlQuery.Append("SELECT XMSZX AS 项目所在县");
            sqlQuery.Append(" FROM   JXXM ");
            sqlQuery.Append(" WHERE  XMMC='" + strProjectName + "'");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSetCountyName.Clear();
            OleDbDataAdapter MyAdapter1 = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter1.Fill(dataSetCountyName);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            //----------------

            gk01d frmgk01d = new gk01d(importDatasource, dgvr, strProjectName, "已录入", dataSetLandBlockUpdate.Tables[0], dataSetCountyName.Tables[0].Rows[0]["项目所在县"].ToString());
            frmgk01d.Owner = this;
            frmgk01d.FormBorderStyle = FormBorderStyle.FixedSingle;//固定窗体大小不变
            frmgk01d.StartPosition = FormStartPosition.CenterScreen;//窗体居中
            frmgk01d.ShowDialog();
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
            dataSetDistrictName.Clear();
            OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter.Fill(dataSetDistrictName);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();

            comboBox2.DataSource = dataSetDistrictName.Tables[0];
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
            dataSetDistrict.Clear();

            OleDbDataAdapter MyAdapter2 = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter2.Fill(dataSetDistrict);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            comboBox3.DataSource = dataSetDistrict.Tables[0];
            comboBox3.DisplayMember = "XZQM";
            comboBox3.ValueMember = "XZQM";
        }


        #endregion

        #region 区县级联选择
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            sqlQuery.Clear();
            sqlQuery.Append(" SELECT BHBS  FROM  XZQ  ");
            sqlQuery.Append(" WHERE XZQM  = '" + comboBox2.SelectedValue + "'");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSetIdentification.Clear();
            OleDbDataAdapter MyAdapter1 = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter1.Fill(dataSetIdentification);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            //取得包含标识
            String strBhbs = dataSetIdentification.Tables[0].Rows[0][0].ToString();


            sqlQuery.Clear();
            sqlQuery.Append(" SELECT XZQM  FROM  XZQ  ");
            sqlQuery.Append(" WHERE CJ ='2'  ");
            sqlQuery.Append(" AND BHBS ='" + strBhbs + "'");
            sqlQuery.Append(" ORDER BY  XZQDM  ");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSetHierarchy.Clear();

            OleDbDataAdapter MyAdapter2 = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter2.Fill(dataSetHierarchy);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            comboBox3.DataSource = dataSetHierarchy.Tables[0];
            comboBox3.DisplayMember = "XZQM";
            comboBox3.ValueMember = "XZQM";
        }
        #endregion

        #region  点击项目 dataGridView 行显示地块
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow dgvr = dataGridView1.CurrentRow;

            string strProjectName = dgvr.Cells["项目名称"].Value.ToString();
            string tableName = gkfqd.Common.DbUse.GetTownCodeByProjectNameBg(strProjectName);
            sqlQuery.Clear();
            sqlQuery.Append("SELECT SMID AS 地块编号, ");
            sqlQuery.Append("       DKBH AS 地块逻辑编号, ");
            sqlQuery.Append("       FKXMMC AS 项目名称, ");
            sqlQuery.Append("       XMSZXM AS 项目所在县名 ");
            sqlQuery.Append(" FROM  " + tableName);
            sqlQuery.Append(" WHERE FKXMMC= '" + strProjectName + "'");

            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSetLandBlockNumber.Clear();
            OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter.Fill(dataSetLandBlockNumber);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            dataGridView2.DataSource = dataSetLandBlockNumber.Tables[0];
            MapShow(tableName);
        }
        #endregion

        #region  显示地图
        public void MapShow(string mapLayerName)
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
            layersTree1.Icons = TreeIconTypes.Editable;

        }
        #endregion

        #region 点击 地块 dataGridView 显示 相应地块
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
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

        #region 地图操作按钮
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            mapControl1.Action = SuperMap.UI.Action.Select;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            mapControl1.Action = SuperMap.UI.Action.ZoomIn;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            mapControl1.Action = SuperMap.UI.Action.Pan;
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

        #region 建新地块信息导出
        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "导出建新地块信息文件(*.xml)|*.xml";
            sfd.FileName = "建新地块信息导出文件" + DateTime.Now.ToString("yyyyMMddhhmmss");
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                if (dataGridView2.CurrentRow == null)
                {
                    MessageBox.Show("请点击地块表行头，选择地块信息！");
                    return;
                }
                DataGridViewRow dgvr = dataGridView2.CurrentRow;
                //取得当前行项目名称值
                string strProjectName = dgvr.Cells["项目名称"].Value.ToString();
                //取得当前行地块编号该编号为用户输入的
                string strDkbh = dgvr.Cells["地块逻辑编号"].Value.ToString();
                string tableName = gkfqd.Common.DbUse.GetTownCodeByProjectNameJx(strProjectName);
                sqlQuery.Clear();
                sqlQuery.Append("SELECT  DKBH AS 建新地块编号, ");
                sqlQuery.Append("        DKWZ AS 位置, ");
                sqlQuery.Append("        DKMC AS 地块名称, ");
                sqlQuery.Append("        DKMJ AS 地块面积, ");
                sqlQuery.Append("        ZBX AS 坐标系, ");
                sqlQuery.Append("        JDFD AS 几度分带, ");
                sqlQuery.Append("        TYLX AS 投影类型, ");
                sqlQuery.Append("        JLDW AS 计量单位, ");
                sqlQuery.Append("        JD AS 精度, ");
                sqlQuery.Append("        JZDS AS 界址点数, ");
                sqlQuery.Append("        JLTCSX AS 记录图形属性 ");
                sqlQuery.Append(" FROM   "+ tableName);
                sqlQuery.Append(" WHERE   FKXMMC = '" + strProjectName + "'");
                sqlQuery.Append(" AND    DKBH = '" + strDkbh + "'");
                gkfqd.Common.DbUse.GetOleDbconnection().Close();
                gkfqd.Common.DbUse.GetOleDbconnection().Open();
                dataSetOutPut.Clear();
                OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
                MyAdapter.Fill(dataSetOutPut);
                gkfqd.Common.DbUse.GetOleDbconnection().Close();
                sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
                sw.WriteLine("<CL_BUILD_NEW_BLOCK>");
                sw.WriteLine("  <BUILDNLANDNO name=\"建新地块编号\">" + dataSetOutPut.Tables[0].Rows[0]["建新地块编号"].ToString() + "</BUILDNLANDNO>");
                sw.WriteLine("  <BLOCATION name=\"位置\"> " + dataSetOutPut.Tables[0].Rows[0]["位置"].ToString() + "</BLOCATION>");
                sw.WriteLine("  <LANDNAME name=\"地块名称\">" + dataSetOutPut.Tables[0].Rows[0]["地块名称"].ToString() + "</LANDNAME>");
                sw.WriteLine("  <LANDAREA name=\"地块面积\">" + dataSetOutPut.Tables[0].Rows[0]["地块面积"].ToString() + "</LANDAREA>");
                sw.WriteLine("  <COORSYSTEM name=\"坐标系\">" + dataSetOutPut.Tables[0].Rows[0]["坐标系"].ToString() + "</COORSYSTEM>");
                sw.WriteLine("  <CUTNU name=\"几度分带\">" + dataSetOutPut.Tables[0].Rows[0]["几度分带"].ToString() + "</CUTNU>");
                sw.WriteLine("  <SHADOWTYPE name=\"投影类型\">" + dataSetOutPut.Tables[0].Rows[0]["投影类型"].ToString() + "</SHADOWTYPE>");
                sw.WriteLine("  <MEASUREMENT name=\"计量单位\">" + dataSetOutPut.Tables[0].Rows[0]["计量单位"].ToString() + "</MEASUREMENT>");
                sw.WriteLine("  <PRECISION name=\"精度\">" + dataSetOutPut.Tables[0].Rows[0]["精度"].ToString() + "</PRECISION>");
                sw.WriteLine("  <DROPNU name=\"界址点数\">" + dataSetOutPut.Tables[0].Rows[0]["界址点数"].ToString() + "</DROPNU>");
                sw.WriteLine("  <DRAWINGPROPERTY name=\"记录图形属性\"> " + dataSetOutPut.Tables[0].Rows[0]["记录图形属性"].ToString() + "</DRAWINGPROPERTY>");
                 sw.Write("  <param ZB=\"@1$");
                //"1,3795840.7720,39534660.2200;2,3795836.7200,39534645.9400;3,3795824.1590,39534648.6700;4,3795780.9330,39534495.1600;5,3796003.2890,39534435.7600;6,3796049.6000,39534601.4200;7,3795840.7720,39534660.2200;" />");
              

                DataGridViewRow dgvr1 = dataGridView2.CurrentRow;
                string strSmid = dgvr1.Cells["地块编号"].Value.ToString();
                // 构造一个查询参数对象，查询选中的记录
                QueryParameter para = new QueryParameter();
                para.AttributeFilter = "SMID =" + strSmid;
                para.CursorType = CursorType.Dynamic;

                DatasetVector importResultShp = null;
                SuperMap.Data.Workspace workspace1;
                workspace1 = new SuperMap.Data.Workspace();
                /*WorkspaceconnectionInfo workspaceconnectionInfo = new WorkspaceconnectionInfo();
                workspaceconnectionInfo.Type = WorkspaceType.Oracle;
                workspaceconnectionInfo.Server = "ORCL";
                workspaceconnectionInfo.Database = "";
                workspaceconnectionInfo.Name = "workspace";
                workspaceconnectionInfo.User = "gkfqd";
                workspaceconnectionInfo.Password = "123456";
                workspace1.Open(workspaceconnectionInfo);*/
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
                                sw.Write((k+1).ToString()+","+point2d.X.ToString() + "," + point2d.Y.ToString() + ";");
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
                sw.WriteLine("</CL_BUILD_NEW_BLOCK>");
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
                fs.Close();
                MessageBox.Show("建新地块信息导出成功！");
            }
        }
        #endregion

        #region 地块坐标导出带属性信息
        private void button5_Click(object sender, EventArgs e)
        {

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "导出txt坐标文件(*.txt)|*.txt";
            sfd.FileName = "坐标导出文件" + DateTime.Now.ToString("yyyyMMddhhmmss");

            if (sfd.ShowDialog() == DialogResult.OK)
            {

                FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);

                if (dataGridView2.CurrentRow == null) return;
                DataGridViewRow dgvr = dataGridView2.CurrentRow;

                string strSmid = dgvr.Cells["地块编号"].Value.ToString();//获取smid值
                // 构造一个查询参数对象，查询选中的记录
                QueryParameter para = new QueryParameter();
                para.AttributeFilter = "SMID =" + strSmid;
                para.CursorType = CursorType.Dynamic;

               // DataGridViewRow dgvr1 = dataGridView2.CurrentRow;
                //取得当前行项目名称值
                string strProjectName = dgvr.Cells["项目名称"].Value.ToString();
                //取得当前行地块编号该编号为用户输入的
                string strDkbh = dgvr.Cells["地块逻辑编号"].Value.ToString();
                string tableName = gkfqd.Common.DbUse.GetTownCodeByProjectNameJx(strProjectName);

                DatasetVector importResultShp = null;
                SuperMap.Data.Workspace workspace1;
                workspace1 = new SuperMap.Data.Workspace();
               
                workspace1.Open(gkfqd.Common.Tool.GetConnectionInfo());
                importDatasource = workspace1.Datasources[gkfqd.Common.Tool.GetWorkspaceDataDatasources()];
                importResultShp = importDatasource.Datasets[tableName] as DatasetVector;
                Recordset recordset = null;
                //取系统时间
                DateTime dt = DateTime.Now;

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
                sqlQuery.Append(" FROM    "+ tableName);
                sqlQuery.Append(" WHERE   FKXMMC = '" + strProjectName + "'");
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
                sw.WriteLine("数据产生日期=" + dt.ToLongDateString().ToString());
                sw.WriteLine("坐标系=" + dataSetOutPutZb.Tables[0].Rows[0]["坐标系"].ToString());
                sw.WriteLine("几度分带=" + dataSetOutPutZb.Tables[0].Rows[0]["几度分带"].ToString());
                sw.WriteLine("投影类型=" + dataSetOutPutZb.Tables[0].Rows[0]["投影类型"].ToString());
                sw.WriteLine("计量单位=" + dataSetOutPutZb.Tables[0].Rows[0]["计量单位"].ToString());
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
                                sw.WriteLine((k + 1).ToString() + "," + (i + 1).ToString() + "," + point2d.X.ToString() + "," + point2d.Y.ToString());
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
                MessageBox.Show("建新属性坐标信息导出成功！");
            }
        }
        #endregion

    }
}
