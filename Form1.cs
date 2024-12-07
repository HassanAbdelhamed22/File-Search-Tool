using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileSearch
{
    public partial class Form1 : Form
    {
        internal FileSearchTool fileSearchTool = new FileSearchTool();
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            {
                ofd.Multiselect = true;
                ofd.Filter = "Text Files(*.txt)|*.txt|All Files(*.*)|*.*";
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (var filePath in ofd.FileNames)
                {
                    if (!lstFiles.Items.Contains(filePath))
                    {
                        lstFiles.Items.Add(filePath);
                    }
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (lstFiles.Items.Count == 0)
            {
                MessageBox.Show("No files to search", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(textKeyword.Text))
            {
                MessageBox.Show("Please enter a keyword to search", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string[] filePaths = new string[lstFiles.Items.Count];
            lstFiles.Items.CopyTo(filePaths, 0);

            IProgress<string> progress = new Progress<string>(status => lblStatus.Text = status);
            _ = SearchFilesAsync(filePaths, textKeyword.Text, progress);
        }

        private async Task SearchFilesAsync(string[] filePaths, string keyword, IProgress<string> progress)
        {
            ClearFields();
            lblStatus.Text = "Starting search...";  // Initial status

            await fileSearchTool.SearchFilesAsync(filePaths, keyword, new Progress<string>(async status =>
            {
                string[] parts = status.Split(", ");
                if (parts.Length == 2 && parts[0].StartsWith("Processed"))
                {
                    string filePath = Path.GetFullPath(parts[0].Split(':')[1].Trim());
                }

                // Update the status during the search
                if (lblStatus.InvokeRequired)
                {
                    lblStatus.Invoke(new Action(() => lblStatus.Text = status));
                }
                else
                {
                    lblStatus.Text = status;
                }

                await Task.Delay(50000);
            }));

            // After all tasks are completed, update the final status
            var results = fileSearchTool.GetResults().Sum(result => result.MatchCount);
            lblStatus.Text = $"Completed. Total matches: {results}";
        }

        

        private void ClearFields()
        {
            lstFiles.Items.Clear();
            progressPanel.Controls.Clear();
            fileSearchTool.GetResults().Clear();
            textKeyword.Text = "";
            lblStatus.Text = "";
        }
    }
}
