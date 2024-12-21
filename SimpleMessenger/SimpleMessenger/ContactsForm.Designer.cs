namespace SimpleMessenger
{
    partial class ContactsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dataGridViewContacts;
        private System.Windows.Forms.Button buttonAddContact;
        private System.Windows.Forms.Button buttonDeleteContact;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Button buttonOpenChat;

        private void InitializeComponent()
        {
            dataGridViewContacts = new DataGridView();
            buttonAddContact = new Button();
            buttonDeleteContact = new Button();
            textBoxSearch = new TextBox();
            buttonOpenChat = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewContacts).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewContacts
            // 
            dataGridViewContacts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewContacts.Location = new Point(30, 61);
            dataGridViewContacts.Name = "dataGridViewContacts";
            dataGridViewContacts.RowHeadersWidth = 51;
            dataGridViewContacts.Size = new Size(300, 548);
            dataGridViewContacts.TabIndex = 0;
            // 
            // buttonAddContact
            // 
            buttonAddContact.Location = new Point(30, 615);
            buttonAddContact.Name = "buttonAddContact";
            buttonAddContact.Size = new Size(100, 30);
            buttonAddContact.TabIndex = 1;
            buttonAddContact.Text = "Add Contact";
            buttonAddContact.Click += buttonAddContact_Click;
            // 
            // buttonDeleteContact
            // 
            buttonDeleteContact.Location = new Point(150, 615);
            buttonDeleteContact.Name = "buttonDeleteContact";
            buttonDeleteContact.Size = new Size(100, 30);
            buttonDeleteContact.TabIndex = 2;
            buttonDeleteContact.Text = "Delete Contact";
            buttonDeleteContact.Click += buttonDeleteContact_Click;
            // 
            // textBoxSearch
            // 
            textBoxSearch.Location = new Point(30, 25);
            textBoxSearch.Name = "textBoxSearch";
            textBoxSearch.Size = new Size(300, 27);
            textBoxSearch.TabIndex = 3;
            textBoxSearch.TextChanged += textBoxSearch_TextChanged;
            // 
            // buttonOpenChat
            // 
            buttonOpenChat.Location = new Point(260, 615);
            buttonOpenChat.Name = "buttonOpenChat";
            buttonOpenChat.Size = new Size(100, 30);
            buttonOpenChat.TabIndex = 4;
            buttonOpenChat.Text = "Open Chat";
            buttonOpenChat.Click += buttonOpenChat_Click;
            // 
            // ContactsForm
            // 
            ClientSize = new Size(385, 657);
            Controls.Add(dataGridViewContacts);
            Controls.Add(buttonAddContact);
            Controls.Add(buttonDeleteContact);
            Controls.Add(textBoxSearch);
            Controls.Add(buttonOpenChat);
            Name = "ContactsForm";
            Text = "Contacts";
            ((System.ComponentModel.ISupportInitialize)dataGridViewContacts).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
