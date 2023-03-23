namespace windows_form_app_tutorial
{
    partial class Home
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnBackgroundWorker = new System.Windows.Forms.Button();
            this.lblVersion = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(12, 19);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(404, 15);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Shuシュー NetFramework 4.8 Windows Form App - Full Tutorial";
            // 
            // btnBackgroundWorker
            // 
            this.btnBackgroundWorker.Location = new System.Drawing.Point(12, 52);
            this.btnBackgroundWorker.Name = "btnBackgroundWorker";
            this.btnBackgroundWorker.Size = new System.Drawing.Size(345, 23);
            this.btnBackgroundWorker.TabIndex = 1;
            this.btnBackgroundWorker.Text = "Background Worker";
            this.btnBackgroundWorker.UseVisualStyleBackColor = true;
            this.btnBackgroundWorker.Click += new System.EventHandler(this.btnBackgroundWorker_Click);
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(724, 19);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(64, 15);
            this.lblVersion.TabIndex = 2;
            this.lblVersion.Text = "Ver 1.0.0";
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.btnBackgroundWorker);
            this.Controls.Add(this.lblTitle);
            this.Name = "Home";
            this.Text = "Home";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnBackgroundWorker;
        private System.Windows.Forms.Label lblVersion;
    }
}

