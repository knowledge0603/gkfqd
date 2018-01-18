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
using System.Data.OleDb;
using SuperMap.Data.Conversion;
using SuperMap.Analyst.SpatialAnalyst;

namespace gkfqd.ui.gk04
{
    public partial class gk044 : WinFormsUI.Docking.DockContent
    {
        #region 变量区

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


        #endregion

        #region 初始化
        public gk044()
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
        }

        private void gk044_Load(object sender, EventArgs e)
        {
            
        }
        //释放地图
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            mapControl1.Dispose();
            workspace1.Dispose();
        }
        #endregion

        #region 点击地图出属性
        private void mapControl1_GeometrySelected(object sender, SuperMap.UI.GeometrySelectedEventArgs e)
        {
            //获取选择集
            Selection[] selection = mapControl1.Map.FindSelection(true);

            //判断选择集是否为空
            if (selection == null || selection.Length == 0)
            {
                MessageBox.Show("请选择要查询属性的空间对象");
                return;
            }

            //将选择集转换为记录
            Recordset recordset = selection[0].ToRecordset();

            this.dataGridView1.Columns.Clear();
            this.dataGridView1.Rows.Clear();

            for (int i = 0; i < recordset.FieldCount; i++)
            {
                //定义并获得字段名称
                String fieldName = recordset.GetFieldInfos()[i].Name;

                //将得到的字段名称添加到dataGridView列中
                this.dataGridView1.Columns.Add(fieldName, fieldName);
            }

            //初始化row
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
        #endregion

        #region 取消选中
        //private void mapControl1_Paint(object sender, PaintEventArgs e)
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
        #endregion

        #region 点击属性标记地图
        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            string tableName = gkfqd.Common.DbUse.GetTownCode(comboBox3.Text);
            MapShow(tableName);
            if (dataGridView1.CurrentRow == null) return;
            DataGridViewRow dgvr = dataGridView1.CurrentRow;

            string strSmid = dgvr.Cells["项目名称"].Value.ToString();
            QueryParameter para = new QueryParameter();
            para.AttributeFilter = "FKXMMC = '" + strSmid + "'";
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
        //放大
        private void MapZoomIn_Click(object sender, EventArgs e)
        {
            mapControl1.Action = SuperMap.UI.Action.ZoomIn;
        }
        //缩小
        private void MapZoomOut_Click(object sender, EventArgs e)
        {
            mapControl1.Action = SuperMap.UI.Action.ZoomOut;
        }
        //漫游
        private void MapPan_Click(object sender, EventArgs e)
        {
            mapControl1.Action = SuperMap.UI.Action.Pan;
        }
        //全幅显示
        private void MapEntire_Click(object sender, EventArgs e)
        {
            mapControl1.Map.ViewEntire();
        }
        //选中
        private void MapSelect_Click(object sender, EventArgs e)
        {
            mapControl1.Action = SuperMap.UI.Action.Select;
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

        #region 图层叠加

        private void button2_Click(object sender, EventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            splashScreenManager1.SetWaitFormCaption("正在叠加图层");
            splashScreenManager1.SetWaitFormDescription("请等待...");
            //年份选择
            string strYear = "T" + comboBox5.Text;
            

            DatasetVector datasetOperated = importDatasource.Datasets[strYear] as DatasetVector;
            Recordset recordset2 = datasetOperated.GetRecordset(false, CursorType.Dynamic);
            //创建一个面矢量数据集，用于存储相交分析获得的结果
            importDatasource.Datasets.Delete("JBNTsuperposition");
            String resultDatasetInteresectName = importDatasource.Datasets.GetAvailableDatasetName("JBNTsuperposition");
            DatasetVectorInfo datasetvectorInfoIntersect = new DatasetVectorInfo();
            datasetvectorInfoIntersect.Type = DatasetType.Region;
            datasetvectorInfoIntersect.Name = resultDatasetInteresectName;
            datasetvectorInfoIntersect.EncodeType = EncodeType.None;
            DatasetVector resultDatasetIntersect = importDatasource.Datasets.Create(datasetvectorInfoIntersect);
            //设置投影信息 不设置 报投影不一致错误
            resultDatasetIntersect.Datasource.Datasets["JBNTsuperposition"].PrjCoordSys = importDatasource.Datasets[strYear].PrjCoordSys;
            //设置叠加分析参数
            OverlayAnalystParameter overlayAnalystParamIntersect = new OverlayAnalystParameter();
            overlayAnalystParamIntersect.Tolerance = 0.0000011074;
            //  overlayAnalystParamIntersect.SourceRetainedFields= {"FKXMMC";"DLMC";"DLBM"};
            string[] gk = { "FKXMMC", "DLMC", "DLBM" };
            overlayAnalystParamIntersect.SourceRetainedFields = gk;
            // overlayAnalystParamIntersect.SourceRetainedFields= [ "TBBH"];
            string[] languages = { "ZYMJ" };
            //  overlayAnalystParamIntersect.OperationRetainedFields= {"GD";"LD"};
            overlayAnalystParamIntersect.OperationRetainedFields = languages;
            //调用相交叠加分析方法实相交分析
            bool flag = OverlayAnalyst.Intersect(recordset2, recordset, resultDatasetIntersect, overlayAnalystParamIntersect);
            if (flag)
            {
                StringBuilder sqlQuery = new StringBuilder();
                sqlQuery.Append("SELECT * FROM JBNTsuperposition");
                gkfqd.Common.DbUse.GetOleDbconnection().Close();
                gkfqd.Common.DbUse.GetOleDbconnection().Open();
                dataSetIntersectTemp.Clear();
                OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
                MyAdapter.Fill(dataSetIntersectTemp);
                gkfqd.Common.DbUse.GetOleDbconnection().Close();
                if (flag == true)
                {
                    MessageBox.Show("图层叠加完成");
                    this.reportViewer1.RefreshReport();
                    this.gHDLTBTableAdapter.Fill(this.gK0441.GHDLTB);
                    this.reportViewer1.RefreshReport();

                }

                splashScreenManager1.CloseWaitForm();
                // TODO: 这行代码将数据加载到表“gK0441.所属乡镇与面积”中。您可以根据需要移动或删除它。
                this.所属乡镇与面积TableAdapter.Fill(this.gK0441.所属乡镇与面积);
                // TODO: 这行代码将数据加载到表“gK0441.规划地类与面积”中。您可以根据需要移动或删除它。
                this.规划地类与面积TableAdapter.Fill(this.gK0441.规划地类与面积);
                // TODO: 这行代码将数据加载到表“gK0441.地类名称与面积”中。您可以根据需要移动或删除它。
                this.地类名称与面积TableAdapter.Fill(this.gK0441.地类名称与面积);
                // TODO: 这行代码将数据加载到表“gK0441.GHDLTB”中。您可以根据需要移动或删除它。
                this.gHDLTBTableAdapter.Fill(this.gK0441.GHDLTB);

                this.reportViewer1.RefreshReport();

            }
            else
            {

                MessageBox.Show("叠加失败");
                return;

            }
            // this.reportViewer1.RefreshReport();
        }

        #endregion

    }
}
