using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace SimpleMessenger
{
    public partial class ChatForm : Form
    {
        private readonly string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\Database.accdb;Persist Security Info=False;";
        private string currentUsername;
        private string contactUsername;
        private string draftMessage = "";
        private System.Windows.Forms.Timer messageRefreshTimer;

        public ChatForm(string currentUsername, string contactUsername)
        {
            InitializeComponent();
            this.currentUsername = currentUsername;
            this.contactUsername = contactUsername;
            LoadMessages();

            messageRefreshTimer = new System.Windows.Forms.Timer();
            messageRefreshTimer.Interval = 5000; // 5 секунд
            messageRefreshTimer.Tick += new EventHandler(RefreshMessages);
            messageRefreshTimer.Start();
        }

        private void LoadMessages()
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Id, Sender, Content, Timestamp FROM Messages WHERE (Sender = ? AND Recipient = ?) OR (Sender = ? AND Recipient = ?) ORDER BY Timestamp";
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("?", currentUsername);
                    command.Parameters.AddWithValue("?", contactUsername);
                    command.Parameters.AddWithValue("?", contactUsername);
                    command.Parameters.AddWithValue("?", currentUsername);

                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        listBoxMessages.Items.Clear();
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string sender = reader.GetString(1);
                            string content = reader.GetString(2);
                            string displayedContent;

                            if (content.StartsWith("[STICKER]"))
                            {
                                string stickerUrl = content.Substring("[STICKER]".Length);
                                displayedContent = $"[Sticker: {stickerUrl}]";
                            }
                            else
                            {
                                displayedContent = EncryptionHelper.Decrypt(content);
                            }

                            DateTime timestamp = reader.GetDateTime(3);
                            listBoxMessages.Items.Add(new MessageItem(id, $"{timestamp} - {sender}: {displayedContent}"));
                        }
                    }
                }
            }
        }

        private void listBoxMessages_Click(object sender, EventArgs e)
        {
            if (listBoxMessages.SelectedItem != null)
            {
                var selectedMessage = (MessageItem)listBoxMessages.SelectedItem;
                if (selectedMessage.DisplayText.StartsWith("[Sticker:"))
                {
                    string stickerUrl = selectedMessage.DisplayText.Substring(10, selectedMessage.DisplayText.Length - 11);
                    ShowStickerInForm(stickerUrl);
                }
            }
        }

        private void ShowStickerInForm(string stickerUrl)
        {
            try
            {
                Form stickerForm = new Form
                {
                    Size = new Size(300, 300)
                };

                PictureBox pictureBox = new PictureBox
                {
                    ImageLocation = stickerUrl,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Dock = DockStyle.Fill
                };

                stickerForm.Controls.Add(pictureBox);
                stickerForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load sticker: " + ex.Message);
            }
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            string messageContent = textBoxInput.Text.Trim();
            if (!string.IsNullOrEmpty(messageContent))
            {
                string encryptedContent = EncryptionHelper.Encrypt(messageContent);
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Messages (Sender, Recipient, Content, [Timestamp]) VALUES (?, ?, ?, ?)";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.Add("Sender", OleDbType.VarChar).Value = currentUsername;
                        command.Parameters.Add("Recipient", OleDbType.VarChar).Value = contactUsername;
                        command.Parameters.Add("Content", OleDbType.VarChar).Value = encryptedContent;
                        command.Parameters.Add("Timestamp", OleDbType.Date).Value = DateTime.Now;
                        command.ExecuteNonQuery();
                        textBoxInput.Clear();
                        LoadMessages();
                    }
                }
            }
        }

        private void buttonDeleteMessage_Click(object sender, EventArgs e)
        {
            if (listBoxMessages.SelectedItem != null)
            {
                var selectedMessage = (MessageItem)listBoxMessages.SelectedItem;

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Messages WHERE Id = ?";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("?", selectedMessage.Id);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Message deleted!");
                        LoadMessages();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a message to delete.");
            }
        }

        private void buttonSelectSticker_Click(object sender, EventArgs e)
        {
            // Пример использования ссылки на стикер
            string stickerUrl = "https://imgur.com/XBQSoDb";
            SendSticker(stickerUrl);
        }

        private void SendSticker(string stickerUrl)
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Messages (Sender, Recipient, Content, [Timestamp]) VALUES (?, ?, ?, ?)";
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.Add("Sender", OleDbType.VarChar).Value = currentUsername;
                    command.Parameters.Add("Recipient", OleDbType.VarChar).Value = contactUsername;
                    command.Parameters.Add("Content", OleDbType.VarChar).Value = "[STICKER]" + stickerUrl;
                    command.Parameters.Add("Timestamp", OleDbType.Date).Value = DateTime.Now;

                    command.ExecuteNonQuery();
                    LoadMessages();
                }
            }
        }

        private void RefreshMessages(object sender, EventArgs e)
        {
            LoadMessages();
        }

        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            messageRefreshTimer.Stop();
        }

        private class MessageItem
        {
            public int Id { get; }
            public string DisplayText { get; }

            public MessageItem(int id, string displayText)
            {
                Id = id;
                DisplayText = displayText;
            }

            public override string ToString()
            {
                return DisplayText;
            }
        }

        private void buttonBackToContacts_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Would you like to save your draft?", "Exit Chat", MessageBoxButtons.YesNoCancel);

            if (result == DialogResult.Yes)
            {
                SaveDraft();
                this.Close();
            }
            else if (result == DialogResult.No)
            {
                this.Close();
            }
            // Cancel: остаемся в чате
        }

        private void SaveDraft()
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                string deleteQuery = "DELETE FROM Drafts WHERE Sender = ? AND Recipient = ?";
                using (OleDbCommand deleteCommand = new OleDbCommand(deleteQuery, connection))
                {
                    deleteCommand.Parameters.AddWithValue("?", currentUsername);
                    deleteCommand.Parameters.AddWithValue("?", contactUsername);
                    deleteCommand.ExecuteNonQuery();
                }

                if (!string.IsNullOrEmpty(draftMessage))
                {
                    string insertQuery = "INSERT INTO Drafts (Sender, Recipient, Content) VALUES (?, ?, ?)";
                    using (OleDbCommand insertCommand = new OleDbCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("?", currentUsername);
                        insertCommand.Parameters.AddWithValue("?", contactUsername);
                        insertCommand.Parameters.AddWithValue("?", draftMessage);
                        insertCommand.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}