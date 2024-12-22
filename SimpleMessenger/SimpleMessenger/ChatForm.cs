using System;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Http;
using System.Threading.Tasks;
using System.Drawing;

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

            messageRefreshTimer = new System.Windows.Forms.Timer();
            messageRefreshTimer.Interval = 5000; // 5 секунд
            messageRefreshTimer.Tick += new EventHandler(RefreshMessages);
            messageRefreshTimer.Start();

            LoadMessages();
            textBoxInput.KeyDown += TextBoxInput_KeyDown;
        }

        private void LoadMessages()
        {
            try
            {
                tableLayoutPanelMessages.Controls.Clear(); // Очищаем существующие сообщения

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
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string sender = reader.GetString(1);
                                string encryptedContent = reader.GetString(2);
                                DateTime timestamp = reader.GetDateTime(3);

                                string content;

                                if (encryptedContent.StartsWith("[STICKER]"))
                                {
                                    // Для стикеров не расшифровываем, используем как есть
                                    content = encryptedContent.Substring(9); // Убираем [STICKER] префикс для показа URL
                                }
                                else
                                {
                                    // Расшифровываем обычные текстовые сообщения
                                    content = EncryptionHelper.Decrypt(encryptedContent);
                                }

                                AddMessageToTable(id, sender == currentUsername, content, timestamp);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading messages: " + ex.Message);
            }
        }

        private void AddMessageToTable(int id, bool isCurrentUser, string content, DateTime timestamp)
        {
            Control messageControl;

            if (content.StartsWith("[STICKER]"))
            {
                string stickerFilename = content.Substring("[STICKER]".Length);

                var pictureBox = new PictureBox
                {
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Size = new Size(150, 150),
                    Margin = new Padding(3)
                };

                try
                {
                    string appPath = AppDomain.CurrentDomain.BaseDirectory;
                    string localImagePath = Path.Combine(appPath, "Image", stickerFilename);

                    if (File.Exists(localImagePath))
                    {
                        pictureBox.Image = Image.FromFile(localImagePath);
                    }
                    else
                    {
                        MessageBox.Show("Image file not found: " + localImagePath);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to load local image: " + ex.Message);
                }

                messageControl = pictureBox;
            }
            else
            {
                string displayedText = isCurrentUser
                    ? $"{content} - {timestamp.ToShortTimeString()}"
                    : $"{timestamp.ToShortTimeString()} - {content}";

                var messageLabel = new Label
                {
                    Text = displayedText,
                    AutoSize = true,
                    MaximumSize = new Size(tableLayoutPanelMessages.ClientSize.Width / 2 - 10, 0),
                    BackColor = Color.Transparent,
                    Padding = new Padding(7),
                    Margin = new Padding(3),
                    TextAlign = ContentAlignment.MiddleLeft
                };

                messageControl = messageLabel;
            }

            var roundedPanel = new RoundedPanel
            {
                AutoSize = true,
                MaximumSize = new Size(tableLayoutPanelMessages.ClientSize.Width / 2, int.MaxValue),
                BackColor = isCurrentUser ? Color.LightSkyBlue : Color.LightGray,
                Padding = new Padding(10)
            };

            roundedPanel.Controls.Add(messageControl);

            if (isCurrentUser)
            {
                var deleteButton = new Button
                {
                    Text = "Delete",
                    AutoSize = true,
                    Margin = new Padding(3)
                };
                deleteButton.Click += (s, e) => DeleteMessage(id);
                roundedPanel.Controls.Add(deleteButton);

                // Dock the panel to the right
                roundedPanel.Dock = DockStyle.Right;
                tableLayoutPanelMessages.Controls.Add(roundedPanel, 1, tableLayoutPanelMessages.RowCount - 1);
            }
            else
            {
                roundedPanel.Dock = DockStyle.Left;
                tableLayoutPanelMessages.Controls.Add(roundedPanel, 0, tableLayoutPanelMessages.RowCount - 1);
            }

            tableLayoutPanelMessages.RowCount++;
            tableLayoutPanelMessages.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        }

        // Обработчик события KeyDown
        private void TextBoxInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;  // Предотвращает звуковой сигнал при нажатии Enter
                SendMessage(); // Вызов метода отправки сообщения
            }
        }

        // Метод отправки сообщения
        private void SendMessage()
        {
            string messageContent = textBoxInput.Text.Trim();
            if (!string.IsNullOrEmpty(messageContent))
            {
                string contentToStore = EncryptionHelper.Encrypt(messageContent); // Шифруем сообщение

                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Messages (Sender, Recipient, Content, [Timestamp]) VALUES (?, ?, ?, ?)";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.Add("Sender", OleDbType.VarChar).Value = currentUsername;
                        command.Parameters.Add("Recipient", OleDbType.VarChar).Value = contactUsername;
                        command.Parameters.Add("Content", OleDbType.VarChar).Value = contentToStore;
                        command.Parameters.Add("Timestamp", OleDbType.Date).Value = DateTime.Now;
                        command.ExecuteNonQuery();
                        textBoxInput.Clear();
                        LoadMessages(); // Обновление списка сообщений
                    }
                }
            }
        }

        // Метод удаления сообщения
        private void DeleteMessage(int messageId)
        {
            var dialogResult = MessageBox.Show("Are you sure you want to delete this message?", "Delete Message", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                using (OleDbConnection connection = new OleDbConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Messages WHERE Id = ?";
                    using (OleDbCommand command = new OleDbCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("?", messageId);
                        command.ExecuteNonQuery();
                    }
                }

                LoadMessages(); // Перезагружаем список сообщений после удаления
            }
        }

        private void RefreshMessages(object sender, EventArgs e)
        {
            LoadMessages();
        }

        // Убедитесь, что метод кнопки "Отправить" вызывает тот же метод
        private void buttonSend_Click(object sender, EventArgs e)
        {
            SendMessage();
        }


        private void buttonSelectSticker_Click(object sender, EventArgs e)
        {
            // Укажите здесь название файла стикера
            string stickerFilename = "goku_bnlayt.png"; // Имя файла стикера в папке Image
            SendSticker(stickerFilename);
        }

        private void SendSticker(string stickerFilename)
        {
            // Добавляем [STICKER] префикс для обозначения стикеров (если необходимо)
            string stickerContent = "[STICKER]" + stickerFilename;

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Messages (Sender, Recipient, Content, [Timestamp]) VALUES (?, ?, ?, ?)";
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.Add("Sender", OleDbType.VarChar).Value = currentUsername;
                    command.Parameters.Add("Recipient", OleDbType.VarChar).Value = contactUsername;
                    command.Parameters.Add("Content", OleDbType.VarChar).Value = stickerContent;
                    command.Parameters.Add("Timestamp", OleDbType.Date).Value = DateTime.Now;
                    command.ExecuteNonQuery();
                    LoadMessages();
                }
            }
        }

        private void buttonDeleteMessage_Click(object sender, EventArgs e)
        {
            // хуй тут класть надо
        }

        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            messageRefreshTimer.Stop();
            SaveDraft();
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

        private void textBoxInput_TextChanged(object sender, EventArgs e)
        {
            draftMessage = textBoxInput.Text;
        }

        private void ChatForm_Load(object sender, EventArgs e)
        {
            LoadDraft();
        }

        private void LoadDraft()
        {
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT Content FROM Drafts WHERE Sender = ? AND Recipient = ?";
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    command.Parameters.AddWithValue("?", currentUsername);
                    command.Parameters.AddWithValue("?", contactUsername);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            draftMessage = reader.GetString(0);
                            textBoxInput.Text = draftMessage;
                        }
                    }
                }
            }
        }
    }
}