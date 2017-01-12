using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace LifeForms
{
    public partial class frmSearch : Form
    {
        private List<LifeForm> Results;
        private int RecIndex;

        public frmSearch()
        {
            InitializeComponent();
            Results = new List<LifeForm>();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //Gets the search results based on textbox value

            if (ValidSearchCheck())
                GetResults();
            else
                MessageBox.Show("Invalid Search Criteria: Only alphanumeric characters are allowed.");
        }

        private bool ValidSearchCheck()
        {
            //only allows search for alphanumeric values
            return txtSearchCriteria.Text.All(Char.IsLetterOrDigit) && !string.IsNullOrWhiteSpace(txtSearchCriteria.Text);
        }

        private void GetResults()
        {
            //Resets record index, gets new search results and displays the first record
            RecIndex = 0;
            Results = LifeFormData.SearchData(txtSearchCriteria.Text);

            DisplayRecord();
        }

        private void DisplayRecord()
        {
            //Displays current search result record properties if any
            dgvResults.Rows.Clear();
           

            if (Results.Count == 0)
            {
                MessageBox.Show("No results found");
                lblRecord.Text = "No results found";
            }
            else
            {
                lblRecord.Text = String.Format("Record {0} of {1}", RecIndex + 1, Results.Count);

                foreach (var prop in Results[RecIndex].Properties)
                {
                    DataGridViewRow row = (DataGridViewRow)dgvResults.RowTemplate.Clone();
                    dgvResults.Rows.Add(new string[] { prop.Key, prop.Value });
                }
            }

            lblRecord.Visible = true;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            //User would like to see the next record
            if (Results[RecIndex] != Results.Last())
            {
                RecIndex++;
                DisplayRecord();
            }
            else
                MessageBox.Show("You are currently on the last record");
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            //User would like to see previous record
            if (Results[RecIndex] != Results.First())
            {
                RecIndex--;
                DisplayRecord();
            }
            else
                MessageBox.Show("You are currently on the first record");
        }
    }
}
