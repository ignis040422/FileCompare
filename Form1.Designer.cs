namespace FileCompare
{
    partial class File_Compare
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            splitContainer1 = new SplitContainer();
            panelLeftList = new Panel();
            lvwLeftDir = new ListView();
            panelLeftPath = new Panel();
            btnLeftDir = new Button();
            txtLeftDir = new TextBox();
            panelLeftTop = new Panel();
            btnCopyFromLeft = new Button();
            lblAppName = new Label();
            panelRightList = new Panel();
            lvwRightDir = new ListView();
            panelRightPath = new Panel();
            btnRightDir = new Button();
            txtRightDir = new TextBox();
            panelRightTop = new Panel();
            btnCopyFromRight = new Button();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            panelLeftList.SuspendLayout();
            panelLeftPath.SuspendLayout();
            panelLeftTop.SuspendLayout();
            panelRightList.SuspendLayout();
            panelRightPath.SuspendLayout();
            panelRightTop.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.BorderStyle = BorderStyle.FixedSingle;
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(panelLeftList);
            splitContainer1.Panel1.Controls.Add(panelLeftPath);
            splitContainer1.Panel1.Controls.Add(panelLeftTop);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(panelRightList);
            splitContainer1.Panel2.Controls.Add(panelRightPath);
            splitContainer1.Panel2.Controls.Add(panelRightTop);
            splitContainer1.Size = new Size(884, 561);
            splitContainer1.SplitterDistance = 438;
            splitContainer1.TabIndex = 0;
            // 
            // panelLeftList
            // 
            panelLeftList.Controls.Add(lvwLeftDir);
            panelLeftList.Dock = DockStyle.Fill;
            panelLeftList.Location = new Point(0, 120);
            panelLeftList.Name = "panelLeftList";
            panelLeftList.Padding = new Padding(10);
            panelLeftList.Size = new Size(436, 439);
            panelLeftList.TabIndex = 2;
            // 
            // lvwLeftDir
            // 
            lvwLeftDir.Dock = DockStyle.Fill;
            lvwLeftDir.Location = new Point(10, 10);
            lvwLeftDir.Name = "lvwLeftDir";
            lvwLeftDir.Size = new Size(416, 419);
            lvwLeftDir.TabIndex = 0;
            lvwLeftDir.UseCompatibleStateImageBehavior = false;
            // 
            // panelLeftPath
            // 
            panelLeftPath.Controls.Add(btnLeftDir);
            panelLeftPath.Controls.Add(txtLeftDir);
            panelLeftPath.Dock = DockStyle.Top;
            panelLeftPath.Location = new Point(0, 70);
            panelLeftPath.Name = "panelLeftPath";
            panelLeftPath.Padding = new Padding(10, 5, 10, 5);
            panelLeftPath.Size = new Size(436, 50);
            panelLeftPath.TabIndex = 1;
            // 
            // btnLeftDir
            // 
            btnLeftDir.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLeftDir.Location = new Point(348, 10);
            btnLeftDir.Name = "btnLeftDir";
            btnLeftDir.Size = new Size(70, 27);
            btnLeftDir.TabIndex = 1;
            btnLeftDir.Text = "폴더선택";
            btnLeftDir.UseVisualStyleBackColor = true;
            btnLeftDir.Click += btnLeftDir_Click;
            // 
            // txtLeftDir
            // 
            txtLeftDir.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtLeftDir.Location = new Point(12, 11);
            txtLeftDir.Name = "txtLeftDir";
            txtLeftDir.Size = new Size(330, 23);
            txtLeftDir.TabIndex = 0;
            // 
            // panelLeftTop
            // 
            panelLeftTop.Controls.Add(btnCopyFromLeft);
            panelLeftTop.Controls.Add(lblAppName);
            panelLeftTop.Dock = DockStyle.Top;
            panelLeftTop.Location = new Point(0, 0);
            panelLeftTop.Name = "panelLeftTop";
            panelLeftTop.Padding = new Padding(10);
            panelLeftTop.Size = new Size(436, 70);
            panelLeftTop.TabIndex = 0;
            // 
            // btnCopyFromLeft
            // 
            btnCopyFromLeft.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCopyFromLeft.Location = new Point(348, 18);
            btnCopyFromLeft.Name = "btnCopyFromLeft";
            btnCopyFromLeft.Size = new Size(70, 32);
            btnCopyFromLeft.TabIndex = 1;
            btnCopyFromLeft.Text = ">>>";
            btnCopyFromLeft.UseVisualStyleBackColor = true;
            btnCopyFromLeft.Click += btnCopyFromLeft_Click;
            // 
            // lblAppName
            // 
            lblAppName.AutoSize = true;
            lblAppName.Font = new Font("맑은 고딕", 18F);
            lblAppName.ForeColor = Color.RoyalBlue;
            lblAppName.Location = new Point(12, 16);
            lblAppName.Name = "lblAppName";
            lblAppName.Size = new Size(156, 32);
            lblAppName.TabIndex = 0;
            lblAppName.Text = "File Compare";
            // 
            // panelRightList
            // 
            panelRightList.Controls.Add(lvwRightDir);
            panelRightList.Dock = DockStyle.Fill;
            panelRightList.Location = new Point(0, 120);
            panelRightList.Name = "panelRightList";
            panelRightList.Padding = new Padding(10);
            panelRightList.Size = new Size(440, 439);
            panelRightList.TabIndex = 2;
            // 
            // lvwRightDir
            // 
            lvwRightDir.Dock = DockStyle.Fill;
            lvwRightDir.Location = new Point(10, 10);
            lvwRightDir.Name = "lvwRightDir";
            lvwRightDir.Size = new Size(420, 419);
            lvwRightDir.TabIndex = 0;
            lvwRightDir.UseCompatibleStateImageBehavior = false;
            // 
            // panelRightPath
            // 
            panelRightPath.Controls.Add(btnRightDir);
            panelRightPath.Controls.Add(txtRightDir);
            panelRightPath.Dock = DockStyle.Top;
            panelRightPath.Location = new Point(0, 70);
            panelRightPath.Name = "panelRightPath";
            panelRightPath.Padding = new Padding(10, 5, 10, 5);
            panelRightPath.Size = new Size(440, 50);
            panelRightPath.TabIndex = 1;
            // 
            // btnRightDir
            // 
            btnRightDir.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnRightDir.Location = new Point(358, 10);
            btnRightDir.Name = "btnRightDir";
            btnRightDir.Size = new Size(70, 27);
            btnRightDir.TabIndex = 1;
            btnRightDir.Text = "폴더선택";
            btnRightDir.UseVisualStyleBackColor = true;
            btnRightDir.Click += btnRightDir_Click;
            // 
            // txtRightDir
            // 
            txtRightDir.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtRightDir.Location = new Point(14, 11);
            txtRightDir.Name = "txtRightDir";
            txtRightDir.Size = new Size(338, 23);
            txtRightDir.TabIndex = 0;
            // 
            // panelRightTop
            // 
            panelRightTop.Controls.Add(btnCopyFromRight);
            panelRightTop.Dock = DockStyle.Top;
            panelRightTop.Location = new Point(0, 0);
            panelRightTop.Name = "panelRightTop";
            panelRightTop.Padding = new Padding(10);
            panelRightTop.Size = new Size(440, 70);
            panelRightTop.TabIndex = 0;
            // 
            // btnCopyFromRight
            // 
            btnCopyFromRight.Location = new Point(14, 18);
            btnCopyFromRight.Name = "btnCopyFromRight";
            btnCopyFromRight.Size = new Size(70, 32);
            btnCopyFromRight.TabIndex = 0;
            btnCopyFromRight.Text = "<<<";
            btnCopyFromRight.UseVisualStyleBackColor = true;
            btnCopyFromRight.Click += btnCopyFromRight_Click;
            // 
            // File_Compare
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(884, 561);
            Controls.Add(splitContainer1);
            MinimumSize = new Size(900, 600);
            Name = "File_Compare";
            Text = "File Compare";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            panelLeftList.ResumeLayout(false);
            panelLeftPath.ResumeLayout(false);
            panelLeftPath.PerformLayout();
            panelLeftTop.ResumeLayout(false);
            panelLeftTop.PerformLayout();
            panelRightList.ResumeLayout(false);
            panelRightPath.ResumeLayout(false);
            panelRightPath.PerformLayout();
            panelRightTop.ResumeLayout(false);
            ResumeLayout(false);
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