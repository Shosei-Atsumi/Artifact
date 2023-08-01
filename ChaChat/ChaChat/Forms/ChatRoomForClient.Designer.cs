
namespace ChaChat.Forms
{
    partial class ChatRoomForClient
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
            // 
            // metroTextBox1
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
            // 
            // metroButton1
            // 
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // ChatRoomForClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "ChatRoomForClient";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatRoomForServerForm_FormClosing);
            this.Load += new System.EventHandler(this.ChatRoomForServerForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
    }
}