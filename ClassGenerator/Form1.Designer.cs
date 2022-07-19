namespace ClassGenerator
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
            this.OutputBox = new System.Windows.Forms.TextBox();
            this.ConnectionGroupBox = new System.Windows.Forms.GroupBox();
            this.ConnectButton = new System.Windows.Forms.Button();
            this.CredentialsGroupBox = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ConnectionPassword = new System.Windows.Forms.TextBox();
            this.Username = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ConnectionType = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ConnectionDataSource = new System.Windows.Forms.TextBox();
            this.GenerateCodeGroupBox = new System.Windows.Forms.GroupBox();
            this.GenerateCodeButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.NameSpace = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TableName = new System.Windows.Forms.ComboBox();
            this.ConnectionGroupBox.SuspendLayout();
            this.CredentialsGroupBox.SuspendLayout();
            this.GenerateCodeGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // OutputBox
            // 
            this.OutputBox.Location = new System.Drawing.Point(472, 12);
            this.OutputBox.Multiline = true;
            this.OutputBox.Name = "OutputBox";
            this.OutputBox.ReadOnly = true;
            this.OutputBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.OutputBox.Size = new System.Drawing.Size(683, 571);
            this.OutputBox.TabIndex = 3;
            // 
            // ConnectionGroupBox
            // 
            this.ConnectionGroupBox.Controls.Add(this.ConnectButton);
            this.ConnectionGroupBox.Controls.Add(this.CredentialsGroupBox);
            this.ConnectionGroupBox.Controls.Add(this.label3);
            this.ConnectionGroupBox.Controls.Add(this.ConnectionType);
            this.ConnectionGroupBox.Controls.Add(this.label1);
            this.ConnectionGroupBox.Controls.Add(this.ConnectionDataSource);
            this.ConnectionGroupBox.Location = new System.Drawing.Point(12, 12);
            this.ConnectionGroupBox.Name = "ConnectionGroupBox";
            this.ConnectionGroupBox.Size = new System.Drawing.Size(454, 289);
            this.ConnectionGroupBox.TabIndex = 4;
            this.ConnectionGroupBox.TabStop = false;
            this.ConnectionGroupBox.Text = "Connection";
            // 
            // ConnectButton
            // 
            this.ConnectButton.Location = new System.Drawing.Point(6, 233);
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.Size = new System.Drawing.Size(207, 23);
            this.ConnectButton.TabIndex = 9;
            this.ConnectButton.Text = "Connect";
            this.ConnectButton.UseVisualStyleBackColor = true;
            this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
            // 
            // CredentialsGroupBox
            // 
            this.CredentialsGroupBox.Controls.Add(this.label2);
            this.CredentialsGroupBox.Controls.Add(this.label4);
            this.CredentialsGroupBox.Controls.Add(this.ConnectionPassword);
            this.CredentialsGroupBox.Controls.Add(this.Username);
            this.CredentialsGroupBox.Enabled = false;
            this.CredentialsGroupBox.Location = new System.Drawing.Point(6, 110);
            this.CredentialsGroupBox.Name = "CredentialsGroupBox";
            this.CredentialsGroupBox.Size = new System.Drawing.Size(207, 117);
            this.CredentialsGroupBox.TabIndex = 8;
            this.CredentialsGroupBox.TabStop = false;
            this.CredentialsGroupBox.Text = "Credentials";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Username";
            // 
            // ConnectionPassword
            // 
            this.ConnectionPassword.Location = new System.Drawing.Point(9, 81);
            this.ConnectionPassword.Name = "ConnectionPassword";
            this.ConnectionPassword.PasswordChar = '*';
            this.ConnectionPassword.Size = new System.Drawing.Size(190, 23);
            this.ConnectionPassword.TabIndex = 1;
            // 
            // Username
            // 
            this.Username.Location = new System.Drawing.Point(9, 37);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(190, 23);
            this.Username.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Connection Type";
            // 
            // ConnectionType
            // 
            this.ConnectionType.FormattingEnabled = true;
            this.ConnectionType.Items.AddRange(new object[] {
            "Integrated Security",
            "Username and Password"});
            this.ConnectionType.Location = new System.Drawing.Point(6, 81);
            this.ConnectionType.Name = "ConnectionType";
            this.ConnectionType.Size = new System.Drawing.Size(207, 23);
            this.ConnectionType.TabIndex = 4;
            this.ConnectionType.SelectedIndexChanged += new System.EventHandler(this.ConnectionType_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Data Source";
            // 
            // ConnectionDataSource
            // 
            this.ConnectionDataSource.Location = new System.Drawing.Point(6, 37);
            this.ConnectionDataSource.Name = "ConnectionDataSource";
            this.ConnectionDataSource.Size = new System.Drawing.Size(207, 23);
            this.ConnectionDataSource.TabIndex = 0;
            // 
            // GenerateCodeGroupBox
            // 
            this.GenerateCodeGroupBox.Controls.Add(this.GenerateCodeButton);
            this.GenerateCodeGroupBox.Controls.Add(this.label6);
            this.GenerateCodeGroupBox.Controls.Add(this.NameSpace);
            this.GenerateCodeGroupBox.Controls.Add(this.label5);
            this.GenerateCodeGroupBox.Controls.Add(this.TableName);
            this.GenerateCodeGroupBox.Enabled = false;
            this.GenerateCodeGroupBox.Location = new System.Drawing.Point(12, 307);
            this.GenerateCodeGroupBox.Name = "GenerateCodeGroupBox";
            this.GenerateCodeGroupBox.Size = new System.Drawing.Size(454, 276);
            this.GenerateCodeGroupBox.TabIndex = 5;
            this.GenerateCodeGroupBox.TabStop = false;
            this.GenerateCodeGroupBox.Text = "Generate Code";
            // 
            // GenerateCodeButton
            // 
            this.GenerateCodeButton.Location = new System.Drawing.Point(6, 119);
            this.GenerateCodeButton.Name = "GenerateCodeButton";
            this.GenerateCodeButton.Size = new System.Drawing.Size(205, 23);
            this.GenerateCodeButton.TabIndex = 6;
            this.GenerateCodeButton.Text = "Generate Code";
            this.GenerateCodeButton.UseVisualStyleBackColor = true;
            this.GenerateCodeButton.Click += new System.EventHandler(this.GenerateCodeButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 15);
            this.label6.TabIndex = 4;
            this.label6.Text = "Namespace";
            // 
            // NameSpace
            // 
            this.NameSpace.Location = new System.Drawing.Point(7, 81);
            this.NameSpace.Name = "NameSpace";
            this.NameSpace.Size = new System.Drawing.Size(206, 23);
            this.NameSpace.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 15);
            this.label5.TabIndex = 1;
            this.label5.Text = "Table Name";
            // 
            // TableName
            // 
            this.TableName.FormattingEnabled = true;
            this.TableName.Location = new System.Drawing.Point(6, 37);
            this.TableName.Name = "TableName";
            this.TableName.Size = new System.Drawing.Size(207, 23);
            this.TableName.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1167, 595);
            this.Controls.Add(this.GenerateCodeGroupBox);
            this.Controls.Add(this.ConnectionGroupBox);
            this.Controls.Add(this.OutputBox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ConnectionGroupBox.ResumeLayout(false);
            this.ConnectionGroupBox.PerformLayout();
            this.CredentialsGroupBox.ResumeLayout(false);
            this.CredentialsGroupBox.PerformLayout();
            this.GenerateCodeGroupBox.ResumeLayout(false);
            this.GenerateCodeGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private TextBox OutputBox;
        private GroupBox ConnectionGroupBox;
        private Label label3;
        private ComboBox ConnectionType;
        private Label label2;
        private Label label1;
        private TextBox ConnectionPassword;
        private TextBox ConnectionDataSource;
        private Label label4;
        private TextBox Username;
        private Button ConnectButton;
        private GroupBox CredentialsGroupBox;
        private GroupBox GenerateCodeGroupBox;
        private Label label6;
        private TextBox NameSpace;
        private Label label5;
        private ComboBox TableName;
        private Button GenerateCodeButton;
    }
}