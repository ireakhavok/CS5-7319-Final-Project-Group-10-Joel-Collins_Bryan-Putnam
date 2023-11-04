using DAL_API;
using Microsoft.AspNetCore.Http;
using QRCoder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SoftwareArchitectureWinformsApp
{
    public partial class LoginForm : Form
    {
        public string Message { get; set; }
        public string userID { get; set; }

        public LoginForm()
        {
            this.Text = "Please Log in to steam.";
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var steamLogin = new SteamLogin();
            Dictionary<string, string> loggedin = steamLogin.DoLogin(this.username.Text, this.password.Text);
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\GameButler"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\GameButler");
            }
            var writer = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\GameButler\\cookies.txt");
            foreach (DictionaryEntry item in (IDictionary)loggedin)
            {
                writer.WriteLine(item.Key + "=" + item.Value);
            }
            writer.Close();

            string emailAuthNeeded = "";
            var valIsPresent = loggedin.TryGetValue("EmailAuthNeeded", out emailAuthNeeded);
            if (valIsPresent && emailAuthNeeded == "true")
            {
                AuthForm emailAuth = new AuthForm(AuthForm.typeOfForm.email);
                emailAuth.ShowDialog();
                if (emailAuth.DialogResult == DialogResult.OK)
                {
                    loggedin = steamLogin.DoLogin(this.username.Text, this.password.Text, emailAuth._code, true, false);
                }
                else
                {
                    return;
                }
            }
            string TwoFactorNeeded = "";
            valIsPresent = loggedin.TryGetValue("TwoFactorNeeded", out TwoFactorNeeded);
            if (valIsPresent && TwoFactorNeeded == "true")
            {
                AuthForm twoFactorAuth = new AuthForm(AuthForm.typeOfForm.twofactor);
                twoFactorAuth.ShowDialog();
                if (twoFactorAuth.DialogResult == DialogResult.OK)
                {
                    loggedin = steamLogin.DoLogin(this.username.Text, this.password.Text, twoFactorAuth._code, false, true);
                }
                else
                {
                    return;
                }
            }
            string ChallengeURL = "";
            valIsPresent = loggedin.TryGetValue("ChallengeURL", out ChallengeURL);
            if (valIsPresent && ChallengeURL != "")
            {
                // Encode the link as a QR code
                var qrGenerator = new QRCodeGenerator();
                var qrCodeData = qrGenerator.CreateQrCode(loggedin["ChallengeURL"], QRCodeGenerator.ECCLevel.L);
                var qrCode = new AsciiQRCode(qrCodeData);
                var qrCodeAsAsciiArt = qrCode.GetGraphic(1, drawQuietZones: false);

                this.QRLabel.Text = qrCodeAsAsciiArt;

                //TODO need to send it back to steam for login. 

            }
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\GameButler"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\GameButler");
            }
            writer = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\GameButler\\cookies.txt");
            foreach (DictionaryEntry item in (IDictionary)loggedin)
            {
                writer.WriteLine(item.Key + "=" + item.Value);
            }
            writer.Close();


            this.userID = steamLogin.SteamUserID;
            this.Message = loggedin["success"];
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
