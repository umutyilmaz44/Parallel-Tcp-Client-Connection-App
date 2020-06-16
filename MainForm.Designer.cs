namespace ParallelTcpClientConnectionApp
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnConnection = new System.Windows.Forms.Button();
            this.gvList = new System.Windows.Forms.DataGridView();
            this.dataGridViewDisableButtonColumn1 = new System.Windows.Forms.DataGridViewDisableButtonColumn();
            this.Ip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Port = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewImageColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Send = new System.Windows.Forms.DataGridViewDisableButtonColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnConnection);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 320);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(646, 41);
            this.panel1.TabIndex = 1;
            // 
            // btnConnection
            // 
            this.btnConnection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnection.Location = new System.Drawing.Point(526, 4);
            this.btnConnection.Name = "btnConnection";
            this.btnConnection.Size = new System.Drawing.Size(117, 32);
            this.btnConnection.TabIndex = 0;
            this.btnConnection.Text = "Connect";
            this.btnConnection.UseVisualStyleBackColor = true;
            this.btnConnection.Click += new System.EventHandler(this.btnConnection_Click);
            // 
            // gvList
            // 
            this.gvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Ip,
            this.Port,
            this.Status,
            this.Description,
            this.Send});
            this.gvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvList.Location = new System.Drawing.Point(0, 0);
            this.gvList.Name = "gvList";
            this.gvList.Size = new System.Drawing.Size(646, 320);
            this.gvList.TabIndex = 2;
            this.gvList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvList_CellContentClick);
            this.gvList.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.gvList_RowsAdded);
            // 
            // dataGridViewDisableButtonColumn1
            // 
            this.dataGridViewDisableButtonColumn1.HeaderText = "Send";
            this.dataGridViewDisableButtonColumn1.Name = "dataGridViewDisableButtonColumn1";
            this.dataGridViewDisableButtonColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewDisableButtonColumn1.ToolTipText = "Sen Data";
            this.dataGridViewDisableButtonColumn1.Width = 75;
            // 
            // Ip
            // 
            this.Ip.HeaderText = "IP";
            this.Ip.Name = "Ip";
            // 
            // Port
            // 
            this.Port.HeaderText = "PORT";
            this.Port.Name = "Port";
            this.Port.Width = 75;
            // 
            // Status
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Status.DefaultCellStyle = dataGridViewCellStyle1;
            this.Status.HeaderText = "Status";
            this.Status.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 50;
            // 
            // Description
            // 
            this.Description.HeaderText = "DESCRIPTION";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            this.Description.Width = 300;
            // 
            // Send
            // 
            this.Send.HeaderText = "Send Data";
            this.Send.Name = "Send";
            this.Send.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Send.Text = "Send";
            this.Send.ToolTipText = "Sen Data";
            this.Send.Width = 75;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(646, 361);
            this.Controls.Add(this.gvList);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Parallel Tcp Client Connection App";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnConnection;
        private System.Windows.Forms.DataGridView gvList;
        private System.Windows.Forms.DataGridViewDisableButtonColumn dataGridViewDisableButtonColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ip;
        private System.Windows.Forms.DataGridViewTextBoxColumn Port;
        private System.Windows.Forms.DataGridViewImageColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewDisableButtonColumn Send;
    }
}

