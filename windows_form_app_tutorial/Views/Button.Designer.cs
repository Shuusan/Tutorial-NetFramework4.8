namespace windows_form_app_tutorial.Views
{
    partial class frmButton
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
            this.btnClickAndEnter = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnPaint = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnClickAndEnter
            // 
            this.btnClickAndEnter.Location = new System.Drawing.Point(12, 46);
            this.btnClickAndEnter.Name = "btnClickAndEnter";
            this.btnClickAndEnter.Size = new System.Drawing.Size(776, 72);
            this.btnClickAndEnter.TabIndex = 0;
            this.btnClickAndEnter.Text = "Click me or press enter when focus";
            this.btnClickAndEnter.UseVisualStyleBackColor = true;
            this.btnClickAndEnter.Click += new System.EventHandler(this.btnClickAndEnter_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 124);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(776, 22);
            this.textBox1.TabIndex = 99;
            // 
            // btnPaint
            // 
            this.btnPaint.Location = new System.Drawing.Point(12, 206);
            this.btnPaint.Name = "btnPaint";
            this.btnPaint.Size = new System.Drawing.Size(776, 232);
            this.btnPaint.TabIndex = 2;
            this.btnPaint.Text = "Click to modify me!";
            this.btnPaint.UseVisualStyleBackColor = true;
            this.btnPaint.Click += new System.EventHandler(this.btnPaint_Click);
            this.btnPaint.Paint += new System.Windows.Forms.PaintEventHandler(this.btnPaint_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(392, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(15, 15);
            this.label1.TabIndex = 100;
            this.label1.Text = "1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(392, 179);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 15);
            this.label2.TabIndex = 101;
            this.label2.Text = "2";
            // 
            // frmButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnPaint);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnClickAndEnter);
            this.Name = "frmButton";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Button";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClickAndEnter;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnPaint;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}