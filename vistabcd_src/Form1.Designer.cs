namespace BCDEditor
{
    partial class Maiin
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lstOS = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSetTimeout = new System.Windows.Forms.Button();
            this.txtTimeOut = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnOSSet = new System.Windows.Forms.Button();
            this.lblOSGUID = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.chkDefault = new System.Windows.Forms.CheckBox();
            this.txtDisplayName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lstOS);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(256, 261);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "OS List";
            // 
            // lstOS
            // 
            this.lstOS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstOS.FormattingEnabled = true;
            this.lstOS.Location = new System.Drawing.Point(3, 16);
            this.lstOS.Name = "lstOS";
            this.lstOS.Size = new System.Drawing.Size(250, 238);
            this.lstOS.TabIndex = 0;
            this.lstOS.SelectedIndexChanged += new System.EventHandler(this.lstOS_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.btnSetTimeout);
            this.groupBox2.Controls.Add(this.txtTimeOut);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(262, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(418, 83);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Common Settings";
            // 
            // btnSetTimeout
            // 
            this.btnSetTimeout.Location = new System.Drawing.Point(101, 46);
            this.btnSetTimeout.Name = "btnSetTimeout";
            this.btnSetTimeout.Size = new System.Drawing.Size(75, 23);
            this.btnSetTimeout.TabIndex = 2;
            this.btnSetTimeout.Text = "Set";
            this.btnSetTimeout.UseVisualStyleBackColor = true;
            this.btnSetTimeout.Click += new System.EventHandler(this.btnSetTimeout_Click);
            // 
            // txtTimeOut
            // 
            this.txtTimeOut.Location = new System.Drawing.Point(101, 20);
            this.txtTimeOut.Name = "txtTimeOut";
            this.txtTimeOut.Size = new System.Drawing.Size(311, 20);
            this.txtTimeOut.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Timeout";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.btnImport);
            this.groupBox3.Controls.Add(this.btnExport);
            this.groupBox3.Controls.Add(this.btnOSSet);
            this.groupBox3.Controls.Add(this.lblOSGUID);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.chkDefault);
            this.groupBox3.Controls.Add(this.txtDisplayName);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Location = new System.Drawing.Point(262, 89);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(418, 172);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "OS Settings";
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(293, 141);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 9;
            this.btnImport.Text = "Import";
            this.btnImport.UseVisualStyleBackColor = true;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(196, 141);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 8;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            // 
            // btnOSSet
            // 
            this.btnOSSet.Location = new System.Drawing.Point(101, 141);
            this.btnOSSet.Name = "btnOSSet";
            this.btnOSSet.Size = new System.Drawing.Size(75, 23);
            this.btnOSSet.TabIndex = 7;
            this.btnOSSet.Text = "Set";
            this.btnOSSet.UseVisualStyleBackColor = true;
            this.btnOSSet.Click += new System.EventHandler(this.btnOSSet_Click);
            // 
            // lblOSGUID
            // 
            this.lblOSGUID.AutoSize = true;
            this.lblOSGUID.Location = new System.Drawing.Point(98, 28);
            this.lblOSGUID.Name = "lblOSGUID";
            this.lblOSGUID.Size = new System.Drawing.Size(111, 13);
            this.lblOSGUID.TabIndex = 6;
            this.lblOSGUID.Text = "OS GUID display here";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "OS GUID";
            // 
            // chkDefault
            // 
            this.chkDefault.AutoSize = true;
            this.chkDefault.Enabled = false;
            this.chkDefault.Location = new System.Drawing.Point(101, 99);
            this.chkDefault.Name = "chkDefault";
            this.chkDefault.Size = new System.Drawing.Size(78, 17);
            this.chkDefault.TabIndex = 4;
            this.chkDefault.Text = "Default OS";
            this.chkDefault.UseVisualStyleBackColor = true;
            // 
            // txtDisplayName
            // 
            this.txtDisplayName.Location = new System.Drawing.Point(101, 60);
            this.txtDisplayName.Name = "txtDisplayName";
            this.txtDisplayName.Size = new System.Drawing.Size(311, 20);
            this.txtDisplayName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Display name";
            // 
            // Maiin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 261);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Maiin";
            this.Text = "BCD Editor";
            this.Load += new System.EventHandler(this.Maiin_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox lstOS;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtTimeOut;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSetTimeout;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtDisplayName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkDefault;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblOSGUID;
        private System.Windows.Forms.Button btnOSSet;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnExport;
    }
}

