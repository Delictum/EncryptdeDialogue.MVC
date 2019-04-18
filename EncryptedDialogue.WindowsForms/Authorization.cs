using System;
using System.Windows.Forms;
using EncryptedDialogue.BL;

namespace EncryptedDialogue.WindowsForms
{
    public partial class Autorization : Form
    {
        public Autorization()
        {
            InitializeComponent();
        }

        private void LogIn(object sender, EventArgs e)
        {
            var u = new Unity();

            if (u.CheckLogIn(textBoxLogin.Text, textBoxPassword.Text))
            {
                Hide();
                var messages = new Messages(textBoxLogin.Text);
                messages.Show();
            }
            else
            {
                MessageBox.Show(@"Invalid login or password!");
            }
        }

        private void KeyDownTextBoxes(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                LogIn(sender, e);
        }
    }
}
