using Bdt.GuiClient.Resources;
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
            this.BtClose = new System.Windows.Forms.Button();
            this.BottomPanel = new System.Windows.Forms.Panel();
            this.BtRefresh = new System.Windows.Forms.Button();
            this.Sessions = new System.Windows.Forms.DataGridView();
            this.SSidCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SUsernameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SAdminCol = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SLogonCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SLastAccessCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SessionsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.KillSessionItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SessionsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Connections = new System.Windows.Forms.DataGridView();
            this.CCidCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CHostCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CAddressCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CPortCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CReadCountCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CWriteCountCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CLastAccessCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConnectionsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.KillConnectionItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ConnectionsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.SplitContainer = new System.Windows.Forms.SplitContainer();
            this.SessionsBox = new System.Windows.Forms.GroupBox();
            this.ConnectionsBox = new System.Windows.Forms.GroupBox();
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
            this.SessionsBox.SuspendLayout();
            this.ConnectionsBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtClose
            // 
            this.BtClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.BtClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtClose.Location = new System.Drawing.Point(554, 5);
            this.BtClose.Name = "BtClose";
            this.BtClose.Size = new System.Drawing.Size(75, 23);
            this.BtClose.TabIndex = 3;
            this.BtClose.Text = global::Bdt.GuiClient.Resources.Strings.ADMINFORM_CLOSE;
            this.BtClose.UseVisualStyleBackColor = true;
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.BtRefresh);
            this.BottomPanel.Controls.Add(this.BtClose);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 414);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(632, 32);
            this.BottomPanel.TabIndex = 1;
            // 
            // BtRefresh
            // 
            this.BtRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtRefresh.Location = new System.Drawing.Point(473, 5);
            this.BtRefresh.Name = "BtRefresh";
            this.BtRefresh.Size = new System.Drawing.Size(75, 23);
            this.BtRefresh.TabIndex = 2;
            this.BtRefresh.Text = global::Bdt.GuiClient.Resources.Strings.ADMINFORM_REFRESH;
            this.BtRefresh.UseVisualStyleBackColor = true;
            this.BtRefresh.Click += new System.EventHandler(this.Refresh_Click);
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
            this.Sessions.Location = new System.Drawing.Point(3, 16);
            this.Sessions.Name = "Sessions";
            this.Sessions.ReadOnly = true;
            this.Sessions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Sessions.Size = new System.Drawing.Size(626, 187);
            this.Sessions.TabIndex = 1;
            this.Sessions.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.Sessions_CellFormatting);
            this.Sessions.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Sessions_CellClick);
            // 
            // SSidCol
            // 
            this.SSidCol.DataPropertyName = "Sid";
            this.SSidCol.HeaderText = global::Bdt.GuiClient.Resources.Strings.ADMINFORM_SID;
            this.SSidCol.Name = "SSidCol";
            this.SSidCol.ReadOnly = true;
            // 
            // SUsernameCol
            // 
            this.SUsernameCol.DataPropertyName = "Username";
            this.SUsernameCol.HeaderText = global::Bdt.GuiClient.Resources.Strings.ADMINFORM_USER;
            this.SUsernameCol.Name = "SUsernameCol";
            this.SUsernameCol.ReadOnly = true;
            // 
            // SAdminCol
            // 
            this.SAdminCol.DataPropertyName = "Admin";
            this.SAdminCol.HeaderText = global::Bdt.GuiClient.Resources.Strings.ADMINFORM_ADMIN;
            this.SAdminCol.Name = "SAdminCol";
            this.SAdminCol.ReadOnly = true;
            // 
            // SLogonCol
            // 
            this.SLogonCol.DataPropertyName = "Logon";
            this.SLogonCol.HeaderText = global::Bdt.GuiClient.Resources.Strings.ADMINFORM_LOGON;
            this.SLogonCol.Name = "SLogonCol";
            this.SLogonCol.ReadOnly = true;
            // 
            // SLastAccessCol
            // 
            this.SLastAccessCol.DataPropertyName = "LastAccess";
            this.SLastAccessCol.HeaderText = global::Bdt.GuiClient.Resources.Strings.ADMINFORM_LAST_ACCESS;
            this.SLastAccessCol.Name = "SLastAccessCol";
            this.SLastAccessCol.ReadOnly = true;
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
            this.KillSessionItem.Text = global::Bdt.GuiClient.Resources.Strings.ADMINFORM_KILL_SESSION;
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
            this.Connections.Location = new System.Drawing.Point(3, 16);
            this.Connections.Name = "Connections";
            this.Connections.ReadOnly = true;
            this.Connections.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Connections.Size = new System.Drawing.Size(626, 185);
            this.Connections.TabIndex = 2;
            // 
            // CCidCol
            // 
            this.CCidCol.DataPropertyName = "Cid";
            this.CCidCol.HeaderText = global::Bdt.GuiClient.Resources.Strings.ADMINFORM_CID;
            this.CCidCol.Name = "CCidCol";
            this.CCidCol.ReadOnly = true;
            // 
            // CHostCol
            // 
            this.CHostCol.DataPropertyName = "Host";
            this.CHostCol.HeaderText = global::Bdt.GuiClient.Resources.Strings.ADMINFORM_HOST;
            this.CHostCol.Name = "CHostCol";
            this.CHostCol.ReadOnly = true;
            // 
            // CAddressCol
            // 
            this.CAddressCol.DataPropertyName = "Address";
            this.CAddressCol.HeaderText = global::Bdt.GuiClient.Resources.Strings.ADMINFORM_ADDRESS;
            this.CAddressCol.Name = "CAddressCol";
            this.CAddressCol.ReadOnly = true;
            // 
            // CPortCol
            // 
            this.CPortCol.DataPropertyName = "Port";
            this.CPortCol.HeaderText = Strings.ADMINFORM_PORT;
            this.CPortCol.Name = "CPortCol";
            this.CPortCol.ReadOnly = true;
            // 
            // CReadCountCol
            // 
            this.CReadCountCol.DataPropertyName = "ReadCount";
            this.CReadCountCol.HeaderText = global::Bdt.GuiClient.Resources.Strings.ADMINFORM_READ_COUNT;
            this.CReadCountCol.Name = "CReadCountCol";
            this.CReadCountCol.ReadOnly = true;
            // 
            // CWriteCountCol
            // 
            this.CWriteCountCol.DataPropertyName = "WriteCount";
            this.CWriteCountCol.HeaderText = global::Bdt.GuiClient.Resources.Strings.ADMINFORM_WRITE_COUNT;
            this.CWriteCountCol.Name = "CWriteCountCol";
            this.CWriteCountCol.ReadOnly = true;
            // 
            // CLastAccessCol
            // 
            this.CLastAccessCol.DataPropertyName = "LastAccess";
            this.CLastAccessCol.HeaderText = global::Bdt.GuiClient.Resources.Strings.ADMINFORM_LAST_ACCESS;
            this.CLastAccessCol.Name = "CLastAccessCol";
            this.CLastAccessCol.ReadOnly = true;
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
            this.KillConnectionItem.Text = global::Bdt.GuiClient.Resources.Strings.ADMINFORM_KILL_CONNECTION;
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
            this.SplitContainer.Panel1.Controls.Add(this.SessionsBox);
            // 
            // SplitContainer.Panel2
            // 
            this.SplitContainer.Panel2.Controls.Add(this.ConnectionsBox);
            this.SplitContainer.Size = new System.Drawing.Size(632, 414);
            this.SplitContainer.SplitterDistance = 206;
            this.SplitContainer.TabIndex = 0;
            // 
            // SessionsBox
            // 
            this.SessionsBox.Controls.Add(this.Sessions);
            this.SessionsBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SessionsBox.Location = new System.Drawing.Point(0, 0);
            this.SessionsBox.Name = "SessionsBox";
            this.SessionsBox.Size = new System.Drawing.Size(632, 206);
            this.SessionsBox.TabIndex = 3;
            this.SessionsBox.TabStop = false;
            this.SessionsBox.Text = Strings.ADMINFORM_SESSIONS;
            // 
            // ConnectionsBox
            // 
            this.ConnectionsBox.Controls.Add(this.Connections);
            this.ConnectionsBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConnectionsBox.Location = new System.Drawing.Point(0, 0);
            this.ConnectionsBox.Name = "ConnectionsBox";
            this.ConnectionsBox.Size = new System.Drawing.Size(632, 204);
            this.ConnectionsBox.TabIndex = 4;
            this.ConnectionsBox.TabStop = false;
            this.ConnectionsBox.Text = Strings.ADMINFORM_CONNECTIONS;
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 446);
            this.Controls.Add(this.SplitContainer);
            this.Controls.Add(this.BottomPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(320, 200);
            this.Name = "AdminForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = Strings.ADMINFORM_TITLE;
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
            this.SessionsBox.ResumeLayout(false);
            this.ConnectionsBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel BottomPanel;
        private System.Windows.Forms.Button BtClose;
        private System.Windows.Forms.DataGridView Sessions;
        private System.Windows.Forms.BindingSource SessionsBindingSource;
        private System.Windows.Forms.DataGridView Connections;
        private System.Windows.Forms.BindingSource ConnectionsBindingSource;
        private System.Windows.Forms.SplitContainer SplitContainer;
        private System.Windows.Forms.ContextMenuStrip SessionsMenu;
        private System.Windows.Forms.ToolStripMenuItem KillSessionItem;
        private System.Windows.Forms.ContextMenuStrip ConnectionsMenu;
        private System.Windows.Forms.ToolStripMenuItem KillConnectionItem;
        private System.Windows.Forms.Button BtRefresh;
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
        private System.Windows.Forms.GroupBox SessionsBox;
        private System.Windows.Forms.GroupBox ConnectionsBox;
    }
}

