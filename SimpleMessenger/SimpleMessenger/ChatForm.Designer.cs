namespace SimpleMessenger
{
    partial class ChatForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMessages;
        private System.Windows.Forms.Button buttonDeleteMessage; // Если все еще нужно
        private System.Windows.Forms.Button buttonSelectSticker;
        private System.Windows.Forms.Button buttonBackToContacts;

        private void InitializeComponent()
        {
            textBoxInput = new TextBox();
            buttonSend = new Button();
            tableLayoutPanelMessages = new TableLayoutPanel();
            buttonBackToContacts = new Button();
            buttonDeleteMessage = new Button();
            buttonSelectSticker = new Button();
            SuspendLayout();
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
            // tableLayoutPanelMessages
            // 
            tableLayoutPanelMessages.AutoScroll = true;
            tableLayoutPanelMessages.ColumnCount = 2;
            tableLayoutPanelMessages.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelMessages.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanelMessages.Location = new Point(12, 41);
            tableLayoutPanelMessages.Name = "tableLayoutPanelMessages";
            tableLayoutPanelMessages.RowCount = 1;
            tableLayoutPanelMessages.RowStyles.Add(new RowStyle());
            tableLayoutPanelMessages.Size = new Size(453, 184);
            tableLayoutPanelMessages.TabIndex = 0;
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
            // buttonDeleteMessage
            // 
            buttonDeleteMessage.Location = new Point(297, 261);
            buttonDeleteMessage.Name = "buttonDeleteMessage";
            buttonDeleteMessage.Size = new Size(168, 26);
            buttonDeleteMessage.TabIndex = 4;
            buttonDeleteMessage.Text = "Delete Message";
            buttonDeleteMessage.Click += buttonDeleteMessage_Click;
            // 
            // buttonSelectSticker
            // 
            buttonSelectSticker.Location = new Point(297, 293);
            buttonSelectSticker.Name = "buttonSelectSticker";
            buttonSelectSticker.Size = new Size(168, 26);
            buttonSelectSticker.TabIndex = 5;
            buttonSelectSticker.Text = "Send Sticker";
            buttonSelectSticker.Click += buttonSelectSticker_Click;
            // 
            // ChatForm
            // 
            ClientSize = new Size(477, 330);
            Controls.Add(tableLayoutPanelMessages);
            Controls.Add(buttonBackToContacts);
            Controls.Add(textBoxInput);
            Controls.Add(buttonSend);
            Controls.Add(buttonDeleteMessage);
            Controls.Add(buttonSelectSticker);
            Name = "ChatForm";
            Text = "Chat";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}