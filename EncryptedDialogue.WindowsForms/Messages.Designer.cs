namespace EncryptedDialogue.WindowsForms
{
    partial class Messages
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
            this.richTextBoxMessages = new System.Windows.Forms.RichTextBox();
            this.comboBoxUsersLogin = new System.Windows.Forms.ComboBox();
            this.richTextBoxEncrypting = new System.Windows.Forms.RichTextBox();
            this.labelKey = new System.Windows.Forms.Label();
            this.textBoxKey = new System.Windows.Forms.TextBox();
            this.buttonEncrypt = new System.Windows.Forms.Button();
            this.buttonDecrypt = new System.Windows.Forms.Button();
            this.buttonSend = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonCaesar = new System.Windows.Forms.RadioButton();
            this.radioButtonVizhenera = new System.Windows.Forms.RadioButton();
            this.radioButtonTransposition = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBoxMessages
            // 
            this.richTextBoxMessages.Location = new System.Drawing.Point(12, 12);
            this.richTextBoxMessages.Name = "richTextBoxMessages";
            this.richTextBoxMessages.Size = new System.Drawing.Size(333, 426);
            this.richTextBoxMessages.TabIndex = 0;
            this.richTextBoxMessages.Text = "";
            // 
            // comboBoxUsersLogin
            // 
            this.comboBoxUsersLogin.FormattingEnabled = true;
            this.comboBoxUsersLogin.Location = new System.Drawing.Point(351, 12);
            this.comboBoxUsersLogin.Name = "comboBoxUsersLogin";
            this.comboBoxUsersLogin.Size = new System.Drawing.Size(121, 21);
            this.comboBoxUsersLogin.TabIndex = 1;
            this.comboBoxUsersLogin.SelectedIndexChanged += new System.EventHandler(this.ChooseDialogue);
            // 
            // richTextBoxEncrypting
            // 
            this.richTextBoxEncrypting.Location = new System.Drawing.Point(478, 12);
            this.richTextBoxEncrypting.Name = "richTextBoxEncrypting";
            this.richTextBoxEncrypting.Size = new System.Drawing.Size(333, 426);
            this.richTextBoxEncrypting.TabIndex = 2;
            this.richTextBoxEncrypting.Text = "";
            // 
            // labelKey
            // 
            this.labelKey.AutoSize = true;
            this.labelKey.Location = new System.Drawing.Point(6, 114);
            this.labelKey.Name = "labelKey";
            this.labelKey.Size = new System.Drawing.Size(25, 13);
            this.labelKey.TabIndex = 3;
            this.labelKey.Text = "Key";
            // 
            // textBoxKey
            // 
            this.textBoxKey.Location = new System.Drawing.Point(6, 130);
            this.textBoxKey.Name = "textBoxKey";
            this.textBoxKey.Size = new System.Drawing.Size(109, 20);
            this.textBoxKey.TabIndex = 4;
            // 
            // buttonEncrypt
            // 
            this.buttonEncrypt.Location = new System.Drawing.Point(6, 156);
            this.buttonEncrypt.Name = "buttonEncrypt";
            this.buttonEncrypt.Size = new System.Drawing.Size(109, 23);
            this.buttonEncrypt.TabIndex = 5;
            this.buttonEncrypt.Text = "Encrypt";
            this.buttonEncrypt.UseVisualStyleBackColor = true;
            this.buttonEncrypt.Click += new System.EventHandler(this.Encrypt);
            // 
            // buttonDecrypt
            // 
            this.buttonDecrypt.Location = new System.Drawing.Point(6, 185);
            this.buttonDecrypt.Name = "buttonDecrypt";
            this.buttonDecrypt.Size = new System.Drawing.Size(109, 23);
            this.buttonDecrypt.TabIndex = 6;
            this.buttonDecrypt.Text = "Decrypt";
            this.buttonDecrypt.UseVisualStyleBackColor = true;
            this.buttonDecrypt.Click += new System.EventHandler(this.Decrypt);
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(351, 39);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(121, 23);
            this.buttonSend.TabIndex = 7;
            this.buttonSend.Text = "Send";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.SendMessage);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonTransposition);
            this.groupBox1.Controls.Add(this.radioButtonVizhenera);
            this.groupBox1.Controls.Add(this.radioButtonCaesar);
            this.groupBox1.Controls.Add(this.buttonDecrypt);
            this.groupBox1.Controls.Add(this.buttonEncrypt);
            this.groupBox1.Controls.Add(this.labelKey);
            this.groupBox1.Controls.Add(this.textBoxKey);
            this.groupBox1.Location = new System.Drawing.Point(351, 222);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(121, 216);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // radioButtonCaesar
            // 
            this.radioButtonCaesar.AutoSize = true;
            this.radioButtonCaesar.Location = new System.Drawing.Point(6, 15);
            this.radioButtonCaesar.Name = "radioButtonCaesar";
            this.radioButtonCaesar.Size = new System.Drawing.Size(58, 17);
            this.radioButtonCaesar.TabIndex = 7;
            this.radioButtonCaesar.TabStop = true;
            this.radioButtonCaesar.Text = "Caesar";
            this.radioButtonCaesar.UseVisualStyleBackColor = true;
            // 
            // radioButtonVizhenera
            // 
            this.radioButtonVizhenera.AutoSize = true;
            this.radioButtonVizhenera.Location = new System.Drawing.Point(6, 38);
            this.radioButtonVizhenera.Name = "radioButtonVizhenera";
            this.radioButtonVizhenera.Size = new System.Drawing.Size(72, 17);
            this.radioButtonVizhenera.TabIndex = 8;
            this.radioButtonVizhenera.TabStop = true;
            this.radioButtonVizhenera.Text = "Vizhenera";
            this.radioButtonVizhenera.UseVisualStyleBackColor = true;
            // 
            // radioButtonTransposition
            // 
            this.radioButtonTransposition.AutoSize = true;
            this.radioButtonTransposition.Location = new System.Drawing.Point(6, 61);
            this.radioButtonTransposition.Name = "radioButtonTransposition";
            this.radioButtonTransposition.Size = new System.Drawing.Size(88, 17);
            this.radioButtonTransposition.TabIndex = 9;
            this.radioButtonTransposition.TabStop = true;
            this.radioButtonTransposition.Text = "Transposition";
            this.radioButtonTransposition.UseVisualStyleBackColor = true;
            // 
            // Messages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 450);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.richTextBoxEncrypting);
            this.Controls.Add(this.comboBoxUsersLogin);
            this.Controls.Add(this.richTextBoxMessages);
            this.Name = "Messages";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Messages";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Messages_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxMessages;
        private System.Windows.Forms.ComboBox comboBoxUsersLogin;
        private System.Windows.Forms.RichTextBox richTextBoxEncrypting;
        private System.Windows.Forms.Label labelKey;
        private System.Windows.Forms.TextBox textBoxKey;
        private System.Windows.Forms.Button buttonEncrypt;
        private System.Windows.Forms.Button buttonDecrypt;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonVizhenera;
        private System.Windows.Forms.RadioButton radioButtonCaesar;
        private System.Windows.Forms.RadioButton radioButtonTransposition;
    }
}