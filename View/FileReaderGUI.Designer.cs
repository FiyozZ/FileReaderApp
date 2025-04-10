namespace FileReaderApp
{
    partial class FileReaderGUI
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
            listBoxLog = new ListBox();
            DatasViw = new DataGridView();
            Date = new DataGridViewTextBoxColumn();
            Open = new DataGridViewTextBoxColumn();
            High = new DataGridViewTextBoxColumn();
            Low = new DataGridViewTextBoxColumn();
            Close = new DataGridViewTextBoxColumn();
            Volume = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)DatasViw).BeginInit();
            SuspendLayout();
            // 
            // listBoxLog
            // 
            listBoxLog.ForeColor = Color.FromArgb(72, 70, 68);
            listBoxLog.FormattingEnabled = true;
            listBoxLog.ItemHeight = 15;
            listBoxLog.Location = new Point(2, 12);
            listBoxLog.Name = "listBoxLog";
            listBoxLog.Size = new Size(263, 439);
            listBoxLog.TabIndex = 0;
            listBoxLog.Click += listBoxLog_Click;
            //listBoxLog.SelectedIndexChanged += listBoxLog_SelectedIndexChanged;
            // 
            // DatasViw
            // 
            DatasViw.BackgroundColor = SystemColors.ButtonHighlight;
            DatasViw.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DatasViw.Columns.AddRange(new DataGridViewColumn[] { Date, Open, High, Low, Close, Volume });
            DatasViw.Location = new Point(271, 12);
            DatasViw.Name = "DatasViw";
            DatasViw.ReadOnly = true;
            DatasViw.RowTemplate.Height = 25;
            DatasViw.Size = new Size(645, 439);
            DatasViw.TabIndex = 1;
            //DatasViw.CellContentClick += DatasViw_CellContentClick;
            // 
            // Date
            // 
            Date.Frozen = true;
            Date.HeaderText = "Date";
            Date.Name = "Date";
            Date.ReadOnly = true;
            // 
            // Open
            // 
            Open.Frozen = true;
            Open.HeaderText = "Open";
            Open.Name = "Open";
            Open.ReadOnly = true;
            // 
            // High
            // 
            High.Frozen = true;
            High.HeaderText = "High";
            High.Name = "High";
            High.ReadOnly = true;
            // 
            // Low
            // 
            Low.Frozen = true;
            Low.HeaderText = "Low";
            Low.Name = "Low";
            Low.ReadOnly = true;
            // 
            // Close
            // 
            Close.Frozen = true;
            Close.HeaderText = "Close";
            Close.Name = "Close";
            Close.ReadOnly = true;
            // 
            // Volume
            // 
            Volume.Frozen = true;
            Volume.HeaderText = "Volume";
            Volume.Name = "Volume";
            Volume.ReadOnly = true;
            // 
            // FileReaderGUI
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(928, 462);
            Controls.Add(DatasViw);
            Controls.Add(listBoxLog);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FileReaderGUI";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "UserInterface";
            FormClosing += FileReaderGUI_FormClosing;
            Load += FileReaderGUI_Load;
            ((System.ComponentModel.ISupportInitialize)DatasViw).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private ListBox listBoxLog;
        private DataGridView DatasViw;
        private DataGridViewTextBoxColumn Date;
        private DataGridViewTextBoxColumn Open;
        private DataGridViewTextBoxColumn High;
        private DataGridViewTextBoxColumn Low;
        private DataGridViewTextBoxColumn Close;
        private DataGridViewTextBoxColumn Volume;
    }
}
