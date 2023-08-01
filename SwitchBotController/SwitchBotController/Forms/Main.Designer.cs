namespace SwitchBotController
{
    partial class Main
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
            this.BtnAllOff = new MetroFramework.Controls.MetroButton();
            this.BtnAllOn = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // BtnAllOff
            // 
            this.BtnAllOff.Location = new System.Drawing.Point(23, 63);
            this.BtnAllOff.Name = "BtnAllOff";
            this.BtnAllOff.Size = new System.Drawing.Size(75, 23);
            this.BtnAllOff.TabIndex = 0;
            this.BtnAllOff.Text = "ALL_OFF";
            this.BtnAllOff.UseSelectable = true;
            this.BtnAllOff.Click += new System.EventHandler(this.BtnAllOff_Click);
            // 
            // BtnAllOn
            // 
            this.BtnAllOn.Location = new System.Drawing.Point(104, 63);
            this.BtnAllOn.Name = "BtnAllOn";
            this.BtnAllOn.Size = new System.Drawing.Size(75, 23);
            this.BtnAllOn.TabIndex = 1;
            this.BtnAllOn.Text = "ALL_ON";
            this.BtnAllOn.UseSelectable = true;
            this.BtnAllOn.Click += new System.EventHandler(this.BtnAllOn_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(228, 112);
            this.Controls.Add(this.BtnAllOn);
            this.Controls.Add(this.BtnAllOff);
            this.Name = "Main";
            this.Text = "Controller";
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroButton BtnAllOff;
        private MetroFramework.Controls.MetroButton BtnAllOn;
    }
}

