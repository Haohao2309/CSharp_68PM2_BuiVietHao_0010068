using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp01
{
    public partial class UCQLLH : UserControl
    {
        DataClasses1DataContext dataClasses1 = new DataClasses1DataContext();
        private int currentPage = 1;
        private int pageSize = 5;
        private int totalPages = 1;
        private int totalRecords = 0;
        private string currentKeyword = "";

        public UCQLLH()
        {
            InitializeComponent();
        }

        private void UCQLLH_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            dataGridView1.AutoGenerateColumns = false;
            Column1.DataPropertyName = "id";
            Column2.DataPropertyName = "malop";
            Column3.DataPropertyName = "tenlop";
            Column4.DataPropertyName = "ghichu";
            displayClassList();
        }

        private void LoadData(int page, string keyword)
        {
            var query = dataClasses1.tbl_lophocs.AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.tenlop.Contains(keyword) || x.malop.Contains(keyword));
            }
            totalRecords = query.Count();
            totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);
            if (totalPages == 0) totalPages = 1;
            if (page < 1) page = 1;
            if (page > totalPages) page = totalPages;
            currentPage = page;
            var data = query.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            dataGridView1.DataSource = data;
            label8.Text = $"Trang {currentPage}/{totalPages} | {totalRecords} ban ghi";
            button6.Enabled = (currentPage > 1);
            button7.Enabled = (currentPage > 1);
            button9.Enabled = (currentPage < totalPages);
            button8.Enabled = (currentPage < totalPages);
        }

        public void displayClassList()
        {
            currentKeyword = "";
            textBox3.Text = "";
            LoadData(1, currentKeyword);
        }

        public void addClass()
        {
            try
            {
                string idStr = textBox1.Text.Trim();
                if (string.IsNullOrEmpty(idStr))
                {
                    MessageBox.Show("Ma ID khong duoc de trong!", "Canh bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int id = int.Parse(idStr);
                bool checkId = dataClasses1.tbl_lophocs.Any(x => x.id == id);
                if (checkId)
                {
                    MessageBox.Show("Ma ID da ton tai!", "Canh bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string malop = textBox2.Text.Trim();
                if (string.IsNullOrEmpty(malop))
                {
                    MessageBox.Show("Ma lop khong duoc de trong!", "Canh bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool checkTonTai = dataClasses1.tbl_lophocs.Any(x => x.malop == malop);
                if (checkTonTai)
                {
                    MessageBox.Show("Ma lop da ton tai!", "Canh bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                tbl_lophoc lh = new tbl_lophoc();
                lh.id = id;
                lh.malop = malop;
                lh.tenlop = textBox4.Text.Trim();
                lh.ghichu = textBox5.Text.Trim();

                dataClasses1.tbl_lophocs.InsertOnSubmit(lh);
                dataClasses1.SubmitChanges();
                displayClassList();
                MessageBox.Show("Them lop hoc thanh cong!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xay ra loi: " + ex.Message, "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void updateClass()
        {
            try
            {
                string idStr = textBox1.Text;
                if (string.IsNullOrEmpty(idStr))
                {
                    MessageBox.Show("Vui long chon lop hoc de sua!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int id = int.Parse(idStr);
                tbl_lophoc lh = dataClasses1.tbl_lophocs.SingleOrDefault(x => x.id == id);

                if (lh != null)
                {
                    lh.malop = textBox2.Text.Trim();
                    lh.tenlop = textBox4.Text.Trim();
                    lh.ghichu = textBox5.Text.Trim();

                    dataClasses1.SubmitChanges();
                    displayClassList();
                    MessageBox.Show("Cap nhat thanh cong!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Khong tim thay lop hoc!", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Co loi xay ra: " + ex.Message, "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void deleteClass()
        {
            try
            {
                string idStr = textBox1.Text;
                if (string.IsNullOrEmpty(idStr))
                {
                    MessageBox.Show("Vui long chon mot lop hoc de xoa!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int id = int.Parse(idStr);
                if (id == 0)
                {
                    MessageBox.Show("Khong the xoa lop Chua xep!", "Canh bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult dialogResult = MessageBox.Show(
                    "Ban co chac chan muon xoa lop hoc nay khong?",
                    "Xac nhan xoa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    if (dataClasses1.Connection.State == ConnectionState.Closed)
                    {
                        dataClasses1.Connection.Open();
                    }

                    using (System.Data.Common.DbTransaction trans = dataClasses1.Connection.BeginTransaction())
                    {
                        try
                        {
                            dataClasses1.Transaction = trans;

                            tbl_lophoc lh = dataClasses1.tbl_lophocs.FirstOrDefault(x => x.id == id);
                            if (lh != null)
                            {
                                var students = dataClasses1.tbl_hocsinhs.Where(x => x.malop == id).ToList();
                                foreach (var sv in students)
                                {
                                    sv.malop = 0;
                                }
                                dataClasses1.SubmitChanges();

                                dataClasses1.tbl_lophocs.DeleteOnSubmit(lh);
                                dataClasses1.SubmitChanges();

                                trans.Commit();
                                displayClassList();

                                textBox1.Text = "";
                                textBox2.Text = "";
                                textBox4.Text = "";
                                textBox5.Text = "";
                                textBox1.Enabled = true;
                                MessageBox.Show("Xoa lop hoc thanh cong!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Khong tim thay lop hoc!", "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception innerEx)
                        {
                            trans.Rollback();
                            throw innerEx;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Khong the xoa lop hoc nay! Chi tiet loi: " + ex.Message, "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void groupBox1_Enter(object sender, EventArgs e) { }
        private void lblId_Click(object sender, EventArgs e) { }

        private void button2_Click_1(object sender, EventArgs e)
        {
            updateClass();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox1.Enabled = false;
                textBox1.Text = row.Cells[0].Value?.ToString();
                textBox2.Text = row.Cells[1].Value?.ToString();
                textBox4.Text = row.Cells[2].Value?.ToString();
                textBox5.Text = row.Cells[3].Value?.ToString();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            addClass();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            deleteClass();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox1.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            currentKeyword = textBox3.Text.Trim();
            LoadData(1, currentKeyword);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            LoadData(1, currentKeyword);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (currentPage > 1)
            {
                LoadData(currentPage - 1, currentKeyword);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (currentPage < totalPages)
            {
                LoadData(currentPage + 1, currentKeyword);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            LoadData(totalPages, currentKeyword);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string idStr = textBox1.Text;
            if (string.IsNullOrEmpty(idStr))
            {
                MessageBox.Show("Vui long chon lop hoc de xem danh sach sinh vien!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id = int.Parse(idStr);
            frm_student_in_class frm = new frm_student_in_class(id);
            frm.ShowDialog();
        }
    }
}