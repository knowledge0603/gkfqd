namespace gkfqd.ui.gk06
{
    partial class gk062
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
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
            this.mapLayoutControl1.Location = new System.Drawing.Point(12, 32);
            this.mapLayoutControl1.MapAction = SuperMap.UI.Action.Null;
            this.mapLayoutControl1.Name = "mapLayoutControl1";
            this.mapLayoutControl1.RefreshAtTracked = true;
            this.mapLayoutControl1.RefreshInInvalidArea = false;
            this.mapLayoutControl1.Size = new System.Drawing.Size(713, 408);
            this.mapLayoutControl1.TabIndex = 1;
            this.mapLayoutControl1.TrackMode = SuperMap.UI.TrackMode.Edit;
            // 
            // workspace1
            // 
            this.workspace1.Caption = "UntitledWorkspace";
            this.workspace1.Description = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "平移";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(93, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "锁定地图";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // gk062
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 464);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.mapLayoutControl1);
            this.Name = "gk062";
            this.TabText = "遥感影像图件";
            this.Text = "遥感影像图件";
            this.ResumeLayout(false);

        }

        #endregion

        private SuperMap.UI.MapLayoutControl mapLayoutControl1;
        private SuperMap.Data.Workspace workspace1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;

    }
}