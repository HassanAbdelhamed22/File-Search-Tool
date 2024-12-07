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
            progressPanel = new Panel();
            lstFiles = new ListBox();
            lblStatus = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 112);
            label1.Name = "label1";
            label1.Size = new Size(70, 20);
            label1.TabIndex = 0;
            label1.Text = "Keyword:";
            label1.Click += label1_Click;
            // 
            // textKeyword
            // 
            textKeyword.Location = new Point(99, 109);
            textKeyword.Name = "textKeyword";
            textKeyword.Size = new Size(125, 27);
            textKeyword.TabIndex = 1;
            // 
            // btnSearch
            // 
            btnSearch.Location = new Point(348, 109);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(108, 29);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "Start Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnUpload
            // 
            btnUpload.Location = new Point(348, 36);
            btnUpload.Name = "btnUpload";
            btnUpload.Size = new Size(108, 29);
            btnUpload.TabIndex = 3;
            btnUpload.Text = "Upload Files";
            btnUpload.UseVisualStyleBackColor = true;
            btnUpload.Click += btnUpload_Click;
            // 
            // progressPanel
            // 
            progressPanel.AutoScroll = true;
            progressPanel.Location = new Point(12, 173);
            progressPanel.Name = "progressPanel";
            progressPanel.Size = new Size(444, 173);
            progressPanel.TabIndex = 4;
            // 
            // lstFiles
            // 
            lstFiles.FormattingEnabled = true;
            lstFiles.Location = new Point(12, 21);
            lstFiles.Name = "lstFiles";
            lstFiles.Size = new Size(150, 64);
            lstFiles.TabIndex = 5;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(12, 393);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(0, 20);
            lblStatus.TabIndex = 6;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(lblStatus);
            Controls.Add(lstFiles);
            Controls.Add(progressPanel);
            Controls.Add(btnUpload);
            Controls.Add(btnSearch);
            Controls.Add(textKeyword);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textKeyword;
        private Button btnSearch;
        private Button btnUpload;
        private Panel progressPanel;
        private ListBox lstFiles;
        private Label lblStatus;
    }
}
