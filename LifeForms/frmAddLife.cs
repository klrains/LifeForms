using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Linq;
using System.Drawing;

namespace LifeForms
{
    public partial class frmAddLife : Form
    {
        //Adds a new life form

        public frmAddLife()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ResetBackgroundColor();
            //checks to see if any of the required Taxonomic Rank values are blank, and proceeds
            if (InvalidValueCheck())
            {
                //Adds Taxonomic properties
                LifeForm lifeform = new LifeForm();
                lifeform.Properties.Add("Domain", txtDomain.Text);
                lifeform.Properties.Add("Kingdom", txtKingdom.Text);
                lifeform.Properties.Add("Phylum", txtPhylum.Text);
                lifeform.Properties.Add("Class", txtClass.Text);
                lifeform.Properties.Add("Order", txtOrder.Text);
                lifeform.Properties.Add("Family", txtFamily.Text);
                lifeform.Properties.Add("Genus", txtGenus.Text);
                lifeform.Properties.Add("Species", txtSpecies.Text);

                //Adds custom properties
                foreach (DataGridViewRow row in dgvProperties.Rows)
                {
                   if (row.Cells["propName"].Value != null && row.Cells["propValue"].Value != null)
                        lifeform.Properties.Add(row.Cells["propName"].Value.ToString().Replace(" ",""), row.Cells["propValue"].Value.ToString());
                }
    
                //Adds the new life form to the data, notifies the user, and resets the textboxes
                LifeFormData.AddLifeform(lifeform);

                MessageBox.Show("New lifeform was created");
                ResetFields();
            }
        }

        
        private void ResetBackgroundColor()
        {
            //resets any possible prior invalid entries 
            tlpTaxonomicRank.Controls.OfType<TextBox>().ToList().ForEach(t => t.BackColor = System.Drawing.Color.White);
            foreach (DataGridViewRow row in dgvProperties.Rows)
            {
                row.DefaultCellStyle.BackColor = Color.White;
            }
        }

        private bool InvalidValueCheck()
        {
            //Loops through textboxes for string validation
            bool txtcheck = true;
           
            foreach (TextBox t in tlpTaxonomicRank.Controls.OfType<TextBox>())
            {
                if (string.IsNullOrWhiteSpace(t.Text) || !t.Text.All(Char.IsLetterOrDigit))
                {
                    t.BackColor = Color.Yellow;
                    txtcheck = false;
                }
            }

            bool gridcheck = true;
            //loops through datagrid and validates cell values
            foreach (DataGridViewRow row in dgvProperties.Rows)
            {
                if (row.Cells[0].Value != null || row.Cells[1].Value != null)
                {
                    if (row.Cells[0].Value == null || row.Cells[1].Value == null || !row.Cells[0].Value.ToString().All(Char.IsLetterOrDigit) || !row.Cells[1].Value.ToString().All(Char.IsLetterOrDigit))
                    {
                        gridcheck = false;
                        row.DefaultCellStyle.BackColor = Color.Yellow;
                    }
                }
            }

            if (!txtcheck)
                MessageBox.Show("Taxonomic Rank is required and must have an alphanumeric value.");
            if (!gridcheck)
                MessageBox.Show("Custom property values must be alphanumeric and requires a name if a value is present");

            return txtcheck && gridcheck;
        }

        private void ResetFields()
        {
            //Resets textboxes
            txtDomain.Text = "";
            txtKingdom.Text = "";
            txtPhylum.Text = "";
            txtClass.Text = "";
            txtOrder.Text = "";
            txtFamily.Text = "";
            txtGenus.Text = "";
            txtSpecies.Text = "";

            dgvProperties.Rows.Clear();
       }
    }
}
