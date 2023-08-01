
namespace FixedLengthFileParser.Forms
{
    partial class MainForm
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
            this.btnLoadFomat = new MetroFramework.Controls.MetroButton();
            this.btnLoadFile = new MetroFramework.Controls.MetroButton();
            this.chkCheckFormatLoad = new MetroFramework.Controls.MetroCheckBox();
            this.SuspendLayout();
            // 
            // btnLoadFomat
            // 
            this.btnLoadFomat.Location = new System.Drawing.Point(23, 63);
            this.btnLoadFomat.Name = "btnLoadFomat";
            this.btnLoadFomat.Size = new System.Drawing.Size(75, 23);
            this.btnLoadFomat.TabIndex = 0;
            this.btnLoadFomat.Text = "LoadFomat";
            this.btnLoadFomat.UseSelectable = true;
            this.btnLoadFomat.Click += new System.EventHandler(this.btnLoadFomat_Click);
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.Location = new System.Drawing.Point(23, 92);
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(75, 23);
            this.btnLoadFile.TabIndex = 1;
            this.btnLoadFile.Text = "LoadFile";
            this.btnLoadFile.UseSelectable = true;
            this.btnLoadFile.Click += new System.EventHandler(this.btnLoadFile_Click);
            // 
            // chkCheckFormatLoad
            // 
            this.chkCheckFormatLoad.AutoSize = true;
            this.chkCheckFormatLoad.Location = new System.Drawing.Point(104, 67);
            this.chkCheckFormatLoad.Name = "chkCheckFormatLoad";
            this.chkCheckFormatLoad.Size = new System.Drawing.Size(197, 15);
            this.chkCheckFormatLoad.TabIndex = 3;
            this.chkCheckFormatLoad.Text = "フォーマットをロードするか確認する";
            this.chkCheckFormatLoad.UseSelectable = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 134);
            this.Controls.Add(this.chkCheckFormatLoad);
            this.Controls.Add(this.btnLoadFile);
            this.Controls.Add(this.btnLoadFomat);
            this.MaximumSize = new System.Drawing.Size(308, 134);
            this.MinimumSize = new System.Drawing.Size(308, 134);
            this.Name = "MainForm";
            this.Text = "FixedLengthFileParser";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton btnLoadFomat;
        private MetroFramework.Controls.MetroButton btnLoadFile;
        private MetroFramework.Controls.MetroCheckBox chkCheckFormatLoad;
    }
}