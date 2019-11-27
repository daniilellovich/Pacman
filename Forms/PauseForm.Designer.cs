namespace Pacman
{
    partial class PauseForm
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
            this.resumeB = new System.Windows.Forms.Button();
            this.restartB = new System.Windows.Forms.Button();
            this.exitB = new System.Windows.Forms.Button();
            this.soundB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // resumeB
            // 
            this.resumeB.BackColor = System.Drawing.Color.Black;
            this.resumeB.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.resumeB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resumeB.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.resumeB.Location = new System.Drawing.Point(41, 42);
            this.resumeB.Name = "resumeB";
            this.resumeB.Size = new System.Drawing.Size(75, 23);
            this.resumeB.TabIndex = 2;
            this.resumeB.Text = "resume";
            this.resumeB.UseVisualStyleBackColor = false;
            this.resumeB.Click += new System.EventHandler(this.ResumeB_Click);
            // 
            // restartB
            // 
            this.restartB.BackColor = System.Drawing.Color.Black;
            this.restartB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.restartB.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.restartB.Location = new System.Drawing.Point(41, 90);
            this.restartB.Name = "restartB";
            this.restartB.Size = new System.Drawing.Size(75, 23);
            this.restartB.TabIndex = 3;
            this.restartB.Text = "restart";
            this.restartB.UseVisualStyleBackColor = false;
            this.restartB.Click += new System.EventHandler(this.RestartB_Click);
            // 
            // exitB
            // 
            this.exitB.BackColor = System.Drawing.Color.Black;
            this.exitB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitB.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.exitB.Location = new System.Drawing.Point(41, 143);
            this.exitB.Name = "exitB";
            this.exitB.Size = new System.Drawing.Size(75, 23);
            this.exitB.TabIndex = 4;
            this.exitB.Text = "exit";
            this.exitB.UseVisualStyleBackColor = false;
            this.exitB.Click += new System.EventHandler(this.ExitB_Click);
            // 
            // soundB
            // 
            this.soundB.BackColor = System.Drawing.Color.Black;
            this.soundB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.soundB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.soundB.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.soundB.Location = new System.Drawing.Point(121, 191);
            this.soundB.Name = "soundB";
            this.soundB.Size = new System.Drawing.Size(24, 23);
            this.soundB.TabIndex = 5;
            this.soundB.UseVisualStyleBackColor = false;
            this.soundB.Click += new System.EventHandler(this.SoundB_Click);
            // 
            // PauseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.CancelButton = this.resumeB;
            this.ClientSize = new System.Drawing.Size(185, 264);
            this.Controls.Add(this.soundB);
            this.Controls.Add(this.exitB);
            this.Controls.Add(this.restartB);
            this.Controls.Add(this.resumeB);
            this.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PauseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PauseForm";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.PauseForm_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button resumeB;
        public System.Windows.Forms.Button restartB;
        public System.Windows.Forms.Button exitB;
        public System.Windows.Forms.Button soundB;
    }
}