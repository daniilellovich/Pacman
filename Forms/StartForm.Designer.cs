namespace Pacman
{
    partial class StartForm
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
            this.backgroundI = new System.Windows.Forms.PictureBox();
            this.startGameB = new System.Windows.Forms.Button();
            this.recordsB = new System.Windows.Forms.Button();
            this.exitB = new System.Windows.Forms.Button();
            this.ghostI = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.backgroundI)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ghostI)).BeginInit();
            this.SuspendLayout();
            // 
            // backgroundI
            // 
            this.backgroundI.BackColor = System.Drawing.Color.White;
            this.backgroundI.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.backgroundI.Location = new System.Drawing.Point(16, 16);
            this.backgroundI.Name = "backgroundI";
            this.backgroundI.Size = new System.Drawing.Size(128, 64);
            this.backgroundI.TabIndex = 0;
            this.backgroundI.TabStop = false;
            // 
            // startGameB
            // 
            this.startGameB.BackColor = System.Drawing.Color.Black;
            this.startGameB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.startGameB.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.startGameB.Location = new System.Drawing.Point(81, 89);
            this.startGameB.Name = "startGameB";
            this.startGameB.Size = new System.Drawing.Size(96, 32);
            this.startGameB.TabIndex = 1;
            this.startGameB.Text = "start game";
            this.startGameB.UseVisualStyleBackColor = false;
            this.startGameB.Click += new System.EventHandler(this.StartGameB_Click);
            // 
            // recordsB
            // 
            this.recordsB.BackColor = System.Drawing.Color.Black;
            this.recordsB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.recordsB.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.recordsB.Location = new System.Drawing.Point(81, 123);
            this.recordsB.Name = "recordsB";
            this.recordsB.Size = new System.Drawing.Size(96, 32);
            this.recordsB.TabIndex = 2;
            this.recordsB.Text = "records";
            this.recordsB.UseVisualStyleBackColor = false;
            this.recordsB.Click += new System.EventHandler(this.RecordsB_Click);
            // 
            // exitB
            // 
            this.exitB.BackColor = System.Drawing.Color.Black;
            this.exitB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitB.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.exitB.Location = new System.Drawing.Point(81, 161);
            this.exitB.Name = "exitB";
            this.exitB.Size = new System.Drawing.Size(96, 31);
            this.exitB.TabIndex = 3;
            this.exitB.Text = "exit";
            this.exitB.UseVisualStyleBackColor = false;
            this.exitB.Click += new System.EventHandler(this.ExitB_Click);
            // 
            // ghostI
            // 
            this.ghostI.BackColor = System.Drawing.Color.Maroon;
            this.ghostI.Image = global::Pacman.Properties.Resources.ghostGif;
            this.ghostI.Location = new System.Drawing.Point(12, 161);
            this.ghostI.Name = "ghostI";
            this.ghostI.Size = new System.Drawing.Size(54, 61);
            this.ghostI.TabIndex = 4;
            this.ghostI.TabStop = false;
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(256, 256);
            this.Controls.Add(this.ghostI);
            this.Controls.Add(this.exitB);
            this.Controls.Add(this.recordsB);
            this.Controls.Add(this.startGameB);
            this.Controls.Add(this.backgroundI);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "StartForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StartForm";
            ((System.ComponentModel.ISupportInitialize)(this.backgroundI)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ghostI)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox backgroundI;
        private System.Windows.Forms.Button startGameB;
        private System.Windows.Forms.Button recordsB;
        private System.Windows.Forms.Button exitB;
        private System.Windows.Forms.PictureBox ghostI;
    }
}