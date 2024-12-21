using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace SimpleMessenger
{
    public partial class ContactsForm : Form
    {
        private readonly string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\Database.accdb;Persist Security Info=False;";
        private string currentUsername;

        public ContactsForm(string username)
        {
            InitializeComponent();
            currentUsername = username;
            LoadContacts();
        }

        private void LoadContacts()
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT ContactUsername FROM Contacts WHERE OwnerUsername = ?";
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("?", currentUsername);
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridViewContacts.DataSource = dt;
                    }
                }
            }
        }

        private void buttonAddContact_Click(object sender, EventArgs e)
        {
            string contactUsername = Prompt.ShowDialog("Enter contact username:", "Add Contact");

            if (string.IsNullOrEmpty(contactUsername))
            {
                MessageBox.Show("Please enter a valid username.");
                return;
            }

            if (contactUsername == currentUsername)
            {
                MessageBox.Show("You cannot add yourself as a contact.");
                return;
            }

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                // Проверка на существование пользователя
                string checkUserQuery = "SELECT COUNT(*) FROM Users WHERE Username = ?";
                using (OleDbCommand checkUserCommand = new OleDbCommand(checkUserQuery, connection))
                {
                    checkUserCommand.Parameters.AddWithValue("?", contactUsername);
                    int userCount = (int)checkUserCommand.ExecuteScalar();
                    if (userCount == 0)
                    {
                        MessageBox.Show("User does not exist.");
                        return;
                    }
                }

                // Проверка на дублирование контакта
                string checkContactQuery = "SELECT COUNT(*) FROM Contacts WHERE OwnerUsername = ? AND ContactUsername = ?";
                using (OleDbCommand checkContactCommand = new OleDbCommand(checkContactQuery, connection))
                {
                    checkContactCommand.Parameters.AddWithValue("?", currentUsername);
                    checkContactCommand.Parameters.AddWithValue("?", contactUsername);
                    int contactCount = (int)checkContactCommand.ExecuteScalar();
                    if (contactCount > 0)
                    {
                        MessageBox.Show("This contact is already in your list.");
                        return;
                    }
                }

                // Добавление контакта
                string query = "INSERT INTO Contacts (OwnerUsername, ContactUsername) VALUES (?, ?)";
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("?", currentUsername);
                    command.Parameters.AddWithValue("?", contactUsername);
                    command.ExecuteNonQuery();
                    MessageBox.Show("Contact added!");
                    LoadContacts();
                }
            }
        }

        private void buttonDeleteContact_Click(object sender, EventArgs e)
        {
            if (dataGridViewContacts.SelectedRows.Count > 0)
            {
                string contactUsername = dataGridViewContacts.SelectedRows[0].Cells[0].Value.ToString();

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Contacts WHERE OwnerUsername = ? AND ContactUsername = ?";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("?", currentUsername);
                        command.Parameters.AddWithValue("?", contactUsername);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Contact deleted!");
                        LoadContacts();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a contact to delete.");
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string searchQuery = textBoxSearch.Text.Trim();

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();

                string query;
                if (string.IsNullOrEmpty(searchQuery))
                {
                    query = "SELECT ContactUsername FROM Contacts WHERE OwnerUsername = ?";
                }
                else
                {
                    query = "SELECT ContactUsername FROM Contacts WHERE OwnerUsername = ? AND ContactUsername LIKE ?";
                }

                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("?", currentUsername);
                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        command.Parameters.AddWithValue("?", "%" + searchQuery + "%");
                    }

                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dataGridViewContacts.DataSource = dt;
                    }
                }
            }
        }
        private void buttonOpenChat_Click(object sender, EventArgs e)
        {
            if (dataGridViewContacts.SelectedRows.Count > 0)
            {
                var selectedRow = dataGridViewContacts.SelectedRows[0];
                if (!selectedRow.IsNewRow) // Проверка на новую строку
                {
                    string contactUsername = selectedRow.Cells[0].Value.ToString();
                    OpenChat(contactUsername);
                }
                else
                {
                    MessageBox.Show("Please select a valid contact.");
                }
            }
            else
            {
                MessageBox.Show("Please select a contact to open chat.");
            }
        }

        private void OpenChat(string contactUsername)
        {
            var chatForm = new ChatForm(currentUsername, contactUsername);
            this.Hide();
            
            chatForm.FormClosed += (s, args) => this.Show();
            chatForm.ShowDialog();
        }
    }

    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 300,
                Height = 150,
                Text = caption
            };
            Label textLabel = new Label() { Left = 20, Top = 20, Text = text };
            TextBox textBox = new TextBox() { Left = 20, Top = 50, Width = 240 };
            Button confirmation = new Button() { Text = "OK", Left = 180, Width = 80, Top = 80 };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.ShowDialog();
            return textBox.Text;
        }
    }
}