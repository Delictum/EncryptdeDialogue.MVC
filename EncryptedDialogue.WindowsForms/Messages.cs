using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;
using EncryptedDialogue.BL;
using EncryptedDialogue.BL.Ciphers;

namespace EncryptedDialogue.WindowsForms
{
    public partial class Messages : Form
    {
        protected string RecipientLogin { get; set; }

        public Messages(string login)
        {
            RecipientLogin = login;
            InitializeComponent();
            InitializeComboBox();
#pragma warning disable 4014
            SelectMessages();
#pragma warning restore 4014
            radioButtonCaesar.Checked = true;
        }

        private void InitializeComboBox()
        {
            var u = new Unity();
            try
            {
                // ReSharper disable once CoVariantArrayConversion
                comboBoxUsersLogin.Items.AddRange(u.GetAllLoginUsers().ToArray());
                comboBoxUsersLogin.Text = comboBoxUsersLogin.Items[0].ToString();
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    MessageBox.Show(@"Error initializing users. " + e.InnerException.Message);
                }
                else
                {
                    MessageBox.Show(@"Error initializing users.");
                }
            }
        }

        private async Task SelectMessages()
        {
            var u = new Unity();
            try
            {
                var messages = u.GetMessages(RecipientLogin, comboBoxUsersLogin.Text);
                richTextBoxMessages.Clear();
                if (messages.Count == 0)
                {
                    richTextBoxMessages.Text = @"No messages.";
                    return;
                }
                foreach (var message in messages)
                {
                    richTextBoxMessages.Text += string.Join("\t--->\t", message.Item1, message.Item2, message.Item3) + Environment.NewLine;
                }

                while (true)
                {
                    await Task.Delay(30000);
                    await SelectMessages();
                }
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    MessageBox.Show(@"Error initializing messages. " + e.InnerException.Message);
                }
                else
                {
                    MessageBox.Show(@"Error initializing messages.");
                }
            }
        }

        private void Messages_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void ChooseDialogue(object sender, EventArgs e)
        {
#pragma warning disable 4014
            SelectMessages();
#pragma warning restore 4014
        }

        private void SendMessage(object sender, EventArgs e)
        {
            var u = new Unity();
            try
            {
                u.SendMessage(RecipientLogin, comboBoxUsersLogin.Text, richTextBoxEncrypting.Text);
            }
            catch (Exception exception)
            {
                if (exception.InnerException != null)
                {
                    MessageBox.Show(@"Error send message. " + exception.InnerException.Message);
                }
                else
                {
                    MessageBox.Show(@"Error send message.");
                }
            }
#pragma warning disable 4014
            SelectMessages();
#pragma warning restore 4014
        }

        private void Encrypt(object sender, EventArgs e)
        {
            ICipher cipher;
            if (radioButtonCaesar.Checked)
            {
                cipher = new CaesarCipher();
            }
            else if (radioButtonVizhenera.Checked)
            {
                cipher = new VizheneraCipher();
            }
            else
            {
                cipher = new TranspositionCipher();
            }

            richTextBoxEncrypting.Text = cipher.Encrypt(richTextBoxEncrypting.Text, textBoxKey.Text);
        }

        private void Decrypt(object sender, EventArgs e)
        {
            ICipher cipher;
            if (radioButtonCaesar.Checked)
            {
                cipher = new CaesarCipher();
            }
            else if (radioButtonVizhenera.Checked)
            {
                cipher = new VizheneraCipher();
            }
            else 
            {
                cipher = new TranspositionCipher();
            }

            richTextBoxEncrypting.Text = cipher.Decrypt(richTextBoxEncrypting.Text, textBoxKey.Text);
        }
    }
}
