﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SuperMap.Layout;
using SuperMap.Data;
using System.Diagnostics;
using SuperMap.UI;
using System.IO;
using System.Data.OleDb;

using System.Drawing.Drawing2D;
using SuperMap.Mapping;


namespace gkfqd.ui.gk06
{
    public partial class gk061 : WinFormsUI.Docking.DockContent
    {
        #region 变量区
        private Int32 m_mapID;
        Boolean isLock = false;
        DataSet dataSet4 = new DataSet();
        DataSet dataSet2 = new DataSet();
        DataSet dataSet1 = new DataSet();
        DataSet dataSet  = new DataSet();
        GeoMap geoMapUse;
        //标题ID 为了刷新时候用，先取得id在删除，在添加 达到刷新效果
        int ElementsId = -1;
        int[] ElementsIdS = new int[1];
        GeoText geoText = null;
        private Datasource importDatasource;
        private SuperMap.UI.MapControl mapControl1;
        private Map m_map;
        //影像ID
        int ElementsId1 = -1;
        int[] ElementsIdS1 = new int[1];

        //MapID
        int ElementsIdMap = -1;
        int[] ElementsIdMap1 = new int[1];

        GeoMapScale geoMapScale;
        int ElementsIdScale = -1;
        int[] ElementsIdScaleArrar = new int[1];


        DatasetVector datasetVector=null;
        #endregion 

        #region 添加地图
        private MapControl m_mapControl;
        private void AddMap(string  mapName)
        {
            try
            {
                LayoutElements elements = mapLayoutControl1.MapLayout.Elements;
                //构造GeoMap
                GeoMap geoMapUse = new GeoMap();
                geoMapUse.MapName = mapName;
               

               // geoMapUse. = m_mapControl.Map;
                //设置GeoMap对象的外切矩形
                Rectangle2D rect = new Rectangle2D(new Point2D(850, 1300), new Size2D(
                        1500, 1500));
                GeoRectangle geoRect = new GeoRectangle(rect, 0);
                geoMapUse.Shape = geoRect;
                elements.AddNew(geoMapUse);
                m_mapID = elements.GetID();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
        #endregion

        #region 初始化
        public gk061()
        {
            InitializeComponent();

            #region 添加地图
            workspace1 = new SuperMap.Data.Workspace();
            workspace1.Open(gkfqd.Common.Tool.GetConnectionInfo());
            importDatasource = workspace1.Datasources[gkfqd.Common.Tool.GetWorkspaceDataDatasources()];
            mapLayoutControl1.MapLayout.Workspace = workspace1;
            datasetVector = importDatasource.Datasets["gkfqd"] as DatasetVector;
            mapLayoutControl1.IsHorizontalScrollbarVisible = true;
            mapLayoutControl1.IsVerticalScrollbarVisible = true;
            AddMap("temp");
            mapLayoutControl1.MapLayout.Zoom(4);
              
            #endregion
            //添加系统默认打印服务
            foreach (String printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                toolStripComboBox2.Items.Add(printer);
            }
            toolStripComboBox2.SelectedIndex = 0;
            //不显示dataGridView1最后一行
            // dataGridView1.AllowUserToAddRows = false;
            comboBox1.SelectedIndex = 0;
            //区县comboBox列表加载
            LoadComboBox();
        }
        
        #endregion

        #region 取得 map ID
        public int GetMapID()
        {

            int mapID = -1;

            int count = 0;

            LayoutSelection layoutSelection = mapLayoutControl1.MapLayout.Selection;

            LayoutElements layoutElements = mapLayoutControl1.MapLayout.Elements;

            layoutElements.Refresh();

            for (int i = 0; i < layoutSelection.Count; i++)
            {

                int ID = layoutSelection[i];

                layoutElements.SeekID(ID);

                Geometry geometry = layoutElements.GetGeometry();

                if (geometry.Type == GeometryType.GeoMap)
                {

                    mapID = ID;

                    count++;

                }

                if (count > 1)
                {

                    mapID = -1;

                }

            }

            return mapID;

        }
        #endregion
       
        #region  出图菜单点击功能按钮事件处理

          #region 放大缩小平移
        private void 放大ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isLock)
            {
                mapLayoutControl1.MapAction = SuperMap.UI.Action.ZoomIn;
            }
            else
            {
                mapLayoutControl1.LayoutAction = SuperMap.UI.Action.ZoomIn;
            }
        }

        private void 缩小ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isLock)
            {
                mapLayoutControl1.MapAction = SuperMap.UI.Action.ZoomOut;
            }
            else
            {
                mapLayoutControl1.LayoutAction = SuperMap.UI.Action.ZoomOut;
            }
        }

        private void 平移ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isLock)
            {
                mapLayoutControl1.MapAction = SuperMap.UI.Action.Pan;
            }
            else
            {
                mapLayoutControl1.LayoutAction = SuperMap.UI.Action.Pan;
            }
        }
        #endregion

          #region 取消地图操作
        private void 取消地图操作ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isLock = false;
            Int32 mapID = -1;
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            mapLayoutControl1.ActiveGeoMapID = mapID;
        }
        #endregion

          #region 全幅显示
        private void 全幅显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (isLock)
            {

                Map map = mapLayoutControl1.ActiveMap;

                map.ViewEntire();

                mapLayoutControl1.MapLayout.Refresh();

            }

            else
            {
                mapLayoutControl1.MapLayout.ZoomToPaper();
            }
        }
        #endregion

          #region 打印
        private void 打印ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                mapLayoutControl1.MapLayout.Printer.PrinterName = toolStripComboBox2.SelectedItem as String;
                mapLayoutControl1.MapLayout.Printer.Print();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
        #endregion 

          #region 添加标题
         private void 添加标题ToolStripMenuItem_Click(object sender, EventArgs e)
         {

                gk066 addTitle = new gk066();
                addTitle.StartPosition = FormStartPosition.CenterScreen;//窗体居中
                addTitle.ShowDialog();

                #region 添加标文本题
                //标题添加
                TextStyle textStyle = new TextStyle();
                textStyle.Alignment = TextAlignment.TopCenter;
                textStyle.BackColor = Color.FromArgb(65, 65, 65);
                textStyle.ForeColor = Color.FromArgb(174, 241, 176);
                textStyle.BackOpaque = false;
                textStyle.Bold = true;
                textStyle.FontName = "楷体";
                textStyle.FontHeight = addTitle.FontHeight;
                textStyle.FontWidth = addTitle.FontWidth;
                textStyle.IsSizeFixed = false;
                textStyle.Italic = true;
                textStyle.Outline = true;
                textStyle.Weight = 500;

                //添加文本
                TextPart textPart = new TextPart(addTitle.title, new Point2D(850, 2150), 0);
                geoText= new GeoText(textPart, textStyle);
                //先删除后添加实现刷新效果
                if (ElementsId != -1)
                {
                    ElementsIdS[0] = ElementsId;
                    mapLayoutControl1.MapLayout.Elements.Delete(ElementsIdS);
                }
                //添加地图
                mapLayoutControl1.MapLayout.Elements.AddNew(geoText);
                //取得id 下次设定时删除用
                ElementsId = mapLayoutControl1.MapLayout.Elements.GetID();
                mapLayoutControl1.MapLayout.Elements.Refresh();
                #endregion

                #region 设定图层颜色 与显示比例尺
                geoMapUse = mapLayoutControl1.MapLayout.Elements.GetGeometry() as GeoMap;
                Map map = new Map();
                map.Workspace = workspace1;
                map.FromXML(workspace1.Maps.GetMapXML(geoMapUse.MapName));
                LayerSettingVector setting = map.Layers[0].AdditionalSetting as LayerSettingVector;
                setting.Style.FillForeColor = addTitle.selectColor;
                map.Layers[0].AdditionalSetting = setting;
                workspace1.Maps.SetMapXML(geoMapUse.MapName, map.ToXML());
                workspace1.Save();
                geoMapUse = new GeoMap();
                geoMapUse.MapName = "temp";
                //设置GeoMap对象的外切矩形
                Rectangle2D rect = new Rectangle2D(new Point2D(850, 1300), new Size2D(
                       1500, 1500));
                GeoRectangle geoRect = new GeoRectangle(rect, 0);
               geoMapUse.Shape = geoRect;
               //动态设置缩放比例尺
               geoMapUse.MapScale = addTitle.scaleNumerator / addTitle.scaleDenominato;
               mapLayoutControl1.MapLayout.Elements.AddNew(geoMapUse);
                //记录下mapid
                ElementsIdMap = mapLayoutControl1.MapLayout.Elements.GetID();
                mapLayoutControl1.MapLayout.Elements.Refresh();
                #endregion 

                #region 添加地图比例标尺
                //添加地图带比例标尺
                geoMapScale = new GeoMapScale(ElementsIdMap, new Point2D(1050, 515), 550, 18);
                geoMapScale.ScaleUnit = Unit.Kilometer;
                geoMapScale.LeftDivisionCount = 2;
                geoMapScale.SegmentCount = 2;
                geoMapScale.ScaleType = GeoMapScaleType.Railway;
                ////先删除后添加实现刷新效果
                if (ElementsIdScale != -1)
                {
                    ElementsIdScaleArrar[0] = ElementsIdScale;
                    mapLayoutControl1.MapLayout.Elements.Delete(ElementsIdScaleArrar);
                }
                //取得id 下次设定时删除用
                mapLayoutControl1.MapLayout.Elements.AddNew(geoMapScale);
                ElementsIdScale = mapLayoutControl1.MapLayout.Elements.GetID();
                mapLayoutControl1.MapLayout.Elements.Refresh();
                #endregion

                addTitle.Close();
        }
        #endregion 

          #region 指南针
        private void 指南针ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rectangle2D rectangle2D = new Rectangle2D(
                       new Point2D(200, 1900), new Size2D(150, 150));
            GeoNorthArrow geoNorthArrow = new GeoNorthArrow(
                          NorthArrowStyleType.ArrowWithShadow, rectangle2D, 0);
            geoNorthArrow.BindingGeoMapID = GetMapID();
            mapLayoutControl1.MapLayout.Elements.AddNew(geoNorthArrow);
        }
        #endregion
 
          #region 图例
        private void 图例ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutElements layoutElements = mapLayoutControl1.MapLayout.Elements;

            layoutElements.SeekID(GetMapID());

            GeoMap geoMapUse = (GeoMap)layoutElements.GetGeometry();

            string geoMapName = geoMapUse.MapName;

            GeoLegend geoLegend = new GeoLegend(geoMapName, workspace1);

            geoLegend.Height = 175;

            geoLegend.Width = 350;

            geoLegend.Center = new Point2D(1375, 1855);

            GeoStyle geoLegendStyle = new GeoStyle();

            geoLegendStyle.FillForeColor = Color.FromArgb(255, 235, 175);

            geoLegendStyle.FillOpaqueRate = 30;

            geoLegendStyle.LineWidth = 0.5;

            geoLegendStyle.LineColor = Color.FromArgb(65, 65, 65);

            geoLegend.BackGroundStyle = geoLegendStyle;

            geoLegend.ColumnCount = 3;


            //设置图例项和图例子项的说明文本的风格

            TextStyle geoLegendtextStyle = new TextStyle();

            geoLegendtextStyle.BackColor = Color.Yellow;

            geoLegendtextStyle.ForeColor = Color.Blue;

            geoLegendtextStyle.FontName = "宋体";

            geoLegendtextStyle.FontHeight = 20.0;

            geoLegendtextStyle.FontWidth = 12.0;

            geoLegendtextStyle.IsSizeFixed = false;

            geoLegend.ItemTextStyle = geoLegendtextStyle;

            geoLegend.SubItemTextStyle = geoLegendtextStyle;

            //设置图例标题风格

            TextStyle titleTextStyle = new TextStyle();

            titleTextStyle.BackColor = Color.Yellow;

            titleTextStyle.ForeColor = Color.Blue;

            titleTextStyle.FontName = "宋体";

            titleTextStyle.FontHeight = 40.0;

            titleTextStyle.FontWidth = 25.0;

            titleTextStyle.Italic = true;

            titleTextStyle.Bold = true;

            titleTextStyle.IsSizeFixed = false;

            titleTextStyle.Weight = 200;

            geoLegend.Title = "图例";

            geoLegend.TitleStyle = titleTextStyle;

            //将图例添加到布局图层，而非屏幕图层。

            geoLegend.Load(false);

            mapLayoutControl1.MapLayout.Elements.AddNew(geoLegend);

        }
        #endregion 

          #region 输出格式
        private void 输出EMFToolStripMenuItem_Click(object sender, EventArgs e)
        {
       
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "导出为bmp(*.emf)|*.emf";

            saveFileDialog.Title = "保存";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {

                string emfName = saveFileDialog.FileName;

                mapLayoutControl1.MapLayout.OutputLayoutToEMF(emfName);

            }

        }

        private void 输出EPSToolStripMenuItem_Click(object sender, EventArgs e)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "导出为bmp(*.eps)|*.eps";

            saveFileDialog.Title = "保存";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {

                string epsName = saveFileDialog.FileName;

                mapLayoutControl1.MapLayout.OutputLayoutToEPS(epsName);

            }

        }

        private void 输出PNGToolStripMenuItem_Click(object sender, EventArgs e)
        {
  
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "导出为bmp(*.png)|*.png";

            saveFileDialog.Title = "保存";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {

                string pngName = saveFileDialog.FileName;

                mapLayoutControl1.MapLayout.OutputLayoutToPNG(pngName, true);

            }
        }

        private void 输出BMPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int bmpDPI = 500;

            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "导出为bmp(*.bmp)|*.bmp";

            saveFileDialog.Title = "保存";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {

                string bmpName = saveFileDialog.FileName;

                mapLayoutControl1.MapLayout.OutputLayoutToBMP(bmpName, bmpDPI);

            }

        }

        private void 输出JPGToolStripMenuItem_Click(object sender, EventArgs e)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "导出为JPG(*.jpg)|*.jpg";

            saveFileDialog.Title = "保存";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {

                string pdfName = saveFileDialog.FileName;

                mapLayoutControl1.MapLayout.OutputLayoutToJPG(pdfName);

            }
        }
        #endregion 

          #region 打印预览
        private void 打印预览ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mapLayoutControl1.MapLayout.PrintPreview = true;

            mapLayoutControl1.MapLayout.Refresh();
        }
        #endregion

          #region 打印布局
        private void 打印布局ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Printer printer = mapLayoutControl1.MapLayout.Printer;

            printer.PaperSize = PaperSize.A4;

            printer.Orientation = PaperOrientation.Landscape;

            //设置页边距

            PaperMargin paperMargin = new PaperMargin(70);

            paperMargin.Left = 100;

            paperMargin.Right = 100;

            printer.Margin = paperMargin;

            //打印机设置

            printer.DeviceDPI = 72;

            printer.IsVectorPrint = true;

            printer.PrinterName = "Microsoft XPS Document Writer";

            //打印布局

            if (printer.IsValidPrinter)
            {

                MessageBox.Show("可以打印！");

            }

            else
            {

                MessageBox.Show("打印机不合法！");

            }
        }
        #endregion 
         
          #region 激活地图操作
        private void 地图操作ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isLock = true;
            try
            {
                Int32 mapID = -1;
                if (isLock)
                {
                    mapID = m_mapID;
                }
                mapLayoutControl1.ActiveGeoMapID = mapID;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
        #endregion

          #region 边框
        private void 边框ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GeoMap geoMapUseLine = new GeoMap();

            geoMapUseLine.MapName = "2000年各省人口数分段专题图";

            //设置GeoMap对象的外切矩形

            Rectangle2D rectangle2D = new Rectangle2D(new Point2D(850, 1250), new Size2D(1500, 1500));

            GeoRectangle geoRectangle = new GeoRectangle(rectangle2D, 0);

            geoMapUseLine.Shape = geoRectangle;

            //设置GeoMap对象的MapBorder

            GeoMapBorder geoMapBorder = new GeoMapBorder();

            geoMapBorder.BorderType = GeoMapBorderType.Complex;

            //设置MapBorder的内框

            geoMapBorder.InFrameColor = Color.White;

            geoMapBorder.InFrameWidth = 0.5;

            geoMapBorder.InFrameInterval = 5;

            //设置MapBorder的内线

            geoMapBorder.InLineColor = Color.FromArgb(115, 115, 115);

            geoMapBorder.InLineWidth = 0.5;

            geoMapBorder.InLineInterval = 5;

            //设置MapBorder的外线

            geoMapBorder.OutLineColor = Color.White;

            geoMapBorder.OutLineWidth = 0.5;

            geoMapBorder.OutLineInterval = 30;

            //设置MapBorder的外框

            geoMapBorder.OutFrameColor = Color.FromArgb(115, 115, 115);

            geoMapBorder.OutFrameWidth = 1;

            geoMapBorder.OutFrameInterval = 2;



            //设置填充文本的风格

            TextStyle fillTextStyle = new TextStyle();

            fillTextStyle.Shadow = true;

            fillTextStyle.Alignment = TextAlignment.TopCenter;

            fillTextStyle.BackColor = Color.White;

            fillTextStyle.ForeColor = Color.FromArgb(135, 171, 217);

            fillTextStyle.BackOpaque = true;

            fillTextStyle.Bold = false;

            fillTextStyle.FontName = "宋体";

            fillTextStyle.FontHeight = 10.0;

            fillTextStyle.FontWidth = 10.0;

            fillTextStyle.IsSizeFixed = false;

            fillTextStyle.Weight = 100;
            //设置MapBorder的填充

            geoMapBorder.FillType = GeoMapBorderFillType.Text;

            geoMapBorder.FillDirection = FillDirectionType.Inner;

            geoMapBorder.FillText = " * ";

            geoMapBorder.FillTextStyle = fillTextStyle;

            //设置MapBorder的转角填充

            geoMapBorder.CornerFillType = GeoMapBorderFillType.Image;

            geoMapBorder.CornerFillImageFile = @"../../Resources/SceneBrowse/a18.bmp";

            geoMapBorder.CornerFillStartMode = CornerFillStartMode.LeftBottom;



            geoMapUseLine.MapBorder = geoMapBorder;

            geoMapUseLine.IsBorderVisible = true;

            mapLayoutControl1.MapLayout.Elements.AddNew(geoMapUseLine);

        }
        #endregion

          #region 输出pdf格式
        private void 输出PDFToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //设置PDF文件输出项

            PDFOptions pdfOptions = new PDFOptions();

            pdfOptions.IsEntire = true;

            pdfOptions.IsLineStyleRetained = true;

            pdfOptions.IsPointStyleRetained = true;

            pdfOptions.IsRegionStyleRetained = true;

            pdfOptions.IsVector = true;

            SaveFileDialog saveFileDialog = new SaveFileDialog();

            saveFileDialog.Filter = "导出为PDF(*.pdf)|*.pdf";

            saveFileDialog.Title = "保存";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {

                string pdfName = saveFileDialog.FileName;

                mapLayoutControl1.MapLayout.OutputLayoutToPDF(pdfName, pdfOptions);

            }
        }
        #endregion

        #endregion

        # region 影像添加取消
       
        private void 添加影像ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                LayoutElements elements = mapLayoutControl1.MapLayout.Elements;
                //构造GeoMap
                //添加影像
                GeoMap geoMap1 = new GeoMap();
                geoMap1.MapName = "neimeng6@ORCL";
                //设置GeoMap对象的外切矩形
                Rectangle2D rect1 = new Rectangle2D(new Point2D(850, 1300), new Size2D(
                        1500, 1500));
                GeoRectangle geoRect1 = new GeoRectangle(rect1, 0);
                geoMap1.Shape = geoRect1;
                elements.AddNew(geoMap1);
               
                ElementsId1 = elements.GetID();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
            }
        }
       
        private void 取消影像ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ElementsId1 != -1)
            {
                // mapLayoutControl1.MapLayout.Elements.MoveTo(ElementsId);
                ElementsIdS1[0] = ElementsId1;
                mapLayoutControl1.MapLayout.Elements.Delete(ElementsIdS1);
            }
        }
        # endregion

        #region 区县选择
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            StringBuilder sqlQuery = new StringBuilder();
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

        #region 加载区县
        public void LoadComboBox()
        {
            StringBuilder sqlQuery = new StringBuilder();
            sqlQuery.Append(" SELECT XZQM  FROM  XZQ  ");
            sqlQuery.Append(" WHERE CJ ='1'  ");
            sqlQuery.Append(" AND  BHBS <> '0000'  ");
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
            sqlQuery.Append(" AND BHBS ='1001'");
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

        #region 区县选择跟换地图操作
        private void comboBox3_MouseClick(object sender, MouseEventArgs e)
        {
            // string mapName = comboBox3.Text;
            string mapName = "zjx@ORCL";
            AddMap(mapName);
            mapLayoutControl1.MapLayout.Elements.Refresh();
        }
        #endregion

        #region 比例尺显示
        private void 比例尺ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //添加地图带比例标尺
            geoMapScale = new GeoMapScale(ElementsIdMap, new Point2D(1050, 515), 550, 18);
            geoMapScale.ScaleUnit = Unit.Kilometer;
            geoMapScale.LeftDivisionCount = 2;
            geoMapScale.SegmentCount = 2;
            geoMapScale.ScaleType = GeoMapScaleType.Railway;
            ////先删除后添加实现刷新效果
            if (ElementsIdScale != -1)
            {
                ElementsIdScaleArrar[0] = ElementsIdScale;
                mapLayoutControl1.MapLayout.Elements.Delete(ElementsIdScaleArrar);
            }
            //6651
            //取得id 下次设定时删除用
            mapLayoutControl1.MapLayout.Elements.AddNew(geoMapScale);
            ElementsIdScale = mapLayoutControl1.MapLayout.Elements.GetID();
            mapLayoutControl1.MapLayout.Elements.Refresh();
        }
        #endregion 

        #region 对象移动

        #endregion

    }
}
