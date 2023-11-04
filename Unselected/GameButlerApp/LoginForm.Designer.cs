namespace SoftwareArchitectureWinformsApp
{
    partial class LoginForm
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
            username = new TextBox();
            password = new TextBox();
            button1 = new Button();
            label1 = new Label();
            label2 = new Label();
            QRLabel = new Label();
            SuspendLayout();
            // 
            // username
            // 
            username.Location = new Point(46, 105);
            username.Name = "username";
            username.Size = new Size(252, 23);
            username.TabIndex = 0;
            // 
            // password
            // 
            password.Location = new Point(46, 217);
            password.Name = "password";
            password.PasswordChar = '*';
            password.Size = new Size(252, 23);
            password.TabIndex = 1;
            // 
            // button1
            // 
            button1.Location = new Point(137, 316);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 2;
            button1.Text = "Login";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(125, 87);
            label1.Name = "label1";
            label1.Size = new Size(96, 15);
            label1.TabIndex = 3;
            label1.Text = "Steam Username";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(146, 199);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 4;
            label2.Text = "Password";
            // 
            // QRLabel
            // 
            QRLabel.AutoSize = true;
            QRLabel.Location = new Point(329, 141);
            QRLabel.Name = "QRLabel";
            QRLabel.Size = new Size(0, 15);
            QRLabel.TabIndex = 6;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(343, 450);
            Controls.Add(QRLabel);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(password);
            Controls.Add(username);
            Name = "LoginForm";
            Text = "AuthenticationForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox username;
        private TextBox password;
        private Button button1;
        private Label label1;
        private Label label2;
        private Label QRLabel;
    }
}