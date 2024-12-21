namespace SimpleMessenger
{
    partial class LoginForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Button buttonGoToRegister;

        private void InitializeComponent()
        {
            textBoxUsername = new TextBox();
            textBoxPassword = new TextBox();
            buttonLogin = new Button();
            buttonGoToRegister = new Button();
            SuspendLayout();
            // 
            // textBoxUsername
            // 
            textBoxUsername.Location = new Point(100, 50);
            textBoxUsername.Name = "textBoxUsername";
            textBoxUsername.Size = new Size(200, 27);
            textBoxUsername.TabIndex = 0;
            // 
            // textBoxPassword
            // 
            textBoxPassword.Location = new Point(100, 100);
            textBoxPassword.Name = "textBoxPassword";
            textBoxPassword.Size = new Size(200, 27);
            textBoxPassword.TabIndex = 1;
            // 
            // buttonLogin
            // 
            buttonLogin.Location = new Point(100, 150);
            buttonLogin.Name = "buttonLogin";
            buttonLogin.Size = new Size(200, 44);
            buttonLogin.TabIndex = 2;
            buttonLogin.Text = "Login";
            buttonLogin.Click += buttonLogin_Click;
            // 
            // buttonGoToRegister
            // 
            buttonGoToRegister.Location = new Point(12, 210);
            buttonGoToRegister.Name = "buttonGoToRegister";
            buttonGoToRegister.Size = new Size(145, 28);
            buttonGoToRegister.TabIndex = 3;
            buttonGoToRegister.Text = "Go to Register";
            buttonGoToRegister.Click += buttonGoToRegister_Click;
            // 
            // LoginForm
            // 
            ClientSize = new Size(400, 250);
            Controls.Add(textBoxUsername);
            Controls.Add(textBoxPassword);
            Controls.Add(buttonLogin);
            Controls.Add(buttonGoToRegister);
            Name = "LoginForm";
            Text = "Login Form";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
