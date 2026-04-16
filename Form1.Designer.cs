namespace FileCompare
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panelLeftTop = new System.Windows.Forms.Panel();
            this.btnCopyFromLeft = new System.Windows.Forms.Button();
            this.lblAppName = new System.Windows.Forms.Label();
            this.panelLeftPath = new System.Windows.Forms.Panel();
            this.btnLeftDir = new System.Windows.Forms.Button();
            this.txtLeftDir = new System.Windows.Forms.TextBox();
            this.panelLeftList = new System.Windows.Forms.Panel();
            this.lvwLeftDir = new System.Windows.Forms.ListView();
            this.panelRightTop = new System.Windows.Forms.Panel();
            this.btnCopyFromRight = new System.Windows.Forms.Button();
            this.panelRightPath = new System.Windows.Forms.Panel();
            this.btnRightDir = new System.Windows.Forms.Button();
            this.txtRightDir = new System.Windows.Forms.TextBox();
            this.panelRightList = new System.Windows.Forms.Panel();
            this.lvwRightDir = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panelLeftTop.SuspendLayout();
            this.panelLeftPath.SuspendLayout();
            this.panelLeftList.SuspendLayout();
            this.panelRightTop.SuspendLayout();
            this.panelRightPath.SuspendLayout();
            this.panelRightList.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panelLeftList);
            this.splitContainer1.Panel1.Controls.Add(this.panelLeftPath);
            this.splitContainer1.Panel1.Controls.Add(this.panelLeftTop);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panelRightList);
            this.splitContainer1.Panel2.Controls.Add(this.panelRightPath);
            this.splitContainer1.Panel2.Controls.Add(this.panelRightTop);
            this.splitContainer1.Size = new System.Drawing.Size(984, 611);
            this.splitContainer1.SplitterDistance = 489;
            this.splitContainer1.TabIndex = 0;
            // 
            // panelLeftTop
            // 
            this.panelLeftTop.Controls.Add(this.btnCopyFromLeft);
            this.panelLeftTop.Controls.Add(this.lblAppName);
            this.panelLeftTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLeftTop.Location = new System.Drawing.Point(0, 0);
            this.panelLeftTop.Name = "panelLeftTop";
            this.panelLeftTop.Padding = new System.Windows.Forms.Padding(10);
            this.panelLeftTop.Size = new System.Drawing.Size(487, 70);
            this.panelLeftTop.TabIndex = 0;
            // 
            // btnCopyFromLeft
            // 
            this.btnCopyFromLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyFromLeft.Location = new System.Drawing.Point(400, 18);
            this.btnCopyFromLeft.Name = "btnCopyFromLeft";
            this.btnCopyFromLeft.Size = new System.Drawing.Size(70, 32);
            this.btnCopyFromLeft.TabIndex = 1;
            this.btnCopyFromLeft.Text = ">>>";
            this.btnCopyFromLeft.UseVisualStyleBackColor = true;
            this.btnCopyFromLeft.Click += new System.EventHandler(this.btnCopyFromLeft_Click);
            // 
            // lblAppName
            // 
            this.lblAppName.AutoSize = true;
            this.lblAppName.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblAppName.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblAppName.Location = new System.Drawing.Point(12, 16);
            this.lblAppName.Name = "lblAppName";
            this.lblAppName.Size = new System.Drawing.Size(154, 32);
            this.lblAppName.TabIndex = 0;
            this.lblAppName.Text = "File Compare";
            // 
            // panelLeftPath
            // 
            this.panelLeftPath.Controls.Add(this.btnLeftDir);
            this.panelLeftPath.Controls.Add(this.txtLeftDir);
            this.panelLeftPath.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLeftPath.Location = new System.Drawing.Point(0, 70);
            this.panelLeftPath.Name = "panelLeftPath";
            this.panelLeftPath.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.panelLeftPath.Size = new System.Drawing.Size(487, 50);
            this.panelLeftPath.TabIndex = 1;
            // 
            // btnLeftDir
            // 
            this.btnLeftDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLeftDir.Location = new System.Drawing.Point(400, 10);
            this.btnLeftDir.Name = "btnLeftDir";
            this.btnLeftDir.Size = new System.Drawing.Size(70, 27);
            this.btnLeftDir.TabIndex = 1;
            this.btnLeftDir.Text = "폴더선택";
            this.btnLeftDir.UseVisualStyleBackColor = true;
            this.btnLeftDir.Click += new System.EventHandler(this.btnLeftDir_Click);
            // 
            // txtLeftDir
            // 
            this.txtLeftDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLeftDir.Location = new System.Drawing.Point(12, 11);
            this.txtLeftDir.Name = "txtLeftDir";
            this.txtLeftDir.Size = new System.Drawing.Size(382, 23);
            this.txtLeftDir.TabIndex = 0;
            // 
            // panelLeftList
            // 
            this.panelLeftList.Controls.Add(this.lvwLeftDir);
            this.panelLeftList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLeftList.Location = new System.Drawing.Point(0, 120);
            this.panelLeftList.Name = "panelLeftList";
            this.panelLeftList.Padding = new System.Windows.Forms.Padding(10);
            this.panelLeftList.Size = new System.Drawing.Size(487, 489);
            this.panelLeftList.TabIndex = 2;
            // 
            // lvwLeftDir
            // 
            this.lvwLeftDir.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwLeftDir.HideSelection = false;
            this.lvwLeftDir.Location = new System.Drawing.Point(10, 10);
            this.lvwLeftDir.MultiSelect = false;
            this.lvwLeftDir.Name = "lvwLeftDir";
            this.lvwLeftDir.Size = new System.Drawing.Size(467, 469);
            this.lvwLeftDir.TabIndex = 0;
            this.lvwLeftDir.UseCompatibleStateImageBehavior = false;
            // 
            // panelRightTop
            // 
            this.panelRightTop.Controls.Add(this.btnCopyFromRight);
            this.panelRightTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelRightTop.Location = new System.Drawing.Point(0, 0);
            this.panelRightTop.Name = "panelRightTop";
            this.panelRightTop.Padding = new System.Windows.Forms.Padding(10);
            this.panelRightTop.Size = new System.Drawing.Size(489, 70);
            this.panelRightTop.TabIndex = 0;
            // 
            // btnCopyFromRight
            // 
            this.btnCopyFromRight.Location = new System.Drawing.Point(14, 18);
            this.btnCopyFromRight.Name = "btnCopyFromRight";
            this.btnCopyFromRight.Size = new System.Drawing.Size(70, 32);
            this.btnCopyFromRight.TabIndex = 0;
            this.btnCopyFromRight.Text = "<<<";
            this.btnCopyFromRight.UseVisualStyleBackColor = true;
            this.btnCopyFromRight.Click += new System.EventHandler(this.btnCopyFromRight_Click);
            // 
            // panelRightPath
            // 
            this.panelRightPath.Controls.Add(this.btnRightDir);
            this.panelRightPath.Controls.Add(this.txtRightDir);
            this.panelRightPath.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelRightPath.Location = new System.Drawing.Point(0, 70);
            this.panelRightPath.Name = "panelRightPath";
            this.panelRightPath.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.panelRightPath.Size = new System.Drawing.Size(489, 50);
            this.panelRightPath.TabIndex = 1;
            // 
            // btnRightDir
            // 
            this.btnRightDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRightDir.Location = new System.Drawing.Point(406, 10);
            this.btnRightDir.Name = "btnRightDir";
            this.btnRightDir.Size = new System.Drawing.Size(70, 27);
            this.btnRightDir.TabIndex = 1;
            this.btnRightDir.Text = "폴더선택";
            this.btnRightDir.UseVisualStyleBackColor = true;
            this.btnRightDir.Click += new System.EventHandler(this.btnRightDir_Click);
            // 
            // txtRightDir
            // 
            this.txtRightDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRightDir.Location = new System.Drawing.Point(14, 11);
            this.txtRightDir.Name = "txtRightDir";
            this.txtRightDir.Size = new System.Drawing.Size(386, 23);
            this.txtRightDir.TabIndex = 0;
            // 
            // panelRightList
            // 
            this.panelRightList.Controls.Add(this.lvwRightDir);
            this.panelRightList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRightList.Location = new System.Drawing.Point(0, 120);
            this.panelRightList.Name = "panelRightList";
            this.panelRightList.Padding = new System.Windows.Forms.Padding(10);
            this.panelRightList.Size = new System.Drawing.Size(489, 489);
            this.panelRightList.TabIndex = 2;
            // 
            // lvwRightDir
            // 
            this.lvwRightDir.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwRightDir.HideSelection = false;
            this.lvwRightDir.Location = new System.Drawing.Point(10, 10);
            this.lvwRightDir.MultiSelect = false;
            this.lvwRightDir.Name = "lvwRightDir";
            this.lvwRightDir.Size = new System.Drawing.Size(469, 469);
            this.lvwRightDir.TabIndex = 0;
            this.lvwRightDir.UseCompatibleStateImageBehavior = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 611);
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(1000, 650);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File Compare";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panelLeftTop.ResumeLayout(false);
            this.panelLeftTop.PerformLayout();
            this.panelLeftPath.ResumeLayout(false);
            this.panelLeftPath.PerformLayout();
            this.panelLeftList.ResumeLayout(false);
            this.panelRightTop.ResumeLayout(false);
            this.panelRightPath.ResumeLayout(false);
            this.panelRightPath.PerformLayout();
            this.panelRightList.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panelLeftTop;
        private System.Windows.Forms.Panel panelLeftPath;
        private System.Windows.Forms.Panel panelLeftList;
        private System.Windows.Forms.Panel panelRightTop;
        private System.Windows.Forms.Panel panelRightPath;
        private System.Windows.Forms.Panel panelRightList;
        private System.Windows.Forms.Label lblAppName;
        private System.Windows.Forms.TextBox txtLeftDir;
        private System.Windows.Forms.TextBox txtRightDir;
        private System.Windows.Forms.Button btnLeftDir;
        private System.Windows.Forms.Button btnRightDir;
        private System.Windows.Forms.Button btnCopyFromLeft;
        private System.Windows.Forms.Button btnCopyFromRight;
        private System.Windows.Forms.ListView lvwLeftDir;
        private System.Windows.Forms.ListView lvwRightDir;
    }
}