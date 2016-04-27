namespace ChatServer
{
    partial class Server
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
            this.txtServerChat = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtServerChat
            // 
            this.txtServerChat.BackColor = System.Drawing.SystemColors.Window;
            this.txtServerChat.Location = new System.Drawing.Point(12, 12);
            this.txtServerChat.Multiline = true;
            this.txtServerChat.Name = "txtServerChat";
            this.txtServerChat.ReadOnly = true;
            this.txtServerChat.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtServerChat.Size = new System.Drawing.Size(363, 324);
            this.txtServerChat.TabIndex = 0;
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 348);
            this.Controls.Add(this.txtServerChat);
            this.Name = "Server";
            this.Text = "Chat Server";
            this.Load += new System.EventHandler(this.Server_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtServerChat;
    }
}

