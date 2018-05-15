namespace WaveProcessUI
{
    partial class Form1
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
            this.LblChooseFolder = new System.Windows.Forms.Label();
            this.TbxFolderPath = new System.Windows.Forms.TextBox();
            this.BtnChooseFolder = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.TbxExeMsg = new System.Windows.Forms.TextBox();
            this.BtnExecute = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LblChooseFolder
            // 
            this.LblChooseFolder.AutoSize = true;
            this.LblChooseFolder.Location = new System.Drawing.Point(13, 28);
            this.LblChooseFolder.Name = "LblChooseFolder";
            this.LblChooseFolder.Size = new System.Drawing.Size(67, 13);
            this.LblChooseFolder.TabIndex = 0;
            this.LblChooseFolder.Text = "音檔資料夾";
            // 
            // TbxFolderPath
            // 
            this.TbxFolderPath.Location = new System.Drawing.Point(87, 24);
            this.TbxFolderPath.Name = "TbxFolderPath";
            this.TbxFolderPath.Size = new System.Drawing.Size(114, 20);
            this.TbxFolderPath.TabIndex = 1;
            // 
            // BtnChooseFolder
            // 
            this.BtnChooseFolder.Location = new System.Drawing.Point(220, 21);
            this.BtnChooseFolder.Name = "BtnChooseFolder";
            this.BtnChooseFolder.Size = new System.Drawing.Size(75, 23);
            this.BtnChooseFolder.TabIndex = 2;
            this.BtnChooseFolder.Text = "選擇資料夾";
            this.BtnChooseFolder.UseVisualStyleBackColor = true;
            this.BtnChooseFolder.Click += new System.EventHandler(this.BtnChooseFolder_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "執行訊息";
            // 
            // TbxExeMsg
            // 
            this.TbxExeMsg.Location = new System.Drawing.Point(16, 90);
            this.TbxExeMsg.Multiline = true;
            this.TbxExeMsg.Name = "TbxExeMsg";
            this.TbxExeMsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TbxExeMsg.Size = new System.Drawing.Size(365, 149);
            this.TbxExeMsg.TabIndex = 4;
            // 
            // BtnExecute
            // 
            this.BtnExecute.Location = new System.Drawing.Point(306, 22);
            this.BtnExecute.Name = "BtnExecute";
            this.BtnExecute.Size = new System.Drawing.Size(75, 23);
            this.BtnExecute.TabIndex = 5;
            this.BtnExecute.Text = "執行";
            this.BtnExecute.UseVisualStyleBackColor = true;
            this.BtnExecute.Click += new System.EventHandler(this.BtnExecute_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 251);
            this.Controls.Add(this.BtnExecute);
            this.Controls.Add(this.TbxExeMsg);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BtnChooseFolder);
            this.Controls.Add(this.TbxFolderPath);
            this.Controls.Add(this.LblChooseFolder);
            this.Name = "Form1";
            this.Text = "音檔合併工具";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LblChooseFolder;
        private System.Windows.Forms.TextBox TbxFolderPath;
        private System.Windows.Forms.Button BtnChooseFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TbxExeMsg;
        private System.Windows.Forms.Button BtnExecute;
    }
}

