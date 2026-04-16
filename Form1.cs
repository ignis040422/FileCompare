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
        /// 프로그램 시작 시 기본 상태 설정
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
        /// 왼쪽/오른쪽 ListView를 표 형태로 초기화
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
        /// 왼쪽 폴더 선택 버튼 클릭
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
        /// 오른쪽 폴더 선택 버튼 클릭
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
        /// 양쪽 목록을 다시 읽고 비교 결과까지 갱신
        /// </summary>
        private void RefreshAllViews()
        {
            if (Directory.Exists(txtLeftDir.Text))
                PopulateListView(lvwLeftDir, txtLeftDir.Text);
            else
                lvwLeftDir.Items.Clear();

            if (Directory.Exists(txtRightDir.Text))
                PopulateListView(lvwRightDir, txtRightDir.Text);
            else
                lvwRightDir.Items.Clear();

            CompareEntries();

            lvwLeftDir.SelectedItems.Clear();
            lvwRightDir.SelectedItems.Clear();
        }

        /// <summary>
        /// 선택된 폴더의 하위 폴더 및 파일 목록을 ListView에 표시
        /// </summary>
        private void PopulateListView(ListView lv, string folderPath)
        {
            lv.BeginUpdate();
            lv.Items.Clear();

            try
            {
                // 하위 폴더 먼저 추가
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
        /// 폴더와 파일을 모두 이름 기준으로 비교하여 색상 표시
        /// 동일: 검정 / 최신: 빨강 / 오래됨: 회색 / 단독 항목: 보라
        /// </summary>
        private void CompareEntries()
        {
            if (string.IsNullOrWhiteSpace(txtLeftDir.Text) ||
                string.IsNullOrWhiteSpace(txtRightDir.Text))
                return;

            if (!Directory.Exists(txtLeftDir.Text) || !Directory.Exists(txtRightDir.Text))
                return;

            foreach (ListViewItem item in lvwLeftDir.Items)
                item.ForeColor = Color.Black;

            foreach (ListViewItem item in lvwRightDir.Items)
                item.ForeColor = Color.Black;

            var leftDirs = Directory.EnumerateDirectories(txtLeftDir.Text)
                .Select(p => new DirectoryInfo(p))
                .ToDictionary(d => d.Name, d => d);

            var rightDirs = Directory.EnumerateDirectories(txtRightDir.Text)
                .Select(p => new DirectoryInfo(p))
                .ToDictionary(d => d.Name, d => d);

            var leftFiles = Directory.EnumerateFiles(txtLeftDir.Text)
                .Select(p => new FileInfo(p))
                .ToDictionary(f => f.Name, f => f);

            var rightFiles = Directory.EnumerateFiles(txtRightDir.Text)
                .Select(p => new FileInfo(p))
                .ToDictionary(f => f.Name, f => f);

            // 왼쪽 항목 비교
            foreach (ListViewItem item in lvwLeftDir.Items)
            {
                string name = item.Text;
                bool isDir = item.SubItems[1].Text == "<DIR>";

                if (isDir)
                {
                    if (rightDirs.ContainsKey(name))
                    {
                        var ld = leftDirs[name];
                        var rd = rightDirs[name];

                        if (ld.LastWriteTime == rd.LastWriteTime)
                            item.ForeColor = Color.Black;
                        else if (ld.LastWriteTime > rd.LastWriteTime)
                            item.ForeColor = Color.Red;
                        else
                            item.ForeColor = Color.Gray;
                    }
                    else
                    {
                        item.ForeColor = Color.Purple;
                    }
                }
                else
                {
                    if (rightFiles.ContainsKey(name))
                    {
                        var lf = leftFiles[name];
                        var rf = rightFiles[name];

                        if (lf.LastWriteTime == rf.LastWriteTime)
                            item.ForeColor = Color.Black;
                        else if (lf.LastWriteTime > rf.LastWriteTime)
                            item.ForeColor = Color.Red;
                        else
                            item.ForeColor = Color.Gray;
                    }
                    else
                    {
                        item.ForeColor = Color.Purple;
                    }
                }
            }

            // 오른쪽 항목 비교
            foreach (ListViewItem item in lvwRightDir.Items)
            {
                string name = item.Text;
                bool isDir = item.SubItems[1].Text == "<DIR>";

                if (isDir)
                {
                    if (leftDirs.ContainsKey(name))
                    {
                        var rd = rightDirs[name];
                        var ld = leftDirs[name];

                        if (rd.LastWriteTime == ld.LastWriteTime)
                            item.ForeColor = Color.Black;
                        else if (rd.LastWriteTime > ld.LastWriteTime)
                            item.ForeColor = Color.Red;
                        else
                            item.ForeColor = Color.Gray;
                    }
                    else
                    {
                        item.ForeColor = Color.Purple;
                    }
                }
                else
                {
                    if (leftFiles.ContainsKey(name))
                    {
                        var rf = rightFiles[name];
                        var lf = leftFiles[name];

                        if (rf.LastWriteTime == lf.LastWriteTime)
                            item.ForeColor = Color.Black;
                        else if (rf.LastWriteTime > lf.LastWriteTime)
                            item.ForeColor = Color.Red;
                        else
                            item.ForeColor = Color.Gray;
                    }
                    else
                    {
                        item.ForeColor = Color.Purple;
                    }
                }
            }
        }

        /// <summary>
        /// 파일 복사 시 대상 파일이 있으면 수정일을 보여주고 덮어쓰기 여부 확인
        /// 성공 시 true, 취소/실패 시 false 반환
        /// </summary>
        private bool CopyFileWithConfirmation(string srcPath, string destPath)
        {
            try
            {
                FileInfo srcFile = new FileInfo(srcPath);

                if (File.Exists(destPath))
                {
                    FileInfo destFile = new FileInfo(destPath);

                    DialogResult result = MessageBox.Show(
                        "같은 이름의 파일이 이미 존재합니다.\n\n" +
                        "원본 파일 수정일: " + srcFile.LastWriteTime.ToString("g") + "\n" +
                        "대상 파일 수정일: " + destFile.LastWriteTime.ToString("g") + "\n\n" +
                        "덮어쓰시겠습니까?",
                        "덮어쓰기 확인",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result != DialogResult.Yes)
                    {
                        return false;
                    }
                }

                File.Copy(srcPath, destPath, true);
                return true;
            }
            catch (IOException ex)
            {
                MessageBox.Show("복사 중 오류: " + ex.Message, "오류",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// 폴더 전체를 재귀적으로 복사
        /// 내부 파일과 하위폴더까지 모두 복사
        /// 하나라도 취소/실패하면 false 반환
        /// </summary>
        private bool CopyDirectoryRecursive(string srcDir, string destDir)
        {
            bool allSuccess = true;
            DirectoryInfo source = new DirectoryInfo(srcDir);

            if (!Directory.Exists(destDir))
            {
                Directory.CreateDirectory(destDir);
            }

            // 현재 폴더 안 파일 복사
            foreach (FileInfo file in source.GetFiles())
            {
                string destFilePath = Path.Combine(destDir, file.Name);

                if (!CopyFileWithConfirmation(file.FullName, destFilePath))
                {
                    allSuccess = false;
                }
            }

            // 하위 폴더 재귀 복사
            foreach (DirectoryInfo subDir in source.GetDirectories())
            {
                string nextDestDir = Path.Combine(destDir, subDir.Name);

                if (!CopyDirectoryRecursive(subDir.FullName, nextDestDir))
                {
                    allSuccess = false;
                }
            }

            return allSuccess;
        }

        /// <summary>
        /// 왼쪽에서 선택한 파일 또는 폴더를 오른쪽으로 복사
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
                MessageBox.Show("왼쪽 목록에서 복사할 항목을 선택하세요.", "안내");
                return;
            }

            var selectedItem = lvwLeftDir.SelectedItems[0];
            string name = selectedItem.Text;
            bool isDir = selectedItem.SubItems[1].Text == "<DIR>";
            bool success = false;

            try
            {
                if (isDir)
                {
                    string srcDir = Path.Combine(txtLeftDir.Text, name);
                    string destDir = Path.Combine(txtRightDir.Text, name);

                    success = CopyDirectoryRecursive(srcDir, destDir);
                }
                else
                {
                    string srcFile = Path.Combine(txtLeftDir.Text, name);
                    string destFile = Path.Combine(txtRightDir.Text, name);

                    success = CopyFileWithConfirmation(srcFile, destFile);
                }

                RefreshAllViews();

                if (success)
                {
                    MessageBox.Show("복사가 완료되었습니다.", "안내");
                }
                else
                {
                    MessageBox.Show("복사가 일부만 완료되었거나 취소되었습니다.", "안내");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("복사 중 오류: " + ex.Message, "오류",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 오른쪽에서 선택한 파일 또는 폴더를 왼쪽으로 복사
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
                MessageBox.Show("오른쪽 목록에서 복사할 항목을 선택하세요.", "안내");
                return;
            }

            var selectedItem = lvwRightDir.SelectedItems[0];
            string name = selectedItem.Text;
            bool isDir = selectedItem.SubItems[1].Text == "<DIR>";
            bool success = false;

            try
            {
                if (isDir)
                {
                    string srcDir = Path.Combine(txtRightDir.Text, name);
                    string destDir = Path.Combine(txtLeftDir.Text, name);

                    success = CopyDirectoryRecursive(srcDir, destDir);
                }
                else
                {
                    string srcFile = Path.Combine(txtRightDir.Text, name);
                    string destFile = Path.Combine(txtLeftDir.Text, name);

                    success = CopyFileWithConfirmation(srcFile, destFile);
                }

                RefreshAllViews();

                if (success)
                {
                    MessageBox.Show("복사가 완료되었습니다.", "안내");
                }
                else
                {
                    MessageBox.Show("복사가 일부만 완료되었거나 취소되었습니다.", "안내");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("복사 중 오류: " + ex.Message, "오류",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}