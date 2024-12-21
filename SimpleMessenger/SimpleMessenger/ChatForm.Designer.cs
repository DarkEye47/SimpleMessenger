namespace SimpleMessenger
{
    partial class ChatForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox textBoxMessages;
        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.ListBox listBoxMessages;
        private System.Windows.Forms.Button buttonDeleteMessage;
        private System.Windows.Forms.Button buttonSelectSticker;

        private void InitializeComponent()
        {
            textBoxMessages = new TextBox();
            textBoxInput = new TextBox();
            buttonSend = new Button();
            buttonBackToContacts = new Button();
            listBoxMessages = new ListBox();
            buttonDeleteMessage = new Button();
            buttonSelectSticker = new Button();
            SuspendLayout();
            // 
            // textBoxMessages
            // 
            textBoxMessages.Location = new Point(0, 0);
            textBoxMessages.Name = "textBoxMessages";
            textBoxMessages.Size = new Size(100, 27);
            textBoxMessages.TabIndex = 0;
            // 
            // textBoxInput
            // 
            textBoxInput.Location = new Point(12, 246);
            textBoxInput.Name = "textBoxInput";
            textBoxInput.Size = new Size(260, 27);
            textBoxInput.TabIndex = 1;
            // 
            // buttonSend
            // 
            buttonSend.Location = new Point(297, 231);
            buttonSend.Name = "buttonSend";
            buttonSend.Size = new Size(168, 27);
            buttonSend.TabIndex = 2;
            buttonSend.Text = "Send";
            buttonSend.Click += buttonSend_Click;
            // 
            // buttonBackToContacts
            // 
            buttonBackToContacts.Location = new Point(12, 6);
            buttonBackToContacts.Name = "buttonBackToContacts";
            buttonBackToContacts.Size = new Size(66, 29);
            buttonBackToContacts.TabIndex = 3;
            buttonBackToContacts.Text = "<--";
            buttonBackToContacts.UseVisualStyleBackColor = true;
            buttonBackToContacts.Click += buttonBackToContacts_Click;
            // 
            // listBoxMessages
            // 
            listBoxMessages.Location = new Point(12, 41);
            listBoxMessages.Name = "listBoxMessages";
            listBoxMessages.Size = new Size(453, 184);
            listBoxMessages.TabIndex = 4;
            // 
            // buttonDeleteMessage
            // 
            buttonDeleteMessage.Location = new Point(390, 9);
            buttonDeleteMessage.Name = "buttonDeleteMessage";
            buttonDeleteMessage.Size = new Size(75, 26);
            buttonDeleteMessage.TabIndex = 5;
            buttonDeleteMessage.Text = "Delete Message";
            buttonDeleteMessage.Click += buttonDeleteMessage_Click;
            // 
            // buttonSelectSticker
            // 
            buttonSelectSticker.Location = new Point(297, 261);
            buttonSelectSticker.Name = "buttonSelectSticker";
            buttonSelectSticker.Size = new Size(168, 26);
            buttonSelectSticker.TabIndex = 6;
            buttonSelectSticker.Text = "Send Sticker";
            buttonSelectSticker.Click += buttonSelectSticker_Click;
            // 
            // ChatForm
            // 
            ClientSize = new Size(477, 300);
            Controls.Add(buttonBackToContacts);
            Controls.Add(listBoxMessages);
            Controls.Add(buttonDeleteMessage);
            Controls.Add(textBoxInput);
            Controls.Add(buttonSend);
            Controls.Add(buttonSelectSticker);
            Name = "ChatForm";
            Text = "Chat";
            ResumeLayout(false);
            PerformLayout();
        }

        private Button buttonBackToContacts;
    }
}