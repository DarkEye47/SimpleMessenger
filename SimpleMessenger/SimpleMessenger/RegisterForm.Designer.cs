namespace SimpleMessenger
{
    partial class RegisterForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Button buttonRegister;
        private System.Windows.Forms.Button buttonGoToLogin;

        private void InitializeComponent()
        {
            textBoxUsername = new TextBox();
            textBoxPassword = new TextBox();
            buttonRegister = new Button();
            buttonGoToLogin = new Button();
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
            // buttonRegister
            // 
            buttonRegister.Location = new Point(100, 150);
            buttonRegister.Name = "buttonRegister";
            buttonRegister.Size = new Size(200, 44);
            buttonRegister.TabIndex = 2;
            buttonRegister.Text = "Register";
            buttonRegister.Click += buttonRegister_Click;
            // 
            // buttonGoToLogin
            // 
            buttonGoToLogin.Location = new Point(12, 210);
            buttonGoToLogin.Name = "buttonGoToLogin";
            buttonGoToLogin.Size = new Size(145, 28);
            buttonGoToLogin.TabIndex = 3;
            buttonGoToLogin.Text = "Go to Login";
            buttonGoToLogin.Click += buttonGoToLogin_Click;
            // 
            // RegisterForm
            // 
            ClientSize = new Size(400, 250);
            Controls.Add(textBoxUsername);
            Controls.Add(textBoxPassword);
            Controls.Add(buttonRegister);
            Controls.Add(buttonGoToLogin);
            Name = "RegisterForm";
            Text = "Register Form";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}