namespace SoftwareArchitectureWinformsApp
{
    partial class MainForm
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
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle11 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle12 = new DataGridViewCellStyle();
            button1 = new Button();
            WebViewSidebar = new Microsoft.Web.WebView2.WinForms.WebView2();
            panel2 = new Panel();
            panel1 = new Panel();
            GameGrid = new DataGridView();
            nameDataGridViewTextBoxColumn1 = new DataGridViewButtonColumn();
            playtimeDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            lastNewsDataGridViewTextBoxColumn1 = new DataGridViewButtonColumn();
            lastDatePlayedDataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            playerCountDataGridViewTextBoxColumn1 = new DataGridViewButtonColumn();
            MetacriticScore = new DataGridViewButtonColumn();
            playerGameBindingSource = new BindingSource(components);
            nameDataGridViewTextBoxColumn = new DataGridViewButtonColumn();
            lastNewsDataGridViewTextBoxColumn = new DataGridViewButtonColumn();
            lastDatePlayedDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            playerCountDataGridViewTextBoxColumn = new DataGridViewButtonColumn();
            splitContainer1 = new SplitContainer();
            splitContainer2 = new SplitContainer();
            checkBoxUseMultithreading = new CheckBox();
            checkedListBox1 = new CheckedListBox();
            genreBindingSource = new BindingSource(components);
            ((System.ComponentModel.ISupportInitialize)WebViewSidebar).BeginInit();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)GameGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)playerGameBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)genreBindingSource).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ControlLightLight;
            button1.Dock = DockStyle.Bottom;
            button1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            button1.ForeColor = SystemColors.ControlText;
            button1.Location = new Point(0, 544);
            button1.Name = "button1";
            button1.Size = new Size(1068, 39);
            button1.TabIndex = 0;
            button1.Text = "Get Games List";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // WebViewSidebar
            // 
            WebViewSidebar.AllowExternalDrop = true;
            WebViewSidebar.CreationProperties = null;
            WebViewSidebar.DefaultBackgroundColor = Color.White;
            WebViewSidebar.Dock = DockStyle.Fill;
            WebViewSidebar.Location = new Point(0, 0);
            WebViewSidebar.Name = "WebViewSidebar";
            WebViewSidebar.Size = new Size(645, 544);
            WebViewSidebar.Source = new Uri("https://steamcharts.com/", UriKind.Absolute);
            WebViewSidebar.TabIndex = 1;
            WebViewSidebar.ZoomFactor = 1D;
            WebViewSidebar.NavigationStarting += WebViewSidebar_NavigationStarting;
            // 
            // panel2
            // 
            panel2.AutoSize = true;
            panel2.Controls.Add(WebViewSidebar);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(645, 544);
            panel2.TabIndex = 3;
            // 
            // panel1
            // 
            panel1.Controls.Add(GameGrid);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(419, 406);
            panel1.TabIndex = 4;
            // 
            // GameGrid
            // 
            GameGrid.AllowUserToAddRows = false;
            GameGrid.AllowUserToDeleteRows = false;
            GameGrid.AllowUserToOrderColumns = true;
            GameGrid.AutoGenerateColumns = false;
            GameGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            GameGrid.Columns.AddRange(new DataGridViewColumn[] { nameDataGridViewTextBoxColumn1, playtimeDataGridViewTextBoxColumn, lastNewsDataGridViewTextBoxColumn1, lastDatePlayedDataGridViewTextBoxColumn1, playerCountDataGridViewTextBoxColumn1, MetacriticScore });
            GameGrid.DataSource = playerGameBindingSource;
            GameGrid.Dock = DockStyle.Fill;
            GameGrid.Location = new Point(0, 0);
            GameGrid.Name = "GameGrid";
            GameGrid.ReadOnly = true;
            GameGrid.RowTemplate.Height = 25;
            GameGrid.Size = new Size(419, 406);
            GameGrid.TabIndex = 0;
            GameGrid.CellClick += GameGrid_CellClick;
            // 
            // nameDataGridViewTextBoxColumn1
            // 
            nameDataGridViewTextBoxColumn1.DataPropertyName = "Name";
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft;
            nameDataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle7;
            nameDataGridViewTextBoxColumn1.HeaderText = "Name";
            nameDataGridViewTextBoxColumn1.Name = "nameDataGridViewTextBoxColumn1";
            nameDataGridViewTextBoxColumn1.ReadOnly = true;
            nameDataGridViewTextBoxColumn1.Resizable = DataGridViewTriState.True;
            nameDataGridViewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // playtimeDataGridViewTextBoxColumn
            // 
            playtimeDataGridViewTextBoxColumn.DataPropertyName = "Playtime";
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            playtimeDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle8;
            playtimeDataGridViewTextBoxColumn.HeaderText = "Playtime";
            playtimeDataGridViewTextBoxColumn.Name = "playtimeDataGridViewTextBoxColumn";
            playtimeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // lastNewsDataGridViewTextBoxColumn1
            // 
            lastNewsDataGridViewTextBoxColumn1.DataPropertyName = "LastNews";
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft;
            lastNewsDataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle9;
            lastNewsDataGridViewTextBoxColumn1.HeaderText = "LastNews";
            lastNewsDataGridViewTextBoxColumn1.Name = "lastNewsDataGridViewTextBoxColumn1";
            lastNewsDataGridViewTextBoxColumn1.ReadOnly = true;
            lastNewsDataGridViewTextBoxColumn1.Resizable = DataGridViewTriState.True;
            lastNewsDataGridViewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // lastDatePlayedDataGridViewTextBoxColumn1
            // 
            lastDatePlayedDataGridViewTextBoxColumn1.DataPropertyName = "LastDatePlayed";
            dataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleLeft;
            lastDatePlayedDataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle10;
            lastDatePlayedDataGridViewTextBoxColumn1.HeaderText = "LastDatePlayed";
            lastDatePlayedDataGridViewTextBoxColumn1.Name = "lastDatePlayedDataGridViewTextBoxColumn1";
            lastDatePlayedDataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // playerCountDataGridViewTextBoxColumn1
            // 
            playerCountDataGridViewTextBoxColumn1.DataPropertyName = "PlayerCount";
            dataGridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleLeft;
            playerCountDataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle11;
            playerCountDataGridViewTextBoxColumn1.HeaderText = "PlayerCount";
            playerCountDataGridViewTextBoxColumn1.Name = "playerCountDataGridViewTextBoxColumn1";
            playerCountDataGridViewTextBoxColumn1.ReadOnly = true;
            playerCountDataGridViewTextBoxColumn1.Resizable = DataGridViewTriState.True;
            playerCountDataGridViewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // MetacriticScore
            // 
            MetacriticScore.DataPropertyName = "MetacriticScore";
            MetacriticScore.HeaderText = "Metacritic";
            MetacriticScore.Name = "MetacriticScore";
            MetacriticScore.ReadOnly = true;
            MetacriticScore.Resizable = DataGridViewTriState.True;
            MetacriticScore.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // playerGameBindingSource
            // 
            playerGameBindingSource.DataSource = typeof(DAL_API.PlayerGame);
            // 
            // nameDataGridViewTextBoxColumn
            // 
            nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            dataGridViewCellStyle12.Alignment = DataGridViewContentAlignment.MiddleLeft;
            nameDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle12;
            nameDataGridViewTextBoxColumn.HeaderText = "Name";
            nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            nameDataGridViewTextBoxColumn.Resizable = DataGridViewTriState.True;
            nameDataGridViewTextBoxColumn.SortMode = DataGridViewColumnSortMode.Programmatic;
            nameDataGridViewTextBoxColumn.Width = 125;
            // 
            // lastNewsDataGridViewTextBoxColumn
            // 
            lastNewsDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            lastNewsDataGridViewTextBoxColumn.DataPropertyName = "LastNews";
            lastNewsDataGridViewTextBoxColumn.HeaderText = "LastNews";
            lastNewsDataGridViewTextBoxColumn.Name = "lastNewsDataGridViewTextBoxColumn";
            lastNewsDataGridViewTextBoxColumn.Resizable = DataGridViewTriState.True;
            lastNewsDataGridViewTextBoxColumn.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // lastDatePlayedDataGridViewTextBoxColumn
            // 
            lastDatePlayedDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            lastDatePlayedDataGridViewTextBoxColumn.DataPropertyName = "LastDatePlayed";
            lastDatePlayedDataGridViewTextBoxColumn.HeaderText = "LastDatePlayed";
            lastDatePlayedDataGridViewTextBoxColumn.Name = "lastDatePlayedDataGridViewTextBoxColumn";
            // 
            // playerCountDataGridViewTextBoxColumn
            // 
            playerCountDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            playerCountDataGridViewTextBoxColumn.DataPropertyName = "PlayerCount";
            playerCountDataGridViewTextBoxColumn.HeaderText = "PlayerCount";
            playerCountDataGridViewTextBoxColumn.Name = "playerCountDataGridViewTextBoxColumn";
            playerCountDataGridViewTextBoxColumn.Resizable = DataGridViewTriState.True;
            playerCountDataGridViewTextBoxColumn.SortMode = DataGridViewColumnSortMode.Automatic;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(panel2);
            splitContainer1.Size = new Size(1068, 544);
            splitContainer1.SplitterDistance = 419;
            splitContainer1.TabIndex = 5;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(panel1);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(checkBoxUseMultithreading);
            splitContainer2.Panel2.Controls.Add(checkedListBox1);
            splitContainer2.Size = new Size(419, 544);
            splitContainer2.SplitterDistance = 406;
            splitContainer2.TabIndex = 1;
            // 
            // checkBoxUseMultithreading
            // 
            checkBoxUseMultithreading.AutoSize = true;
            checkBoxUseMultithreading.Location = new Point(176, 13);
            checkBoxUseMultithreading.Name = "checkBoxUseMultithreading";
            checkBoxUseMultithreading.Size = new Size(127, 19);
            checkBoxUseMultithreading.TabIndex = 1;
            checkBoxUseMultithreading.Text = "Use Multithreading";
            checkBoxUseMultithreading.UseVisualStyleBackColor = true;
            // 
            // checkedListBox1
            // 
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Items.AddRange(new object[] { "Playtime", "Last News", "Last Date Played", "Player Count", "Metacritic Score" });
            checkedListBox1.Location = new Point(8, 9);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new Size(133, 94);
            checkedListBox1.TabIndex = 0;
            checkedListBox1.SelectedIndexChanged += checkedListBox1_SelectedIndexChanged;
            // 
            // genreBindingSource
            // 
            genreBindingSource.DataSource = typeof(DAL_API.Genre);
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDark;
            ClientSize = new Size(1068, 583);
            Controls.Add(splitContainer1);
            Controls.Add(button1);
            Name = "MainForm";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)WebViewSidebar).EndInit();
            panel2.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)GameGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)playerGameBindingSource).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)genreBindingSource).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button button1;
        private Microsoft.Web.WebView2.WinForms.WebView2 WebViewSidebar;
        private Panel panel2;
        private Panel panel1;
        private DataGridView GameGrid;
        private DataGridViewTextBoxColumn timelastplayedDataGridViewTextBoxColumn;
        private SplitContainer splitContainer1;
        private DataGridViewButtonColumn nameDataGridViewTextBoxColumn;
        private DataGridViewButtonColumn lastNewsDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn lastDatePlayedDataGridViewTextBoxColumn;
        private DataGridViewButtonColumn playerCountDataGridViewTextBoxColumn;
        private BindingSource playerGameBindingSource;
        private DataGridViewButtonColumn nameDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn playtimeDataGridViewTextBoxColumn;
        private DataGridViewButtonColumn lastNewsDataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn lastDatePlayedDataGridViewTextBoxColumn1;
        private DataGridViewButtonColumn playerCountDataGridViewTextBoxColumn1;
        private DataGridViewButtonColumn MetacriticScore;
        private SplitContainer splitContainer2;
        private BindingSource genreBindingSource;
        private CheckedListBox checkedListBox1;
        private CheckBox checkBoxUseMultithreading;
    }
}