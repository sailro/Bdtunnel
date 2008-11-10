namespace Bdt.GuiClient.Forms
{
    partial class AdminForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose (bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent ()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminForm));
            this.Close = new System.Windows.Forms.Button();
            this.BottomPanel = new System.Windows.Forms.Panel();
            this.Refresh = new System.Windows.Forms.Button();
            this.Sessions = new System.Windows.Forms.DataGridView();
            this.SessionsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.KillSessionItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SessionsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Connections = new System.Windows.Forms.DataGridView();
            this.ConnectionsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.KillConnectionItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ConnectionsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.SplitContainer = new System.Windows.Forms.SplitContainer();
            this.CCidCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CHostCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CAddressCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CPortCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CReadCountCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CWriteCountCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CLastAccessCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SSidCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SUsernameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SAdminCol = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SLogonCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SLastAccessCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BottomPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Sessions)).BeginInit();
            this.SessionsMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SessionsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Connections)).BeginInit();
            this.ConnectionsMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ConnectionsBindingSource)).BeginInit();
            this.SplitContainer.Panel1.SuspendLayout();
            this.SplitContainer.Panel2.SuspendLayout();
            this.SplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // Close
            // 
            this.Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Close.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Close.Location = new System.Drawing.Point(435, 3);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(75, 23);
            this.Close.TabIndex = 2;
            this.Close.Text = "Close";
            this.Close.UseVisualStyleBackColor = true;
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.Refresh);
            this.BottomPanel.Controls.Add(this.Close);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 331);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(513, 32);
            this.BottomPanel.TabIndex = 1;
            // 
            // Refresh
            // 
            this.Refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Refresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Refresh.Location = new System.Drawing.Point(354, 3);
            this.Refresh.Name = "Refresh";
            this.Refresh.Size = new System.Drawing.Size(75, 23);
            this.Refresh.TabIndex = 3;
            this.Refresh.Text = "Refresh";
            this.Refresh.UseVisualStyleBackColor = true;
            this.Refresh.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // Sessions
            // 
            this.Sessions.AllowUserToAddRows = false;
            this.Sessions.AllowUserToDeleteRows = false;
            this.Sessions.AllowUserToOrderColumns = true;
            this.Sessions.AllowUserToResizeRows = false;
            this.Sessions.AutoGenerateColumns = false;
            this.Sessions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Sessions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Sessions.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SSidCol,
            this.SUsernameCol,
            this.SAdminCol,
            this.SLogonCol,
            this.SLastAccessCol});
            this.Sessions.ContextMenuStrip = this.SessionsMenu;
            this.Sessions.DataSource = this.SessionsBindingSource;
            this.Sessions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Sessions.Location = new System.Drawing.Point(0, 0);
            this.Sessions.Name = "Sessions";
            this.Sessions.ReadOnly = true;
            this.Sessions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Sessions.Size = new System.Drawing.Size(513, 165);
            this.Sessions.TabIndex = 2;
            this.Sessions.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.Sessions_CellFormatting);
            this.Sessions.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Sessions_CellClick);
            // 
            // SessionsMenu
            // 
            this.SessionsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.KillSessionItem});
            this.SessionsMenu.Name = "SessionsMenu";
            this.SessionsMenu.Size = new System.Drawing.Size(136, 26);
            this.SessionsMenu.Opening += new System.ComponentModel.CancelEventHandler(this.SessionsMenu_Opening);
            // 
            // KillSessionItem
            // 
            this.KillSessionItem.Name = "KillSessionItem";
            this.KillSessionItem.Size = new System.Drawing.Size(135, 22);
            this.KillSessionItem.Text = "Kill session";
            this.KillSessionItem.Click += new System.EventHandler(this.KillSessionItem_Click);
            // 
            // SessionsBindingSource
            // 
            this.SessionsBindingSource.AllowNew = false;
            this.SessionsBindingSource.DataSource = typeof(Bdt.Shared.Response.Session);
            // 
            // Connections
            // 
            this.Connections.AllowUserToAddRows = false;
            this.Connections.AllowUserToDeleteRows = false;
            this.Connections.AllowUserToOrderColumns = true;
            this.Connections.AllowUserToResizeRows = false;
            this.Connections.AutoGenerateColumns = false;
            this.Connections.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Connections.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Connections.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CCidCol,
            this.CHostCol,
            this.CAddressCol,
            this.CPortCol,
            this.CReadCountCol,
            this.CWriteCountCol,
            this.CLastAccessCol});
            this.Connections.ContextMenuStrip = this.ConnectionsMenu;
            this.Connections.DataSource = this.ConnectionsBindingSource;
            this.Connections.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Connections.Location = new System.Drawing.Point(0, 0);
            this.Connections.Name = "Connections";
            this.Connections.ReadOnly = true;
            this.Connections.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Connections.Size = new System.Drawing.Size(513, 162);
            this.Connections.TabIndex = 3;
            // 
            // ConnectionsMenu
            // 
            this.ConnectionsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.KillConnectionItem});
            this.ConnectionsMenu.Name = "ConnectionsMenu";
            this.ConnectionsMenu.Size = new System.Drawing.Size(153, 26);
            this.ConnectionsMenu.Opening += new System.ComponentModel.CancelEventHandler(this.ConnectionsMenu_Opening);
            // 
            // KillConnectionItem
            // 
            this.KillConnectionItem.Name = "KillConnectionItem";
            this.KillConnectionItem.Size = new System.Drawing.Size(152, 22);
            this.KillConnectionItem.Text = "Kill connection";
            this.KillConnectionItem.Click += new System.EventHandler(this.KillConnectionItem_Click);
            // 
            // ConnectionsBindingSource
            // 
            this.ConnectionsBindingSource.AllowNew = false;
            this.ConnectionsBindingSource.DataSource = typeof(Bdt.Shared.Response.Connection);
            // 
            // SplitContainer
            // 
            this.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer.Name = "SplitContainer";
            this.SplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SplitContainer.Panel1
            // 
            this.SplitContainer.Panel1.Controls.Add(this.Sessions);
            // 
            // SplitContainer.Panel2
            // 
            this.SplitContainer.Panel2.Controls.Add(this.Connections);
            this.SplitContainer.Size = new System.Drawing.Size(513, 331);
            this.SplitContainer.SplitterDistance = 165;
            this.SplitContainer.TabIndex = 4;
            // 
            // CCidCol
            // 
            this.CCidCol.DataPropertyName = "Cid";
            this.CCidCol.HeaderText = "Cid";
            this.CCidCol.Name = "CCidCol";
            this.CCidCol.ReadOnly = true;
            // 
            // CHostCol
            // 
            this.CHostCol.DataPropertyName = "Host";
            this.CHostCol.HeaderText = "Host";
            this.CHostCol.Name = "CHostCol";
            this.CHostCol.ReadOnly = true;
            // 
            // CAddressCol
            // 
            this.CAddressCol.DataPropertyName = "Address";
            this.CAddressCol.HeaderText = "Address";
            this.CAddressCol.Name = "CAddressCol";
            this.CAddressCol.ReadOnly = true;
            // 
            // CPortCol
            // 
            this.CPortCol.DataPropertyName = "Port";
            this.CPortCol.HeaderText = "Port";
            this.CPortCol.Name = "CPortCol";
            this.CPortCol.ReadOnly = true;
            // 
            // CReadCountCol
            // 
            this.CReadCountCol.DataPropertyName = "ReadCount";
            this.CReadCountCol.HeaderText = "ReadCount";
            this.CReadCountCol.Name = "CReadCountCol";
            this.CReadCountCol.ReadOnly = true;
            // 
            // CWriteCountCol
            // 
            this.CWriteCountCol.DataPropertyName = "WriteCount";
            this.CWriteCountCol.HeaderText = "WriteCount";
            this.CWriteCountCol.Name = "CWriteCountCol";
            this.CWriteCountCol.ReadOnly = true;
            // 
            // CLastAccessCol
            // 
            this.CLastAccessCol.DataPropertyName = "LastAccess";
            this.CLastAccessCol.HeaderText = "LastAccess";
            this.CLastAccessCol.Name = "CLastAccessCol";
            this.CLastAccessCol.ReadOnly = true;
            // 
            // SSidCol
            // 
            this.SSidCol.DataPropertyName = "Sid";
            this.SSidCol.HeaderText = "Sid";
            this.SSidCol.Name = "SSidCol";
            this.SSidCol.ReadOnly = true;
            // 
            // SUsernameCol
            // 
            this.SUsernameCol.DataPropertyName = "Username";
            this.SUsernameCol.HeaderText = "Username";
            this.SUsernameCol.Name = "SUsernameCol";
            this.SUsernameCol.ReadOnly = true;
            // 
            // SAdminCol
            // 
            this.SAdminCol.DataPropertyName = "Admin";
            this.SAdminCol.HeaderText = "Admin";
            this.SAdminCol.Name = "SAdminCol";
            this.SAdminCol.ReadOnly = true;
            // 
            // SLogonCol
            // 
            this.SLogonCol.DataPropertyName = "Logon";
            this.SLogonCol.HeaderText = "Logon";
            this.SLogonCol.Name = "SLogonCol";
            this.SLogonCol.ReadOnly = true;
            // 
            // SLastAccessCol
            // 
            this.SLastAccessCol.DataPropertyName = "LastAccess";
            this.SLastAccessCol.HeaderText = "LastAccess";
            this.SLastAccessCol.Name = "SLastAccessCol";
            this.SLastAccessCol.ReadOnly = true;
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 363);
            this.Controls.Add(this.SplitContainer);
            this.Controls.Add(this.BottomPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AdminForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BoutDuTunnel (Client) - Administration ";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.AdminForm_Load);
            this.BottomPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Sessions)).EndInit();
            this.SessionsMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SessionsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Connections)).EndInit();
            this.ConnectionsMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ConnectionsBindingSource)).EndInit();
            this.SplitContainer.Panel1.ResumeLayout(false);
            this.SplitContainer.Panel2.ResumeLayout(false);
            this.SplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel BottomPanel;
        private System.Windows.Forms.Button Close;
        private System.Windows.Forms.DataGridView Sessions;
        private System.Windows.Forms.BindingSource SessionsBindingSource;
        private System.Windows.Forms.DataGridView Connections;
        private System.Windows.Forms.BindingSource ConnectionsBindingSource;
        private System.Windows.Forms.SplitContainer SplitContainer;
        private System.Windows.Forms.ContextMenuStrip SessionsMenu;
        private System.Windows.Forms.ToolStripMenuItem KillSessionItem;
        private System.Windows.Forms.ContextMenuStrip ConnectionsMenu;
        private System.Windows.Forms.ToolStripMenuItem KillConnectionItem;
        private System.Windows.Forms.Button Refresh;
        private System.Windows.Forms.DataGridViewTextBoxColumn CCidCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn CHostCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn CAddressCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn CPortCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn CReadCountCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn CWriteCountCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn CLastAccessCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn SSidCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn SUsernameCol;
        private System.Windows.Forms.DataGridViewCheckBoxColumn SAdminCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLogonCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLastAccessCol;
    }
}

