namespace FileSearch
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            textKeyword = new TextBox();
            btnSearch = new Button();
            btnUpload = new Button();
            lstFiles = new ListBox();
            lblStatus = new Label();
            listView1 = new ListView();
            progressBar = new ProgressBar();
            labelThread = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(23, 180);
            label1.Name = "label1";
            label1.Size = new Size(89, 25);
            label1.TabIndex = 0;
            label1.Text = "Keyword:";
            // 
            // textKeyword
            // 
            textKeyword.Location = new Point(109, 181);
            textKeyword.Name = "textKeyword";
            textKeyword.Size = new Size(164, 27);
            textKeyword.TabIndex = 1;
            // 
            // btnSearch
            // 
            btnSearch.BackColor = Color.Snow;
            btnSearch.Location = new Point(331, 169);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(107, 51);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "Start Search";
            btnSearch.UseVisualStyleBackColor = false;
            btnSearch.Click += btnSearch_Click;
            btnSearch.MouseEnter += Button_MouseEnter;
            btnSearch.MouseLeave += Button_MouseLeave;
            // 
            // btnUpload
            // 
            btnUpload.BackColor = Color.Snow;
            btnUpload.ForeColor = SystemColors.ControlText;
            btnUpload.Location = new Point(444, 66);
            btnUpload.Name = "btnUpload";
            btnUpload.Size = new Size(107, 51);
            btnUpload.TabIndex = 3;
            btnUpload.Text = "Upload Files";
            btnUpload.UseVisualStyleBackColor = false;
            btnUpload.Click += btnUpload_Click;
            btnUpload.MouseEnter += Button_MouseEnter;
            btnUpload.MouseLeave += Button_MouseLeave;
            // 
            // lstFiles
            // 
            lstFiles.FormattingEnabled = true;
            lstFiles.Location = new Point(23, 26);
            lstFiles.Name = "lstFiles";
            lstFiles.Size = new Size(415, 124);
            lstFiles.TabIndex = 5;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(23, 544);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(0, 20);
            lblStatus.TabIndex = 6;
            // 
            // listView1
            // 
            listView1.Location = new Point(23, 241);
            listView1.Name = "listView1";
            listView1.Size = new Size(673, 287);
            listView1.TabIndex = 7;
            listView1.UseCompatibleStateImageBehavior = false;
            // 
            // progressBar
            // 
            progressBar.Location = new Point(560, 255);
            progressBar.Margin = new Padding(3, 4, 3, 4);
            progressBar.Name = "progressBar";
            progressBar.Size = new Size(114, 31);
            progressBar.TabIndex = 8;
            // 
            // labelThread
            // 
            labelThread.AutoSize = true;
            labelThread.Location = new Point(23, 575);
            labelThread.Name = "labelThread";
            labelThread.Size = new Size(0, 20);
            labelThread.TabIndex = 9;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.SlateGray;
            ClientSize = new Size(821, 615);
            Controls.Add(labelThread);
            Controls.Add(progressBar);
            Controls.Add(listView1);
            Controls.Add(lblStatus);
            Controls.Add(lstFiles);
            Controls.Add(btnUpload);
            Controls.Add(btnSearch);
            Controls.Add(textKeyword);
            Controls.Add(label1);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FileSearcher";
            MouseEnter += Button_MouseEnter;
            MouseLeave += Button_MouseLeave;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textKeyword;
        private Button btnSearch;
        private Button btnUpload;
        private ListBox lstFiles;
        private Label lblStatus;
        private ListView listView1;
        private ProgressBar progressBar;
        private Label labelThread;
    }
}
