
namespace EXTUBE.Forms
{
    partial class MainMenuForm
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
            this.URL_Label = new MetroFramework.Drawing.Html.HtmlLabel();
            this.btn_button = new System.Windows.Forms.Button();
            this.TextBoxUrlPath = new MetroFramework.Controls.MetroTextBox();
            this.TextBoxDownloadFolderPath = new MetroFramework.Controls.MetroTextBox();
            this.DownloadFolderLabel = new MetroFramework.Drawing.Html.HtmlLabel();
            this.DownloadFolderSelectButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // URL_Label
            // 
            this.URL_Label.AutoScroll = true;
            this.URL_Label.AutoScrollMinSize = new System.Drawing.Size(32, 22);
            this.URL_Label.AutoSize = false;
            this.URL_Label.BackColor = System.Drawing.SystemColors.Window;
            this.URL_Label.Location = new System.Drawing.Point(23, 66);
            this.URL_Label.Name = "URL_Label";
            this.URL_Label.Size = new System.Drawing.Size(60, 25);
            this.URL_Label.TabIndex = 1;
            this.URL_Label.Text = "URL";
            // 
            // btn_button
            // 
            this.btn_button.Location = new System.Drawing.Point(23, 164);
            this.btn_button.Name = "btn_button";
            this.btn_button.Size = new System.Drawing.Size(75, 29);
            this.btn_button.TabIndex = 2;
            this.btn_button.Text = "ダウンロード";
            this.btn_button.UseVisualStyleBackColor = true;
            this.btn_button.Click += new System.EventHandler(this.btn_button_Click);
            // 
            // TextBoxUrlPath
            // 
            // 
            // 
            // 
            this.TextBoxUrlPath.CustomButton.Image = null;
            this.TextBoxUrlPath.CustomButton.Location = new System.Drawing.Point(311, 1);
            this.TextBoxUrlPath.CustomButton.Name = "";
            this.TextBoxUrlPath.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.TextBoxUrlPath.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.TextBoxUrlPath.CustomButton.TabIndex = 1;
            this.TextBoxUrlPath.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.TextBoxUrlPath.CustomButton.UseSelectable = true;
            this.TextBoxUrlPath.CustomButton.Visible = false;
            this.TextBoxUrlPath.Lines = new string[0];
            this.TextBoxUrlPath.Location = new System.Drawing.Point(23, 83);
            this.TextBoxUrlPath.MaxLength = 32767;
            this.TextBoxUrlPath.Name = "TextBoxUrlPath";
            this.TextBoxUrlPath.PasswordChar = '\0';
            this.TextBoxUrlPath.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.TextBoxUrlPath.SelectedText = "";
            this.TextBoxUrlPath.SelectionLength = 0;
            this.TextBoxUrlPath.SelectionStart = 0;
            this.TextBoxUrlPath.ShortcutsEnabled = true;
            this.TextBoxUrlPath.Size = new System.Drawing.Size(333, 23);
            this.TextBoxUrlPath.TabIndex = 3;
            this.TextBoxUrlPath.UseSelectable = true;
            this.TextBoxUrlPath.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.TextBoxUrlPath.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // TextBoxDownloadFolderPath
            // 
            // 
            // 
            // 
            this.TextBoxDownloadFolderPath.CustomButton.Image = null;
            this.TextBoxDownloadFolderPath.CustomButton.Location = new System.Drawing.Point(311, 1);
            this.TextBoxDownloadFolderPath.CustomButton.Name = "";
            this.TextBoxDownloadFolderPath.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.TextBoxDownloadFolderPath.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.TextBoxDownloadFolderPath.CustomButton.TabIndex = 1;
            this.TextBoxDownloadFolderPath.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.TextBoxDownloadFolderPath.CustomButton.UseSelectable = true;
            this.TextBoxDownloadFolderPath.CustomButton.Visible = false;
            this.TextBoxDownloadFolderPath.Lines = new string[0];
            this.TextBoxDownloadFolderPath.Location = new System.Drawing.Point(23, 135);
            this.TextBoxDownloadFolderPath.MaxLength = 32767;
            this.TextBoxDownloadFolderPath.Name = "TextBoxDownloadFolderPath";
            this.TextBoxDownloadFolderPath.PasswordChar = '\0';
            this.TextBoxDownloadFolderPath.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.TextBoxDownloadFolderPath.SelectedText = "";
            this.TextBoxDownloadFolderPath.SelectionLength = 0;
            this.TextBoxDownloadFolderPath.SelectionStart = 0;
            this.TextBoxDownloadFolderPath.ShortcutsEnabled = true;
            this.TextBoxDownloadFolderPath.Size = new System.Drawing.Size(333, 23);
            this.TextBoxDownloadFolderPath.TabIndex = 4;
            this.TextBoxDownloadFolderPath.UseSelectable = true;
            this.TextBoxDownloadFolderPath.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.TextBoxDownloadFolderPath.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // DownloadFolderLabel
            // 
            this.DownloadFolderLabel.AutoScroll = true;
            this.DownloadFolderLabel.AutoScrollMinSize = new System.Drawing.Size(91, 22);
            this.DownloadFolderLabel.AutoSize = false;
            this.DownloadFolderLabel.BackColor = System.Drawing.SystemColors.Window;
            this.DownloadFolderLabel.Location = new System.Drawing.Point(23, 112);
            this.DownloadFolderLabel.Name = "DownloadFolderLabel";
            this.DownloadFolderLabel.Size = new System.Drawing.Size(91, 17);
            this.DownloadFolderLabel.TabIndex = 5;
            this.DownloadFolderLabel.Text = "DownloadFolder";
            // 
            // DownloadFolderSelectButton
            // 
            this.DownloadFolderSelectButton.Location = new System.Drawing.Point(362, 135);
            this.DownloadFolderSelectButton.Name = "DownloadFolderSelectButton";
            this.DownloadFolderSelectButton.Size = new System.Drawing.Size(83, 23);
            this.DownloadFolderSelectButton.TabIndex = 6;
            this.DownloadFolderSelectButton.Text = "フォルダを開く";
            this.DownloadFolderSelectButton.UseVisualStyleBackColor = true;
            this.DownloadFolderSelectButton.Click += new System.EventHandler(this.DownloadFolderSelectButton_Click);
            // 
            // MainMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 209);
            this.Controls.Add(this.DownloadFolderSelectButton);
            this.Controls.Add(this.DownloadFolderLabel);
            this.Controls.Add(this.TextBoxDownloadFolderPath);
            this.Controls.Add(this.TextBoxUrlPath);
            this.Controls.Add(this.btn_button);
            this.Controls.Add(this.URL_Label);
            this.Name = "MainMenuForm";
            this.Resizable = false;
            this.Text = "EXTUBE";
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Drawing.Html.HtmlLabel URL_Label;
        private System.Windows.Forms.Button btn_button;
        private MetroFramework.Controls.MetroTextBox TextBoxUrlPath;
        private MetroFramework.Controls.MetroTextBox TextBoxDownloadFolderPath;
        private MetroFramework.Drawing.Html.HtmlLabel DownloadFolderLabel;
        private System.Windows.Forms.Button DownloadFolderSelectButton;
    }
}