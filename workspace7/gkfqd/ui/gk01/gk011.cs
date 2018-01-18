using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


using SuperMap.Data;
using SuperMap.UI;
using SuperMap.Mapping;
using SuperMap.Data.Conversion;
using System.Diagnostics;

using gkfqd.ui.gk01;
using System.Data.OleDb;
using System.Collections;


//shp地图文件导入
//dwg地图文件导入
//add by  2016
//22
namespace gkfqd.ui.gk01
{
    public partial class gk011 : WinFormsUI.Docking.DockContent
    {
        #region 变量区域
     //   public static OleDbconnection conn = new OleDbconnection("Provider=MSDAORA.1;User ID=gkfqd;Password=123456;Data Source=(DESCRIPTION = (ADDRESS_LIST= (ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.1.103)(PORT = 1521))) (connECT_DATA = (SERVICE_NAME = orcl)))");
        DataSet dataSet6 = new DataSet();
        private SuperMap.Data.Workspace fileWorkspace;
      
        private Datasource m_srcDatasource;
        private DatasetVector m_sourceRegion;

        private DataImport dataImport ;
        private Datasource importDatasource;
        private Datasource gk013Datasource;
     
        String imgPath = "";
        String strFileName = "";
        String strFilePath = "";
    
        //打开工作空间及地图
        WorkspaceConnectionInfo conInfo = new WorkspaceConnectionInfo(@"..\..\template\temp.smwu");

        //工矿废弃地临时图层Recordset
        Recordset gkfqdRecordset;
        //工矿废弃地正式图层Recordset
        Recordset formatRecordset;
        //工矿废弃地临时图层DatasetVector
        DatasetVector importResultShp = null;
        //共同用  sqlQuery
        StringBuilder sqlQuery = new StringBuilder();
        //工矿废弃地正式图层DatasetVector
        DatasetVector formatDatasetVector = null;
        //导入坐标文件用
        public ArrayList LineList;
        #endregion

        #region 页面载入
        public gk011()
        {
            InitializeComponent();
            toolStripInitialize();
            //不显示dataGridView1最后一行
            dataGridView1.AllowUserToAddRows = false;
          //  fileWorkSpaceOpen();
            textBox1.ReadOnly = true;
        }

        public gk011(string projectId,string projectType,string townName)
        {
            InitializeComponent();
            toolStripInitialize();
            //不显示dataGridView1最后一行
            dataGridView1.AllowUserToAddRows = false;
            //  fileWorkSpaceOpen();
            textBox1.Text = projectId;
            textBox1.ReadOnly = true;

            textBox2.Text = projectType;
            textBox2.ReadOnly = true;

            textBox3.Text = townName;
            textBox3.ReadOnly = true;
        }
        #endregion

        #region 数据库连接打开地图
        private void WorkspaceconnectionInfo()
        {
            //数据库连接打开地图
            //WorkspaceconnectionInfo workspaceconnectionInfo = new WorkspaceconnectionInfo();
            //workspaceconnectionInfo.Type = WorkspaceType.Oracle;
            //workspaceconnectionInfo.Server = "ORCL";
            //workspaceconnectionInfo.Database = "";
            //workspaceconnectionInfo.Name = "workspace";
            //workspaceconnectionInfo.User = "gkfqd";
            //workspaceconnectionInfo.Password = "123456";
            //workspace1.Open(workspaceconnectionInfo);
            //importDatasource = workspace1.Datasources["ORCL_gkfqd"];
            //dataImport = new DataImport();
        }
        #endregion

        #region toolStripButton 初始化
        private void toolStripInitialize()
        {
            //toolStripButton 初始化
            toolStripButton4.ToolTipText = "选择";
            toolStripButton2.ToolTipText = "漫游";
            toolStripButton6.ToolTipText = "放大";
            toolStripButton7.ToolTipText = "缩小";
            toolStripButton5.ToolTipText = "自由缩放";
            toolStripButton1.ToolTipText = "全幅显示";
            toolStripButton3.ToolTipText = "刷新";
        }
        #endregion

        #region 打开文件
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog objFile = new OpenFileDialog();
          //  objFile.Filter = "shp文件(.shp)|*.shp|dwg文件(.dwg)|*.dwg";
            objFile.Filter = "shp文件(.shp)|*.shp";
            objFile.ShowDialog();
            imgPath = objFile.FileName;
            if (imgPath=="")
            {
                MessageBox.Show("请选择导入文件！");
                return;
            }
            strFileName = objFile.SafeFileName;
            strFilePath = objFile.FileName.Replace(strFileName,"");
            String sourcePath = imgPath;

            // true=覆盖已存在的同名文件,false则反之
            bool isrewrite = true;
            if (strFilePath.Contains("shp"))
            {
                MessageBox.Show("导入文件夹名称不要包含【shp】字样,请跟换其他文件夹!");
                return;
            }
            if (imgPath.Contains(".SHP"))
            {
                MessageBox.Show("请将导入文件后缀SHP改为小写!");
                return;
            }
            //if (File.Exists(strFilePath + strFileName.Replace(".SHP",".SHX")))
            //{

            //    MessageBox.Show("请将导入文件后缀SHX改为小写!");
            //    return;
            //}
            //if (File.Exists(strFilePath + strFileName.Replace(".SHP", ".PRJ")))
            //{

            //    MessageBox.Show("请将导入文件后缀PRJ改为小写!");
            //    return;
            //}
            //if (File.Exists(strFilePath + strFileName.Replace(".SHP", ".DBF")))
            //{

            //    MessageBox.Show("请将导入文件后缀DBF改为小写!");
            //    return;
            //}
            if (imgPath.Contains(".shp") )
            {
                //string str = System.AppDomain.CurrentDomain.BaseDirectory;

                string str = System.IO.Directory.GetCurrentDirectory();
                //复制选择文件到临时文件夹，目的是重命名文件，导入到数据库指定文件中
                String targetPath = str+"\\tempFolder\\temp_gkfqd.shp";
                System.IO.File.Copy(sourcePath, targetPath, isrewrite);
                string tempSourcePath = sourcePath.Replace("shp", "shx");
                string tempTargetPath = targetPath.Replace("shp", "shx");
                System.IO.File.Copy(tempSourcePath, tempTargetPath, isrewrite);
                string tempSourcePath1 = sourcePath.Replace(".shp", ".prj");
                string tempTargetPath1 = targetPath.Replace(".shp", ".prj");
                System.IO.File.Copy(tempSourcePath1, tempTargetPath1, isrewrite);
                string tempSourcePath2 = sourcePath.Replace(".shp", ".dbf");
                string tempTargetPath2 = targetPath.Replace(".shp", ".dbf");
                System.IO.File.Copy(tempSourcePath2, tempTargetPath2, isrewrite);

                ImportToShp();
            }
            if (imgPath.Contains(".dwg"))
            {
                //数据库导入
                ImportToDwg();

            }
        }
        #endregion

        #region 工具条
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

        #region  打开文件形工作空间
        /// <summary>
        /// 打开文件形工作空间
        /// </summary>
        public void fileWorkSpaceOpen()
        {
            this.fileWorkspace = new SuperMap.Data.Workspace();
            
          

            try
            {
                //打开工作空间及地图文件类型
                WorkspaceConnectionInfo conInfo = new WorkspaceConnectionInfo(@"..\..\template\temp.smwu");

                fileWorkspace.Open(conInfo);
                m_srcDatasource = fileWorkspace.Datasources["temp"];
                m_sourceRegion = m_srcDatasource.Datasets["gkfqd"] as DatasetVector;
                dataImport = new DataImport();

                this.mapControl1.Map.Workspace = fileWorkspace;
                mapControl1.Map.Layers.Clear();
                mapControl1.Map.Layers.Add(m_sourceRegion, true);
                mapControl1.Map.ViewEntire();
                mapControl1.Map.Refresh();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
        #endregion

        #region Dwg 导入操作
        /// <summary>
        /// 导入为Dwg
        /// </summary>
        public void ImportToDwg()
        {

            //-----------
            this.fileWorkspace = new SuperMap.Data.Workspace();
           
            //打开工作空间及地图
            WorkspaceConnectionInfo conInfo = new WorkspaceConnectionInfo(@"..\..\template\temp.smwu");
            fileWorkspace.Open(conInfo);

            dataImport = new DataImport();
           // m_srcDatasource = fileWorkspace.Datasources["temp"];
            importDatasource = fileWorkspace.Datasources["temp"];
            //----------
            try
            {
              
                dataImport.ImportSettings.Clear();
                ImportSettingDWG dwgSetting = new ImportSettingDWG();
                dwgSetting.ImportMode = ImportMode.Append;
                //dwgSetting.SourceFilePath = @"..\..\SampleData\DataExchange\DwgImport\Polyline.dwg";
                dwgSetting.SourceFilePath = imgPath;
                dwgSetting.TargetDatasource = importDatasource;
                dwgSetting.ImportingAsCAD = true;
                dataImport.ImportSettings.Add(dwgSetting);
                dataImport.Run();
                DatasetVector importResult = importDatasource.Datasets["Polyline"] as DatasetVector;
                this.mapControl1.Map.Workspace = fileWorkspace;
                mapControl1.Map.Layers.Clear();
                mapControl1.Map.Layers.Add(importResult, true);
                mapControl1.Map.ViewEntire();
                mapControl1.Map.Refresh();

            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }
        #endregion

        #region 导入为shp

        /// <summary>
        /// 导入为shp
        /// </summary>
        public void ImportToShp()
        {
            //-------------数据库打开工作空间
            workspace1 = new SuperMap.Data.Workspace();
           /* WorkspaceconnectionInfo workspaceconnectionInfo = new WorkspaceconnectionInfo();
            workspaceconnectionInfo.Type = WorkspaceType.Oracle;
            workspaceconnectionInfo.Server = "ORCL";
            workspaceconnectionInfo.Database = "";
            workspaceconnectionInfo.Name = "workspace";
            workspaceconnectionInfo.User = "gkfqd";
            workspaceconnectionInfo.Password = "123456";
            workspace1.Open(workspaceconnectionInfo);*/
            workspace1.Open(gkfqd.Common.Tool.GetConnectionInfo());
            importDatasource = workspace1.Datasources[gkfqd.Common.Tool.GetWorkspaceDataDatasources()];
            dataImport = new DataImport();
           
            try
            {
                dataImport.ImportSettings.Clear();
                ImportSettingSHP shpSetting = new ImportSettingSHP();
                shpSetting.ImportMode = ImportMode.Overwrite;
                string str = System.IO.Directory.GetCurrentDirectory();
                //复制选择文件到临时文件夹，目的是重命名文件，导入到数据库指定文件中
                String targetPath = str + "\\tempFolder\\temp_gkfqd.shp";
                shpSetting.SourceFilePath = targetPath;
                shpSetting.TargetDatasource = importDatasource;
                dataImport.ImportSettings.Add(shpSetting);
                dataImport.Run();

                importResultShp = importDatasource.Datasets["temp_gkfqd"] as DatasetVector;
                mapControl1.Map.Workspace = workspace1;
                mapControl1.Map.Layers.Clear();
                mapControl1.Map.Layers.Add(importResultShp, true);
                mapControl1.Map.ViewEntire();
                mapControl1.Map.Refresh();
               
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }
            RefreshDataGridView();
        }
        #endregion

        #region  A B 图层 图斑 copy
        public void copyPic(Workspace workspace, Datasource datasource)
        {
            // 取出数据源中名为 "Countries" 的矢量数据集（datasetVector）
            DatasetVector datasetVector = (DatasetVector)datasource.Datasets["Countries"];

            // 构造一个查询参数对象，查询Countries数据集中 date 字段为"1984-08-10"的记录。
            QueryParameter para = new QueryParameter();
            para.AttributeFilter = "date =  to_date(1984-08-10 00:00:00)";
            para.CursorType = CursorType.Dynamic;

            // 进行排序查询，并将其结果存储在 recordset 对象中
            Recordset recordset = datasetVector.Query(para);

            // 以 datasetVector 为模板创建数据集
            DatasetVector dataset_result = (DatasetVector)workspace.Datasources[0].Datasets.CreateFromTemplate(workspace.Datasources[0].Datasets.GetAvailableDatasetName("Results"), datasetVector);

            // 将空间查询结果追加到新建的数据集中
            dataset_result.Append(recordset);

            // 依次关闭所有对象
            recordset.Dispose();
        }


        public void TestAppend( Datasource datasource)
        {
            //获得用于操作的两个数据集
            DatasetVector datasetVector = (DatasetVector)datasource.Datasets["World"];
            DatasetVector datasetVector1 = (DatasetVector)datasource.Datasets["Ocean"];

            // 获得名为“Ocean”的数据集的所有记录，将其追加到名为“World”的数据集中
            Recordset recordset = datasetVector1.GetRecordset(false, CursorType.Dynamic);
            if (datasetVector.Append(recordset))
            {
                Console.WriteLine("追加数据集成功");
            }
            recordset.Dispose();
        }

        #endregion

        #region 属性表操作

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
          if(textBox2.Text=="复垦"){
            FkImport();
          }
          if (textBox2.Text == "建新") {
              JxImport();
          }
          if (textBox2.Text == "补耕")
          {
              BgImport();
          }
        }
        #endregion

        #region 临时图层中的地图是否在正式图层中存在判断
        public bool layerExist( Recordset tempRecordset)
        {
            bool flag = false;
            //项目地块是否存在标记
            bool isExit = false;
            if (formatRecordset == null)
            {
                return flag;
            }
            //地块编号
            string strDkbh = "";
            if (formatRecordset.RecordCount>0) {
                for (int i = 0; i <= formatRecordset.RecordCount; i++)
                {
                    formatRecordset.MoveTo(i);
                    

                    //判断该图斑是否已经由临时图层导入正式图层
                    if (formatRecordset.GetGeometry().Bounds.Equals(tempRecordset.GetGeometry().Bounds))
                    {
                        //取得地块编号
                        strDkbh = formatRecordset.GetFieldValue("DKBH").ToString();
                        flag = true;
                        break;
                    }
                }
            }
            //补耕地块录入时同一个旗县可能存在多个复垦项目的情况,该种情况下，判断该项目下是否存在上面处理中取得的地块编号
            //获取项目名称值
            if (textBox2.Text == "补耕")
            {
                sqlQuery.Clear();
                sqlQuery.Append("SELECT  DKBH   AS 地块编号, ");
                sqlQuery.Append("  FKXMMC           AS 所属复垦项目名称, ");
                sqlQuery.Append("  XMSZXM           AS 项目所在县名称 ");
                sqlQuery.Append(",  BZ           AS 备注 ");
                sqlQuery.Append(" FROM " + "BG" + gkfqd.Common.DbUse.GetTownCode(textBox3.Text));
                sqlQuery.Append(" WHERE         FKXMMC='" + textBox1.Text + "'AND DKBH='" + strDkbh + "'");
                gkfqd.Common.DbUse.GetOleDbconnection().Close();
                gkfqd.Common.DbUse.GetOleDbconnection().Open();
                dataSet6.Clear();
                OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
                MyAdapter.Fill(dataSet6);
                gkfqd.Common.DbUse.GetOleDbconnection().Close();
                if (dataSet6.Tables[0].Rows.Count>0)
                {
                    isExit = true;
                }
                //当地块存在，并且该项目名称下有该地块的情况
                return (flag && isExit);
            }
            return flag ;
        }
        #endregion

        #region DataGridView 刷新
        public void RefreshDataGridView()
        {
            //工矿废弃地临时图层Recordset
            gkfqdRecordset = importResultShp.GetRecordset(false, CursorType.Dynamic);

            //工矿废弃地正式图层
            if(textBox2.Text=="复垦"){
                //formatDatasetVector = importDatasource.Datasets["gkfqd"] as DatasetVector;
                formatDatasetVector = importDatasource.Datasets[gkfqd.Common.DbUse.GetTownCode(textBox3.Text)] as DatasetVector;
            }
            //建新图层录入时执行以下处理
            if (textBox2.Text == "建新")
            {
                //formatDatasetVector = importDatasource.Datasets["JXTC"] as DatasetVector;
                formatDatasetVector = importDatasource.Datasets["JX"+gkfqd.Common.DbUse.GetTownCode(textBox3.Text)] as DatasetVector;
            }

            //建新图层录入时执行以下处理
            if (textBox2.Text == "补耕")
            {
                //formatDatasetVector = importDatasource.Datasets["JXTC"] as DatasetVector;
                formatDatasetVector = importDatasource.Datasets["BG" + gkfqd.Common.DbUse.GetTownCode(textBox3.Text)] as DatasetVector;
            }
            // 刷新指定区县地块导入状态
           
            formatRecordset = formatDatasetVector.GetRecordset(false, CursorType.Dynamic);

            this.dataGridView1.Columns.Clear();
            this.dataGridView1.Rows.Clear();

            //添加dataGridView1 第一列为  地块导入状态，临时图层地块是否在正式图层存在
            this.dataGridView1.Columns.Add("录入状态", "录入状态");
            //取得字段名

            for (int i = 0; i < gkfqdRecordset.FieldCount; i++)
            {
                //定义并获得字段名称
                String fieldName = gkfqdRecordset.GetFieldInfos()[i].Name;

                //将得到的字段名称添加到dataGridView列中
                this.dataGridView1.Columns.Add(fieldName, fieldName);
            }

            //初始化row
            DataGridViewRow row = null;

            //根据选中记录的个数，将选中对象的信息添加到dataGridView中显示
            while (!gkfqdRecordset.IsEOF)
            {
                row = new DataGridViewRow();
                //将字段值添加到dataGridView中对应的位置
                DataGridViewTextBoxCell cell1 = new DataGridViewTextBoxCell();
                if (layerExist(gkfqdRecordset))
                {
                    cell1.Value = "已录入";
                    cell1.Style.BackColor = Color.Green;
                }
                else
                {
                    cell1.Value = "未录入";
                }
                row.Cells.Add(cell1);
                for (int i = 0; i < gkfqdRecordset.FieldCount; i++)
                {
                    //定义并获得字段值
                    Object fieldValue = gkfqdRecordset.GetFieldValue(i);

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

                gkfqdRecordset.MoveNext();
            }
            this.dataGridView1.Update();

            gkfqdRecordset.Dispose();
        }
        #endregion

        # region 坐标文件导入

      
        private void button2_Click(object sender, EventArgs e)
        {
            //打开指定文件读文件
            OpenFileDialog objFile = new OpenFileDialog();
            objFile.Filter = "txt文件(.txt)|*.txt";
            //objFile.Filter = "shp文件(.shp)|*.shp|dwg文件(.dwg)|*.dwg";
            objFile.ShowDialog();
            imgPath = objFile.FileName;
            String sourcePath = imgPath;

            if (imgPath=="")
            {
                MessageBox.Show("请选择导入坐标文件！");
                return;
            }

            #region 坐标点导入处理
            //坐标点导入处理
            StreamReader objReader = new StreamReader(imgPath);
            string sLine = "";
            LineList = new ArrayList();
            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null && !sLine.Equals(""))
                    LineList.Add(sLine);
            }
            objReader.Close();
            //打开数据源
            //-------------数据库打开工作空间
            workspace1 = new SuperMap.Data.Workspace();
            /* WorkspaceconnectionInfo workspaceconnectionInfo = new WorkspaceconnectionInfo();
             workspaceconnectionInfo.Type = WorkspaceType.Oracle;
             workspaceconnectionInfo.Server = "ORCL";
             workspaceconnectionInfo.Database = "";
             workspaceconnectionInfo.Name = "workspace";
             workspaceconnectionInfo.User = "gkfqd";
             workspaceconnectionInfo.Password = "123456";
             workspace1.Open(workspaceconnectionInfo);*/
            workspace1.Open(gkfqd.Common.Tool.GetConnectionInfo());
            importDatasource = workspace1.Datasources[gkfqd.Common.Tool.GetWorkspaceDataDatasources()];
            //导入坐标
            try
            {
                importResultShp = importDatasource.Datasets["temp_gkfqd"] as DatasetVector;
                Recordset recordset = (importResultShp as DatasetVector).GetRecordset(false, CursorType.Dynamic);
                // 获得记录集对应的批量更新对象
                Recordset.BatchEditor editor = recordset.Batch;
                // 开始批量添加，将 example 数据集每条记录对应的几何对象添加到数据集中
                editor.Begin();
                //删除所有记录
                recordset.DeleteAll();
                Point2Ds points = new Point2Ds();
                for (int i = 1; i < LineList.Count - 1; i++)
                {
                    string[] fieldInfoListZ = LineList[i].ToString().Split(',');
                    Point2D point2D = new Point2D();
                    point2D.X = double.Parse(fieldInfoListZ[0].ToString());
                    point2D.Y = double.Parse(fieldInfoListZ[1].ToString());
                    points.Add(point2D);
                }
                GeoLine geolineE = new GeoLine();
                geolineE.AddPart(points);
                GeoRegion georegion = geolineE.ConvertToRegion();
                recordset.AddNew(georegion);
                editor.Update();
                recordset.Dispose();
                mapControl1.Map.Workspace = workspace1;
                mapControl1.Map.Layers.Clear();
                mapControl1.Map.Layers.Add(importResultShp, true);
                mapControl1.Map.ViewEntire();
                mapControl1.Map.Refresh();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }
            RefreshDataGridView();
          
            # endregion
        }

        # endregion

        #region 复垦导入
        public void FkImport() {

            if (dataGridView1.CurrentRow == null) return;
            DataGridViewRow dgvr = dataGridView1.CurrentRow;

            string strSmid = dgvr.Cells["SMID"].Value.ToString();//获取smid值
            // 构造一个查询参数对象，查询选中的记录
            QueryParameter para = new QueryParameter();
            para.AttributeFilter = "SMID = " + strSmid;
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

            string strInputStatus = dgvr.Cells["录入状态"].Value.ToString();//获取smid值

            if (strInputStatus == "已录入")
            {
                //  根据图形查找地块编号 dkbh

                DatasetVector gkfqdDatasetVector = (DatasetVector)importDatasource.Datasets[gkfqd.Common.DbUse.GetTownCode(textBox3.Text)];
                DatasetVector tempDatasetVector = (DatasetVector)importDatasource.Datasets["temp_gkfqd"];

                Recordset tempRecordset = tempDatasetVector.Query(para);
                Recordset gkfqdRecordset = gkfqdDatasetVector.GetRecordset(false, CursorType.Dynamic);
                //判断添加地块在目标图层是否存在 标记
                Boolean flag = false;

                //从正式图层中找到和临时图层一样的地块  取得 该地块 的地块编号 为以下查询用
                string strDkbh = "";
                //复垦项目名称，已录入的项目名称不一定等于界面上的项目名称，所以这里再取得项目名称
                string strProjectName = "";
                if (gkfqdRecordset.RecordCount > 0)
                {
                    for (int i = 0; i <= gkfqdRecordset.RecordCount; i++)
                    {
                        gkfqdRecordset.MoveTo(i);
                        if (gkfqdRecordset.GetGeometry().Bounds.Equals(tempRecordset.GetGeometry().Bounds))
                        {
                            strDkbh = gkfqdRecordset.GetFieldValue("DKBH").ToString();
                            strProjectName = gkfqdRecordset.GetFieldValue("FKXMMC").ToString();
                            flag = true;
                            break;
                        }
                    }
                }

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
                sqlQuery.Append("JSYDHF           AS 建设用地合法性,  ");
                sqlQuery.Append("FKYWR           AS 复垦义务人情况,  ");
                sqlQuery.Append("DKMC           AS 地块名称,  ");
                sqlQuery.Append("DKMJ           AS 地块面积,  ");
                sqlQuery.Append("ZBX           AS 坐标系,  ");
                sqlQuery.Append("JZDS           AS 界址点数,  ");
                sqlQuery.Append("JDFD           AS 几度分带,  ");
                sqlQuery.Append("JD          AS 精度,  ");
                sqlQuery.Append("JLDW          AS 计量单位,  ");
                sqlQuery.Append("JLTCSX          AS 记录图形属性,  ");
                sqlQuery.Append("TYLX          AS 投影类型 ");
                sqlQuery.Append("FROM  " + gkfqd.Common.DbUse.GetTownCode(textBox3.Text));
                sqlQuery.Append(" WHERE         FKXMMC='" + strProjectName + "'AND DKBH='" + strDkbh + "'");
                gkfqd.Common.DbUse.GetOleDbconnection().Close();
                gkfqd.Common.DbUse.GetOleDbconnection().Open();
                dataSet6.Clear();
                OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
                MyAdapter.Fill(dataSet6);
                gkfqd.Common.DbUse.GetOleDbconnection().Close();
                gk013 frmgk013 = new gk013(importDatasource, dgvr, strProjectName, "已录入", dataSet6.Tables[0],textBox3.Text);
                frmgk013.Owner = this;
                frmgk013.FormBorderStyle = FormBorderStyle.FixedSingle;//固定窗体大小不变
                frmgk013.StartPosition = FormStartPosition.CenterScreen;//窗体居中
                frmgk013.ShowDialog();
            }
            else
            {
                gk013 frmgk013 = new gk013(importDatasource, dgvr, textBox1.Text, "未录入", null,textBox3.Text);
                frmgk013.Owner = this;
                frmgk013.FormBorderStyle = FormBorderStyle.FixedSingle;//固定窗体大小不变
                frmgk013.StartPosition = FormStartPosition.CenterScreen;//窗体居中
                frmgk013.ShowDialog();
            }

            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("请点击行头选择数据！");
                return;
            }
            
        }
        #endregion

        #region 建新导入
        public void JxImport() {

            if (dataGridView1.CurrentRow == null) return;
            DataGridViewRow dgvr = dataGridView1.CurrentRow;

            string strSmid = dgvr.Cells["SMID"].Value.ToString();//获取smid值
            // 构造一个查询参数对象，查询选中的记录
            QueryParameter para = new QueryParameter();
            para.AttributeFilter = "SMID = " + strSmid;
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

            string strInputStatus = dgvr.Cells["录入状态"].Value.ToString();//获取smid值

            if (strInputStatus == "已录入")
            {
                //  根据图形查找地块编号 dkbh
                //动态拼接图层名，建新数据要导入的图层
                DatasetVector gkfqdDatasetVector = (DatasetVector)importDatasource.Datasets["JX"+gkfqd.Common.DbUse.GetTownCode(textBox3.Text)];
                DatasetVector tempDatasetVector = (DatasetVector)importDatasource.Datasets["temp_gkfqd"];

                Recordset tempRecordset = tempDatasetVector.Query(para);
                Recordset gkfqdRecordset = gkfqdDatasetVector.GetRecordset(false, CursorType.Dynamic);
                //判断添加地块在目标图层是否存在 标记
                Boolean flag = false;

                //从正式图层中找到和临时图层一样的地块  取得 该地块 的地块编号 为以下查询用
                string strDkbh = "";
                if (gkfqdRecordset.RecordCount > 0)
                {
                    for (int i = 0; i <= gkfqdRecordset.RecordCount; i++)
                    {
                        gkfqdRecordset.MoveTo(i);
                        if (gkfqdRecordset.GetGeometry().Bounds.Equals(tempRecordset.GetGeometry().Bounds))
                        {
                            strDkbh = gkfqdRecordset.GetFieldValue("DKBH").ToString();
                            flag = true;
                            break;
                        }
                    }
                }

                //获取项目名称值
                sqlQuery.Clear();
                sqlQuery.Append("SELECT  DKBH   AS 地块编号, ");
                sqlQuery.Append("  DKWZ           AS 地块位置, ");
                sqlQuery.Append("  DKMC           AS 地块名称, ");
                 sqlQuery.Append(" DKMJ           AS 地块面积, ");
                sqlQuery.Append("  ZBX            AS 坐标系, ");
                sqlQuery.Append("  JZDS           AS 界址点数, ");
                sqlQuery.Append("  JDFD           AS 几度分带, ");
                sqlQuery.Append("  JD             AS 精度, ");
                sqlQuery.Append("  JLDW           AS 计量单位, ");
                sqlQuery.Append("  JLTCSX         AS 记录图形属性, ");
                sqlQuery.Append("  TYLX           AS 投影类型 ");
                sqlQuery.Append(" FROM " + "JX" + gkfqd.Common.DbUse.GetTownCode(textBox3.Text));
                sqlQuery.Append(" WHERE         FKXMMC='" + textBox1.Text + "'AND DKBH='" + strDkbh + "'");
                gkfqd.Common.DbUse.GetOleDbconnection().Close();
                gkfqd.Common.DbUse.GetOleDbconnection().Open();
                dataSet6.Clear();
                OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
                MyAdapter.Fill(dataSet6);
                gkfqd.Common.DbUse.GetOleDbconnection().Close();
                gk019 frmgk019 = new gk019(importDatasource, dgvr, textBox1.Text, "已录入", dataSet6.Tables[0], textBox3.Text);
                frmgk019.Owner = this;
                frmgk019.FormBorderStyle = FormBorderStyle.FixedSingle;//固定窗体大小不变
                frmgk019.StartPosition = FormStartPosition.CenterScreen;//窗体居中
                frmgk019.ShowDialog();
            }
            else
            {
                gk019 frmgk019 = new gk019(importDatasource, dgvr, textBox1.Text, "未录入", null, textBox3.Text);
                frmgk019.Owner = this;
                frmgk019.FormBorderStyle = FormBorderStyle.FixedSingle;//固定窗体大小不变
                frmgk019.StartPosition = FormStartPosition.CenterScreen;//窗体居中
                frmgk019.ShowDialog();
            }

            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("请点击行头选择数据！");
                return;
            }
        
        }
        #endregion

        #region 补充耕地地块录入
        public void BgImport() {
            if (dataGridView1.CurrentRow == null) return;
            DataGridViewRow dgvr = dataGridView1.CurrentRow;
            string strSmid = dgvr.Cells["SMID"].Value.ToString();//获取smid值
            // 构造一个查询参数对象，查询选中的记录
            QueryParameter para = new QueryParameter();
            para.AttributeFilter = "SMID = " + strSmid;
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

            string strInputStatus = dgvr.Cells["录入状态"].Value.ToString();//获取smid值

            if (strInputStatus == "已录入")
            {
                //  根据图形查找地块编号 dkbh
                //动态拼接图层名，建新数据要导入的图层
                DatasetVector gkfqdDatasetVector = (DatasetVector)importDatasource.Datasets["BG" + gkfqd.Common.DbUse.GetTownCode(textBox3.Text)];
                DatasetVector tempDatasetVector = (DatasetVector)importDatasource.Datasets["temp_gkfqd"];

                Recordset tempRecordset = tempDatasetVector.Query(para);
                Recordset gkfqdRecordset = gkfqdDatasetVector.GetRecordset(false, CursorType.Dynamic);
                //判断添加地块在目标图层是否存在 标记
                Boolean flag = false;

                //从正式图层中找到和临时图层一样的地块  取得 该地块 的地块编号 为以下查询用
                string strDkbh = "";
                if (gkfqdRecordset.RecordCount > 0)
                {
                    for (int i = 0; i <= gkfqdRecordset.RecordCount; i++)
                    {
                        gkfqdRecordset.MoveTo(i);
                        if (gkfqdRecordset.GetGeometry().Bounds.Equals(tempRecordset.GetGeometry().Bounds))
                        {
                            strDkbh = gkfqdRecordset.GetFieldValue("DKBH").ToString();
                            flag = true;
                            break;
                        }
                    }
                }

                //获取项目名称值
                sqlQuery.Clear();
                sqlQuery.Append("SELECT  DKBH   AS 地块编号, ");
                sqlQuery.Append("  FKXMMC           AS 所属复垦项目名称, ");
                sqlQuery.Append("  XMSZXM           AS 项目所在县名称, ");
                sqlQuery.Append(" BZ           AS 备注 ");
                sqlQuery.Append(" FROM " + "BG" + gkfqd.Common.DbUse.GetTownCode(textBox3.Text));
                sqlQuery.Append(" WHERE         FKXMMC='" + textBox1.Text + "'AND DKBH='" + strDkbh + "'");
                gkfqd.Common.DbUse.GetOleDbconnection().Close();
                gkfqd.Common.DbUse.GetOleDbconnection().Open();
                dataSet6.Clear();
                OleDbDataAdapter MyAdapter = new OleDbDataAdapter(sqlQuery.ToString(), gkfqd.Common.DbUse.GetOleDbconnection());
                MyAdapter.Fill(dataSet6);
                gkfqd.Common.DbUse.GetOleDbconnection().Close();
                gk01d frmgk01d = new gk01d(importDatasource, dgvr, textBox1.Text, "已录入", dataSet6.Tables[0], textBox3.Text);
                frmgk01d.Owner = this;
                frmgk01d.FormBorderStyle = FormBorderStyle.FixedSingle;//固定窗体大小不变
                frmgk01d.StartPosition = FormStartPosition.CenterScreen;//窗体居中
                frmgk01d.ShowDialog();
            }
            else
            {
                gk01d frmgk01d = new gk01d(importDatasource, dgvr, textBox1.Text, "未录入", null, textBox3.Text);
                frmgk01d.Owner = this;
                frmgk01d.FormBorderStyle = FormBorderStyle.FixedSingle;//固定窗体大小不变
                frmgk01d.StartPosition = FormStartPosition.CenterScreen;//窗体居中
                frmgk01d.ShowDialog();
            }

            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("请点击行头选择数据！");
                return;
            }
        }
        #endregion

        #region 点击地图地块录入属性操作
        private void mapControl1_GeometrySelected(object sender, GeometrySelectedEventArgs e)
        {
            //获取选择集
           /* Selection[] selection = mapControl1.Map.FindSelection(true);

            //判断选择集是否为空
            if (selection == null || selection.Length == 0)
            {
                MessageBox.Show("请选择要查询属性的空间对象");
                return;
            }
            //将选择集转换为记录
            Recordset recordset = selection[0].ToRecordset();
            Selection selection1 = mapControl1.Map.Layers[0].Selection;

            selection1.FromRecordset(recordset);

            mapControl1.Map.EnsureVisible(recordset.GetGeometry(), 0.8);

            mapControl1.Map.Refresh();

            recordset.Dispose();
            if (dataGridView1.CurrentRow == null) return;
            DataGridViewRow dgvr = dataGridView1.CurrentRow;
            gk013 frmgk013 = new gk013(importDatasource, dgvr, textBox1.Text, "未录入", null);
            frmgk013.Owner = this;
            frmgk013.FormBorderStyle = FormBorderStyle.FixedSingle;//固定窗体大小不变
            frmgk013.StartPosition = FormStartPosition.CenterScreen;//窗体居中
            frmgk013.ShowDialog();*/
        }
        #endregion

    }
}
