using System;
using System;
using System.Windows.Forms;

namespace FileCompare
{
    public partial class File_Compare : Form
    {
        public File_Compare()
        {
            InitializeComponent();
            InitUI();
        }

        private void InitUI()
        {
            lblAppName.Text = "File Compare";

            txtLeftDir.ReadOnly = true;
            txtRightDir.ReadOnly = true;

            InitListView(lvwLeftDir);
            InitListView(lvwRightDir);
        }

        private void InitListView(ListView lv)
        {
            lv.View = View.Details;
            lv.FullRowSelect = true;
            lv.GridLines = true;

            lv.Columns.Clear();
            lv.Columns.Add("이름", 220);
            lv.Columns.Add("크기", 100);
            lv.Columns.Add("수정일", 150);
        }

        private void btnLeftDir_Click(object sender, EventArgs e)
        {
            MessageBox.Show("과제2에서 왼쪽 폴더 선택 기능을 구현합니다.", "안내");
        }

        private void btnRightDir_Click(object sender, EventArgs e)
        {
            MessageBox.Show("과제2에서 오른쪽 폴더 선택 기능을 구현합니다.", "안내");
        }

        private void btnCopyFromLeft_Click(object sender, EventArgs e)
        {
            MessageBox.Show("과제3에서 왼쪽 → 오른쪽 복사 기능을 구현합니다.", "안내");
        }

        private void btnCopyFromRight_Click(object sender, EventArgs e)
        {
            MessageBox.Show("과제3에서 오른쪽 → 왼쪽 복사 기능을 구현합니다.", "안내");
        }
    }
}