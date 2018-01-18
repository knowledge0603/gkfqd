namespace gkfqd.ui.gk06
{
    partial class gk065
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
            this.mapLayoutControl1 = new SuperMap.UI.MapLayoutControl();
            this.workspace1 = new SuperMap.Data.Workspace(this.components);
            this.SuspendLayout();
            // 
            // mapLayoutControl1
            // 
            this.mapLayoutControl1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.mapLayoutControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mapLayoutControl1.IsGridSnapable = false;
            this.mapLayoutControl1.IsHorizontalScrollbarVisible = true;
            this.mapLayoutControl1.IsVerticalScrollbarVisible = true;
            this.mapLayoutControl1.LayoutAction = SuperMap.UI.Action.Select2;
            this.mapLayoutControl1.Location = new System.Drawing.Point(12, 12);
            this.mapLayoutControl1.MapAction = SuperMap.UI.Action.Null;
            this.mapLayoutControl1.Name = "mapLayoutControl1";
            this.mapLayoutControl1.RefreshAtTracked = true;
            this.mapLayoutControl1.RefreshInInvalidArea = false;
            this.mapLayoutControl1.Size = new System.Drawing.Size(607, 350);
            this.mapLayoutControl1.TabIndex = 3;
            this.mapLayoutControl1.TrackMode = SuperMap.UI.TrackMode.Edit;
            // 
            // workspace1
            // 
            this.workspace1.Caption = "UntitledWorkspace";
            this.workspace1.Description = "";
            // 
            // gk065
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(639, 376);
            this.Controls.Add(this.mapLayoutControl1);
            this.Name = "gk065";
            this.TabText = "业务调查底图";
            this.Text = "业务调查底图";
            this.ResumeLayout(false);

        }

        #endregion

        private SuperMap.UI.MapLayoutControl mapLayoutControl1;
        private SuperMap.Data.Workspace workspace1;
    }
}