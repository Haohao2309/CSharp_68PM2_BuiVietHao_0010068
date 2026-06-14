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
    public partial class frm_student_in_class : Form
    {
        DataClasses1DataContext dataClasses1 = new DataClasses1DataContext();
        private int classId;

        public frm_student_in_class(int id)
        {
            InitializeComponent();
            classId = id;
            this.Load += frm_student_in_class_Load;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frm_student_in_class_Load(object sender, EventArgs e)
        {
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = dataClasses1.tbl_hocsinhs.Where(x => x.malop == classId).ToList();
        }
    }
}
