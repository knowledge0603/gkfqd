using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data.OleDb;
using System.Diagnostics;
using SuperMap.Data;
using SuperMap.Mapping;
using SuperMap.UI;
using SuperMap.Analyst.SpatialAnalyst;
using System.IO;

namespace gkfqd.ui.gk03
{

    public partial class gk032 : WinFormsUI.Docking.DockContent
    {
        #region 定义区
        //oracle 连接 操作
     //   public static OleDbConnection conn = new OleDbConnection("Provider=MSDAORA.1;User ID=gkfqd;Password=123456;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST =192.168.1.103)(PORT = 1521))) (CONNECT_DATA = (SERVICE_NAME = orcl)))");
        DataSet dataSet = new DataSet();
     //   private Datasource gk032Datasource;
        private MapControl mapControl1;
        //工矿废弃地临时图层DatasetVector
        DatasetVector importResultShp = null;
       
        //文件表查询用
        DataSet dataSet1 = new DataSet();
        DataSet dataSetCity  = new DataSet();
        DataSet dataSetTown = new DataSet();
        DataSet dataSetTownSelect = new DataSet();
        DataSet dataSetCitySelect = new DataSet();
       //图层smid查询用
        Recordset recordset = null;
        //叠加图层计算叠加后面积用DataSet
        DataSet dataSetIntersectTemp = new DataSet();
        private Datasource importDatasource;
        public GeoPoint geoPointNew;
        #endregion

        #region 初始化
        public gk032(Datasource datasource, MapControl mapControl1Send)
        {
            InitializeComponent();
            comboBox1.Text = "全部";
            comboBox2.Text = "全部";
            comboBox3.Text = "全部";
            comboBox5.Text = "全部";
           
           // gk032Datasource = datasource;
            mapControl1 = mapControl1Send;
            //不显示dataGridView1最后一行
            dataGridView1.AllowUserToAddRows = false;
           
            comboBox7.SelectedIndex = 0;
            //区县comboBox列表加载
            LoadComboBox();
        }


        public gk032()
        {
            InitializeComponent();
            comboBox1.Text = "全部";
            comboBox2.Text = "全部";
            comboBox3.Text = "全部";
            comboBox5.Text = "全部";

           
            //不显示dataGridView1最后一行
            dataGridView1.AllowUserToAddRows = false;

            comboBox7.SelectedIndex = 0;
            //区县comboBox列表加载
            LoadComboBox();
            MapShow("T150102_2017");
        }
        #endregion

        #region  查询
        private void button1_Click(object sender, EventArgs e)
        {
            string tableName = gkfqd.Common.DbUse.GetTownCode(comboBox4.Text);
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT B.SMID AS 地块编号, ");
            sqlQuery.Append("       B.DKBH AS 地块逻辑编号, ");
            sqlQuery.Append("       B.FKXMMC AS 项目名称, ");
            sqlQuery.Append("       B.DKWZ AS 地块位置, ");
            sqlQuery.Append("       B.DKZGYMJ AS 地块中的国有面积, ");
            sqlQuery.Append("       B.DKZJTMJ AS  地块中的集体面积, ");
            sqlQuery.Append("       B.TYBZQK AS 土源保障情况 ");
            sqlQuery.Append("FROM      FKXM  A, "+ tableName + " B" );
            sqlQuery.Append("  WHERE  1=1  ");
            sqlQuery.Append("  AND A.XMMC = B.FKXMMC  ");
            //项目区分
            if(comboBox1.Text != "全部"){
                sqlQuery.Append(" AND  A.XMQF ='" + comboBox1.Text+"'");
            }
            //指标使用
            if (comboBox2.Text != "全部")
            {
                sqlQuery.Append(" AND  A.ZBSY ='" + comboBox2.Text + "'");
            }
            //验收状态
            if (comboBox3.Text != "全部")
            {
                sqlQuery.Append(" AND A.YSZT ='" + comboBox3.Text + "'");
            }
            //资金安排
            if (comboBox5.Text != "全部")
            {
                sqlQuery.Append(" AND  A.ZJAP ='" + comboBox5.Text + "'");
            }
            //项目名称
            if (textBox1.Text != "")
            {
                sqlQuery.Append(" AND B.FKXMMC LIKE '%" + textBox1.Text + "%'");
            }
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSet.Clear();
            OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            try 
            { 
                MyAdapter.Fill(dataSet);
            }
            catch(Exception ex1)
            {
                if (ex1.Message.Contains("ORA-00942"))
                {
                    MessageBox.Show("查询对应县图层不存在，请导入对应县图层！");
                    return;
                }
            }
            
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            dataGridView1.DataSource = dataSet.Tables[0];
            
        }
        #endregion
        
        #region  点击链接详细内容
        
        private void linkLable_Click(object sender, System.EventArgs e)
        {
           string tempUrl =  ((LinkLabel)sender).Text;
           Process.Start(tempUrl);
        }
        #endregion

        #region  dataGridView1_CellClick
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            #region 查找图斑对应文件

           //重叠面积不显示
            label7.Visible = false;
            DataGridViewRow dgvr = dataGridView1.CurrentRow;


            string strProjectName = dgvr.Cells["项目名称"].Value.ToString();//获取smid值
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append("SELECT FILE_NAME, ");
            sqlQuery.Append("       FILE_PATH, ");
            sqlQuery.Append("     TO_CHAR(UPLOAD_DATE, 'YYYYMMDD ') AS UPLOAD_DATE ");
            sqlQuery.Append("FROM       GKFQD_WD ");
            sqlQuery.Append("WHERE      PROJECT_NAME ='" + strProjectName + "'");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSet1.Clear();
            OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter.Fill(dataSet1);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            string url = "http://localhost:6721/gkfqdFile/";
            string[] strTemp1 = new String[dataSet1.Tables[0].Rows.Count];
            for (int i = 0; i < dataSet1.Tables[0].Rows.Count; i++)
            {
                strTemp1[i] = url + dataSet1.Tables[0].Rows[i]["UPLOAD_DATE"].ToString().Trim() + "/" + dataSet1.Tables[0].Rows[i]["FILE_NAME"].ToString();
            }
            LinkLabel[] linkLabel1 = new LinkLabel[dataSet1.Tables[0].Rows.Count];
            this.flowLayoutPanel1.Controls.Clear();
            for (int i = 0; i < dataSet1.Tables[0].Rows.Count; i++)
            {
                linkLabel1[i] = new LinkLabel();
                linkLabel1[i].Click += new System.EventHandler(this.linkLable_Click);
                linkLabel1[i].Size = new Size(370, 30);
                linkLabel1[0].Top = 25;
                linkLabel1[0].Left = 10;
                if (i != 0)
                {
                    linkLabel1[i].Top = 25;
                    linkLabel1[i].Top = linkLabel1[i - 1].Top + linkLabel1[i - 1].Height + 5;
                }
                linkLabel1[i].Visible = true;
                linkLabel1[i].Text = strTemp1[i];
                this.flowLayoutPanel1.AutoScroll = true;
                this.flowLayoutPanel1.Controls.Add(linkLabel1[i]);
            }
            #endregion

            #region 定位图斑
            //if (dataGridView1.CurrentRow == null) return;
           // DataGridViewRow dgvr = dataGridView1.CurrentRow;
            string tableName = gkfqd.Common.DbUse.GetTownCode(comboBox4.Text);
           // importResultShp = gk032Datasource.Datasets[tableName] as DatasetVector;
            MapShow(tableName);
            string strSmid1 = dgvr.Cells["地块编号"].Value.ToString();//获取smid值
            // 构造一个查询参数对象，查询选中的记录
            QueryParameter para = new QueryParameter();
            para.AttributeFilter = "SMID = " + strSmid1;
            para.CursorType = CursorType.Dynamic;
          
            try
            {
                recordset = importResultShp.Query(para);
                Selection selection = mapControl2.Map.Layers[0].Selection;
                selection.FromRecordset(recordset);
                mapControl2.Map.EnsureVisible(recordset.GetGeometry(), 0.8);
                mapControl2.Map.Refresh();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
           
            #endregion

        }
        #endregion

        #region 地块查重叠加分析
        private void button3_Click(object sender, EventArgs e)
        {
            string[] arrayYear = { "2014", "2015", "2016" };
           // string[] arrayYear = { "GHDLTB" };
            for(int i=0;i<arrayYear.Length;i++){
                //此处写循环 每个年份数据 现在执行一个年份数据 并手动选择 自动执行多个年份数据
                //不用手动选择  年份和图层对应
                splashScreenManager1.ShowWaitForm();
                splashScreenManager1.SetWaitFormCaption("正在查重" + arrayYear[i]+"年份数据");
                splashScreenManager1.SetWaitFormDescription("请等待...");
                //年份选择
               //string strYear = "T" + comboBox4.Text;
                string strYear = "T" + arrayYear[i];
                //string strYear = arrayYear[i];
               
               //面积查询前不显示
                label7.Text = "";
                label7.Name = "";
                label7.Visible = false;

                DatasetVector datasetOperated = importDatasource.Datasets[strYear] as DatasetVector;
                Recordset recordset2 = datasetOperated.GetRecordset(false, CursorType.Dynamic);
                //创建一个面矢量数据集，用于存储相交分析获得的结果
                importDatasource.Datasets.Delete("IntersectTemp");
                String resultDatasetIntersectName = importDatasource.Datasets.GetAvailableDatasetName("IntersectTemp");
                DatasetVectorInfo datasetvectorInfoIntersect = new DatasetVectorInfo();
                datasetvectorInfoIntersect.Type = DatasetType.Region;
                datasetvectorInfoIntersect.Name = resultDatasetIntersectName;
                datasetvectorInfoIntersect.EncodeType = EncodeType.None;
                DatasetVector resultDatasetIntersect = importDatasource.Datasets.Create(datasetvectorInfoIntersect);
                //设置投影信息 不设置 报投影不一致错误
                resultDatasetIntersect.Datasource.Datasets["IntersectTemp"].PrjCoordSys = importDatasource.Datasets[strYear].PrjCoordSys;
                //设置叠加分析参数
                OverlayAnalystParameter overlayAnalystParamIntersect = new OverlayAnalystParameter();
                overlayAnalystParamIntersect.Tolerance = 0.0000011074;
                //调用相交叠加分析方法实相交分析
                bool flag =   OverlayAnalyst.Intersect(recordset2, recordset, resultDatasetIntersect, overlayAnalystParamIntersect);
                if (flag)
                {
                    StringBuilder sqlQuery = new StringBuilder();
                    sqlQuery.Append("SELECT SUM(SMAREA) AS SMAREA");
                    sqlQuery.Append(" FROM       IntersectTemp ");
                    gkfqd.Common.DbUse.GetOleDbconnection().Close();
                    gkfqd.Common.DbUse.GetOleDbconnection().Open();
                    dataSetIntersectTemp.Clear();
                    OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
                    MyAdapter.Fill(dataSetIntersectTemp);
                    gkfqd.Common.DbUse.GetOleDbconnection().Close();
                    if (dataSetIntersectTemp.Tables[0].Rows[0]["SMAREA"].ToString().Trim() != "")
                    {
                        label7.Text = "重叠面积:" + dataSetIntersectTemp.Tables[0].Rows[0]["SMAREA"].ToString().Trim();
                        label7.Visible = true;
                        //MessageBox.Show(label7.Text);
                        //弹出重叠面积叠加后图形

                        gk03b frmgk03b = new gk03b();
                        frmgk03b.Owner = this;
                        frmgk03b.FormBorderStyle = FormBorderStyle.FixedSingle;//固定窗体大小不变
                        frmgk03b.StartPosition = FormStartPosition.CenterScreen;//窗体居中
                        frmgk03b.ShowDialog();
                    }
                    else 
                    {
                        label7.Text = "没有重叠地块";
                        label7.Visible = true;
                        MessageBox.Show(label7.Text);
                    }
                }
                splashScreenManager1.CloseWaitForm();
            }
        }
        #endregion

        #region 导出坐标
        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "导出txt坐标文件(*.txt)|*.txt";
            sfd.FileName = "坐标导出文件" + DateTime.Now.ToString("yyyyMMddhhmmss");

            if (sfd.ShowDialog() == DialogResult.OK)
            {

                FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                if (dataGridView1.CurrentRow == null) return;
                DataGridViewRow dgvr = dataGridView1.CurrentRow;


                string strSmid = dgvr.Cells["地块编号"].Value.ToString();//获取smid值
                // 构造一个查询参数对象，查询选中的记录
                QueryParameter para = new QueryParameter();
                para.AttributeFilter = "SMID =" + strSmid;
                para.CursorType = CursorType.Dynamic;

                DatasetVector importResultShp = null;
                SuperMap.Data.Workspace workspace1;
                workspace1 = new SuperMap.Data.Workspace();
              
                workspace1.Open(gkfqd.Common.Tool.GetConnectionInfo());
                string tableName = gkfqd.Common.DbUse.GetTownCode(comboBox4.Text);
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
                                sw.WriteLine(point2d.X.ToString() + "," + point2d.Y.ToString());
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
                MessageBox.Show("坐标导出成功！");
            }
        }
        #endregion

        # region 省 市 县 选择 
        public void LoadComboBox()
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append(" SELECT XZQM  FROM  XZQ  ");
            sqlQuery.Append(" WHERE CJ ='1'  ");
            sqlQuery.Append(" AND  BHBS <> '0000'  ");
            sqlQuery.Append(" ORDER BY  XZQDM  ");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSetTown.Clear();
            OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter.Fill(dataSetTown);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();

            comboBox6.DataSource = dataSetTown.Tables[0];
            comboBox6.DisplayMember = "XZQM";
            comboBox6.ValueMember = "XZQM";


            sqlQuery.Clear();
            sqlQuery.Append(" SELECT XZQM  FROM  XZQ  ");
            sqlQuery.Append(" WHERE CJ ='2'  ");
            sqlQuery.Append(" AND BHBS ='1001'");
            sqlQuery.Append(" ORDER BY  XZQDM  ");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSetCity.Clear();

            OleDbDataAdapter MyAdapter2 = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter2.Fill(dataSetCity);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            comboBox4.DataSource = dataSetCity.Tables[0];
            comboBox4.DisplayMember = "XZQM";
            comboBox4.ValueMember = "XZQM";
        }
        #endregion

        # region 市选择 事件
        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append(" SELECT BHBS  FROM  XZQ  ");
            sqlQuery.Append(" WHERE XZQM  = '" + comboBox6.SelectedValue + "'");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSetCitySelect.Clear();
            OleDbDataAdapter MyAdapter1 = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter1.Fill(dataSetCitySelect);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            //取得包含标识
            String strBhbs = dataSetCitySelect.Tables[0].Rows[0][0].ToString();
            sqlQuery.Clear();
            sqlQuery.Append(" SELECT XZQM  FROM  XZQ  ");
            sqlQuery.Append(" WHERE CJ ='2'  ");
            sqlQuery.Append(" AND BHBS ='" + strBhbs + "'");
            sqlQuery.Append(" ORDER BY  XZQDM  ");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSetTownSelect.Clear();
            OleDbDataAdapter MyAdapter2 = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter2.Fill(dataSetTownSelect);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            comboBox4.DataSource = dataSetTownSelect.Tables[0];
            comboBox4.DisplayMember = "XZQM";
            comboBox4.ValueMember = "XZQM";
        }

        #endregion

        #region  显示地图
        public void MapShow(string mapLayerName)
        {
            workspace1 = new SuperMap.Data.Workspace();
            workspace1.Open(gkfqd.Common.Tool.GetConnectionInfo());
            importDatasource = workspace1.Datasources[gkfqd.Common.Tool.GetWorkspaceDataDatasources()];
            importResultShp = importDatasource.Datasets[mapLayerName] as DatasetVector;
            mapControl2.Map.Workspace = workspace1;
            mapControl2.Map.Layers.Clear();
            mapControl2.Map.Layers.Add(importResultShp, true);
            mapControl2.Map.ViewEntire();
            mapControl2.Map.Refresh();

           

        }
        #endregion

        # region 地图操作

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            mapControl2.Action = SuperMap.UI.Action.Select;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            mapControl2.Action = SuperMap.UI.Action.Pan;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            mapControl2.Action = SuperMap.UI.Action.ZoomIn;
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            mapControl2.Action = SuperMap.UI.Action.ZoomOut;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            mapControl2.Action = SuperMap.UI.Action.ZoomFree;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            mapControl2.Map.ViewEntire();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            mapControl2.Map.Refresh();
        }
        #endregion 

    }
}
