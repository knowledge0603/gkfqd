﻿namespace gkfqd.ui.gk04
{
    partial class gk045
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            SuperMap.Data.GeoStyle geoStyle7 = new SuperMap.Data.GeoStyle();
            SuperMap.Data.GeoRegion geoRegion3 = new SuperMap.Data.GeoRegion();
            SuperMap.Mapping.GridSetting gridSetting3 = new SuperMap.Mapping.GridSetting();
            SuperMap.Data.GeoStyle geoStyle8 = new SuperMap.Data.GeoStyle();
            SuperMap.Data.GeoStyle geoStyle9 = new SuperMap.Data.GeoStyle();
            SuperMap.Mapping.MapOverlapDisplayedOptions mapOverlapDisplayedOptions3 = new SuperMap.Mapping.MapOverlapDisplayedOptions();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.xZDLTBBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gK0451 = new gkfqd.GK045();
            this.workspace1 = new SuperMap.Data.Workspace(this.components);
            this.map1 = new SuperMap.Mapping.Map(this.components);
            this.mapControl1 = new SuperMap.UI.MapControl();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.gK0451BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.xZDLTBTableAdapter = new gkfqd.GK045TableAdapters.XZDLTBTableAdapter();
            this.comboBox5 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.splashScreenManager1 = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::gkfqd.ui.gk03.WaitForm2), true, true);
            ((System.ComponentModel.ISupportInitialize)(this.xZDLTBBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gK0451)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gK0451BindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // xZDLTBBindingSource
            // 
            this.xZDLTBBindingSource.DataMember = "XZDLTB";
            this.xZDLTBBindingSource.DataSource = this.gK0451;
            // 
            // gK0451
            // 
            this.gK0451.DataSetName = "GK045";
            this.gK0451.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // workspace1
            // 
            this.workspace1.Caption = "UntitledWorkspace";
            this.workspace1.Description = "";
            // 
            // map1
            // 
            geoStyle7.FillBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            geoStyle7.FillBackOpaque = true;
            geoStyle7.FillForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            geoStyle7.FillGradientAngle = 0D;
            geoStyle7.FillGradientMode = SuperMap.Data.FillGradientMode.None;
            geoStyle7.FillGradientOffsetRatioX = 0;
            geoStyle7.FillGradientOffsetRatioY = 0;
            geoStyle7.FillOpaqueRate = 100;
            geoStyle7.FillSymbolID = 0;
            geoStyle7.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            geoStyle7.LineSymbolID = 0;
            geoStyle7.LineWidth = 0.1D;
            geoStyle7.MarkerAngle = 0D;
            geoStyle7.MarkerSymbolID = 0;
            this.map1.BackgroundStyle = geoStyle7;
            geoRegion3.ID = 0;
            geoRegion3.Style = null;
            this.map1.ClipRegion = geoRegion3;
            this.map1.DPI = 96D;
            geoStyle8.FillBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            geoStyle8.FillBackOpaque = true;
            geoStyle8.FillForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(235)))), ((int)(((byte)(255)))));
            geoStyle8.FillGradientAngle = 0D;
            geoStyle8.FillGradientMode = SuperMap.Data.FillGradientMode.None;
            geoStyle8.FillGradientOffsetRatioX = 0;
            geoStyle8.FillGradientOffsetRatioY = 0;
            geoStyle8.FillOpaqueRate = 100;
            geoStyle8.FillSymbolID = 0;
            geoStyle8.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            geoStyle8.LineSymbolID = 2;
            geoStyle8.LineWidth = 0.1D;
            geoStyle8.MarkerAngle = 0D;
            geoStyle8.MarkerSymbolID = 0;
            gridSetting3.DashStyle = geoStyle8;
            gridSetting3.HorizontalSpacing = 0D;
            gridSetting3.IsSizeFixed = false;
            gridSetting3.IsSnapable = false;
            gridSetting3.IsVisible = false;
            geoStyle9.FillBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            geoStyle9.FillBackOpaque = true;
            geoStyle9.FillForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(235)))), ((int)(((byte)(255)))));
            geoStyle9.FillGradientAngle = 0D;
            geoStyle9.FillGradientMode = SuperMap.Data.FillGradientMode.None;
            geoStyle9.FillGradientOffsetRatioX = 0;
            geoStyle9.FillGradientOffsetRatioY = 0;
            geoStyle9.FillOpaqueRate = 100;
            geoStyle9.FillSymbolID = 0;
            geoStyle9.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            geoStyle9.LineSymbolID = 0;
            geoStyle9.LineWidth = 0.1D;
            geoStyle9.MarkerAngle = 0D;
            geoStyle9.MarkerSymbolID = 0;
            gridSetting3.SolidStyle = geoStyle9;
            gridSetting3.Type = SuperMap.Mapping.GridType.Point;
            gridSetting3.VerticalSpacing = 0D;
            this.map1.Grid = gridSetting3;
            this.map1.IsCustomBoundsEnabled = false;
            this.map1.IsDisableDynamicEffect = false;
            this.map1.IsFillMarkerAngleFixed = true;
            this.map1.IsModified = true;
            this.map1.IsOrthographView = false;
            this.map1.IsOverlapDisplayed = false;
            this.map1.IsSymbolFillIgnored = false;
            this.map1.IsVisibleScalesEnabled = false;
            this.map1.MaxScale = 1000000000000D;
            this.map1.MaxVisibleVertex = 3600000;
            this.map1.MinScale = 0D;
            this.map1.Name = "UntitledMap";
            mapOverlapDisplayedOptions3.AllowPointOverlap = true;
            mapOverlapDisplayedOptions3.AllowPointWithTextDisplay = true;
            mapOverlapDisplayedOptions3.AllowTextAndPointOverlap = true;
            mapOverlapDisplayedOptions3.AllowTextOverlap = false;
            mapOverlapDisplayedOptions3.AllowThemeGraduatedSymbolOverlap = false;
            mapOverlapDisplayedOptions3.AllowThemeGraphOverlap = false;
            this.map1.OverlapDisplayedOptions = mapOverlapDisplayedOptions3;
            this.map1.Resources = null;
            this.map1.UseSystemDPI = true;
            this.map1.VisibleScales = new double[0];
            // 
            // mapControl1
            // 
            this.mapControl1.Action = SuperMap.UI.Action.Select2;
            this.mapControl1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.mapControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mapControl1.InteractionMode = SuperMap.UI.InteractionMode.Default;
            this.mapControl1.IsCursorCustomized = false;
            this.mapControl1.IsWaitCursorEnabled = true;
            this.mapControl1.Location = new System.Drawing.Point(0, 265);
            this.mapControl1.Margin = new System.Windows.Forms.Padding(48, 22, 48, 22);
            this.mapControl1.MarginPanEnabled = true;
            this.mapControl1.MarginPanPercent = 0.5D;
            this.mapControl1.Name = "mapControl1";
            this.mapControl1.RefreshAtTracked = true;
            this.mapControl1.RefreshInInvalidArea = false;
            this.mapControl1.RollingWheelWithoutDelay = false;
            this.mapControl1.SelectionMode = SuperMap.UI.SelectionMode.ContainInnerPoint;
            this.mapControl1.SelectionPixelTolerance = 1;
            this.mapControl1.Size = new System.Drawing.Size(608, 303);
            this.mapControl1.TabIndex = 0;
            this.mapControl1.TrackMode = SuperMap.UI.TrackMode.Edit;
            this.mapControl1.GeometrySelected += new SuperMap.UI.GeometrySelectedEventHandler(this.mapControl1_GeometrySelected);
            //this.mapControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.mapControl1_Paint);
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 121);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(608, 146);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseUp);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripButton4,
            this.toolStripButton5});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1230, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::gkfqd.Properties.Resources.MapSelect;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "选中";
            this.toolStripButton1.Click += new System.EventHandler(this.MapSelect_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::gkfqd.Properties.Resources.MapZoomIn;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "放大";
            this.toolStripButton2.Click += new System.EventHandler(this.MapZoomIn_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = global::gkfqd.Properties.Resources.MapZoomOut;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "缩小";
            this.toolStripButton3.Click += new System.EventHandler(this.MapZoomOut_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = global::gkfqd.Properties.Resources.MapPan;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "漫游";
            this.toolStripButton4.Click += new System.EventHandler(this.MapPan_Click);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = global::gkfqd.Properties.Resources.MapEntire;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton5.Text = "全幅显示";
            this.toolStripButton5.Click += new System.EventHandler(this.MapEntire_Click);
            // 
            // reportViewer1
            // 
            reportDataSource3.Name = "gk045";
            reportDataSource3.Value = this.xZDLTBBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "gkfqd.ui.gk04.gk045.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(607, 28);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(623, 540);
            this.reportViewer1.TabIndex = 3;
            // 
            // gK0451BindingSource
            // 
            this.gK0451BindingSource.DataSource = this.gK0451;
            this.gK0451BindingSource.Position = 0;
            // 
            // xZDLTBTableAdapter
            // 
            this.xZDLTBTableAdapter.ClearBeforeFill = true;
            // 
            // comboBox5
            // 
            this.comboBox5.FormattingEnabled = true;
            this.comboBox5.Items.AddRange(new object[] {
            "2014",
            "2015",
            "2016",
            "2017"});
            this.comboBox5.Location = new System.Drawing.Point(107, 91);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.Size = new System.Drawing.Size(97, 20);
            this.comboBox5.TabIndex = 55;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 54;
            this.label3.Text = "选择叠加年份";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(513, 89);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 53;
            this.button2.Text = "图层叠加";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(513, 61);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 52;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(107, 63);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(197, 21);
            this.textBox1.TabIndex = 51;
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(322, 36);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(87, 20);
            this.comboBox3.TabIndex = 50;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(217, 36);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(87, 20);
            this.comboBox2.TabIndex = 49;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // comboBox4
            // 
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Items.AddRange(new object[] {
            "全部",
            "2014",
            "2015",
            "2016",
            "2017"});
            this.comboBox4.Location = new System.Drawing.Point(495, 36);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(93, 20);
            this.comboBox4.TabIndex = 48;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 44;
            this.label2.Text = "项目名称";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "内蒙古自治区"});
            this.comboBox1.Location = new System.Drawing.Point(107, 36);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(93, 20);
            this.comboBox1.TabIndex = 47;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(425, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 45;
            this.label1.Text = "批准年份";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 39);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 12);
            this.label11.TabIndex = 46;
            this.label11.Text = "所在省、市、县";
            // 
            // gk045
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1230, 568);
            this.Controls.Add(this.comboBox5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.comboBox3);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.mapControl1);
            this.Name = "gk045";
            this.TabText = "土地复垦后地类分析";
            this.Text = "土地复垦后地类分析";
            this.Load += new System.EventHandler(this.gk045_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xZDLTBBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gK0451)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gK0451BindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SuperMap.Data.Workspace workspace1;
        private SuperMap.Mapping.Map map1;
        private SuperMap.UI.MapControl mapControl1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.BindingSource gK0451BindingSource;
        private GK045 gK0451;
        private System.Windows.Forms.BindingSource xZDLTBBindingSource;
        private GK045TableAdapters.XZDLTBTableAdapter xZDLTBTableAdapter;
        private System.Windows.Forms.ComboBox comboBox5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label11;
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManager1;
    }
}