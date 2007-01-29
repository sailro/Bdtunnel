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
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(512, 327);
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
            this.ServicePage.Name = "ServicePage";
            this.ServicePage.Padding = new System.Windows.Forms.Padding(3);
            this.ServicePage.Size = new System.Drawing.Size(504, 295);
            this.ServicePage.TabIndex = 0;
            this.ServicePage.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_TUNNEL;
            this.ServicePage.UseVisualStyleBackColor = true;
            // 
            // ServicePortEdit
            // 
            this.ServicePortEdit.Location = new System.Drawing.Point(96, 80);
            this.ServicePortEdit.Name = "ServicePortEdit";
            this.ServicePortEdit.Size = new System.Drawing.Size(64, 20);
            this.ServicePortEdit.TabIndex = 3;
            // 
            // ServicePasswordEdit
            // 
            this.ServicePasswordEdit.Location = new System.Drawing.Point(96, 127);
            this.ServicePasswordEdit.Name = "ServicePasswordEdit";
            this.ServicePasswordEdit.PasswordChar = '*';
            this.ServicePasswordEdit.Size = new System.Drawing.Size(192, 20);
            this.ServicePasswordEdit.TabIndex = 5;
            // 
            // ServicePasswordLabel
            // 
            this.ServicePasswordLabel.AutoSize = true;
            this.ServicePasswordLabel.Location = new System.Drawing.Point(8, 132);
            this.ServicePasswordLabel.Name = "ServicePasswordLabel";
            this.ServicePasswordLabel.Size = new System.Drawing.Size(71, 13);
            this.ServicePasswordLabel.TabIndex = 12;
            this.ServicePasswordLabel.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_PASSWORD;
            // 
            // ServiceUserEdit
            // 
            this.ServiceUserEdit.Location = new System.Drawing.Point(96, 103);
            this.ServiceUserEdit.Name = "ServiceUserEdit";
            this.ServiceUserEdit.Size = new System.Drawing.Size(192, 20);
            this.ServiceUserEdit.TabIndex = 4;
            // 
            // ServiceUserNameLabel
            // 
            this.ServiceUserNameLabel.AutoSize = true;
            this.ServiceUserNameLabel.Location = new System.Drawing.Point(8, 107);
            this.ServiceUserNameLabel.Name = "ServiceUserNameLabel";
            this.ServiceUserNameLabel.Size = new System.Drawing.Size(53, 13);
            this.ServiceUserNameLabel.TabIndex = 10;
            this.ServiceUserNameLabel.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_USER;
            // 
            // ServicePortLabel
            // 
            this.ServicePortLabel.AutoSize = true;
            this.ServicePortLabel.Location = new System.Drawing.Point(8, 84);
            this.ServicePortLabel.Name = "ServicePortLabel";
            this.ServicePortLabel.Size = new System.Drawing.Size(26, 13);
            this.ServicePortLabel.TabIndex = 9;
            this.ServicePortLabel.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_PORT;
            // 
            // ServiceAddressEdit
            // 
            this.ServiceAddressEdit.Location = new System.Drawing.Point(96, 56);
            this.ServiceAddressEdit.Name = "ServiceAddressEdit";
            this.ServiceAddressEdit.Size = new System.Drawing.Size(192, 20);
            this.ServiceAddressEdit.TabIndex = 2;
            // 
            // ServiceAddressLabel
            // 
            this.ServiceAddressLabel.AutoSize = true;
            this.ServiceAddressLabel.Location = new System.Drawing.Point(8, 60);
            this.ServiceAddressLabel.Name = "ServiceAddressLabel";
            this.ServiceAddressLabel.Size = new System.Drawing.Size(45, 13);
            this.ServiceAddressLabel.TabIndex = 7;
            this.ServiceAddressLabel.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_ADDRESS;
            // 
            // ServiceProtocolLabel
            // 
            this.ServiceProtocolLabel.AutoSize = true;
            this.ServiceProtocolLabel.Location = new System.Drawing.Point(8, 35);
            this.ServiceProtocolLabel.Name = "ServiceProtocolLabel";
            this.ServiceProtocolLabel.Size = new System.Drawing.Size(52, 13);
            this.ServiceProtocolLabel.TabIndex = 6;
            this.ServiceProtocolLabel.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_PROTOCOL;
            // 
            // ServiceNameEdit
            // 
            this.ServiceNameEdit.Location = new System.Drawing.Point(96, 8);
            this.ServiceNameEdit.Name = "ServiceNameEdit";
            this.ServiceNameEdit.Size = new System.Drawing.Size(192, 20);
            this.ServiceNameEdit.TabIndex = 0;
            // 
            // ServiceProtocolEdit
            // 
            this.ServiceProtocolEdit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ServiceProtocolEdit.FormattingEnabled = true;
            this.ServiceProtocolEdit.Location = new System.Drawing.Point(96, 31);
            this.ServiceProtocolEdit.Name = "ServiceProtocolEdit";
            this.ServiceProtocolEdit.Size = new System.Drawing.Size(192, 21);
            this.ServiceProtocolEdit.TabIndex = 1;
            // 
            // ServiceNameLabel
            // 
            this.ServiceNameLabel.AutoSize = true;
            this.ServiceNameLabel.Location = new System.Drawing.Point(8, 12);
            this.ServiceNameLabel.Name = "ServiceNameLabel";
            this.ServiceNameLabel.Size = new System.Drawing.Size(29, 13);
            this.ServiceNameLabel.TabIndex = 0;
            this.ServiceNameLabel.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_NAME;
            // 
            // SocksPage
            // 
            this.SocksPage.Controls.Add(this.SocksPortEdit);
            this.SocksPage.Controls.Add(this.SocksPortLabel);
            this.SocksPage.Controls.Add(this.SocksSharedEdit);
            this.SocksPage.Controls.Add(this.SocksEnabledEdit);
            this.SocksPage.Location = new System.Drawing.Point(4, 28);
            this.SocksPage.Name = "SocksPage";
            this.SocksPage.Padding = new System.Windows.Forms.Padding(3);
            this.SocksPage.Size = new System.Drawing.Size(504, 295);
            this.SocksPage.TabIndex = 1;
            this.SocksPage.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_SOCKS;
            this.SocksPage.UseVisualStyleBackColor = true;
            // 
            // SocksPortEdit
            // 
            this.SocksPortEdit.Location = new System.Drawing.Point(48, 56);
            this.SocksPortEdit.Name = "SocksPortEdit";
            this.SocksPortEdit.Size = new System.Drawing.Size(64, 20);
            this.SocksPortEdit.TabIndex = 2;
            // 
            // SocksPortLabel
            // 
            this.SocksPortLabel.AutoSize = true;
            this.SocksPortLabel.Location = new System.Drawing.Point(8, 60);
            this.SocksPortLabel.Name = "SocksPortLabel";
            this.SocksPortLabel.Size = new System.Drawing.Size(26, 13);
            this.SocksPortLabel.TabIndex = 11;
            this.SocksPortLabel.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_PORT;
            // 
            // SocksSharedEdit
            // 
            this.SocksSharedEdit.AutoSize = true;
            this.SocksSharedEdit.Location = new System.Drawing.Point(8, 35);
            this.SocksSharedEdit.Name = "SocksSharedEdit";
            this.SocksSharedEdit.Size = new System.Drawing.Size(135, 17);
            this.SocksSharedEdit.TabIndex = 1;
            this.SocksSharedEdit.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_SOCKS_SERVER_SHARED;
            this.SocksSharedEdit.UseVisualStyleBackColor = true;
            // 
            // SocksEnabledEdit
            // 
            this.SocksEnabledEdit.AutoSize = true;
            this.SocksEnabledEdit.Location = new System.Drawing.Point(8, 12);
            this.SocksEnabledEdit.Name = "SocksEnabledEdit";
            this.SocksEnabledEdit.Size = new System.Drawing.Size(119, 17);
            this.SocksEnabledEdit.TabIndex = 0;
            this.SocksEnabledEdit.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_SOCKS_SERVER_ENABLED;
            this.SocksEnabledEdit.UseVisualStyleBackColor = true;
            // 
            // ProxyPage
            // 
            this.ProxyPage.Controls.Add(this.ConfigBox);
            this.ProxyPage.Controls.Add(this.AuthBox);
            this.ProxyPage.Controls.Add(this.ProxyEnabledEdit);
            this.ProxyPage.Location = new System.Drawing.Point(4, 28);
            this.ProxyPage.Name = "ProxyPage";
            this.ProxyPage.Padding = new System.Windows.Forms.Padding(3);
            this.ProxyPage.Size = new System.Drawing.Size(504, 295);
            this.ProxyPage.TabIndex = 2;
            this.ProxyPage.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_HTTP_PROXY;
            this.ProxyPage.UseVisualStyleBackColor = true;
            // 
            // ConfigBox
            // 
            this.ConfigBox.Controls.Add(this.ProxyPortEdit);
            this.ConfigBox.Controls.Add(this.ProxyPortLabel);
            this.ConfigBox.Controls.Add(this.ProxyAddressEdit);
            this.ConfigBox.Controls.Add(this.ProxyAddressLabel);
            this.ConfigBox.Controls.Add(this.ProxyAutoConfigurationEdit);
            this.ConfigBox.Location = new System.Drawing.Point(8, 164);
            this.ConfigBox.Name = "ConfigBox";
            this.ConfigBox.Size = new System.Drawing.Size(296, 94);
            this.ConfigBox.TabIndex = 2;
            this.ConfigBox.TabStop = false;
            this.ConfigBox.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_CONFIGURATION;
            // 
            // ProxyPortEdit
            // 
            this.ProxyPortEdit.Location = new System.Drawing.Point(95, 62);
            this.ProxyPortEdit.Name = "ProxyPortEdit";
            this.ProxyPortEdit.Size = new System.Drawing.Size(64, 20);
            this.ProxyPortEdit.TabIndex = 2;
            // 
            // ProxyPortLabel
            // 
            this.ProxyPortLabel.AutoSize = true;
            this.ProxyPortLabel.Location = new System.Drawing.Point(7, 67);
            this.ProxyPortLabel.Name = "ProxyPortLabel";
            this.ProxyPortLabel.Size = new System.Drawing.Size(26, 13);
            this.ProxyPortLabel.TabIndex = 13;
            this.ProxyPortLabel.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_PORT;
            // 
            // ProxyAddressEdit
            // 
            this.ProxyAddressEdit.Location = new System.Drawing.Point(95, 38);
            this.ProxyAddressEdit.Name = "ProxyAddressEdit";
            this.ProxyAddressEdit.Size = new System.Drawing.Size(192, 20);
            this.ProxyAddressEdit.TabIndex = 1;
            // 
            // ProxyAddressLabel
            // 
            this.ProxyAddressLabel.AutoSize = true;
            this.ProxyAddressLabel.Location = new System.Drawing.Point(7, 42);
            this.ProxyAddressLabel.Name = "ProxyAddressLabel";
            this.ProxyAddressLabel.Size = new System.Drawing.Size(45, 13);
            this.ProxyAddressLabel.TabIndex = 11;
            this.ProxyAddressLabel.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_ADDRESS;
            // 
            // ProxyAutoConfigurationEdit
            // 
            this.ProxyAutoConfigurationEdit.AutoSize = true;
            this.ProxyAutoConfigurationEdit.Location = new System.Drawing.Point(6, 19);
            this.ProxyAutoConfigurationEdit.Name = "ProxyAutoConfigurationEdit";
            this.ProxyAutoConfigurationEdit.Size = new System.Drawing.Size(85, 17);
            this.ProxyAutoConfigurationEdit.TabIndex = 0;
            this.ProxyAutoConfigurationEdit.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_AUTO;
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
            this.AuthBox.Location = new System.Drawing.Point(8, 37);
            this.AuthBox.Name = "AuthBox";
            this.AuthBox.Size = new System.Drawing.Size(296, 117);
            this.AuthBox.TabIndex = 1;
            this.AuthBox.TabStop = false;
            this.AuthBox.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_AUTHENTIFICATION;
            // 
            // ProxyDomainEdit
            // 
            this.ProxyDomainEdit.Location = new System.Drawing.Point(95, 86);
            this.ProxyDomainEdit.Name = "ProxyDomainEdit";
            this.ProxyDomainEdit.Size = new System.Drawing.Size(192, 20);
            this.ProxyDomainEdit.TabIndex = 3;
            // 
            // ProxyDomainLabel
            // 
            this.ProxyDomainLabel.AutoSize = true;
            this.ProxyDomainLabel.Location = new System.Drawing.Point(7, 91);
            this.ProxyDomainLabel.Name = "ProxyDomainLabel";
            this.ProxyDomainLabel.Size = new System.Drawing.Size(49, 13);
            this.ProxyDomainLabel.TabIndex = 18;
            this.ProxyDomainLabel.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_DOMAIN;
            // 
            // ProxyPasswordEdit
            // 
            this.ProxyPasswordEdit.Location = new System.Drawing.Point(95, 62);
            this.ProxyPasswordEdit.Name = "ProxyPasswordEdit";
            this.ProxyPasswordEdit.PasswordChar = '*';
            this.ProxyPasswordEdit.Size = new System.Drawing.Size(192, 20);
            this.ProxyPasswordEdit.TabIndex = 2;
            // 
            // ProxyPasswordLabel
            // 
            this.ProxyPasswordLabel.AutoSize = true;
            this.ProxyPasswordLabel.Location = new System.Drawing.Point(7, 67);
            this.ProxyPasswordLabel.Name = "ProxyPasswordLabel";
            this.ProxyPasswordLabel.Size = new System.Drawing.Size(71, 13);
            this.ProxyPasswordLabel.TabIndex = 16;
            this.ProxyPasswordLabel.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_PASSWORD;
            // 
            // ProxyUserNameEdit
            // 
            this.ProxyUserNameEdit.Location = new System.Drawing.Point(95, 38);
            this.ProxyUserNameEdit.Name = "ProxyUserNameEdit";
            this.ProxyUserNameEdit.Size = new System.Drawing.Size(192, 20);
            this.ProxyUserNameEdit.TabIndex = 1;
            // 
            // ProxyUserNameLabel
            // 
            this.ProxyUserNameLabel.AutoSize = true;
            this.ProxyUserNameLabel.Location = new System.Drawing.Point(7, 42);
            this.ProxyUserNameLabel.Name = "ProxyUserNameLabel";
            this.ProxyUserNameLabel.Size = new System.Drawing.Size(53, 13);
            this.ProxyUserNameLabel.TabIndex = 14;
            this.ProxyUserNameLabel.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_USER;
            // 
            // ProxyAuthenticationEdit
            // 
            this.ProxyAuthenticationEdit.AutoSize = true;
            this.ProxyAuthenticationEdit.Location = new System.Drawing.Point(6, 19);
            this.ProxyAuthenticationEdit.Name = "ProxyAuthenticationEdit";
            this.ProxyAuthenticationEdit.Size = new System.Drawing.Size(85, 17);
            this.ProxyAuthenticationEdit.TabIndex = 0;
            this.ProxyAuthenticationEdit.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_AUTO;
            this.ProxyAuthenticationEdit.UseVisualStyleBackColor = true;
            this.ProxyAuthenticationEdit.CheckedChanged += new System.EventHandler(this.ProxyAuthenticationEdit_CheckedChanged);
            // 
            // ProxyEnabledEdit
            // 
            this.ProxyEnabledEdit.AutoSize = true;
            this.ProxyEnabledEdit.Location = new System.Drawing.Point(8, 12);
            this.ProxyEnabledEdit.Name = "ProxyEnabledEdit";
            this.ProxyEnabledEdit.Size = new System.Drawing.Size(221, 17);
            this.ProxyEnabledEdit.TabIndex = 0;
            this.ProxyEnabledEdit.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_CONNECT_THROUGH_PROXY;
            this.ProxyEnabledEdit.UseVisualStyleBackColor = true;
            // 
            // ForwardsPage
            // 
            this.ForwardsPage.Controls.Add(this.BindingNavigator);
            this.ForwardsPage.Controls.Add(this.PortForwardDataGridView);
            this.ForwardsPage.Location = new System.Drawing.Point(4, 28);
            this.ForwardsPage.Name = "ForwardsPage";
            this.ForwardsPage.Padding = new System.Windows.Forms.Padding(3);
            this.ForwardsPage.Size = new System.Drawing.Size(504, 295);
            this.ForwardsPage.TabIndex = 3;
            this.ForwardsPage.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_FORWARDS;
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
            this.BindingNavigator.Location = new System.Drawing.Point(3, 3);
            this.BindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.BindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.BindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.BindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.BindingNavigator.Name = "BindingNavigator";
            this.BindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.BindingNavigator.Size = new System.Drawing.Size(498, 25);
            this.BindingNavigator.TabIndex = 5;
            this.BindingNavigator.Text = "BindingNavigator";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_ADD_NEW;
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(38, 22);
            this.bindingNavigatorCountItem.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_ITEM_COUNT_OF;
            this.bindingNavigatorCountItem.ToolTipText = Bdt.GuiClient.Resources.Strings.SETUP_FORM_ITEM_COUNT;
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_DELETE;
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_MOVE_FIRST;
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_MOVE_UP;
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 21);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = Bdt.GuiClient.Resources.Strings.SETUP_FORM_CURRENT_POSITION;
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_MOVE_DOWN;
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_MOVE_LAST;
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
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
            this.PortForwardDataGridView.Location = new System.Drawing.Point(3, 31);
            this.PortForwardDataGridView.Name = "PortForwardDataGridView";
            this.PortForwardDataGridView.Size = new System.Drawing.Size(498, 267);
            this.PortForwardDataGridView.TabIndex = 4;
            this.PortForwardDataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.PortForwardDataGridView_DataError);
            // 
            // PortForwardLocalPort
            // 
            this.PortForwardLocalPort.DataPropertyName = "LocalPort";
            this.PortForwardLocalPort.HeaderText = Bdt.GuiClient.Resources.Strings.SETUP_FORM_LOCAL_PORT;
            this.PortForwardLocalPort.Name = "PortForwardLocalPort";
            this.PortForwardLocalPort.Width = 76;
            // 
            // PortForwardEnabledEdit
            // 
            this.PortForwardEnabledEdit.DataPropertyName = "Enabled";
            this.PortForwardEnabledEdit.HeaderText = Bdt.GuiClient.Resources.Strings.SETUP_FORM_ENABLED;
            this.PortForwardEnabledEdit.Name = "PortForwardEnabledEdit";
            this.PortForwardEnabledEdit.Width = 34;
            // 
            // PortForwardSharedEdit
            // 
            this.PortForwardSharedEdit.DataPropertyName = "Shared";
            this.PortForwardSharedEdit.HeaderText = Bdt.GuiClient.Resources.Strings.SETUP_FORM_SHARED;
            this.PortForwardSharedEdit.Name = "PortForwardSharedEdit";
            this.PortForwardSharedEdit.Width = 50;
            // 
            // PortForwardAddress
            // 
            this.PortForwardAddress.DataPropertyName = "Address";
            this.PortForwardAddress.HeaderText = Bdt.GuiClient.Resources.Strings.SETUP_FORM_ADDRESS;
            this.PortForwardAddress.MinimumWidth = 190;
            this.PortForwardAddress.Name = "PortForwardAddress";
            this.PortForwardAddress.Width = 190;
            // 
            // PortForwardRemotePort
            // 
            this.PortForwardRemotePort.DataPropertyName = "RemotePort";
            this.PortForwardRemotePort.HeaderText = Bdt.GuiClient.Resources.Strings.SETUP_FORM_REMOTE_PORT;
            this.PortForwardRemotePort.Name = "PortForwardRemotePort";
            this.PortForwardRemotePort.Width = 85;
            // 
            // LogsPage
            // 
            this.LogsPage.Controls.Add(this.FileLogBox);
            this.LogsPage.Controls.Add(this.ConsoleLogBox);
            this.LogsPage.Location = new System.Drawing.Point(4, 28);
            this.LogsPage.Name = "LogsPage";
            this.LogsPage.Padding = new System.Windows.Forms.Padding(3);
            this.LogsPage.Size = new System.Drawing.Size(504, 295);
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
            this.FileLogBox.Location = new System.Drawing.Point(8, 128);
            this.FileLogBox.Name = "FileLogBox";
            this.FileLogBox.Size = new System.Drawing.Size(296, 164);
            this.FileLogBox.TabIndex = 1;
            this.FileLogBox.TabStop = false;
            this.FileLogBox.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_FILE;
            // 
            // FileLogAppendEdit
            // 
            this.FileLogAppendEdit.AutoSize = true;
            this.FileLogAppendEdit.Location = new System.Drawing.Point(95, 136);
            this.FileLogAppendEdit.Name = "FileLogAppendEdit";
            this.FileLogAppendEdit.Size = new System.Drawing.Size(149, 17);
            this.FileLogAppendEdit.TabIndex = 5;
            this.FileLogAppendEdit.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_APPEND_FILE;
            this.FileLogAppendEdit.UseVisualStyleBackColor = true;
            // 
            // FileSearch
            // 
            this.FileSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.FileSearch.Location = new System.Drawing.Point(267, 112);
            this.FileSearch.Name = "FileSearch";
            this.FileSearch.Size = new System.Drawing.Size(20, 20);
            this.FileSearch.TabIndex = 4;
            this.FileSearch.Text = "...";
            this.FileSearch.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.FileSearch.UseVisualStyleBackColor = true;
            this.FileSearch.Click += new System.EventHandler(this.FileSearch_Click);
            // 
            // FileLogNameEdit
            // 
            this.FileLogNameEdit.Location = new System.Drawing.Point(95, 112);
            this.FileLogNameEdit.Name = "FileLogNameEdit";
            this.FileLogNameEdit.ReadOnly = true;
            this.FileLogNameEdit.Size = new System.Drawing.Size(166, 20);
            this.FileLogNameEdit.TabIndex = 16;
            this.FileLogNameEdit.TabStop = false;
            // 
            // FileLogNameLabel
            // 
            this.FileLogNameLabel.AutoSize = true;
            this.FileLogNameLabel.Location = new System.Drawing.Point(7, 116);
            this.FileLogNameLabel.Name = "FileLogNameLabel";
            this.FileLogNameLabel.Size = new System.Drawing.Size(38, 13);
            this.FileLogNameLabel.TabIndex = 15;
            this.FileLogNameLabel.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_FILE;
            // 
            // FileLogDateFormatEdit
            // 
            this.FileLogDateFormatEdit.Location = new System.Drawing.Point(95, 88);
            this.FileLogDateFormatEdit.Name = "FileLogDateFormatEdit";
            this.FileLogDateFormatEdit.Size = new System.Drawing.Size(192, 20);
            this.FileLogDateFormatEdit.TabIndex = 3;
            // 
            // FileLogDateFormatLabel
            // 
            this.FileLogDateFormatLabel.AutoSize = true;
            this.FileLogDateFormatLabel.Location = new System.Drawing.Point(7, 92);
            this.FileLogDateFormatLabel.Name = "FileLogDateFormatLabel";
            this.FileLogDateFormatLabel.Size = new System.Drawing.Size(78, 13);
            this.FileLogDateFormatLabel.TabIndex = 13;
            this.FileLogDateFormatLabel.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_DATE_FORMAT;
            // 
            // FileLogStringFormatEdit
            // 
            this.FileLogStringFormatEdit.Location = new System.Drawing.Point(95, 64);
            this.FileLogStringFormatEdit.Name = "FileLogStringFormatEdit";
            this.FileLogStringFormatEdit.Size = new System.Drawing.Size(192, 20);
            this.FileLogStringFormatEdit.TabIndex = 2;
            // 
            // FileLogStringFormatLabel
            // 
            this.FileLogStringFormatLabel.AutoSize = true;
            this.FileLogStringFormatLabel.Location = new System.Drawing.Point(7, 68);
            this.FileLogStringFormatLabel.Name = "FileLogStringFormatLabel";
            this.FileLogStringFormatLabel.Size = new System.Drawing.Size(39, 13);
            this.FileLogStringFormatLabel.TabIndex = 11;
            this.FileLogStringFormatLabel.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_FORMAT;
            // 
            // FileLogFilterLabel
            // 
            this.FileLogFilterLabel.AutoSize = true;
            this.FileLogFilterLabel.Location = new System.Drawing.Point(7, 43);
            this.FileLogFilterLabel.Name = "FileLogFilterLabel";
            this.FileLogFilterLabel.Size = new System.Drawing.Size(29, 13);
            this.FileLogFilterLabel.TabIndex = 10;
            this.FileLogFilterLabel.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_FILTER;
            // 
            // FileLogFilterEdit
            // 
            this.FileLogFilterEdit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FileLogFilterEdit.FormattingEnabled = true;
            this.FileLogFilterEdit.Location = new System.Drawing.Point(95, 39);
            this.FileLogFilterEdit.Name = "FileLogFilterEdit";
            this.FileLogFilterEdit.Size = new System.Drawing.Size(192, 21);
            this.FileLogFilterEdit.TabIndex = 1;
            // 
            // FileLogEnabledEdit
            // 
            this.FileLogEnabledEdit.AutoSize = true;
            this.FileLogEnabledEdit.Location = new System.Drawing.Point(6, 19);
            this.FileLogEnabledEdit.Name = "FileLogEnabledEdit";
            this.FileLogEnabledEdit.Size = new System.Drawing.Size(47, 17);
            this.FileLogEnabledEdit.TabIndex = 0;
            this.FileLogEnabledEdit.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_ENABLED;
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
            this.ConsoleLogBox.Location = new System.Drawing.Point(8, 6);
            this.ConsoleLogBox.Name = "ConsoleLogBox";
            this.ConsoleLogBox.Size = new System.Drawing.Size(296, 116);
            this.ConsoleLogBox.TabIndex = 0;
            this.ConsoleLogBox.TabStop = false;
            this.ConsoleLogBox.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_CONSOLE;
            // 
            // ConsoleLogDateFormatEdit
            // 
            this.ConsoleLogDateFormatEdit.Location = new System.Drawing.Point(95, 88);
            this.ConsoleLogDateFormatEdit.Name = "ConsoleLogDateFormatEdit";
            this.ConsoleLogDateFormatEdit.Size = new System.Drawing.Size(192, 20);
            this.ConsoleLogDateFormatEdit.TabIndex = 3;
            // 
            // ConsoleLogDateFormatLabel
            // 
            this.ConsoleLogDateFormatLabel.AutoSize = true;
            this.ConsoleLogDateFormatLabel.Location = new System.Drawing.Point(7, 92);
            this.ConsoleLogDateFormatLabel.Name = "ConsoleLogDateFormatLabel";
            this.ConsoleLogDateFormatLabel.Size = new System.Drawing.Size(78, 13);
            this.ConsoleLogDateFormatLabel.TabIndex = 13;
            this.ConsoleLogDateFormatLabel.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_DATE_FORMAT;
            // 
            // ConsoleLogStringFormatEdit
            // 
            this.ConsoleLogStringFormatEdit.Location = new System.Drawing.Point(95, 64);
            this.ConsoleLogStringFormatEdit.Name = "ConsoleLogStringFormatEdit";
            this.ConsoleLogStringFormatEdit.Size = new System.Drawing.Size(192, 20);
            this.ConsoleLogStringFormatEdit.TabIndex = 2;
            // 
            // ConsoleLogStringFormatLabel
            // 
            this.ConsoleLogStringFormatLabel.AutoSize = true;
            this.ConsoleLogStringFormatLabel.Location = new System.Drawing.Point(7, 68);
            this.ConsoleLogStringFormatLabel.Name = "ConsoleLogStringFormatLabel";
            this.ConsoleLogStringFormatLabel.Size = new System.Drawing.Size(39, 13);
            this.ConsoleLogStringFormatLabel.TabIndex = 11;
            this.ConsoleLogStringFormatLabel.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_FORMAT;
            // 
            // ConsoleLogFilterLabel
            // 
            this.ConsoleLogFilterLabel.AutoSize = true;
            this.ConsoleLogFilterLabel.Location = new System.Drawing.Point(7, 43);
            this.ConsoleLogFilterLabel.Name = "ConsoleLogFilterLabel";
            this.ConsoleLogFilterLabel.Size = new System.Drawing.Size(29, 13);
            this.ConsoleLogFilterLabel.TabIndex = 10;
            this.ConsoleLogFilterLabel.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_FILTER;
            // 
            // ConsoleLogFilterEdit
            // 
            this.ConsoleLogFilterEdit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ConsoleLogFilterEdit.FormattingEnabled = true;
            this.ConsoleLogFilterEdit.Location = new System.Drawing.Point(95, 39);
            this.ConsoleLogFilterEdit.Name = "ConsoleLogFilterEdit";
            this.ConsoleLogFilterEdit.Size = new System.Drawing.Size(192, 21);
            this.ConsoleLogFilterEdit.TabIndex = 1;
            // 
            // ConsoleLogEnabledEdit
            // 
            this.ConsoleLogEnabledEdit.AutoSize = true;
            this.ConsoleLogEnabledEdit.Location = new System.Drawing.Point(6, 19);
            this.ConsoleLogEnabledEdit.Name = "ConsoleLogEnabledEdit";
            this.ConsoleLogEnabledEdit.Size = new System.Drawing.Size(47, 17);
            this.ConsoleLogEnabledEdit.TabIndex = 0;
            this.ConsoleLogEnabledEdit.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_ENABLED;
            this.ConsoleLogEnabledEdit.UseVisualStyleBackColor = true;
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Cancel.Location = new System.Drawing.Point(435, 3);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 2;
            this.Cancel.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_CANCEL;
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // BottomPanel
            // 
            this.BottomPanel.Controls.Add(this.Apply);
            this.BottomPanel.Controls.Add(this.Cancel);
            this.BottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BottomPanel.Location = new System.Drawing.Point(0, 331);
            this.BottomPanel.Name = "BottomPanel";
            this.BottomPanel.Size = new System.Drawing.Size(513, 32);
            this.BottomPanel.TabIndex = 1;
            // 
            // Apply
            // 
            this.Apply.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Apply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Apply.Location = new System.Drawing.Point(352, 3);
            this.Apply.Name = "Apply";
            this.Apply.Size = new System.Drawing.Size(75, 23);
            this.Apply.TabIndex = 1;
            this.Apply.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_APPLY;
            this.Apply.UseVisualStyleBackColor = true;
            this.Apply.Click += new System.EventHandler(this.Apply_Click);
            // 
            // SaveFileDialog
            // 
            this.SaveFileDialog.DefaultExt = "log";
            this.SaveFileDialog.Filter = Bdt.GuiClient.Resources.Strings.SETUP_FORM_FILE_FILTER;
            this.SaveFileDialog.Title = Bdt.GuiClient.Resources.Strings.SETUP_FORM_CONFIGURATION;
            // 
            // SetupForm
            // 
            this.AcceptButton = this.Apply;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(513, 363);
            this.Controls.Add(this.BottomPanel);
            this.Controls.Add(this.Tabs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SetupForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = Bdt.GuiClient.Resources.Strings.SETUP_FORM_TITLE;
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
    }
}

