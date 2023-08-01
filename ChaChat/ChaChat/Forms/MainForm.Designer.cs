
namespace ChaChat
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCreateRoom = new MetroFramework.Controls.MetroButton();
            this.btnJoinRoom = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // btnCreateRoom
            // 
            this.btnCreateRoom.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCreateRoom.Location = new System.Drawing.Point(23, 63);
            this.btnCreateRoom.Name = "btnCreateRoom";
            this.btnCreateRoom.Size = new System.Drawing.Size(142, 48);
            this.btnCreateRoom.TabIndex = 0;
            this.btnCreateRoom.Text = "ルームを作成する";
            this.btnCreateRoom.UseSelectable = true;
            this.btnCreateRoom.Click += new System.EventHandler(this.btnCreateRoom_Click);
            // 
            // btnJoinRoom
            // 
            this.btnJoinRoom.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnJoinRoom.Location = new System.Drawing.Point(171, 63);
            this.btnJoinRoom.Name = "btnJoinRoom";
            this.btnJoinRoom.Size = new System.Drawing.Size(142, 48);
            this.btnJoinRoom.TabIndex = 1;
            this.btnJoinRoom.Text = "ルームに入室する";
            this.btnJoinRoom.UseSelectable = true;
            this.btnJoinRoom.Click += new System.EventHandler(this.btnJoinRoom_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(335, 126);
            this.Controls.Add(this.btnJoinRoom);
            this.Controls.Add(this.btnCreateRoom);
            this.MaximumSize = new System.Drawing.Size(335, 126);
            this.MinimumSize = new System.Drawing.Size(335, 126);
            this.Name = "MainForm";
            this.Text = "ChaChat";
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroButton btnCreateRoom;
        private MetroFramework.Controls.MetroButton btnJoinRoom;
    }
}

