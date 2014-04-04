namespace Bdt.GuiClient.Forms
{
    partial class SetupForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupForm));
			this.Tabs = new System.Windows.Forms.TabControl();
			this.ServicePage = new System.Windows.Forms.TabPage();
			this.ServicePortEdit = new System.Windows.Forms.NumericUpDown();
			this.ServicePasswordEdit = new System.Windows.Forms.TextBox();
			this.ServicePasswordLabel = new System.Windows.Forms.Label();
			this.ServiceUserEdit = new System.Windows.Forms.TextBox();
			this.ServiceUserNameLabel = new System.Windows.Forms.Label();
			this.ServicePortLabel = new System.Windows.Forms.Label();
			this.ServiceAddressEdit = new System.Windows.Forms.TextBox();
			this.ServiceAddressLabel = new System.Windows.Forms.Label();
			this.ServiceProtocolLabel = new System.Windows.Forms.Label();
			this.ServiceNameEdit = new System.Windows.Forms.TextBox();
			this.ServiceProtocolEdit = new System.Windows.Forms.ComboBox();
			this.ServiceNameLabel = new System.Windows.Forms.Label();
			this.SocksPage = new System.Windows.Forms.TabPage();
			this.SocksPortEdit = new System.Windows.Forms.NumericUpDown();
			this.SocksPortLabel = new System.Windows.Forms.Label();
			this.SocksSharedEdit = new System.Windows.Forms.CheckBox();
			this.SocksEnabledEdit = new System.Windows.Forms.CheckBox();
			this.ProxyPage = new System.Windows.Forms.TabPage();
			this.ConfigBox = new System.Windows.Forms.GroupBox();
			this.ProxyPortEdit = new System.Windows.Forms.NumericUpDown();
			this.ProxyPortLabel = new System.Windows.Forms.Label();
			this.ProxyAddressEdit = new System.Windows.Forms.TextBox();
			this.ProxyAddressLabel = new System.Windows.Forms.Label();
			this.ProxyAutoConfigurationEdit = new System.Windows.Forms.CheckBox();
			this.AuthBox = new System.Windows.Forms.GroupBox();
			this.ProxyDomainEdit = new System.Windows.Forms.TextBox();
			this.ProxyDomainLabel = new System.Windows.Forms.Label();
			this.ProxyPasswordEdit = new System.Windows.Forms.TextBox();
			this.ProxyPasswordLabel = new System.Windows.Forms.Label();
			this.ProxyUserNameEdit = new System.Windows.Forms.TextBox();
			this.ProxyUserNameLabel = new System.Windows.Forms.Label();
			this.ProxyAuthenticationEdit = new System.Windows.Forms.CheckBox();
			this.ProxyEnabledEdit = new System.Windows.Forms.CheckBox();
			this.ForwardsPage = new System.Windows.Forms.TabPage();
			this.BindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
			this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
			this.BindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
			this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
			this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
			this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
			this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
			this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
			this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
			this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.PortForwardDataGridView = new System.Windows.Forms.DataGridView();
			this.PortForwardLocalPort = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.PortForwardEnabledEdit = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.PortForwardSharedEdit = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.PortForwardAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.PortForwardRemotePort = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.LogsPage = new System.Windows.Forms.TabPage();
			this.FileLogBox = new System.Windows.Forms.GroupBox();
			this.FileLogAppendEdit = new System.Windows.Forms.CheckBox();
			this.FileSearch = new System.Windows.Forms.Button();
			this.FileLogNameEdit = new System.Windows.Forms.TextBox();
			this.FileLogNameLabel = new System.Windows.Forms.Label();
			this.FileLogDateFormatEdit = new System.Windows.Forms.TextBox();
			this.FileLogDateFormatLabel = new System.Windows.Forms.Label();
			this.FileLogStringFormatEdit = new System.Windows.Forms.TextBox();
			this.FileLogStringFormatLabel = new System.Windows.Forms.Label();
			this.FileLogFilterLabel = new System.Windows.Forms.Label();
			this.FileLogFilterEdit = new System.Windows.Forms.ComboBox();
			this.FileLogEnabledEdit = new System.Windows.Forms.CheckBox();
			this.ConsoleLogBox = new System.Windows.Forms.GroupBox();
			this.ConsoleLogDateFormatEdit = new System.Windows.Forms.TextBox();
			this.ConsoleLogDateFormatLabel = new System.Windows.Forms.Label();
			this.ConsoleLogStringFormatEdit = new System.Windows.Forms.TextBox();
			this.ConsoleLogStringFormatLabel = new System.Windows.Forms.Label();
			this.ConsoleLogFilterLabel = new System.Windows.Forms.Label();
			this.ConsoleLogFilterEdit = new System.Windows.Forms.ComboBox();
			this.ConsoleLogEnabledEdit = new System.Windows.Forms.CheckBox();
			this.Cancel = new System.Windows.Forms.Button();
			this.BottomPanel = new System.Windows.Forms.Panel();
			this.Apply = new System.Windows.Forms.Button();
			this.SaveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.Proxy100Continue = new System.Windows.Forms.CheckBox();
			this.Tabs.SuspendLayout();
			this.ServicePage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ServicePortEdit)).BeginInit();
			this.SocksPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.SocksPortEdit)).BeginInit();
			this.ProxyPage.SuspendLayout();
			this.ConfigBox.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.ProxyPortEdit)).BeginInit();
			this.AuthBox.SuspendLayout();
			this.ForwardsPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.BindingNavigator)).BeginInit();
			this.BindingNavigator.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.BindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.PortForwardDataGridView)).BeginInit();
			this.LogsPage.SuspendLayout();
			this.FileLogBox.SuspendLayout();
			this.ConsoleLogBox.SuspendLayout();
			this.BottomPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// Tabs
			// 
			this.Tabs.Controls.Add(this.ServicePage);
			this.Tabs.Controls.Add(this.SocksPage);
			this.Tabs.Controls.Add(this.ProxyPage);
			this.Tabs.Controls.Add(this.ForwardsPage);
			this.Tabs.Controls.Add(this.LogsPage);
			this.Tabs.ItemSize = new System.Drawing.Size(64, 24);
			this.Tabs.Location = new System.Drawing.Point(0, 1);
			this.Tabs.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Tabs.Name = "Tabs";
			this.Tabs.SelectedIndex = 0;
			this.Tabs.Size = new System.Drawing.Size(683, 402);
			this.Tabs.TabIndex = 0;
			// 
			// ServicePage
			// 
			this.ServicePage.Controls.Add(this.ServicePortEdit);
			this.ServicePage.Controls.Add(this.ServicePasswordEdit);
			this.ServicePage.Controls.Add(this.ServicePasswordLabel);
			this.ServicePage.Controls.Add(this.ServiceUserEdit);
			this.ServicePage.Controls.Add(this.ServiceUserNameLabel);
			this.ServicePage.Controls.Add(this.ServicePortLabel);
			this.ServicePage.Controls.Add(this.ServiceAddressEdit);
			this.ServicePage.Controls.Add(this.ServiceAddressLabel);
			this.ServicePage.Controls.Add(this.ServiceProtocolLabel);
			this.ServicePage.Controls.Add(this.ServiceNameEdit);
			this.ServicePage.Controls.Add(this.ServiceProtocolEdit);
			this.ServicePage.Controls.Add(this.ServiceNameLabel);
			this.ServicePage.ImageIndex = 0;
			this.ServicePage.Location = new System.Drawing.Point(4, 28);
			this.ServicePage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.ServicePage.Name = "ServicePage";
			this.ServicePage.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.ServicePage.Size = new System.Drawing.Size(675, 370);
			this.ServicePage.TabIndex = 0;
			this.ServicePage.Text = global::Bdt.GuiClient.Resources.Strings.SETUPFORM_TUNNEL;
			this.ServicePage.UseVisualStyleBackColor = true;
			// 
			// ServicePortEdit
			// 
			this.ServicePortEdit.Location = new System.Drawing.Point(128, 98);
			this.ServicePortEdit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.ServicePortEdit.Name = "ServicePortEdit";
			this.ServicePortEdit.Size = new System.Drawing.Size(85, 22);
			this.ServicePortEdit.TabIndex = 3;
			// 
			// ServicePasswordEdit
			// 
			this.ServicePasswordEdit.Location = new System.Drawing.Point(128, 156);
			this.ServicePasswordEdit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.ServicePasswordEdit.Name = "ServicePasswordEdit";
			this.ServicePasswordEdit.PasswordChar = '*';
			this.ServicePasswordEdit.Size = new System.Drawing.Size(255, 22);
			this.ServicePasswordEdit.TabIndex = 5;
			// 
			// ServicePasswordLabel
			// 
			this.ServicePasswordLabel.AutoSize = true;
			this.ServicePasswordLabel.Location = new System.Drawing.Point(11, 162);
			this.ServicePasswordLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ServicePasswordLabel.Name = "ServicePasswordLabel";
			this.ServicePasswordLabel.Size = new System.Drawing.Size(69, 17);
			this.ServicePasswordLabel.TabIndex = 12;
			this.ServicePasswordLabel.Text = "Password";
			// 
			// ServiceUserEdit
			// 
			this.ServiceUserEdit.Location = new System.Drawing.Point(128, 127);
			this.ServiceUserEdit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.ServiceUserEdit.Name = "ServiceUserEdit";
			this.ServiceUserEdit.Size = new System.Drawing.Size(255, 22);
			this.ServiceUserEdit.TabIndex = 4;
			// 
			// ServiceUserNameLabel
			// 
			this.ServiceUserNameLabel.AutoSize = true;
			this.ServiceUserNameLabel.Location = new System.Drawing.Point(11, 132);
			this.ServiceUserNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ServiceUserNameLabel.Name = "ServiceUserNameLabel";
			this.ServiceUserNameLabel.Size = new System.Drawing.Size(38, 17);
			this.ServiceUserNameLabel.TabIndex = 10;
			this.ServiceUserNameLabel.Text = "User";
			// 
			// ServicePortLabel
			// 
			this.ServicePortLabel.AutoSize = true;
			this.ServicePortLabel.Location = new System.Drawing.Point(11, 103);
			this.ServicePortLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ServicePortLabel.Name = "ServicePortLabel";
			this.ServicePortLabel.Size = new System.Drawing.Size(34, 17);
			this.ServicePortLabel.TabIndex = 9;
			this.ServicePortLabel.Text = "Port";
			// 
			// ServiceAddressEdit
			// 
			this.ServiceAddressEdit.Location = new System.Drawing.Point(128, 69);
			this.ServiceAddressEdit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.ServiceAddressEdit.Name = "ServiceAddressEdit";
			this.ServiceAddressEdit.Size = new System.Drawing.Size(255, 22);
			this.ServiceAddressEdit.TabIndex = 2;
			// 
			// ServiceAddressLabel
			// 
			this.ServiceAddressLabel.AutoSize = true;
			this.ServiceAddressLabel.Location = new System.Drawing.Point(11, 74);
			this.ServiceAddressLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ServiceAddressLabel.Name = "ServiceAddressLabel";
			this.ServiceAddressLabel.Size = new System.Drawing.Size(60, 17);
			this.ServiceAddressLabel.TabIndex = 7;
			this.ServiceAddressLabel.Text = "Address";
			// 
			// ServiceProtocolLabel
			// 
			this.ServiceProtocolLabel.AutoSize = true;
			this.ServiceProtocolLabel.Location = new System.Drawing.Point(11, 43);
			this.ServiceProtocolLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ServiceProtocolLabel.Name = "ServiceProtocolLabel";
			this.ServiceProtocolLabel.Size = new System.Drawing.Size(60, 17);
			this.ServiceProtocolLabel.TabIndex = 6;
			this.ServiceProtocolLabel.Text = "Protocol";
			// 
			// ServiceNameEdit
			// 
			this.ServiceNameEdit.Location = new System.Drawing.Point(128, 10);
			this.ServiceNameEdit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.ServiceNameEdit.Name = "ServiceNameEdit";
			this.ServiceNameEdit.Size = new System.Drawing.Size(255, 22);
			this.ServiceNameEdit.TabIndex = 0;
			// 
			// ServiceProtocolEdit
			// 
			this.ServiceProtocolEdit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ServiceProtocolEdit.FormattingEnabled = true;
			this.ServiceProtocolEdit.Location = new System.Drawing.Point(128, 38);
			this.ServiceProtocolEdit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.ServiceProtocolEdit.Name = "ServiceProtocolEdit";
			this.ServiceProtocolEdit.Size = new System.Drawing.Size(255, 24);
			this.ServiceProtocolEdit.TabIndex = 1;
			// 
			// ServiceNameLabel
			// 
			this.ServiceNameLabel.AutoSize = true;
			this.ServiceNameLabel.Location = new System.Drawing.Point(11, 15);
			this.ServiceNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ServiceNameLabel.Name = "ServiceNameLabel";
			this.ServiceNameLabel.Size = new System.Drawing.Size(45, 17);
			this.ServiceNameLabel.TabIndex = 0;
			this.ServiceNameLabel.Text = "Name";
			// 
			// SocksPage
			// 
			this.SocksPage.Controls.Add(this.SocksPortEdit);
			this.SocksPage.Controls.Add(this.SocksPortLabel);
			this.SocksPage.Controls.Add(this.SocksSharedEdit);
			this.SocksPage.Controls.Add(this.SocksEnabledEdit);
			this.SocksPage.Location = new System.Drawing.Point(4, 28);
			this.SocksPage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.SocksPage.Name = "SocksPage";
			this.SocksPage.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.SocksPage.Size = new System.Drawing.Size(675, 370);
			this.SocksPage.TabIndex = 1;
			this.SocksPage.Text = global::Bdt.GuiClient.Resources.Strings.SETUPFORM_SOCKS;
			this.SocksPage.UseVisualStyleBackColor = true;
			// 
			// SocksPortEdit
			// 
			this.SocksPortEdit.Location = new System.Drawing.Point(64, 69);
			this.SocksPortEdit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.SocksPortEdit.Name = "SocksPortEdit";
			this.SocksPortEdit.Size = new System.Drawing.Size(85, 22);
			this.SocksPortEdit.TabIndex = 2;
			// 
			// SocksPortLabel
			// 
			this.SocksPortLabel.AutoSize = true;
			this.SocksPortLabel.Location = new System.Drawing.Point(11, 74);
			this.SocksPortLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.SocksPortLabel.Name = "SocksPortLabel";
			this.SocksPortLabel.Size = new System.Drawing.Size(34, 17);
			this.SocksPortLabel.TabIndex = 11;
			this.SocksPortLabel.Text = "Port";
			// 
			// SocksSharedEdit
			// 
			this.SocksSharedEdit.AutoSize = true;
			this.SocksSharedEdit.Location = new System.Drawing.Point(11, 43);
			this.SocksSharedEdit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.SocksSharedEdit.Name = "SocksSharedEdit";
			this.SocksSharedEdit.Size = new System.Drawing.Size(154, 21);
			this.SocksSharedEdit.TabIndex = 1;
			this.SocksSharedEdit.Text = global::Bdt.GuiClient.Resources.Strings.SETUPFORM_SOCKS_SERVER_SHARED;
			this.SocksSharedEdit.UseVisualStyleBackColor = true;
			// 
			// SocksEnabledEdit
			// 
			this.SocksEnabledEdit.AutoSize = true;
			this.SocksEnabledEdit.Location = new System.Drawing.Point(11, 15);
			this.SocksEnabledEdit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.SocksEnabledEdit.Name = "SocksEnabledEdit";
			this.SocksEnabledEdit.Size = new System.Drawing.Size(160, 21);
			this.SocksEnabledEdit.TabIndex = 0;
			this.SocksEnabledEdit.Text = global::Bdt.GuiClient.Resources.Strings.SETUPFORM_SOCKS_SERVER_ENABLED;
			this.SocksEnabledEdit.UseVisualStyleBackColor = true;
			// 
			// ProxyPage
			// 
			this.ProxyPage.Controls.Add(this.Proxy100Continue);
			this.ProxyPage.Controls.Add(this.ConfigBox);
			this.ProxyPage.Controls.Add(this.AuthBox);
			this.ProxyPage.Controls.Add(this.ProxyEnabledEdit);
			this.ProxyPage.Location = new System.Drawing.Point(4, 28);
			this.ProxyPage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.ProxyPage.Name = "ProxyPage";
			this.ProxyPage.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.ProxyPage.Size = new System.Drawing.Size(675, 370);
			this.ProxyPage.TabIndex = 2;
			this.ProxyPage.Text = global::Bdt.GuiClient.Resources.Strings.SETUPFORM_HTTP_PROXY;
			this.ProxyPage.UseVisualStyleBackColor = true;
			// 
			// ConfigBox
			// 
			this.ConfigBox.Controls.Add(this.ProxyPortEdit);
			this.ConfigBox.Controls.Add(this.ProxyPortLabel);
			this.ConfigBox.Controls.Add(this.ProxyAddressEdit);
			this.ConfigBox.Controls.Add(this.ProxyAddressLabel);
			this.ConfigBox.Controls.Add(this.ProxyAutoConfigurationEdit);
			this.ConfigBox.Location = new System.Drawing.Point(11, 230);
			this.ConfigBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.ConfigBox.Name = "ConfigBox";
			this.ConfigBox.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.ConfigBox.Size = new System.Drawing.Size(395, 116);
			this.ConfigBox.TabIndex = 2;
			this.ConfigBox.TabStop = false;
			this.ConfigBox.Text = "Configuration";
			// 
			// ProxyPortEdit
			// 
			this.ProxyPortEdit.Location = new System.Drawing.Point(127, 76);
			this.ProxyPortEdit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.ProxyPortEdit.Name = "ProxyPortEdit";
			this.ProxyPortEdit.Size = new System.Drawing.Size(85, 22);
			this.ProxyPortEdit.TabIndex = 2;
			// 
			// ProxyPortLabel
			// 
			this.ProxyPortLabel.AutoSize = true;
			this.ProxyPortLabel.Location = new System.Drawing.Point(9, 82);
			this.ProxyPortLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ProxyPortLabel.Name = "ProxyPortLabel";
			this.ProxyPortLabel.Size = new System.Drawing.Size(34, 17);
			this.ProxyPortLabel.TabIndex = 13;
			this.ProxyPortLabel.Text = "Port";
			// 
			// ProxyAddressEdit
			// 
			this.ProxyAddressEdit.Location = new System.Drawing.Point(127, 47);
			this.ProxyAddressEdit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.ProxyAddressEdit.Name = "ProxyAddressEdit";
			this.ProxyAddressEdit.Size = new System.Drawing.Size(255, 22);
			this.ProxyAddressEdit.TabIndex = 1;
			// 
			// ProxyAddressLabel
			// 
			this.ProxyAddressLabel.AutoSize = true;
			this.ProxyAddressLabel.Location = new System.Drawing.Point(9, 52);
			this.ProxyAddressLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ProxyAddressLabel.Name = "ProxyAddressLabel";
			this.ProxyAddressLabel.Size = new System.Drawing.Size(60, 17);
			this.ProxyAddressLabel.TabIndex = 11;
			this.ProxyAddressLabel.Text = "Address";
			// 
			// ProxyAutoConfigurationEdit
			// 
			this.ProxyAutoConfigurationEdit.AutoSize = true;
			this.ProxyAutoConfigurationEdit.Location = new System.Drawing.Point(8, 23);
			this.ProxyAutoConfigurationEdit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.ProxyAutoConfigurationEdit.Name = "ProxyAutoConfigurationEdit";
			this.ProxyAutoConfigurationEdit.Size = new System.Drawing.Size(59, 21);
			this.ProxyAutoConfigurationEdit.TabIndex = 0;
			this.ProxyAutoConfigurationEdit.Text = global::Bdt.GuiClient.Resources.Strings.SETUPFORM_AUTO;
			this.ProxyAutoConfigurationEdit.UseVisualStyleBackColor = true;
			this.ProxyAutoConfigurationEdit.CheckedChanged += new System.EventHandler(this.ProxyAutoConfigurationEdit_CheckedChanged);
			// 
			// AuthBox
			// 
			this.AuthBox.Controls.Add(this.ProxyDomainEdit);
			this.AuthBox.Controls.Add(this.ProxyDomainLabel);
			this.AuthBox.Controls.Add(this.ProxyPasswordEdit);
			this.AuthBox.Controls.Add(this.ProxyPasswordLabel);
			this.AuthBox.Controls.Add(this.ProxyUserNameEdit);
			this.AuthBox.Controls.Add(this.ProxyUserNameLabel);
			this.AuthBox.Controls.Add(this.ProxyAuthenticationEdit);
			this.AuthBox.Location = new System.Drawing.Point(11, 74);
			this.AuthBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.AuthBox.Name = "AuthBox";
			this.AuthBox.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.AuthBox.Size = new System.Drawing.Size(395, 144);
			this.AuthBox.TabIndex = 1;
			this.AuthBox.TabStop = false;
			this.AuthBox.Text = "Authentication";
			// 
			// ProxyDomainEdit
			// 
			this.ProxyDomainEdit.Location = new System.Drawing.Point(127, 106);
			this.ProxyDomainEdit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.ProxyDomainEdit.Name = "ProxyDomainEdit";
			this.ProxyDomainEdit.Size = new System.Drawing.Size(255, 22);
			this.ProxyDomainEdit.TabIndex = 3;
			// 
			// ProxyDomainLabel
			// 
			this.ProxyDomainLabel.AutoSize = true;
			this.ProxyDomainLabel.Location = new System.Drawing.Point(9, 112);
			this.ProxyDomainLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ProxyDomainLabel.Name = "ProxyDomainLabel";
			this.ProxyDomainLabel.Size = new System.Drawing.Size(56, 17);
			this.ProxyDomainLabel.TabIndex = 18;
			this.ProxyDomainLabel.Text = "Domain";
			// 
			// ProxyPasswordEdit
			// 
			this.ProxyPasswordEdit.Location = new System.Drawing.Point(127, 76);
			this.ProxyPasswordEdit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.ProxyPasswordEdit.Name = "ProxyPasswordEdit";
			this.ProxyPasswordEdit.PasswordChar = '*';
			this.ProxyPasswordEdit.Size = new System.Drawing.Size(255, 22);
			this.ProxyPasswordEdit.TabIndex = 2;
			// 
			// ProxyPasswordLabel
			// 
			this.ProxyPasswordLabel.AutoSize = true;
			this.ProxyPasswordLabel.Location = new System.Drawing.Point(9, 82);
			this.ProxyPasswordLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ProxyPasswordLabel.Name = "ProxyPasswordLabel";
			this.ProxyPasswordLabel.Size = new System.Drawing.Size(69, 17);
			this.ProxyPasswordLabel.TabIndex = 16;
			this.ProxyPasswordLabel.Text = "Password";
			// 
			// ProxyUserNameEdit
			// 
			this.ProxyUserNameEdit.Location = new System.Drawing.Point(127, 47);
			this.ProxyUserNameEdit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.ProxyUserNameEdit.Name = "ProxyUserNameEdit";
			this.ProxyUserNameEdit.Size = new System.Drawing.Size(255, 22);
			this.ProxyUserNameEdit.TabIndex = 1;
			// 
			// ProxyUserNameLabel
			// 
			this.ProxyUserNameLabel.AutoSize = true;
			this.ProxyUserNameLabel.Location = new System.Drawing.Point(9, 52);
			this.ProxyUserNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ProxyUserNameLabel.Name = "ProxyUserNameLabel";
			this.ProxyUserNameLabel.Size = new System.Drawing.Size(38, 17);
			this.ProxyUserNameLabel.TabIndex = 14;
			this.ProxyUserNameLabel.Text = "User";
			// 
			// ProxyAuthenticationEdit
			// 
			this.ProxyAuthenticationEdit.AutoSize = true;
			this.ProxyAuthenticationEdit.Location = new System.Drawing.Point(8, 23);
			this.ProxyAuthenticationEdit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.ProxyAuthenticationEdit.Name = "ProxyAuthenticationEdit";
			this.ProxyAuthenticationEdit.Size = new System.Drawing.Size(59, 21);
			this.ProxyAuthenticationEdit.TabIndex = 0;
			this.ProxyAuthenticationEdit.Text = global::Bdt.GuiClient.Resources.Strings.SETUPFORM_AUTO;
			this.ProxyAuthenticationEdit.UseVisualStyleBackColor = true;
			this.ProxyAuthenticationEdit.CheckedChanged += new System.EventHandler(this.ProxyAuthenticationEdit_CheckedChanged);
			// 
			// ProxyEnabledEdit
			// 
			this.ProxyEnabledEdit.AutoSize = true;
			this.ProxyEnabledEdit.Location = new System.Drawing.Point(11, 15);
			this.ProxyEnabledEdit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.ProxyEnabledEdit.Name = "ProxyEnabledEdit";
			this.ProxyEnabledEdit.Size = new System.Drawing.Size(214, 21);
			this.ProxyEnabledEdit.TabIndex = 0;
			this.ProxyEnabledEdit.Text = global::Bdt.GuiClient.Resources.Strings.SETUPFORM_CONNECT_THROUGH_PROXY;
			this.ProxyEnabledEdit.UseVisualStyleBackColor = true;
			// 
			// ForwardsPage
			// 
			this.ForwardsPage.Controls.Add(this.BindingNavigator);
			this.ForwardsPage.Controls.Add(this.PortForwardDataGridView);
			this.ForwardsPage.Location = new System.Drawing.Point(4, 28);
			this.ForwardsPage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.ForwardsPage.Name = "ForwardsPage";
			this.ForwardsPage.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.ForwardsPage.Size = new System.Drawing.Size(675, 370);
			this.ForwardsPage.TabIndex = 3;
			this.ForwardsPage.Text = global::Bdt.GuiClient.Resources.Strings.SETUPFORM_FORWARDS;
			this.ForwardsPage.UseVisualStyleBackColor = true;
			// 
			// BindingNavigator
			// 
			this.BindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
			this.BindingNavigator.BindingSource = this.BindingSource;
			this.BindingNavigator.CountItem = this.bindingNavigatorCountItem;
			this.BindingNavigator.DeleteItem = this.bindingNavigatorDeleteItem;
			this.BindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem});
			this.BindingNavigator.Location = new System.Drawing.Point(4, 4);
			this.BindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
			this.BindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
			this.BindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
			this.BindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
			this.BindingNavigator.Name = "BindingNavigator";
			this.BindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
			this.BindingNavigator.Size = new System.Drawing.Size(667, 27);
			this.BindingNavigator.TabIndex = 5;
			this.BindingNavigator.Text = "BindingNavigator";
			// 
			// bindingNavigatorAddNewItem
			// 
			this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
			this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
			this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
			this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 24);
			this.bindingNavigatorAddNewItem.Text = global::Bdt.GuiClient.Resources.Strings.SETUPFORM_ADD_NEW;
			// 
			// bindingNavigatorCountItem
			// 
			this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
			this.bindingNavigatorCountItem.Size = new System.Drawing.Size(45, 24);
			this.bindingNavigatorCountItem.Text = global::Bdt.GuiClient.Resources.Strings.SETUPFORM_ITEM_COUNT_OF;
			this.bindingNavigatorCountItem.ToolTipText = global::Bdt.GuiClient.Resources.Strings.SETUPFORM_ITEM_COUNT;
			// 
			// bindingNavigatorDeleteItem
			// 
			this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
			this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
			this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
			this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 24);
			this.bindingNavigatorDeleteItem.Text = global::Bdt.GuiClient.Resources.Strings.SETUPFORM_DELETE;
			// 
			// bindingNavigatorMoveFirstItem
			// 
			this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
			this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
			this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
			this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 24);
			this.bindingNavigatorMoveFirstItem.Text = global::Bdt.GuiClient.Resources.Strings.SETUPFORM_MOVE_FIRST;
			// 
			// bindingNavigatorMovePreviousItem
			// 
			this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
			this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
			this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
			this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 24);
			this.bindingNavigatorMovePreviousItem.Text = global::Bdt.GuiClient.Resources.Strings.SETUPFORM_MOVE_UP;
			// 
			// bindingNavigatorSeparator
			// 
			this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
			this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 27);
			// 
			// bindingNavigatorPositionItem
			// 
			this.bindingNavigatorPositionItem.AccessibleName = "Position";
			this.bindingNavigatorPositionItem.AutoSize = false;
			this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
			this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(65, 27);
			this.bindingNavigatorPositionItem.Text = "0";
			this.bindingNavigatorPositionItem.ToolTipText = global::Bdt.GuiClient.Resources.Strings.SETUPFORM_CURRENT_POSITION;
			// 
			// bindingNavigatorSeparator1
			// 
			this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
			this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 27);
			// 
			// bindingNavigatorMoveNextItem
			// 
			this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
			this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
			this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
			this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 24);
			this.bindingNavigatorMoveNextItem.Text = global::Bdt.GuiClient.Resources.Strings.SETUPFORM_MOVE_DOWN;
			// 
			// bindingNavigatorMoveLastItem
			// 
			this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
			this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
			this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
			this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 24);
			this.bindingNavigatorMoveLastItem.Text = global::Bdt.GuiClient.Resources.Strings.SETUPFORM_MOVE_LAST;
			// 
			// bindingNavigatorSeparator2
			// 
			this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
			this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 27);
			// 
			// PortForwardDataGridView
			// 
			this.PortForwardDataGridView.AutoGenerateColumns = false;
			this.PortForwardDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
			this.PortForwardDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.PortForwardDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PortForwardLocalPort,
            this.PortForwardEnabledEdit,
            this.PortForwardSharedEdit,
            this.PortForwardAddress,
            this.PortForwardRemotePort});
			this.PortForwardDataGridView.DataSource = this.BindingSource;
			this.PortForwardDataGridView.Location = new System.Drawing.Point(4, 38);
			this.PortForwardDataGridView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.PortForwardDataGridView.Name = "PortForwardDataGridView";
			this.PortForwardDataGridView.Size = new System.Drawing.Size(664, 329);
			this.PortForwardDataGridView.TabIndex = 4;
			this.PortForwardDataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.PortForwardDataGridView_DataError);
			// 
			// PortForwardLocalPort
			// 
			this.PortForwardLocalPort.DataPropertyName = "LocalPort";
			this.PortForwardLocalPort.HeaderText = global::Bdt.GuiClient.Resources.Strings.SETUPFORM_LOCAL_PORT;
			this.PortForwardLocalPort.Name = "PortForwardLocalPort";
			this.PortForwardLocalPort.Width = 96;
			// 
			// PortForwardEnabledEdit
			// 
			this.PortForwardEnabledEdit.DataPropertyName = "Enabled";
			this.PortForwardEnabledEdit.HeaderText = global::Bdt.GuiClient.Resources.Strings.SETUPFORM_ENABLED;
			this.PortForwardEnabledEdit.Name = "PortForwardEnabledEdit";
			this.PortForwardEnabledEdit.Width = 66;
			// 
			// PortForwardSharedEdit
			// 
			this.PortForwardSharedEdit.DataPropertyName = "Shared";
			this.PortForwardSharedEdit.HeaderText = global::Bdt.GuiClient.Resources.Strings.SETUPFORM_SHARED;
			this.PortForwardSharedEdit.Name = "PortForwardSharedEdit";
			this.PortForwardSharedEdit.Width = 60;
			// 
			// PortForwardAddress
			// 
			this.PortForwardAddress.DataPropertyName = "Address";
			this.PortForwardAddress.HeaderText = global::Bdt.GuiClient.Resources.Strings.SETUPFORM_ADDRESS;
			this.PortForwardAddress.MinimumWidth = 190;
			this.PortForwardAddress.Name = "PortForwardAddress";
			this.PortForwardAddress.Width = 190;
			// 
			// PortForwardRemotePort
			// 
			this.PortForwardRemotePort.DataPropertyName = "RemotePort";
			this.PortForwardRemotePort.HeaderText = global::Bdt.GuiClient.Resources.Strings.SETUPFORM_REMOTE_PORT;
			this.PortForwardRemotePort.Name = "PortForwardRemotePort";
			this.PortForwardRemotePort.Width = 111;
			// 
			// LogsPage
			// 
			this.LogsPage.Controls.Add(this.FileLogBox);
			this.LogsPage.Controls.Add(this.ConsoleLogBox);
			this.LogsPage.Location = new System.Drawing.Point(4, 28);
			this.LogsPage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.LogsPage.Name = "LogsPage";
			this.LogsPage.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.LogsPage.Size = new System.Drawing.Size(675, 370);
			this.LogsPage.TabIndex = 4;
			this.LogsPage.Text = "Logs";
			this.LogsPage.UseVisualStyleBackColor = true;
			// 
			// FileLogBox
			// 
			this.FileLogBox.Controls.Add(this.FileLogAppendEdit);
			this.FileLogBox.Controls.Add(this.FileSearch);
			this.FileLogBox.Controls.Add(this.FileLogNameEdit);
			this.FileLogBox.Controls.Add(this.FileLogNameLabel);
			this.FileLogBox.Controls.Add(this.FileLogDateFormatEdit);
			this.FileLogBox.Controls.Add(this.FileLogDateFormatLabel);
			this.FileLogBox.Controls.Add(this.FileLogStringFormatEdit);
			this.FileLogBox.Controls.Add(this.FileLogStringFormatLabel);
			this.FileLogBox.Controls.Add(this.FileLogFilterLabel);
			this.FileLogBox.Controls.Add(this.FileLogFilterEdit);
			this.FileLogBox.Controls.Add(this.FileLogEnabledEdit);
			this.FileLogBox.Location = new System.Drawing.Point(11, 158);
			this.FileLogBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.FileLogBox.Name = "FileLogBox";
			this.FileLogBox.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.FileLogBox.Size = new System.Drawing.Size(395, 202);
			this.FileLogBox.TabIndex = 1;
			this.FileLogBox.TabStop = false;
			this.FileLogBox.Text = "File";
			// 
			// FileLogAppendEdit
			// 
			this.FileLogAppendEdit.AutoSize = true;
			this.FileLogAppendEdit.Location = new System.Drawing.Point(127, 167);
			this.FileLogAppendEdit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.FileLogAppendEdit.Name = "FileLogAppendEdit";
			this.FileLogAppendEdit.Size = new System.Drawing.Size(117, 21);
			this.FileLogAppendEdit.TabIndex = 5;
			this.FileLogAppendEdit.Text = global::Bdt.GuiClient.Resources.Strings.SETUPFORM_APPEND_FILE;
			this.FileLogAppendEdit.UseVisualStyleBackColor = true;
			// 
			// FileSearch
			// 
			this.FileSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.FileSearch.Location = new System.Drawing.Point(356, 138);
			this.FileSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.FileSearch.Name = "FileSearch";
			this.FileSearch.Size = new System.Drawing.Size(27, 25);
			this.FileSearch.TabIndex = 4;
			this.FileSearch.Text = "...";
			this.FileSearch.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			this.FileSearch.UseVisualStyleBackColor = true;
			this.FileSearch.Click += new System.EventHandler(this.FileSearch_Click);
			// 
			// FileLogNameEdit
			// 
			this.FileLogNameEdit.Location = new System.Drawing.Point(127, 138);
			this.FileLogNameEdit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.FileLogNameEdit.Name = "FileLogNameEdit";
			this.FileLogNameEdit.ReadOnly = true;
			this.FileLogNameEdit.Size = new System.Drawing.Size(220, 22);
			this.FileLogNameEdit.TabIndex = 16;
			this.FileLogNameEdit.TabStop = false;
			// 
			// FileLogNameLabel
			// 
			this.FileLogNameLabel.AutoSize = true;
			this.FileLogNameLabel.Location = new System.Drawing.Point(9, 143);
			this.FileLogNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.FileLogNameLabel.Name = "FileLogNameLabel";
			this.FileLogNameLabel.Size = new System.Drawing.Size(30, 17);
			this.FileLogNameLabel.TabIndex = 15;
			this.FileLogNameLabel.Text = "File";
			// 
			// FileLogDateFormatEdit
			// 
			this.FileLogDateFormatEdit.Location = new System.Drawing.Point(127, 108);
			this.FileLogDateFormatEdit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.FileLogDateFormatEdit.Name = "FileLogDateFormatEdit";
			this.FileLogDateFormatEdit.Size = new System.Drawing.Size(255, 22);
			this.FileLogDateFormatEdit.TabIndex = 3;
			// 
			// FileLogDateFormatLabel
			// 
			this.FileLogDateFormatLabel.AutoSize = true;
			this.FileLogDateFormatLabel.Location = new System.Drawing.Point(9, 113);
			this.FileLogDateFormatLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.FileLogDateFormatLabel.Name = "FileLogDateFormatLabel";
			this.FileLogDateFormatLabel.Size = new System.Drawing.Size(82, 17);
			this.FileLogDateFormatLabel.TabIndex = 13;
			this.FileLogDateFormatLabel.Text = "Date format";
			// 
			// FileLogStringFormatEdit
			// 
			this.FileLogStringFormatEdit.Location = new System.Drawing.Point(127, 79);
			this.FileLogStringFormatEdit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.FileLogStringFormatEdit.Name = "FileLogStringFormatEdit";
			this.FileLogStringFormatEdit.Size = new System.Drawing.Size(255, 22);
			this.FileLogStringFormatEdit.TabIndex = 2;
			// 
			// FileLogStringFormatLabel
			// 
			this.FileLogStringFormatLabel.AutoSize = true;
			this.FileLogStringFormatLabel.Location = new System.Drawing.Point(9, 84);
			this.FileLogStringFormatLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.FileLogStringFormatLabel.Name = "FileLogStringFormatLabel";
			this.FileLogStringFormatLabel.Size = new System.Drawing.Size(52, 17);
			this.FileLogStringFormatLabel.TabIndex = 11;
			this.FileLogStringFormatLabel.Text = "Format";
			// 
			// FileLogFilterLabel
			// 
			this.FileLogFilterLabel.AutoSize = true;
			this.FileLogFilterLabel.Location = new System.Drawing.Point(9, 53);
			this.FileLogFilterLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.FileLogFilterLabel.Name = "FileLogFilterLabel";
			this.FileLogFilterLabel.Size = new System.Drawing.Size(39, 17);
			this.FileLogFilterLabel.TabIndex = 10;
			this.FileLogFilterLabel.Text = "Filter";
			// 
			// FileLogFilterEdit
			// 
			this.FileLogFilterEdit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.FileLogFilterEdit.FormattingEnabled = true;
			this.FileLogFilterEdit.Location = new System.Drawing.Point(127, 48);
			this.FileLogFilterEdit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.FileLogFilterEdit.Name = "FileLogFilterEdit";
			this.FileLogFilterEdit.Size = new System.Drawing.Size(255, 24);
			this.FileLogFilterEdit.TabIndex = 1;
			// 
			// FileLogEnabledEdit
			// 
			this.FileLogEnabledEdit.AutoSize = true;
			this.FileLogEnabledEdit.Location = new System.Drawing.Point(8, 23);
			this.FileLogEnabledEdit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.FileLogEnabledEdit.Name = "FileLogEnabledEdit";
			this.FileLogEnabledEdit.Size = new System.Drawing.Size(82, 21);
			this.FileLogEnabledEdit.TabIndex = 0;
			this.FileLogEnabledEdit.Text = global::Bdt.GuiClient.Resources.Strings.SETUPFORM_ENABLED;
			this.FileLogEnabledEdit.UseVisualStyleBackColor = true;
			// 
			// ConsoleLogBox
			// 
			this.ConsoleLogBox.Controls.Add(this.ConsoleLogDateFormatEdit);
			this.ConsoleLogBox.Controls.Add(this.ConsoleLogDateFormatLabel);
			this.ConsoleLogBox.Controls.Add(this.ConsoleLogStringFormatEdit);
			this.ConsoleLogBox.Controls.Add(this.ConsoleLogStringFormatLabel);
			this.ConsoleLogBox.Controls.Add(this.ConsoleLogFilterLabel);
			this.ConsoleLogBox.Controls.Add(this.ConsoleLogFilterEdit);
			this.ConsoleLogBox.Controls.Add(this.ConsoleLogEnabledEdit);
			this.ConsoleLogBox.Location = new System.Drawing.Point(11, 7);
			this.ConsoleLogBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.ConsoleLogBox.Name = "ConsoleLogBox";
			this.ConsoleLogBox.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.ConsoleLogBox.Size = new System.Drawing.Size(395, 143);
			this.ConsoleLogBox.TabIndex = 0;
			this.ConsoleLogBox.TabStop = false;
			this.ConsoleLogBox.Text = "Console (unused by GUI client)";
			// 
			// ConsoleLogDateFormatEdit
			// 
			this.ConsoleLogDateFormatEdit.Location = new System.Drawing.Point(127, 108);
			this.ConsoleLogDateFormatEdit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.ConsoleLogDateFormatEdit.Name = "ConsoleLogDateFormatEdit";
			this.ConsoleLogDateFormatEdit.Size = new System.Drawing.Size(255, 22);
			this.ConsoleLogDateFormatEdit.TabIndex = 3;
			// 
			// ConsoleLogDateFormatLabel
			// 
			this.ConsoleLogDateFormatLabel.AutoSize = true;
			this.ConsoleLogDateFormatLabel.Location = new System.Drawing.Point(9, 113);
			this.ConsoleLogDateFormatLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ConsoleLogDateFormatLabel.Name = "ConsoleLogDateFormatLabel";
			this.ConsoleLogDateFormatLabel.Size = new System.Drawing.Size(82, 17);
			this.ConsoleLogDateFormatLabel.TabIndex = 13;
			this.ConsoleLogDateFormatLabel.Text = "Date format";
			// 
			// ConsoleLogStringFormatEdit
			// 
			this.ConsoleLogStringFormatEdit.Location = new System.Drawing.Point(127, 79);
			this.ConsoleLogStringFormatEdit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.ConsoleLogStringFormatEdit.Name = "ConsoleLogStringFormatEdit";
			this.ConsoleLogStringFormatEdit.Size = new System.Drawing.Size(255, 22);
			this.ConsoleLogStringFormatEdit.TabIndex = 2;
			// 
			// ConsoleLogStringFormatLabel
			// 
			this.ConsoleLogStringFormatLabel.AutoSize = true;
			this.ConsoleLogStringFormatLabel.Location = new System.Drawing.Point(9, 84);
			this.ConsoleLogStringFormatLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ConsoleLogStringFormatLabel.Name = "ConsoleLogStringFormatLabel";
			this.ConsoleLogStringFormatLabel.Size = new System.Drawing.Size(52, 17);
			this.ConsoleLogStringFormatLabel.TabIndex = 11;
			this.ConsoleLogStringFormatLabel.Text = "Format";
			// 
			// ConsoleLogFilterLabel
			// 
			this.ConsoleLogFilterLabel.AutoSize = true;
			this.ConsoleLogFilterLabel.Location = new System.Drawing.Point(9, 53);
			this.ConsoleLogFilterLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.ConsoleLogFilterLabel.Name = "ConsoleLogFilterLabel";
			this.ConsoleLogFilterLabel.Size = new System.Drawing.Size(39, 17);
			this.ConsoleLogFilterLabel.TabIndex = 10;
			this.ConsoleLogFilterLabel.Text = "Filter";
			// 
			// ConsoleLogFilterEdit
			// 
			this.ConsoleLogFilterEdit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ConsoleLogFilterEdit.FormattingEnabled = true;
			this.ConsoleLogFilterEdit.Location = new System.Drawing.Point(127, 48);
			this.ConsoleLogFilterEdit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.ConsoleLogFilterEdit.Name = "ConsoleLogFilterEdit";
			this.ConsoleLogFilterEdit.Size = new System.Drawing.Size(255, 24);
			this.ConsoleLogFilterEdit.TabIndex = 1;
			// 
			// ConsoleLogEnabledEdit
			// 
			this.ConsoleLogEnabledEdit.AutoSize = true;
			this.ConsoleLogEnabledEdit.Location = new System.Drawing.Point(8, 23);
			this.ConsoleLogEnabledEdit.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.ConsoleLogEnabledEdit.Name = "ConsoleLogEnabledEdit";
			this.ConsoleLogEnabledEdit.Size = new System.Drawing.Size(82, 21);
			this.ConsoleLogEnabledEdit.TabIndex = 0;
			this.ConsoleLogEnabledEdit.Text = global::Bdt.GuiClient.Resources.Strings.SETUPFORM_ENABLED;
			this.ConsoleLogEnabledEdit.UseVisualStyleBackColor = true;
			// 
			// Cancel
			// 
			this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Cancel.Location = new System.Drawing.Point(580, 4);
			this.Cancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Cancel.Name = "Cancel";
			this.Cancel.Size = new System.Drawing.Size(100, 28);
			this.Cancel.TabIndex = 2;
			this.Cancel.Text = global::Bdt.GuiClient.Resources.Strings.SETUPFORM_CANCEL;
			this.Cancel.UseVisualStyleBackColor = true;
			// 
			// BottomPanel
			// 
			this.BottomPanel.Controls.Add(this.Apply);
			this.BottomPanel.Controls.Add(this.Cancel);
			this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.BottomPanel.Location = new System.Drawing.Point(0, 408);
			this.BottomPanel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.BottomPanel.Name = "BottomPanel";
			this.BottomPanel.Size = new System.Drawing.Size(684, 39);
			this.BottomPanel.TabIndex = 1;
			// 
			// Apply
			// 
			this.Apply.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Apply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.Apply.Location = new System.Drawing.Point(469, 4);
			this.Apply.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Apply.Name = "Apply";
			this.Apply.Size = new System.Drawing.Size(100, 28);
			this.Apply.TabIndex = 1;
			this.Apply.Text = global::Bdt.GuiClient.Resources.Strings.SETUPFORM_APPLY;
			this.Apply.UseVisualStyleBackColor = true;
			this.Apply.Click += new System.EventHandler(this.Apply_Click);
			// 
			// SaveFileDialog
			// 
			this.SaveFileDialog.DefaultExt = "log";
			this.SaveFileDialog.Filter = global::Bdt.GuiClient.Resources.Strings.SETUPFORM_FILE_FILTER;
			this.SaveFileDialog.Title = global::Bdt.GuiClient.Resources.Strings.SETUPFORM_CONFIGURATION;
			// 
			// Proxy100Continue
			// 
			this.Proxy100Continue.AutoSize = true;
			this.Proxy100Continue.Location = new System.Drawing.Point(11, 44);
			this.Proxy100Continue.Margin = new System.Windows.Forms.Padding(4);
			this.Proxy100Continue.Name = "Proxy100Continue";
			this.Proxy100Continue.Size = new System.Drawing.Size(203, 21);
			this.Proxy100Continue.TabIndex = 3;
			this.Proxy100Continue.Text = global::Bdt.GuiClient.Resources.Strings.SETUPFORM_EXPECT100;
			this.Proxy100Continue.UseVisualStyleBackColor = true;
			// 
			// SetupForm
			// 
			this.AcceptButton = this.Apply;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.Cancel;
			this.ClientSize = new System.Drawing.Size(684, 447);
			this.Controls.Add(this.BottomPanel);
			this.Controls.Add(this.Tabs);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.MaximizeBox = false;
			this.Name = "SetupForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "BoutDuTunnel (Client) - Configuration";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.SetupForm_Load);
			this.Tabs.ResumeLayout(false);
			this.ServicePage.ResumeLayout(false);
			this.ServicePage.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.ServicePortEdit)).EndInit();
			this.SocksPage.ResumeLayout(false);
			this.SocksPage.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.SocksPortEdit)).EndInit();
			this.ProxyPage.ResumeLayout(false);
			this.ProxyPage.PerformLayout();
			this.ConfigBox.ResumeLayout(false);
			this.ConfigBox.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.ProxyPortEdit)).EndInit();
			this.AuthBox.ResumeLayout(false);
			this.AuthBox.PerformLayout();
			this.ForwardsPage.ResumeLayout(false);
			this.ForwardsPage.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.BindingNavigator)).EndInit();
			this.BindingNavigator.ResumeLayout(false);
			this.BindingNavigator.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.BindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.PortForwardDataGridView)).EndInit();
			this.LogsPage.ResumeLayout(false);
			this.FileLogBox.ResumeLayout(false);
			this.FileLogBox.PerformLayout();
			this.ConsoleLogBox.ResumeLayout(false);
			this.ConsoleLogBox.PerformLayout();
			this.BottomPanel.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl Tabs;
        private System.Windows.Forms.TabPage ServicePage;
        private System.Windows.Forms.TabPage SocksPage;
        private System.Windows.Forms.TabPage ProxyPage;
        private System.Windows.Forms.TabPage ForwardsPage;
        private System.Windows.Forms.TabPage LogsPage;
        private System.Windows.Forms.Panel BottomPanel;
        private System.Windows.Forms.TextBox ServiceNameEdit;
        private System.Windows.Forms.ComboBox ServiceProtocolEdit;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Label ServiceNameLabel;
        private System.Windows.Forms.TextBox ServiceAddressEdit;
        private System.Windows.Forms.Label ServiceAddressLabel;
        private System.Windows.Forms.Label ServiceProtocolLabel;
        private System.Windows.Forms.TextBox ServicePasswordEdit;
        private System.Windows.Forms.Label ServicePasswordLabel;
        private System.Windows.Forms.TextBox ServiceUserEdit;
        private System.Windows.Forms.Label ServiceUserNameLabel;
        private System.Windows.Forms.Label ServicePortLabel;
        private System.Windows.Forms.NumericUpDown ServicePortEdit;
        private System.Windows.Forms.CheckBox SocksSharedEdit;
        private System.Windows.Forms.CheckBox SocksEnabledEdit;
        private System.Windows.Forms.CheckBox ProxyEnabledEdit;
        private System.Windows.Forms.NumericUpDown SocksPortEdit;
        private System.Windows.Forms.Label SocksPortLabel;
        private System.Windows.Forms.GroupBox ConfigBox;
        private System.Windows.Forms.CheckBox ProxyAutoConfigurationEdit;
        private System.Windows.Forms.GroupBox AuthBox;
        private System.Windows.Forms.Label ProxyDomainLabel;
        private System.Windows.Forms.TextBox ProxyPasswordEdit;
        private System.Windows.Forms.Label ProxyPasswordLabel;
        private System.Windows.Forms.TextBox ProxyUserNameEdit;
        private System.Windows.Forms.Label ProxyUserNameLabel;
        private System.Windows.Forms.CheckBox ProxyAuthenticationEdit;
        private System.Windows.Forms.NumericUpDown ProxyPortEdit;
        private System.Windows.Forms.Label ProxyPortLabel;
        private System.Windows.Forms.TextBox ProxyAddressEdit;
        private System.Windows.Forms.Label ProxyAddressLabel;
        private System.Windows.Forms.GroupBox ConsoleLogBox;
        private System.Windows.Forms.CheckBox ConsoleLogEnabledEdit;
        private System.Windows.Forms.GroupBox FileLogBox;
        private System.Windows.Forms.Button FileSearch;
        private System.Windows.Forms.TextBox FileLogNameEdit;
        private System.Windows.Forms.Label FileLogNameLabel;
        private System.Windows.Forms.TextBox FileLogDateFormatEdit;
        private System.Windows.Forms.Label FileLogDateFormatLabel;
        private System.Windows.Forms.TextBox FileLogStringFormatEdit;
        private System.Windows.Forms.Label FileLogStringFormatLabel;
        private System.Windows.Forms.Label FileLogFilterLabel;
        private System.Windows.Forms.ComboBox FileLogFilterEdit;
        private System.Windows.Forms.CheckBox FileLogEnabledEdit;
        private System.Windows.Forms.TextBox ConsoleLogDateFormatEdit;
        private System.Windows.Forms.Label ConsoleLogDateFormatLabel;
        private System.Windows.Forms.TextBox ConsoleLogStringFormatEdit;
        private System.Windows.Forms.Label ConsoleLogStringFormatLabel;
        private System.Windows.Forms.Label ConsoleLogFilterLabel;
        private System.Windows.Forms.ComboBox ConsoleLogFilterEdit;
        private System.Windows.Forms.CheckBox FileLogAppendEdit;
        private System.Windows.Forms.TextBox ProxyDomainEdit;
        private System.Windows.Forms.SaveFileDialog SaveFileDialog;
        private System.Windows.Forms.DataGridView PortForwardDataGridView;
        private System.Windows.Forms.BindingNavigator BindingNavigator;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.BindingSource BindingSource;
        private System.Windows.Forms.Button Apply;
        private System.Windows.Forms.DataGridViewTextBoxColumn PortForwardLocalPort;
        private System.Windows.Forms.DataGridViewCheckBoxColumn PortForwardEnabledEdit;
        private System.Windows.Forms.DataGridViewCheckBoxColumn PortForwardSharedEdit;
        private System.Windows.Forms.DataGridViewTextBoxColumn PortForwardAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn PortForwardRemotePort;
		private System.Windows.Forms.CheckBox Proxy100Continue;
    }
}

