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
    public partial class frm_main : Form
    {
        public frm_main()
        {
            InitializeComponent();
        }

        private void pn_main_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frm_main_Load(object sender, EventArgs e)
        {
            UCQLSV uCQLSV = new UCQLSV();
            pn_main.Controls.Clear();
            pn_main.Controls.Add(uCQLSV);
        }

        private void quảnLýSinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UCQLSV uCQLSV = new UCQLSV();
            pn_main.Controls.Clear();
            pn_main.Controls.Add(uCQLSV);

        }

        private void quảnLýLớpHọcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UCQLLH uCQLLH = new UCQLLH();
            pn_main.Controls.Clear();
            pn_main.Controls.Add(uCQLLH);
        }
    }
}
