namespace Bdt.GuiClient.Forms
{
    partial class ProxyForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProxyForm));
            this.UserNameLabel = new System.Windows.Forms.Label();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.PasswordEdit = new System.Windows.Forms.TextBox();
            this.UserNameEdit = new System.Windows.Forms.TextBox();
            this.SubTitleLabel = new System.Windows.Forms.Label();
            this.DomainLabel = new System.Windows.Forms.Label();
            this.DomainEdit = new System.Windows.Forms.TextBox();
            this.Apply = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // UserNameLabel
            // 
            this.UserNameLabel.AutoSize = true;
            this.UserNameLabel.Location = new System.Drawing.Point(12, 39);
            this.UserNameLabel.Name = "UserNameLabel";
            this.UserNameLabel.Size = new System.Drawing.Size(53, 13);
            this.UserNameLabel.TabIndex = 0;
            this.UserNameLabel.Text = Bdt.GuiClient.Resources.Strings.PROXYFORM_USER;
            // 
            // PasswordLabel
            // 
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Location = new System.Drawing.Point(12, 69);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(71, 13);
            this.PasswordLabel.TabIndex = 1;
            this.PasswordLabel.Text = Bdt.GuiClient.Resources.Strings.PROXYFORM_PASSWORD;
            // 
            // PasswordEdit
            // 
            this.PasswordEdit.Location = new System.Drawing.Point(89, 69);
            this.PasswordEdit.Name = "PasswordEdit";
            this.PasswordEdit.PasswordChar = '*';
            this.PasswordEdit.Size = new System.Drawing.Size(216, 20);
            this.PasswordEdit.TabIndex = 1;
            // 
            // UserNameEdit
            // 
            this.UserNameEdit.Location = new System.Drawing.Point(89, 39);
            this.UserNameEdit.Name = "UserNameEdit";
            this.UserNameEdit.Size = new System.Drawing.Size(216, 20);
            this.UserNameEdit.TabIndex = 0;
            // 
            // SubTitleLabel
            // 
            this.SubTitleLabel.AutoSize = true;
            this.SubTitleLabel.Location = new System.Drawing.Point(41, 9);
            this.SubTitleLabel.Name = "SubTitleLabel";
            this.SubTitleLabel.Size = new System.Drawing.Size(232, 13);
            this.SubTitleLabel.TabIndex = 4;
            this.SubTitleLabel.Text = Bdt.GuiClient.Resources.Strings.PROXYFORM_AUTH_REQUESTED;
            // 
            // DomainLabel
            // 
            this.DomainLabel.AutoSize = true;
            this.DomainLabel.Location = new System.Drawing.Point(12, 95);
            this.DomainLabel.Name = "DomainLabel";
            this.DomainLabel.Size = new System.Drawing.Size(49, 13);
            this.DomainLabel.TabIndex = 5;
            this.DomainLabel.Text = Bdt.GuiClient.Resources.Strings.PROXYFORM_DOMAIN;
            // 
            // DomainEdit
            // 
            this.DomainEdit.Location = new System.Drawing.Point(89, 95);
            this.DomainEdit.Name = "DomainEdit";
            this.DomainEdit.Size = new System.Drawing.Size(216, 20);
            this.DomainEdit.TabIndex = 2;
            // 
            // Apply
            // 
            this.Apply.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Apply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Apply.Location = new System.Drawing.Point(149, 131);
            this.Apply.Name = "Apply";
            this.Apply.Size = new System.Drawing.Size(75, 23);
            this.Apply.TabIndex = 3;
            this.Apply.Text = Bdt.GuiClient.Resources.Strings.PROXYFORM_APPLY;
            this.Apply.UseVisualStyleBackColor = true;
            this.Apply.Click += new System.EventHandler(this.Apply_Click);
            // 
            // Cancel
            // 
            this.Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Cancel.Location = new System.Drawing.Point(230, 131);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(75, 23);
            this.Cancel.TabIndex = 4;
            this.Cancel.Text = Bdt.GuiClient.Resources.Strings.PROXYFORM_CANCEL;
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // ProxyForm
            // 
            this.AcceptButton = this.Apply;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel;
            this.ClientSize = new System.Drawing.Size(308, 161);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Apply);
            this.Controls.Add(this.DomainEdit);
            this.Controls.Add(this.DomainLabel);
            this.Controls.Add(this.SubTitleLabel);
            this.Controls.Add(this.UserNameEdit);
            this.Controls.Add(this.PasswordEdit);
            this.Controls.Add(this.PasswordLabel);
            this.Controls.Add(this.UserNameLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProxyForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = Bdt.GuiClient.Resources.Strings.PROXYFORM_TITLE;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ProxyForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label UserNameLabel;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.TextBox PasswordEdit;
        private System.Windows.Forms.TextBox UserNameEdit;
        private System.Windows.Forms.Label SubTitleLabel;
        private System.Windows.Forms.Label DomainLabel;
        private System.Windows.Forms.TextBox DomainEdit;
        private System.Windows.Forms.Button Apply;
        private System.Windows.Forms.Button Cancel;
    }
}