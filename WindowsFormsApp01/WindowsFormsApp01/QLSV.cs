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
    public partial class QLSV : Form
    {
        public QLSV()
        {
            InitializeComponent();
        }

        private void lbl01_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userName = txtUser.Text;
            string pass = txtPass.Text;
            if(userName == "0010068@st.huce.edu.vn" && pass == "0010068")
            {
                MessageBox.Show("Đăng nhập thành công !!!");
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại !!!");
            }
        }
    }
}
