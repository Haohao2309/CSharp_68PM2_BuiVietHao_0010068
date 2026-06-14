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
    }
}
