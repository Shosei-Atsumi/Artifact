
namespace ChaChat.Forms
{
    partial class ChatRoomFormBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtChat = new MetroFramework.Controls.MetroTextBox();
            this.btnExit = new MetroFramework.Controls.MetroButton();
            this.txtSendMessage = new MetroFramework.Controls.MetroTextBox();
            this.btnSend = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // txtChat
            // 
            // 
            // 
            // 
            this.txtChat.CustomButton.Image = null;
            this.txtChat.CustomButton.Location = new System.Drawing.Point(412, 1);
            this.txtChat.CustomButton.Name = "";
            this.txtChat.CustomButton.Size = new System.Drawing.Size(341, 341);
            this.txtChat.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtChat.CustomButton.TabIndex = 1;
            this.txtChat.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtChat.CustomButton.UseSelectable = true;
            this.txtChat.CustomButton.Visible = false;
            this.txtChat.Lines = new string[0];
            this.txtChat.Location = new System.Drawing.Point(23, 63);
            this.txtChat.MaxLength = 32767;
            this.txtChat.Multiline = true;
            this.txtChat.Name = "txtChat";
            this.txtChat.PasswordChar = '\0';
            this.txtChat.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtChat.SelectedText = "";
            this.txtChat.SelectionLength = 0;
            this.txtChat.SelectionStart = 0;
            this.txtChat.ShortcutsEnabled = true;
            this.txtChat.Size = new System.Drawing.Size(754, 343);
            this.txtChat.TabIndex = 0;
            this.txtChat.UseSelectable = true;
            this.txtChat.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtChat.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(702, 34);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "終了";
            this.btnExit.UseSelectable = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // txtSendMessage
            // 
            // 
            // 
            // 
            this.txtSendMessage.CustomButton.Image = null;
            this.txtSendMessage.CustomButton.Location = new System.Drawing.Point(656, 1);
            this.txtSendMessage.CustomButton.Name = "";
            this.txtSendMessage.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.txtSendMessage.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtSendMessage.CustomButton.TabIndex = 1;
            this.txtSendMessage.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtSendMessage.CustomButton.UseSelectable = true;
            this.txtSendMessage.CustomButton.Visible = false;
            this.txtSendMessage.Lines = new string[0];
            this.txtSendMessage.Location = new System.Drawing.Point(23, 412);
            this.txtSendMessage.MaxLength = 32767;
            this.txtSendMessage.Multiline = true;
            this.txtSendMessage.Name = "txtSendMessage";
            this.txtSendMessage.PasswordChar = '\0';
            this.txtSendMessage.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtSendMessage.SelectedText = "";
            this.txtSendMessage.SelectionLength = 0;
            this.txtSendMessage.SelectionStart = 0;
            this.txtSendMessage.ShortcutsEnabled = true;
            this.txtSendMessage.Size = new System.Drawing.Size(678, 23);
            this.txtSendMessage.TabIndex = 2;
            this.txtSendMessage.UseSelectable = true;
            this.txtSendMessage.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtSendMessage.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(702, 412);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "送信";
            this.btnSend.UseSelectable = true;
            // 
            // ChatRoomFormBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtSendMessage);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.txtChat);
            this.Name = "ChatRoomFormBase";
            this.Text = "ChatRoomForm";
            this.ResumeLayout(false);

        }

        #endregion
        protected MetroFramework.Controls.MetroTextBox txtChat;
        protected MetroFramework.Controls.MetroTextBox txtSendMessage;
        protected MetroFramework.Controls.MetroButton btnSend;
        private MetroFramework.Controls.MetroButton btnExit;
    }
}