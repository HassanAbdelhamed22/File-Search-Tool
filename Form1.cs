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
        // create a file search tool
        internal FileSearchTool fileSearchTool = new FileSearchTool();

        public Form1()
        {
            InitializeComponent();
            listView1.View = View.Details;
            listView1.Columns.Add("Status", 190, HorizontalAlignment.Left);
            listView1.Columns.Add("Time Taken", 140, HorizontalAlignment.Center);
            listView1.Columns.Add("Progress", 140, HorizontalAlignment.Center); 
        }

        private void Button_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                button.BackColor = Color.DarkGray;
            }
        }

        private void Button_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Button button)
            {
                button.BackColor = Color.Snow;
            }
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

        // function to start the search
        private async Task StartFileSearchAsync(string[] filePaths, string keyword, IProgress<string> progress)
        {
            listView1.Items.Clear();
            ClearFields();
            lblStatus.Text = "Searching in Progress...";

            await Task.Delay(1000);

            // create a progress handler
            var progressHandler = CreateProgressHandler();

            // start the search
            await Task.Run(() => fileSearchTool.SearchKeywordInFilesAsync(filePaths, keyword, progressHandler));

            // update the final status
            UpdateFinalStatus(fileSearchTool);
        }

        // function to create a progress handler
        private Progress<(string status, TimeSpan timeTaken, int progressPercentage)> CreateProgressHandler()
        {
            // create a progress handler
            return new Progress<(string status, TimeSpan timeTaken, int progressPercentage)>(data =>
            {
                // update the list view
                var item = listView1.Items.Cast<ListViewItem>().FirstOrDefault(i => i.Text.StartsWith(data.status.Split(',')[0]));
                if (item != null)
                {
                    // update the item
                    var matchCount = int.Parse(data.status.Split(',')[1].Split(':')[1]);
                    item.SubItems[0].Text = $"{data.status.Split(',')[0]}, Match count: {matchCount}";
                    item.SubItems[2].Text = data.progressPercentage.ToString() + "%";
                }
                // add a new item
                else
                {
                    AddNewItemToListView(data);
                }
                // refresh the list view
                listView1.Refresh();
            });
        }

        // function to add a new item to the list view
        private void AddNewItemToListView((string status, TimeSpan timeTaken, int progressPercentage) data)
        {
            // add a new item
            var item = new ListViewItem(data.status);
            item.SubItems.Add(data.timeTaken.TotalMilliseconds.ToString());
            item.SubItems.Add(data.progressPercentage.ToString() + "%");
            listView1.Items.Add(item);
        }

        // function to update the final status
        private void UpdateFinalStatus(FileSearchTool fileSearchTool)
        {
            // Get the total number of matches
            var results = fileSearchTool.GetResults().Sum(result => result.MatchCount);
            lblStatus.Text = "Search completed!, " + results + " matches found.";
            labelThread.Text = $" Total threads used: {fileSearchTool.GetThreadCount()}";
        }

        private void ClearFields()
        {
            lstFiles.Items.Clear();
            fileSearchTool.GetResults().Clear();
            textKeyword.Text = "";
        }
    }
}