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
    public partial class UCQLSV : UserControl
    {

        DataClasses1DataContext dataClasses1 = new DataClasses1DataContext();
        public UCQLSV()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void grbQLSV_Enter(object sender, EventArgs e)
        {

        }

        private void UCQLSV_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            displayStudentList();
            displayClassList4CBX();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addStudent();
        }
        public void addStudent()
        {
            try
            {
                bool gt = true;
                string masv = textBox1.Text;
                string hoTen = textBox2.Text;
                string gioiTinh = comboBox1.Text;
                string dateTime = dateTimePicker1.Text;
                int maLop = Convert.ToInt32(comboBox2.SelectedValue);
                if (gioiTinh == "Nu") gt = false;
                else gt = true;
                tbl_hocsinh hs = new tbl_hocsinh();
                hs.masv = masv;
                hs.hovaten = hoTen;
                hs.ngaysinh = DateTime.Parse(dateTime);
                hs.gioitinh = gt;
                hs.malop = maLop;

                dataClasses1.tbl_hocsinhs.InsertOnSubmit(hs);
                dataClasses1.SubmitChanges();
                dataGridView1.DataSource = dataClasses1.tbl_hocsinhs.ToList();
                MessageBox.Show("Them sinh vien thanh cong!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xay ra loi: " + ex.Message, "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void updateStudent()
        {
            try
            {
                string masv = textBox1.Text;
                if (string.IsNullOrEmpty(masv))
                {
                    MessageBox.Show("Vui long chon sinh vien de sua!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                tbl_hocsinh hs = dataClasses1.tbl_hocsinhs.SingleOrDefault(x => x.masv == masv);

                if (hs != null)
                {
                    hs.hovaten = textBox2.Text;
                    hs.ngaysinh = dateTimePicker1.Value;
                    hs.gioitinh = (comboBox1.Text == "Nam") ? true : false;
                    hs.malop = Convert.ToInt32(comboBox2.SelectedValue);
                    dataClasses1.SubmitChanges();
                    displayStudentList();

                    MessageBox.Show("Cap nhat thanh cong!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Khong tim thay sinh vien!: " + masv, "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Co loi xay ra: " + ex.Message, "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void deleteStudent()
        {
            try
            {
                string masv = textBox1.Text;

                if (string.IsNullOrEmpty(masv))
                {
                    MessageBox.Show("Vui long chon mot sinh vien de xoa!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DialogResult dialogResult = MessageBox.Show(
                    $"Ban co chac chan muon xoa sinh vien co ma '{masv}' khong?",
                    "Xac nhan xoa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    tbl_hocsinh hs = dataClasses1.tbl_hocsinhs.FirstOrDefault(x => x.masv == masv);
                    if (hs != null)
                    {
                        dataClasses1.tbl_hocsinhs.DeleteOnSubmit(hs);
                        dataClasses1.SubmitChanges();
                        displayStudentList();

                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox1.Enabled = true;
                        MessageBox.Show("Xoa sinh vien thanh cong!", "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Khong tim thay sinh vien co ma: " + masv, "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Khong the xoa sinh vien nay! Chi tiet loi: " + ex.Message, "Loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void displayStudentList()
        {
            List<tbl_hocsinh> dSSV = dataClasses1.tbl_hocsinhs.ToList();
            dataGridView1.DataSource = dSSV;
        }


        public void displayClassList4CBX()
        {
            List<tbl_lophoc> dSLH = dataClasses1.tbl_lophocs.ToList();
            comboBox2.DataSource = dSLH;
            comboBox2.DisplayMember = "malop";
            comboBox2.ValueMember = "id";
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox1.Enabled = false;
                textBox1.Text = row.Cells["masv"].Value?.ToString();
                textBox2.Text = row.Cells["hovaten"].Value?.ToString();

                if (row.Cells["ngaysinh"].Value != null)
                {
                    dateTimePicker1.Value = Convert.ToDateTime(row.Cells["ngaysinh"].Value);
                }

                if (row.Cells["gioitinh"].Value != null)
                {
                    bool gioiTinh = Convert.ToBoolean(row.Cells["gioitinh"].Value);
                    comboBox1.Text = gioiTinh ? "Nam" : "Nu";
                }

                if (row.Cells["malop"].Value != null)
                {
                    comboBox2.SelectedValue = row.Cells["malop"].Value;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            updateStudent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            deleteStudent();
        }
    }
}
