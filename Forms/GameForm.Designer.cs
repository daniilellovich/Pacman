namespace Pacman
{
    partial class GameForm
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
            this.components = new System.ComponentModel.Container();
            this.GameLoopTimer = new System.Windows.Forms.Timer(this.components);
            this.scoreL = new System.Windows.Forms.Label();
            this.highScoreTextL = new System.Windows.Forms.Label();
            this.highScoreL = new System.Windows.Forms.Label();
            this.GameControllerTimer = new System.Windows.Forms.Timer(this.components);
            this.readyLabelP = new System.Windows.Forms.PictureBox();
            this.gameoverLabelP = new System.Windows.Forms.PictureBox();
            this.pnPacmanLevel = new LevelPanel();
            this.stageL = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.readyLabelP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gameoverLabelP)).BeginInit();
            this.SuspendLayout();
            // 
            // GameLoopTimer
            // 
            this.GameLoopTimer.Interval = 10;
            this.GameLoopTimer.Tick += new System.EventHandler(this.GameLoopTimer_Tick);
            // 
            // scoreL
            // 
            this.scoreL.AutoSize = true;
            this.scoreL.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.scoreL.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.scoreL.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.scoreL.Location = new System.Drawing.Point(199, 9);
            this.scoreL.Name = "scoreL";
            this.scoreL.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.scoreL.Size = new System.Drawing.Size(45, 18);
            this.scoreL.TabIndex = 5;
            this.scoreL.Text = "0       \r\n";
            this.scoreL.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // highScoreTextL
            // 
            this.highScoreTextL.AutoSize = true;
            this.highScoreTextL.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.highScoreTextL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.highScoreTextL.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.highScoreTextL.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.highScoreTextL.Location = new System.Drawing.Point(71, 8);
            this.highScoreTextL.Name = "highScoreTextL";
            this.highScoreTextL.Size = new System.Drawing.Size(106, 18);
            this.highScoreTextL.TabIndex = 5;
            this.highScoreTextL.Text = "HIGH SCORE";
            // 
            // highScoreL
            // 
            this.highScoreL.AutoSize = true;
            this.highScoreL.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.highScoreL.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.highScoreL.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.highScoreL.Location = new System.Drawing.Point(112, 26);
            this.highScoreL.Name = "highScoreL";
            this.highScoreL.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.highScoreL.Size = new System.Drawing.Size(17, 18);
            this.highScoreL.TabIndex = 5;
            this.highScoreL.Text = "0";
            this.highScoreL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GameControllerTimer
            // 
            this.GameControllerTimer.Interval = 1000;
            this.GameControllerTimer.Tick += new System.EventHandler(this.GameControllerTimer_Tick);
            // 
            // readyLabelP
            // 
            this.readyLabelP.Location = new System.Drawing.Point(74, 117);
            this.readyLabelP.Name = "readyLabelP";
            this.readyLabelP.Size = new System.Drawing.Size(100, 19);
            this.readyLabelP.TabIndex = 8;
            this.readyLabelP.TabStop = false;
            this.readyLabelP.Visible = false;
            // 
            // gameoverLabelP
            // 
            this.gameoverLabelP.Location = new System.Drawing.Point(74, 142);
            this.gameoverLabelP.Name = "gameoverLabelP";
            this.gameoverLabelP.Size = new System.Drawing.Size(100, 19);
            this.gameoverLabelP.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.gameoverLabelP.TabIndex = 8;
            this.gameoverLabelP.TabStop = false;
            this.gameoverLabelP.Visible = false;
            // 
            // pnPacmanLevel
            // 
            this.pnPacmanLevel.AutoSize = true;
            this.pnPacmanLevel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnPacmanLevel.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pnPacmanLevel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnPacmanLevel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pnPacmanLevel.Location = new System.Drawing.Point(0, 0);
            this.pnPacmanLevel.Name = "pnPacmanLevel";
            this.pnPacmanLevel.Size = new System.Drawing.Size(256, 256);
            this.pnPacmanLevel.TabIndex = 6;
            this.pnPacmanLevel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pnPacmanLevel_KeyDown);
            this.pnPacmanLevel.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.pnPacmanLevel_PreviewKeyDown);
            // 
            // stageL
            // 
            this.stageL.AutoSize = true;
            this.stageL.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.stageL.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.stageL.Location = new System.Drawing.Point(191, 31);
            this.stageL.Name = "stageL";
            this.stageL.Size = new System.Drawing.Size(65, 13);
            this.stageL.TabIndex = 9;
            this.stageL.Text = "stage 1/256";
            this.stageL.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 256);
            this.Controls.Add(this.gameoverLabelP);
            this.Controls.Add(this.readyLabelP);
            this.Controls.Add(this.stageL);
            this.Controls.Add(this.highScoreTextL);
            this.Controls.Add(this.highScoreL);
            this.Controls.Add(this.scoreL);
            this.Controls.Add(this.pnPacmanLevel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GameForm";
            this.Text = "Game";
            ((System.ComponentModel.ISupportInitialize)(this.readyLabelP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gameoverLabelP)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public LevelPanel pnPacmanLevel;
        public System.Windows.Forms.Timer GameLoopTimer;
        public System.Windows.Forms.Label scoreL;
        public System.Windows.Forms.Label highScoreTextL;
        public System.Windows.Forms.Label highScoreL;
        public System.Windows.Forms.Timer GameControllerTimer;
        public System.Windows.Forms.PictureBox gameoverLabelP;
        public System.Windows.Forms.Label stageL;
        private System.Windows.Forms.PictureBox readyLabelP;
    }
}

