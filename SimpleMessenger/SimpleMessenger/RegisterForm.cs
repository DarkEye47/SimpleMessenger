using System;
using System.Data.OleDb;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace SimpleMessenger
{
    public partial class RegisterForm : Form
    {
        private readonly string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\Database.accdb;Persist Security Info=False;";

        public RegisterForm()
        {
            InitializeComponent();
        }

        private void buttonRegister_Click(object sender, EventArgs e)
        {
            string username = textBoxUsername.Text;
            string password = textBoxPassword.Text;
            string passwordHash = ComputeSha256Hash(password);

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                string checkUserQuery = "SELECT COUNT(*) FROM Users WHERE Username = ?";
                using (OleDbCommand checkCommand = new OleDbCommand(checkUserQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("?", username);
                    int userCount = (int)checkCommand.ExecuteScalar();
                    if (userCount > 0)
                    {
                        MessageBox.Show("Username already exists. Please choose another one.");
                        return;
                    }
                }

                string insertQuery = "INSERT INTO Users (Username, PasswordHash) VALUES (?, ?)";
                using (OleDbCommand command = new OleDbCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("?", username);
                    command.Parameters.AddWithValue("?", passwordHash);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Registration successful!");
                    
                    // Очистка полей ввода
                    textBoxUsername.Clear();
                    textBoxPassword.Clear();

                    // Фокус на поле логина
                    textBoxUsername.Focus();
                }
            }
        }

        private static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void buttonGoToLogin_Click(object sender, EventArgs e)
        {
            var loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }
    }
}