namespace Steganography
{
    partial class Steganography
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
            this.btnHideText = new System.Windows.Forms.Button();
            this.txtHiddenText = new System.Windows.Forms.TextBox();
            this.pBImage = new System.Windows.Forms.PictureBox();
            this.btnOpenImage = new System.Windows.Forms.Button();
            this.btnExtractText = new System.Windows.Forms.Button();
            this.txtExtractedText = new System.Windows.Forms.TextBox();
            this.lblHiddenText = new System.Windows.Forms.Label();
            this.lblExtractedText = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pBImage)).BeginInit();
            this.SuspendLayout();
            // 
            // btnHideText
            // 
            this.btnHideText.Enabled = false;
            this.btnHideText.Location = new System.Drawing.Point(12, 454);
            this.btnHideText.Name = "btnHideText";
            this.btnHideText.Size = new System.Drawing.Size(231, 23);
            this.btnHideText.TabIndex = 0;
            this.btnHideText.Text = "Hide Text and Save";
            this.btnHideText.UseVisualStyleBackColor = true;
            this.btnHideText.Click += new System.EventHandler(this.btnHideText_Click);
            // 
            // txtHiddenText
            // 
            this.txtHiddenText.Location = new System.Drawing.Point(12, 367);
            this.txtHiddenText.Multiline = true;
            this.txtHiddenText.Name = "txtHiddenText";
            this.txtHiddenText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtHiddenText.Size = new System.Drawing.Size(231, 81);
            this.txtHiddenText.TabIndex = 1;
            // 
            // pBImage
            // 
            this.pBImage.Location = new System.Drawing.Point(12, 34);
            this.pBImage.Name = "pBImage";
            this.pBImage.Size = new System.Drawing.Size(465, 313);
            this.pBImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pBImage.TabIndex = 2;
            this.pBImage.TabStop = false;
            // 
            // btnOpenImage
            // 
            this.btnOpenImage.Location = new System.Drawing.Point(12, 5);
            this.btnOpenImage.Name = "btnOpenImage";
            this.btnOpenImage.Size = new System.Drawing.Size(465, 23);
            this.btnOpenImage.TabIndex = 3;
            this.btnOpenImage.Text = "Open Image";
            this.btnOpenImage.UseVisualStyleBackColor = true;
            this.btnOpenImage.Click += new System.EventHandler(this.btnOpenImage_Click);
            // 
            // btnExtractText
            // 
            this.btnExtractText.Enabled = false;
            this.btnExtractText.Location = new System.Drawing.Point(246, 454);
            this.btnExtractText.Name = "btnExtractText";
            this.btnExtractText.Size = new System.Drawing.Size(231, 23);
            this.btnExtractText.TabIndex = 5;
            this.btnExtractText.Text = "Extract Text";
            this.btnExtractText.UseVisualStyleBackColor = true;
            this.btnExtractText.Click += new System.EventHandler(this.btnExtractText_Click);
            // 
            // txtExtractedText
            // 
            this.txtExtractedText.Location = new System.Drawing.Point(249, 367);
            this.txtExtractedText.Multiline = true;
            this.txtExtractedText.Name = "txtExtractedText";
            this.txtExtractedText.ReadOnly = true;
            this.txtExtractedText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtExtractedText.Size = new System.Drawing.Size(228, 81);
            this.txtExtractedText.TabIndex = 6;
            // 
            // lblHiddenText
            // 
            this.lblHiddenText.AutoSize = true;
            this.lblHiddenText.Location = new System.Drawing.Point(12, 351);
            this.lblHiddenText.Name = "lblHiddenText";
            this.lblHiddenText.Size = new System.Drawing.Size(65, 13);
            this.lblHiddenText.TabIndex = 7;
            this.lblHiddenText.Text = "Hidden Text";
            // 
            // lblExtractedText
            // 
            this.lblExtractedText.AutoSize = true;
            this.lblExtractedText.Location = new System.Drawing.Point(246, 350);
            this.lblExtractedText.Name = "lblExtractedText";
            this.lblExtractedText.Size = new System.Drawing.Size(76, 13);
            this.lblExtractedText.TabIndex = 8;
            this.lblExtractedText.Text = "Extracted Text";
            // 
            // Steganography
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 484);
            this.Controls.Add(this.lblExtractedText);
            this.Controls.Add(this.lblHiddenText);
            this.Controls.Add(this.txtExtractedText);
            this.Controls.Add(this.btnExtractText);
            this.Controls.Add(this.btnOpenImage);
            this.Controls.Add(this.pBImage);
            this.Controls.Add(this.txtHiddenText);
            this.Controls.Add(this.btnHideText);
            this.Name = "Steganography";
            this.Text = "Steganography Application";
            ((System.ComponentModel.ISupportInitialize)(this.pBImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnHideText;
        private System.Windows.Forms.TextBox txtHiddenText;
        private System.Windows.Forms.PictureBox pBImage;
        private System.Windows.Forms.Button btnOpenImage;
        private System.Windows.Forms.Button btnExtractText;
        private System.Windows.Forms.TextBox txtExtractedText;
        private System.Windows.Forms.Label lblHiddenText;
        private System.Windows.Forms.Label lblExtractedText;
    }
}

