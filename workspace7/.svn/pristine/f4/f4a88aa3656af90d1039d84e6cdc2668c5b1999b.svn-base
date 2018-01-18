using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SuperMap.Data;
using SuperMap.Mapping;
using System.Diagnostics;
using System.Data.OracleClient;
using Microsoft.Reporting.WinForms;
using SuperMap.SampleCode.Mapping;
using SuperMap.Data.Conversion;
using System.Data.OleDb;
using SuperMap.Analyst.SpatialAnalyst;

namespace gkfqd.ui.gk04
{
    public partial class gk043 : WinFormsUI.Docking.DockContent
    {
        #region 变量区

        private SampleRun m_sampleRun;
        private SuperMap.Data.Workspace x_workspace;
        private SuperMap.UI.MapControl m_mapControl;



        //图层smid查询用
        Recordset recordset = null;
        //叠加图层计算叠加后面积用DataSet
        DataSet dataSetIntersectTemp = new DataSet();
        //工矿废弃地临时图层DatasetVector
        DatasetVector importResultShp = null;
        private Datasource importDatasource;
        StringBuilder sqlQuery = new StringBuilder();
        DataSet dataSet = new DataSet();
        DataSet dataSet1 = new DataSet();
        DataSet dataSet2 = new DataSet();
        DataSet dataSetCounty = new DataSet();
        DataSet dataSetCity = new DataSet();
        DataSet dataSetQuery = new DataSet();
        String imgPath = "";
        private SuperMap.Data.Workspace fileWorkspace;
        private DataImport dataImport;
        //工矿废弃地临时图层Recordset
        Recordset gkfqdRecordset;
        //工矿废弃地正式图层Recordset
        Recordset formatRecordset;
        //工矿废弃地正式图层DatasetVector
        DatasetVector formatDatasetVector = null;
        private Datasource gk041Datasource;
        //public static OleDbConnection conn = new OleDbConnection("Provider=MSDAORA.1;User ID=gkfqd;Password=123456;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.1.103)(PORT = 1521))) (CONNECT_DATA = (SERVICE_NAME = orcl)))");

        //public static OracleConnection gkfqd.Common.DbUse.GetOleDbconnection() = new OracleConnection("Datasource =ORCL; User = gkfqd; Password = 123456;");
        #endregion

        #region 初始化
        public gk043()
        {
            try
            {
                InitializeComponent();
                //区县comboBox列表加载
                LoadComboBox();
                //默认选内蒙古自治区
                comboBox1.SelectedIndex = 0;
                //默认年份为 全部
                comboBox4.SelectedIndex = 0;
                //不显示dataGridView1最后一行
                dataGridView1.AllowUserToAddRows = false;
                //默认叠加年份2014
                comboBox4.Text = "2017";
                comboBox5.Text = "2017";
                this.x_workspace = new SuperMap.Data.Workspace();
                //this.mapControl1 = new SuperMap.UI.MapControl();
                //m_mapControl.Dock = DockStyle.Fill;
                m_sampleRun = new SampleRun(x_workspace, mapControl1);
                //x_sampleRun = new SampleRun(x_workspace, mapControl1);
                //base.Controls.Add(mapControl1);
                base.Controls.SetChildIndex(mapControl1, 0);
                mapControl1.GeometrySelected += new SuperMap.UI.GeometrySelectedEventHandler(m_mapControl_GeometrySelected);
               // mapControl1.Paint += new PaintEventHandler(m_mapControl_Paint);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        private void gk043_Load(object sender, EventArgs e)
        {
            

            //打开地图
            //Workspace x_workspace = new Workspace();
            //WorkspaceConnectionInfo wci = new WorkspaceConnectionInfo() { Type = WorkspaceType.Oracle, Server = "ORCL", Database = "", Name = "workspace", User = "gkfqd", Password = "123456" };
            //x_workspace.Open(wci);
            //importDatasource = x_workspace.Datasources["ORCL"];
            //importResultShp = importDatasource.Datasets["T2016"] as DatasetVector;
            //mapControl1.Map.Workspace = x_workspace;
            //mapControl1.Map.Layers.Clear();
            //mapControl1.Map.Layers.Add(importResultShp, true);
            //mapControl1.Map.ViewEntire();
            //mapControl1.Map.Refresh();
        }

        #endregion

        #region 点击属性定位地图与点击地图定位属性
        /// <summary>
        /// 对象选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_mapControl_GeometrySelected(object sender, SuperMap.UI.GeometrySelectedEventArgs e)
        {
            try
            {
                this.FillDataGridView(m_sampleRun.GetSelectedRecordset(false));
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 没有选择对象的时候表格清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void m_mapControl_Paint(object sender, PaintEventArgs e)
        //{
        //    try
        //    {
        //        if (mapControl1.Map.Layers[0].Selection.Count < 1)
        //        {
        //            dataGridView1.Columns.Clear();
        //            dataGridView1.Rows.Clear();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Trace.WriteLine(ex.Message);
        //    }
        //}

        /// <summary>
        /// 使用记录集填充DataGridView
        /// </summary>
        /// <param name="recordset">获取的记录集</param>
        private void FillDataGridView(Recordset recordset)
        {
            try
            {
                dataGridView1.Columns.Clear();
                dataGridView1.Rows.Clear();

                for (int i = 0; i < recordset.FieldCount; i++)
                {
                    String fieldName = recordset.GetFieldInfos()[i].Name;

                    dataGridView1.Columns.Add(fieldName, fieldName);
                }

                DataGridViewRow row = null;
                //根据选中记录的个数，将选中对象的信息添加到dataGridView中显示
                while (!recordset.IsEOF)
                {
                    row = new DataGridViewRow();
                    for (int i = 0; i < recordset.FieldCount; i++)
                    {
                        //定义并获得字段值
                        Object fieldValue = recordset.GetFieldValue(i);
                        //将字段值添加到dataGridView中对应的位置
                        DataGridViewTextBoxCell cell = new DataGridViewTextBoxCell();
                        if (fieldValue != null)
                        {
                            cell.ValueType = fieldValue.GetType();
                            cell.Value = fieldValue;
                        }

                        row.Cells.Add(cell);
                    }

                    this.dataGridView1.Rows.Add(row);

                    recordset.MoveNext();
                }

                this.dataGridView1.Update();

                recordset.Dispose();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 选择表格中的数据加入到选择集
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_MouseUp(object sender, MouseEventArgs e)
        {
            string townNameJX = gkfqd.Common.DbUse.GetTownCodeJX(comboBox3.Text);
            MapShow(townNameJX);
            if (dataGridView1.CurrentRow == null) return;
            DataGridViewRow dgvr = dataGridView1.CurrentRow;

            string strSmid = dgvr.Cells["项目名称"].Value.ToString();
            QueryParameter para = new QueryParameter();
            para.AttributeFilter = "FKXMMC = " + strSmid;
            para.CursorType = CursorType.Dynamic;



            Recordset recordset = null;
            try
            {
                recordset = importResultShp.Query(para);

                Selection selection = mapControl1.Map.Layers[0].Selection;

                selection.FromRecordset(recordset);

                mapControl1.Map.ViewEntire();
                //mapControl1.Map.EnsureVisible(recordset.GetGeometry(), 0.1);

                mapControl1.Map.Refresh();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
        #endregion

        #region 对地图进行放大缩小
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            mapControl1.Action = SuperMap.UI.Action.Select;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            mapControl1.Action = SuperMap.UI.Action.ZoomIn;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            mapControl1.Action = SuperMap.UI.Action.ZoomOut;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            mapControl1.Action = SuperMap.UI.Action.Pan;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            mapControl1.Map.ViewEntire();
        }

        #endregion

        #region 查询项目
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
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSetQuery.Clear();
            OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter.Fill(dataSetQuery);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            dataGridView1.DataSource = dataSetQuery.Tables[0];
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
        #endregion

        #region 叠加图层
        private void button2_Click(object sender, EventArgs e)
        {
            //建新图层
            string tableName1 = gkfqd.Common.DbUse.JXGetTownCode(comboBox3.Text) + "_" + comboBox5.Text;
            //现状图层
            string tableName2 = gkfqd.Common.DbUse.GHGetTownCode(comboBox3.Text) + "_" + comboBox5.Text;
            splashScreenManager1.ShowWaitForm();
            splashScreenManager1.SetWaitFormCaption("正在叠加图层");
            splashScreenManager1.SetWaitFormDescription("请等待...");
            //年份选择
            string strYear = tableName2;
            DatasetVector datasetOperated = importDatasource.Datasets[strYear] as DatasetVector;
            if (datasetOperated == null)
            {
                MessageBox.Show("请选择有效的叠加年份");
                splashScreenManager1.CloseWaitForm();
                return;
            }
            Recordset recordset2 = datasetOperated.GetRecordset(false, CursorType.Dynamic);
            //创建一个面矢量数据集，用于存储相交分析获得的结果
            importDatasource.Datasets.Delete("JXsuperposition");
            String resultDatasetInteresectName = importDatasource.Datasets.GetAvailableDatasetName("JXsuperposition");
            DatasetVectorInfo datasetvectorInfoIntersect = new DatasetVectorInfo();
            datasetvectorInfoIntersect.Type = DatasetType.Region;
            datasetvectorInfoIntersect.Name = resultDatasetInteresectName;
            datasetvectorInfoIntersect.EncodeType = EncodeType.None;
            DatasetVector resultDatasetIntersect = importDatasource.Datasets.Create(datasetvectorInfoIntersect);
            //设置投影信息 不设置 报投影不一致错误
            resultDatasetIntersect.Datasource.Datasets["JXsuperposition"].PrjCoordSys = importDatasource.Datasets[strYear].PrjCoordSys;
            //设置叠加分析参数
            OverlayAnalystParameter overlayAnalystParamIntersect = new OverlayAnalystParameter();
            overlayAnalystParamIntersect.Tolerance = 0.0000011074;
            //  overlayAnalystParamIntersect.SourceRetainedFields= {"FKXMMC";"DLMC";"DLBM"};
            // 现状图层 jxtc
            FieldInfos fieldInfos = datasetOperated.FieldInfos;
            string[] mFiels = new string[fieldInfos.Count];
            for (int i = 0; i < fieldInfos.Count; i++)
            {
                mFiels[i] = fieldInfos[i].Name;
            }
            //string[] gk = { "ZYMJ", "DLMC", "DLBM" };
            overlayAnalystParamIntersect.SourceRetainedFields = mFiels;
            // overlayAnalystParamIntersect.SourceRetainedFields= [ "TBBH"];
            //工矿废弃地图层
            string[] languages = { tableName1 };
            //  overlayAnalystParamIntersect.OperationRetainedFields= {"GD";"LD"};
            overlayAnalystParamIntersect.OperationRetainedFields = languages;
            //调用相交叠加分析方法实相交分析
            bool flag = OverlayAnalyst.Intersect(recordset2, recordset, resultDatasetIntersect, overlayAnalystParamIntersect);
            if (flag)
            {
                this.v_XJQKTableAdapter.Fill(this.gk0431.V_XJQK);
                this.reportViewer1.RefreshReport();
                splashScreenManager1.CloseWaitForm();
            }
            else
            {
                MessageBox.Show("叠加失败");
                return;
            }
        }
        #endregion

        #region 区县级联选择处理


        public void LoadComboBox()
        {
            sqlQuery.Clear();
            sqlQuery.Append(" SELECT XZQM  FROM  XZQ  ");
            sqlQuery.Append(" WHERE CJ ='1'  ");
            sqlQuery.Append(" AND BHBS <>'0000'");
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
            sqlQuery.Append(" AND BHBS <>'0000'");
            sqlQuery.Append(" ORDER BY  XZQDM  ");
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            gkfqd.Common.DbUse.GetOleDbconnection().Open();
            dataSet1.Clear();

            OleDbDataAdapter MyAdapter2 = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter2.Fill(dataSet1);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            comboBox3.DataSource = dataSet1.Tables[0];
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
            dataSetCounty.Clear();
            OleDbDataAdapter MyAdapter1 = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter1.Fill(dataSetCounty);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            //取得包含标识
            if (dataSetCounty.Tables[0].Rows.Count > 0)
            {
                String strBhbs = dataSetCounty.Tables[0].Rows[0][0].ToString();


                sqlQuery.Clear();
                sqlQuery.Append(" SELECT XZQM  FROM  XZQ  ");
                sqlQuery.Append(" WHERE CJ ='2'  ");
                sqlQuery.Append(" AND BHBS ='" + strBhbs + "'");
                sqlQuery.Append(" ORDER BY  XZQDM  ");
                gkfqd.Common.DbUse.GetOleDbconnection().Close();
                gkfqd.Common.DbUse.GetOleDbconnection().Open();
                dataSetCity.Clear();
            }
            else
            {
                return;
            }


            OleDbDataAdapter MyAdapter2 = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
            MyAdapter2.Fill(dataSetCity);
            gkfqd.Common.DbUse.GetOleDbconnection().Close();
            comboBox3.DataSource = dataSetCity.Tables[0];
            comboBox3.DisplayMember = "XZQM";
            comboBox3.ValueMember = "XZQM";
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
        }
        #endregion
    }
}
