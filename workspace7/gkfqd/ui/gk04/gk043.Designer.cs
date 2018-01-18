namespace gkfqd.ui.gk04
{
    partial class gk043
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
            SuperMap.Data.GeoStyle geoStyle1 = new SuperMap.Data.GeoStyle();
            SuperMap.Data.GeoRegion geoRegion1 = new SuperMap.Data.GeoRegion();
            SuperMap.Mapping.GridSetting gridSetting1 = new SuperMap.Mapping.GridSetting();
            SuperMap.Data.GeoStyle geoStyle2 = new SuperMap.Data.GeoStyle();
            SuperMap.Data.GeoStyle geoStyle3 = new SuperMap.Data.GeoStyle();
            SuperMap.Mapping.MapOverlapDisplayedOptions mapOverlapDisplayedOptions1 = new SuperMap.Mapping.MapOverlapDisplayedOptions();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.vXJQKBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gk0431BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gk0431 = new gkfqd.gk043();
            this.map1 = new SuperMap.Mapping.Map(this.components);
            this.mapControl1 = new SuperMap.UI.MapControl();
            this.workspace1 = new SuperMap.Data.Workspace(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.v_XJQKTableAdapter = new gkfqd.gk043TableAdapters.V_XJQKTableAdapter();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
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
            ((System.ComponentModel.ISupportInitialize)(this.vXJQKBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gk0431BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gk0431)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // vXJQKBindingSource
            // 
            this.vXJQKBindingSource.DataMember = "V_XJQK";
            this.vXJQKBindingSource.DataSource = this.gk0431BindingSource;
            // 
            // gk0431BindingSource
            // 
            this.gk0431BindingSource.DataSource = this.gk0431;
            this.gk0431BindingSource.Position = 0;
            // 
            // gk0431
            // 
            this.gk0431.DataSetName = "gk043";
            this.gk0431.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // map1
            // 
            geoStyle1.FillBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            geoStyle1.FillBackOpaque = true;
            geoStyle1.FillForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            geoStyle1.FillGradientAngle = 0D;
            geoStyle1.FillGradientMode = SuperMap.Data.FillGradientMode.None;
            geoStyle1.FillGradientOffsetRatioX = 0;
            geoStyle1.FillGradientOffsetRatioY = 0;
            geoStyle1.FillOpaqueRate = 100;
            geoStyle1.FillSymbolID = 0;
            geoStyle1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            geoStyle1.LineSymbolID = 0;
            geoStyle1.LineWidth = 0.1D;
            geoStyle1.MarkerAngle = 0D;
            geoStyle1.MarkerSymbolID = 0;
            this.map1.BackgroundStyle = geoStyle1;
            geoRegion1.ID = 0;
            geoRegion1.Style = null;
            this.map1.ClipRegion = geoRegion1;
            this.map1.DPI = 96D;
            geoStyle2.FillBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            geoStyle2.FillBackOpaque = true;
            geoStyle2.FillForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(235)))), ((int)(((byte)(255)))));
            geoStyle2.FillGradientAngle = 0D;
            geoStyle2.FillGradientMode = SuperMap.Data.FillGradientMode.None;
            geoStyle2.FillGradientOffsetRatioX = 0;
            geoStyle2.FillGradientOffsetRatioY = 0;
            geoStyle2.FillOpaqueRate = 100;
            geoStyle2.FillSymbolID = 0;
            geoStyle2.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            geoStyle2.LineSymbolID = 2;
            geoStyle2.LineWidth = 0.1D;
            geoStyle2.MarkerAngle = 0D;
            geoStyle2.MarkerSymbolID = 0;
            gridSetting1.DashStyle = geoStyle2;
            gridSetting1.HorizontalSpacing = 0D;
            gridSetting1.IsSizeFixed = false;
            gridSetting1.IsSnapable = false;
            gridSetting1.IsVisible = false;
            geoStyle3.FillBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            geoStyle3.FillBackOpaque = true;
            geoStyle3.FillForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(235)))), ((int)(((byte)(255)))));
            geoStyle3.FillGradientAngle = 0D;
            geoStyle3.FillGradientMode = SuperMap.Data.FillGradientMode.None;
            geoStyle3.FillGradientOffsetRatioX = 0;
            geoStyle3.FillGradientOffsetRatioY = 0;
            geoStyle3.FillOpaqueRate = 100;
            geoStyle3.FillSymbolID = 0;
            geoStyle3.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            geoStyle3.LineSymbolID = 0;
            geoStyle3.LineWidth = 0.1D;
            geoStyle3.MarkerAngle = 0D;
            geoStyle3.MarkerSymbolID = 0;
            gridSetting1.SolidStyle = geoStyle3;
            gridSetting1.Type = SuperMap.Mapping.GridType.Point;
            gridSetting1.VerticalSpacing = 0D;
            this.map1.Grid = gridSetting1;
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
            mapOverlapDisplayedOptions1.AllowPointOverlap = true;
            mapOverlapDisplayedOptions1.AllowPointWithTextDisplay = true;
            mapOverlapDisplayedOptions1.AllowTextAndPointOverlap = true;
            mapOverlapDisplayedOptions1.AllowTextOverlap = false;
            mapOverlapDisplayedOptions1.AllowThemeGraduatedSymbolOverlap = false;
            mapOverlapDisplayedOptions1.AllowThemeGraphOverlap = false;
            this.map1.OverlapDisplayedOptions = mapOverlapDisplayedOptions1;
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
            this.mapControl1.Location = new System.Drawing.Point(0, 264);
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
            this.mapControl1.GeometrySelected += new SuperMap.UI.GeometrySelectedEventHandler(this.m_mapControl_GeometrySelected);
            //this.mapControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.m_mapControl_Paint);
            // 
            // workspace1
            // 
            this.workspace1.Caption = "UntitledWorkspace";
            this.workspace1.Description = "";
            // 
            // reportViewer1
            // 
            reportDataSource1.Name = "gk043";
            reportDataSource1.Value = this.vXJQKBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "gkfqd.ui.gk04.gk043.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(607, 28);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(623, 539);
            this.reportViewer1.TabIndex = 1;
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
            this.toolStrip1.TabIndex = 3;
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
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = global::gkfqd.Properties.Resources.MapZoomIn;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "放大";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = global::gkfqd.Properties.Resources.MapZoomOut;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "缩小";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = global::gkfqd.Properties.Resources.MapPan;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "漫游";
            this.toolStripButton4.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = global::gkfqd.Properties.Resources.MapEntire;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton5.Text = "全幅显示";
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // v_XJQKTableAdapter
            // 
            this.v_XJQKTableAdapter.ClearBeforeFill = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 119);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(608, 146);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridView_MouseUp);
            // 
            // comboBox5
            // 
            this.comboBox5.FormattingEnabled = true;
            this.comboBox5.Items.AddRange(new object[] {
            "2014",
            "2015",
            "2016",
            "2017"});
            this.comboBox5.Location = new System.Drawing.Point(107, 89);
            this.comboBox5.Name = "comboBox5";
            this.comboBox5.Size = new System.Drawing.Size(97, 20);
            this.comboBox5.TabIndex = 55;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 92);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 54;
            this.label3.Text = "选择叠加年份";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(513, 87);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 53;
            this.button2.Text = "图层叠加";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(513, 59);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 52;
            this.button1.Text = "查询";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(107, 61);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(197, 21);
            this.textBox1.TabIndex = 51;
            // 
            // comboBox3
            // 
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Location = new System.Drawing.Point(322, 34);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(87, 20);
            this.comboBox3.TabIndex = 50;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(217, 34);
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
            this.comboBox4.Location = new System.Drawing.Point(495, 34);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(93, 20);
            this.comboBox4.TabIndex = 48;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 64);
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
            this.comboBox1.Location = new System.Drawing.Point(107, 34);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(93, 20);
            this.comboBox1.TabIndex = 47;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(425, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 45;
            this.label1.Text = "批准年份";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 37);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 12);
            this.label11.TabIndex = 46;
            this.label11.Text = "所在省、市、县";
            // 
            // gk043
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
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.reportViewer1);
            this.Controls.Add(this.mapControl1);
            this.Name = "gk043";
            this.TabText = "土地供应（新建区）";
            this.Text = "土地供应（新建区）";
            this.Load += new System.EventHandler(this.gk043_Load);
            ((System.ComponentModel.ISupportInitialize)(this.vXJQKBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gk0431BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gk0431)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SuperMap.Mapping.Map map1;
        private SuperMap.UI.MapControl mapControl1;
        private SuperMap.Data.Workspace workspace1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.BindingSource gk0431BindingSource;
        private gkfqd.gk043 gk0431;
        private System.Windows.Forms.BindingSource vXJQKBindingSource;
        private gk043TableAdapters.V_XJQKTableAdapter v_XJQKTableAdapter;
        private System.Windows.Forms.DataGridView dataGridView1;
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