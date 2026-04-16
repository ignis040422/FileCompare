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
            // 처음 실행 시 경로 비우기
            txtLeftDir.Text = "";
            txtRightDir.Text = "";

            // 처음 실행 시 목록 비우기
            lvwLeftDir.Items.Clear();
            lvwRightDir.Items.Clear();

            // 처음 실행 시 파란색 선택 표시가 없도록 선택 해제
            lvwLeftDir.SelectedItems.Clear();
            lvwRightDir.SelectedItems.Clear();

            // 혹시 포커스가 ListView에 가서 선택처럼 보이는 것을 줄이기 위해
            // 폼 제목 Label 쪽으로 포커스를 한번 넘김
            lblAppName.Focus();
        }

        /// <summary>
        /// 왼쪽/오른쪽 ListView를 표 형태로 초기화하는 함수
        /// </summary>
        private void InitListView()
        {
            // 왼쪽 ListView 설정
            lvwLeftDir.View = View.Details;          // 표 형태
            lvwLeftDir.FullRowSelect = true;        // 한 줄 전체 선택
            lvwLeftDir.GridLines = true;            // 격자선 표시
            lvwLeftDir.HideSelection = false;       // 포커스를 잃어도 선택 표시 유지
            lvwLeftDir.MultiSelect = false;         // 다중 선택 방지
            lvwLeftDir.Columns.Clear();             // 기존 컬럼 제거
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
        /// FolderBrowserDialog를 사용하여 폴더를 선택하고
        /// 선택한 폴더의 내용을 왼쪽 ListView에 표시한다.
        /// </summary>
        private void btnLeftDir_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "폴더를 선택하세요.";

                // 이미 경로가 입력되어 있고 실제 존재하면 그 위치부터 열기
                if (!string.IsNullOrWhiteSpace(txtLeftDir.Text) &&
                    Directory.Exists(txtLeftDir.Text))
                {
                    dlg.SelectedPath = txtLeftDir.Text;
                }

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    // 선택한 폴더 경로를 텍스트박스에 출력
                    txtLeftDir.Text = dlg.SelectedPath;

                    // 왼쪽 ListView에 폴더 내용 표시
                    PopulateListView(lvwLeftDir, dlg.SelectedPath);

                    // 양쪽 폴더가 모두 선택된 경우 비교 수행
                    CompareFiles();

                    // 폴더 선택 후에도 아무 항목도 자동 선택되지 않게 정리
                    lvwLeftDir.SelectedItems.Clear();
                    lvwRightDir.SelectedItems.Clear();
                }
            }
        }

        /// <summary>
        /// 오른쪽 폴더 선택 버튼 클릭 이벤트
        /// FolderBrowserDialog를 사용하여 폴더를 선택하고
        /// 선택한 폴더의 내용을 오른쪽 ListView에 표시한다.
        /// </summary>
        private void btnRightDir_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.Description = "폴더를 선택하세요.";

                // 이미 경로가 입력되어 있고 실제 존재하면 그 위치부터 열기
                if (!string.IsNullOrWhiteSpace(txtRightDir.Text) &&
                    Directory.Exists(txtRightDir.Text))
                {
                    dlg.SelectedPath = txtRightDir.Text;
                }

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    // 선택한 폴더 경로를 텍스트박스에 출력
                    txtRightDir.Text = dlg.SelectedPath;

                    // 오른쪽 ListView에 폴더 내용 표시
                    PopulateListView(lvwRightDir, dlg.SelectedPath);

                    // 양쪽 폴더가 모두 선택된 경우 비교 수행
                    CompareFiles();

                    // 폴더 선택 후에도 아무 항목도 자동 선택되지 않게 정리
                    lvwLeftDir.SelectedItems.Clear();
                    lvwRightDir.SelectedItems.Clear();
                }
            }
        }

        /// <summary>
        /// 선택된 폴더의 하위 폴더 및 파일 목록을 ListView에 표시하는 함수
        /// </summary>
        /// <param name="lv">출력할 ListView</param>
        /// <param name="folderPath">선택된 폴더 경로</param>
        private void PopulateListView(ListView lv, string folderPath)
        {
            // 화면 깜빡임을 줄이기 위해 업데이트 잠시 중지
            lv.BeginUpdate();

            // 기존 항목 삭제
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

                // 컬럼 너비 자동 조정
                for (int i = 0; i < lv.Columns.Count; i++)
                {
                    lv.AutoResizeColumn(i, ColumnHeaderAutoResizeStyle.ColumnContent);
                }

                // 목록 출력 후에도 자동 선택 제거
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
                // 업데이트 재개
                lv.EndUpdate();
            }
        }

        /// <summary>
        /// 양쪽 폴더의 파일을 이름과 수정시간 기준으로 비교하여 색상을 표시하는 함수
        /// 동일 파일: 검정
        /// 더 최신 파일: 빨강
        /// 더 오래된 파일: 회색
        /// 한쪽에만 있는 파일: 보라
        /// </summary>
        private void CompareFiles()
        {
            // 양쪽 폴더가 모두 선택되지 않았으면 비교하지 않음
            if (string.IsNullOrWhiteSpace(txtLeftDir.Text) ||
                string.IsNullOrWhiteSpace(txtRightDir.Text))
                return;

            // 실제 폴더가 존재하지 않으면 비교하지 않음
            if (!Directory.Exists(txtLeftDir.Text) || !Directory.Exists(txtRightDir.Text))
                return;

            // 이전 색상 상태를 정리하기 위해 기본색으로 초기화
            foreach (ListViewItem item in lvwLeftDir.Items)
            {
                item.ForeColor = Color.Black;
            }

            foreach (ListViewItem item in lvwRightDir.Items)
            {
                item.ForeColor = Color.Black;
            }

            // 왼쪽 폴더 파일 정보를 Dictionary로 저장
            var leftFiles = Directory.EnumerateFiles(txtLeftDir.Text)
                .Select(p => new FileInfo(p))
                .ToDictionary(f => f.Name, f => f);

            // 오른쪽 폴더 파일 정보를 Dictionary로 저장
            var rightFiles = Directory.EnumerateFiles(txtRightDir.Text)
                .Select(p => new FileInfo(p))
                .ToDictionary(f => f.Name, f => f);

            // 왼쪽 ListView 항목 색상 처리
            foreach (ListViewItem item in lvwLeftDir.Items)
            {
                // 폴더는 비교 대상에서 제외
                if (item.SubItems.Count > 1 && item.SubItems[1].Text == "<DIR>")
                    continue;

                string fileName = item.Text;

                // 같은 이름 파일이 오른쪽에도 있으면 시간 비교
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
                    // 오른쪽에 없는 단독 파일
                    item.ForeColor = Color.Purple;
                }
            }

            // 오른쪽 ListView 항목 색상 처리
            foreach (ListViewItem item in lvwRightDir.Items)
            {
                // 폴더는 비교 대상에서 제외
                if (item.SubItems.Count > 1 && item.SubItems[1].Text == "<DIR>")
                    continue;

                string fileName = item.Text;

                // 같은 이름 파일이 왼쪽에도 있으면 시간 비교
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
                    // 왼쪽에 없는 단독 파일
                    item.ForeColor = Color.Purple;
                }
            }

            // 비교 후에도 자동 선택 제거
            lvwLeftDir.SelectedItems.Clear();
            lvwRightDir.SelectedItems.Clear();
        }

        /// <summary>
        /// 왼쪽 → 오른쪽 복사 버튼 클릭 이벤트
        /// 과제2 단계에서는 아직 복사 기능을 구현하지 않았으므로 안내만 표시
        /// </summary>
        private void btnCopyFromLeft_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLeftDir.Text) ||
                string.IsNullOrWhiteSpace(txtRightDir.Text))
            {
                MessageBox.Show("양쪽 폴더를 먼저 선택하세요.", "안내");
                return;
            }

            MessageBox.Show("과제3에서 구현합니다.", "안내");
        }

        /// <summary>
        /// 오른쪽 → 왼쪽 복사 버튼 클릭 이벤트
        /// 과제2 단계에서는 아직 복사 기능을 구현하지 않았으므로 안내만 표시
        /// </summary>
        private void btnCopyFromRight_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLeftDir.Text) ||
                string.IsNullOrWhiteSpace(txtRightDir.Text))
            {
                MessageBox.Show("양쪽 폴더를 먼저 선택하세요.", "안내");
                return;
            }

            MessageBox.Show("과제3에서 구현합니다.", "안내");
        }
    }
}