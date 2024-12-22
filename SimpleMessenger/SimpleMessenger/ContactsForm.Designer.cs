namespace SimpleMessenger
{
    partial class ContactsForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ListView listViewContacts;
        private System.Windows.Forms.ColumnHeader columnHeaderContact;
        private System.Windows.Forms.Button buttonAddContact;
        private System.Windows.Forms.Button buttonDeleteContact;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Button buttonOpenChat;
        private System.Windows.Forms.Button buttonLogout;
        private System.Windows.Forms.Label labelLastMessage;

        private void InitializeComponent()
        {
            listViewContacts = new ListView();
            columnHeaderContact = new ColumnHeader();
            buttonAddContact = new Button();
            buttonDeleteContact = new Button();
            textBoxSearch = new TextBox();
            buttonOpenChat = new Button();
            buttonLogout = new Button();
            labelLastMessage = new Label();

            SuspendLayout();
            // 
            // listViewContacts
            // 
            listViewContacts.Columns.AddRange(new ColumnHeader[] { columnHeaderContact });
            listViewContacts.FullRowSelect = true;
            listViewContacts.GridLines = true;
            listViewContacts.Location = new Point(30, 61);
            listViewContacts.MultiSelect = false;
            listViewContacts.Name = "listViewContacts";
            listViewContacts.Size = new Size(300, 548);
            listViewContacts.TabIndex = 0;
            listViewContacts.UseCompatibleStateImageBehavior = false;
            listViewContacts.View = View.Details;
            // 
            // columnHeaderContact
            // 
            columnHeaderContact.Text = "Contact Username";
            columnHeaderContact.Width = 280;
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
            // buttonLogout
            // 
            buttonLogout.Location = new Point(30, 655);
            buttonLogout.Name = "buttonLogout";
            buttonLogout.Size = new Size(100, 30);
            buttonLogout.TabIndex = 5;
            buttonLogout.Text = "Logout";
            buttonLogout.Click += buttonLogout_Click;
            // 
            // labelLastMessage
            // 
            labelLastMessage.Location = new Point(30, 695);
            labelLastMessage.Size = new Size(300, 50);
            labelLastMessage.Name = "labelLastMessage";
            labelLastMessage.Text = "  ";
            labelLastMessage.AutoSize = false;
            // 
            // ContactsForm
            // 
            ClientSize = new Size(400, 700);
            Controls.Add(listViewContacts);
            Controls.Add(buttonAddContact);
            Controls.Add(buttonDeleteContact);
            Controls.Add(textBoxSearch);
            Controls.Add(buttonOpenChat);
            Controls.Add(buttonLogout);
            Name = "ContactsForm";
            Text = "Contacts";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}