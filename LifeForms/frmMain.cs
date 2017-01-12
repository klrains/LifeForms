using System;
using System.Windows.Forms;

namespace LifeForms
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            //Loads the xml document
            LifeFormData.LoadXMLDoc();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddLife frm = new frmAddLife();
            frm.ShowDialog();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            frmSearch frm = new frmSearch();
            frm.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
