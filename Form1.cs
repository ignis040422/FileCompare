using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FileCompare
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // 프로그램 시작 시 ListView 기본 설정
            InitListView();

            // 처음 실행 시 아무것도 선택되지 않은 상태로 시작
            InitDefaultState();
        }

        /// <summary>
        /// 프로그램 시작 시 기본 상태를 설정하는 함수
        /// 경로는 비워 두고, 파일 목록도 비워 두며, 선택된 항목도 없게 만든다.
        /// </summary>
        private void InitDefaultState()
        {
            txtLeftDir.Text = "";
            txtRightDir.Text = "";

            lvwLeftDir.Items.Clear();
            lvwRightDir.Items.Clear();

            lvwLeftDir.SelectedItems.Clear();
            lvwRightDir.SelectedItems.Clear();

            lblAppName.Focus();
        }

        /// <summary>
        /// 왼쪽/오른쪽 ListView를 표 형태로 초기화하는 함수
        /// </summary>
        private void InitListView()
        {
            // 왼쪽 ListView 설정
            lvwLeftDir.View = View.Details;
            lvwLeftDir.FullRowSelect = true;
            lvwLeftDir.GridLines = true;
            lvwLeftDir.HideSelection = false;
            lvwLeftDir.MultiSelect = false;
            lvwLeftDir.Columns.Clear();
            lvwLeftDir.Columns.Add("이름", 300);
            lvwLeftDir.Columns.Add("크기", 100);
            lvwLeftDir.Columns.Add("수정일", 160);

            // 오른쪽 ListView 설정
            lvwRightDir.View = View.Details;
            lvwRightDir.FullRowSelect = true;
            lvwRightDir.GridLines = true;
            lvwRightDir.HideSelection = false;
            lvwRightDir.MultiSelect = false;
            lvwRightDir.Columns.Clear();
            lvwRightDir.Columns.Add("이름", 300);
            lvwRightDir.Columns.Add("크기", 100);
            lvwRightDir.Columns.Add("수정일", 160);
        }

        /// <summary>
        /// 왼쪽 폴더 선택 버튼 클릭 이벤트
        /// </summary>
        private void btnLeftDir_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "폴더를 선택하세요.";

                if (!string.IsNullOrWhiteSpace(txtLeftDir.Text) &&
                    Directory.Exists(txtLeftDir.Text))
                {
                    dlg.SelectedPath = txtLeftDir.Text;
                }

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtLeftDir.Text = dlg.SelectedPath;
                    RefreshAllViews();
                }
            }
        }

        /// <summary>
        /// 오른쪽 폴더 선택 버튼 클릭 이벤트
        /// </summary>
        private void btnRightDir_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "폴더를 선택하세요.";

                if (!string.IsNullOrWhiteSpace(txtRightDir.Text) &&
                    Directory.Exists(txtRightDir.Text))
                {
                    dlg.SelectedPath = txtRightDir.Text;
                }

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtRightDir.Text = dlg.SelectedPath;
                    RefreshAllViews();
                }
            }
        }

        /// <summary>
        /// 양쪽 목록을 다시 읽고 비교 결과까지 갱신하는 함수
        /// </summary>
        private void RefreshAllViews()
        {
            if (Directory.Exists(txtLeftDir.Text))
            {
                PopulateListView(lvwLeftDir, txtLeftDir.Text);
            }
            else
            {
                lvwLeftDir.Items.Clear();
            }

            if (Directory.Exists(txtRightDir.Text))
            {
                PopulateListView(lvwRightDir, txtRightDir.Text);
            }
            else
            {
                lvwRightDir.Items.Clear();
            }

            CompareFiles();

            // 새로고침 후 자동 선택 제거
            lvwLeftDir.SelectedItems.Clear();
            lvwRightDir.SelectedItems.Clear();
        }

        /// <summary>
        /// 선택된 폴더의 하위 폴더 및 파일 목록을 ListView에 표시하는 함수
        /// </summary>
        private void PopulateListView(ListView lv, string folderPath)
        {
            lv.BeginUpdate();
            lv.Items.Clear();

            try
            {
                // 폴더(디렉터리) 먼저 추가
                var dirs = Directory.EnumerateDirectories(folderPath)
                    .Select(p => new DirectoryInfo(p))
                    .OrderBy(d => d.Name);

                foreach (var d in dirs)
                {
                    var item = new ListViewItem(d.Name);
                    item.SubItems.Add("<DIR>");
                    item.SubItems.Add(d.LastWriteTime.ToString("g"));
                    lv.Items.Add(item);
                }

                // 파일 추가
                var files = Directory.EnumerateFiles(folderPath)
                    .Select(p => new FileInfo(p))
                    .OrderBy(f => f.Name);

                foreach (var f in files)
                {
                    var item = new ListViewItem(f.Name);
                    item.SubItems.Add(f.Length.ToString("N0") + " 바이트");
                    item.SubItems.Add(f.LastWriteTime.ToString("g"));
                    lv.Items.Add(item);
                }

                for (int i = 0; i < lv.Columns.Count; i++)
                {
                    lv.AutoResizeColumn(i, ColumnHeaderAutoResizeStyle.ColumnContent);
                }

                lv.SelectedItems.Clear();
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show(this, "폴더를 찾을 수 없습니다.", "오류",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (IOException ex)
            {
                MessageBox.Show(this, "입출력 오류: " + ex.Message, "오류",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                lv.EndUpdate();
            }
        }

        /// <summary>
        /// 양쪽 폴더의 파일을 이름과 수정시간 기준으로 비교하여 색상을 표시하는 함수
        /// </summary>
        private void CompareFiles()
        {
            if (string.IsNullOrWhiteSpace(txtLeftDir.Text) ||
                string.IsNullOrWhiteSpace(txtRightDir.Text))
                return;

            if (!Directory.Exists(txtLeftDir.Text) || !Directory.Exists(txtRightDir.Text))
                return;

            foreach (ListViewItem item in lvwLeftDir.Items)
            {
                item.ForeColor = Color.Black;
            }

            foreach (ListViewItem item in lvwRightDir.Items)
            {
                item.ForeColor = Color.Black;
            }

            var leftFiles = Directory.EnumerateFiles(txtLeftDir.Text)
                .Select(p => new FileInfo(p))
                .ToDictionary(f => f.Name, f => f);

            var rightFiles = Directory.EnumerateFiles(txtRightDir.Text)
                .Select(p => new FileInfo(p))
                .ToDictionary(f => f.Name, f => f);

            foreach (ListViewItem item in lvwLeftDir.Items)
            {
                if (item.SubItems.Count > 1 && item.SubItems[1].Text == "<DIR>")
                    continue;

                string fileName = item.Text;

                if (rightFiles.ContainsKey(fileName))
                {
                    var lf = leftFiles[fileName];
                    var rf = rightFiles[fileName];

                    if (lf.LastWriteTime == rf.LastWriteTime)
                    {
                        item.ForeColor = Color.Black;
                    }
                    else if (lf.LastWriteTime > rf.LastWriteTime)
                    {
                        item.ForeColor = Color.Red;
                    }
                    else
                    {
                        item.ForeColor = Color.Gray;
                    }
                }
                else
                {
                    item.ForeColor = Color.Purple;
                }
            }

            foreach (ListViewItem item in lvwRightDir.Items)
            {
                if (item.SubItems.Count > 1 && item.SubItems[1].Text == "<DIR>")
                    continue;

                string fileName = item.Text;

                if (leftFiles.ContainsKey(fileName))
                {
                    var rf = rightFiles[fileName];
                    var lf = leftFiles[fileName];

                    if (rf.LastWriteTime == lf.LastWriteTime)
                    {
                        item.ForeColor = Color.Black;
                    }
                    else if (rf.LastWriteTime > lf.LastWriteTime)
                    {
                        item.ForeColor = Color.Red;
                    }
                    else
                    {
                        item.ForeColor = Color.Gray;
                    }
                }
                else
                {
                    item.ForeColor = Color.Purple;
                }
            }

            lvwLeftDir.SelectedItems.Clear();
            lvwRightDir.SelectedItems.Clear();
        }

        /// <summary>
        /// 왼쪽에서 선택한 파일을 오른쪽 폴더로 복사하는 중간 단계 기능
        /// 아직 덮어쓰기 확인창은 구현하지 않음
        /// </summary>
        private void btnCopyFromLeft_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(txtLeftDir.Text) || !Directory.Exists(txtRightDir.Text))
            {
                MessageBox.Show("양쪽 폴더를 먼저 선택하세요.", "안내");
                return;
            }

            if (lvwLeftDir.SelectedItems.Count == 0)
            {
                MessageBox.Show("왼쪽 목록에서 복사할 파일을 선택하세요.", "안내");
                return;
            }

            var selectedItem = lvwLeftDir.SelectedItems[0];

            // 폴더는 아직 복사하지 않음
            if (selectedItem.SubItems[1].Text == "<DIR>")
            {
                MessageBox.Show("현재 단계에서는 폴더가 아닌 파일만 복사할 수 있습니다.", "안내");
                return;
            }

            string fileName = selectedItem.Text;
            string srcPath = Path.Combine(txtLeftDir.Text, fileName);
            string destPath = Path.Combine(txtRightDir.Text, fileName);

            try
            {
                // 중간 단계이므로 단순 복사만 수행
                // 기존 파일이 있으면 덮어쓰기(true)
                File.Copy(srcPath, destPath, true);

                MessageBox.Show("파일 복사가 완료되었습니다.", "안내");

                RefreshAllViews();
            }
            catch (IOException ex)
            {
                MessageBox.Show("복사 중 오류: " + ex.Message, "오류",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 오른쪽에서 선택한 파일을 왼쪽 폴더로 복사하는 중간 단계 기능
        /// 아직 덮어쓰기 확인창은 구현하지 않음
        /// </summary>
        private void btnCopyFromRight_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(txtLeftDir.Text) || !Directory.Exists(txtRightDir.Text))
            {
                MessageBox.Show("양쪽 폴더를 먼저 선택하세요.", "안내");
                return;
            }

            if (lvwRightDir.SelectedItems.Count == 0)
            {
                MessageBox.Show("오른쪽 목록에서 복사할 파일을 선택하세요.", "안내");
                return;
            }

            var selectedItem = lvwRightDir.SelectedItems[0];

            // 폴더는 아직 복사하지 않음
            if (selectedItem.SubItems[1].Text == "<DIR>")
            {
                MessageBox.Show("현재 단계에서는 폴더가 아닌 파일만 복사할 수 있습니다.", "안내");
                return;
            }

            string fileName = selectedItem.Text;
            string srcPath = Path.Combine(txtRightDir.Text, fileName);
            string destPath = Path.Combine(txtLeftDir.Text, fileName);

            try
            {
                // 중간 단계이므로 단순 복사만 수행
                File.Copy(srcPath, destPath, true);

                MessageBox.Show("파일 복사가 완료되었습니다.", "안내");

                RefreshAllViews();
            }
            catch (IOException ex)
            {
                MessageBox.Show("복사 중 오류: " + ex.Message, "오류",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}