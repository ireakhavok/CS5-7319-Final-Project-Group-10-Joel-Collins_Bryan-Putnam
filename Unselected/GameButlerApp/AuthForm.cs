using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoftwareArchitectureWinformsApp
{
    public partial class AuthForm : Form
    {
        public enum typeOfForm
        {
            twofactor,
            email,
            APIkey
        }
        public string _code;
        public AuthForm(typeOfForm typeOfFormToMake)
        {
            InitializeComponent();
            if (typeOfFormToMake == typeOfForm.twofactor)
            {
                this.Code.Text = "Two Factor Authenticator code:\n (From steam app in phone):";
                this.Text = "Two Factor Authenticator.";
            }
            else if (typeOfFormToMake == typeOfForm.email)
            {
                this.Code.Text = "Email Authenticator code:\n (In Email from Steam):";
                this.Text = "Email Authenticator.";
            }
            else if (typeOfFormToMake == typeOfForm.APIkey)
            {
                this.Code.Text = "Please Provide your API key\n It will be saved in App Data.\n You can get one here:\n https://steamcommunity.com/dev/apikey";
                this.Text = "Steam API key.";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _code = this.textBoxCode.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
