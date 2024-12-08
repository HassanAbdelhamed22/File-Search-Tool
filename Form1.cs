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
            listView1.View = View.Details;
            listView1.Columns.Add("Status", -2, HorizontalAlignment.Left);
            listView1.Columns.Add("Time Taken", -2, HorizontalAlignment.Left);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        // function to upload files
        private void btnUpload_Click(object sender, EventArgs e)
        {
            // open file dialog
            OpenFileDialog ofd = new OpenFileDialog();
            {
                // allow multiple file selection
                ofd.Multiselect = true;
                // show only text files
                ofd.Filter = "Text Files(*.txt)|*.txt|All Files(*.*)|*.*";
            };
          
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // add selected files to the list
                foreach (var filePath in ofd.FileNames)
                {
                    if (!lstFiles.Items.Contains(filePath))
                    {
                        lstFiles.Items.Add(filePath);
                    }
                }
            }
        }

        // function for click search button
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            // check if list is empty
            if (lstFiles.Items.Count == 0)
            {
                MessageBox.Show("No files to search", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // check if keyword is empty
            if (string.IsNullOrWhiteSpace(textKeyword.Text))
            {
                MessageBox.Show("Please enter a keyword to search", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // convert items in a listBox to an array of strings
            // Cast is a LINQ method used to convert each item in the list to a string
            // ToArray is a method used to convert the resulting collection to an array
            string[] filePaths = lstFiles.Items.Cast<string>().ToArray();

            // use IProgress to update the progress panel
            IProgress<string> progress = new Progress<string>(status => lblStatus.Text = status);
            // start the search
            await StartFileSearchAsync(filePaths, textKeyword.Text, progress);
        }

        private async Task StartFileSearchAsync(string[] filePaths, string keyword, IProgress<string> progress)
        {
            listView1.Items.Clear();
            ClearFields();
            lblStatus.Text = "Starting search...";

            var progressHandler = new Progress<(string status, TimeSpan timeTaken)>(data =>
            {
                // Add progress details to the panel
                AddToProgressList(data.status, data.timeTaken);
            });

            // call fileSearchTool.SearchKeywordInFilesAsync in a separate task to avoid blocking the UI
            await Task.Run(() => fileSearchTool.SearchKeywordInFilesAsync(filePaths, keyword, progressHandler));

            // After all tasks are completed, update the final status
            var  results = fileSearchTool.GetResults().Sum(result => result.MatchCount);
            lblStatus.Text = $"Completed. Total matches: {results}";

            
        }

        private void AddToProgressList(string message, TimeSpan timeTaken)
        {
            //listView1.Items.Clear();

            ListViewItem item = new ListViewItem(message);
            item.SubItems.Add(timeTaken.TotalMilliseconds.ToString());
            listView1.Items.Add(item);
            listView1.Refresh();
        }

        private void ClearFields()
        {
            lstFiles.Items.Clear();
            fileSearchTool.GetResults().Clear();
            textKeyword.Text = "";
            lblStatus.Text = "";
        }
    }
}
