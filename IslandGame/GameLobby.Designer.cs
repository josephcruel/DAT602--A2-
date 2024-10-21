namespace IslandGame
{
    partial class GameLobby
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
            this.btnJoinGame = new System.Windows.Forms.Button();
            this.btnNewGame = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.lblPlayersOnline = new System.Windows.Forms.Label();
            this.lblPlayer = new System.Windows.Forms.Label();
            this.lblCurrentGames = new System.Windows.Forms.Label();
            this.lsbPlayersOnline = new System.Windows.Forms.ListBox();
            this.lsbCurrentGames = new System.Windows.Forms.ListBox();
            this.btnAdminConsole = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnJoinGame
            // 
            this.btnJoinGame.Location = new System.Drawing.Point(550, 380);
            this.btnJoinGame.Name = "btnJoinGame";
            this.btnJoinGame.Size = new System.Drawing.Size(97, 46);
            this.btnJoinGame.TabIndex = 1;
            this.btnJoinGame.Text = "Join Game";
            this.btnJoinGame.UseVisualStyleBackColor = true;
            this.btnJoinGame.Click += new System.EventHandler(this.btnJoinGame_Click);
            // 
            // btnNewGame
            // 
            this.btnNewGame.Location = new System.Drawing.Point(430, 380);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Size = new System.Drawing.Size(97, 46);
            this.btnNewGame.TabIndex = 2;
            this.btnNewGame.Text = "New Game";
            this.btnNewGame.UseVisualStyleBackColor = true;
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(118, 9);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(111, 35);
            this.btnLogout.TabIndex = 3;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // lblPlayersOnline
            // 
            this.lblPlayersOnline.AutoSize = true;
            this.lblPlayersOnline.Location = new System.Drawing.Point(35, 85);
            this.lblPlayersOnline.Name = "lblPlayersOnline";
            this.lblPlayersOnline.Size = new System.Drawing.Size(94, 16);
            this.lblPlayersOnline.TabIndex = 4;
            this.lblPlayersOnline.Text = "Players Online";
            // 
            // lblPlayer
            // 
            this.lblPlayer.AutoSize = true;
            this.lblPlayer.Location = new System.Drawing.Point(12, 9);
            this.lblPlayer.Name = "lblPlayer";
            this.lblPlayer.Size = new System.Drawing.Size(100, 16);
            this.lblPlayer.TabIndex = 5;
            this.lblPlayer.Text = "Player : Joseph";
            this.lblPlayer.Click += new System.EventHandler(this.label2_Click);
            // 
            // lblCurrentGames
            // 
            this.lblCurrentGames.AutoSize = true;
            this.lblCurrentGames.Location = new System.Drawing.Point(382, 85);
            this.lblCurrentGames.Name = "lblCurrentGames";
            this.lblCurrentGames.Size = new System.Drawing.Size(96, 16);
            this.lblCurrentGames.TabIndex = 6;
            this.lblCurrentGames.Text = "Current Games";
            // 
            // lsbPlayersOnline
            // 
            this.lsbPlayersOnline.FormattingEnabled = true;
            this.lsbPlayersOnline.ItemHeight = 16;
            this.lsbPlayersOnline.Location = new System.Drawing.Point(38, 115);
            this.lsbPlayersOnline.Name = "lsbPlayersOnline";
            this.lsbPlayersOnline.Size = new System.Drawing.Size(262, 228);
            this.lsbPlayersOnline.TabIndex = 7;
            // 
            // lsbCurrentGames
            // 
            this.lsbCurrentGames.FormattingEnabled = true;
            this.lsbCurrentGames.ItemHeight = 16;
            this.lsbCurrentGames.Location = new System.Drawing.Point(385, 115);
            this.lsbCurrentGames.Name = "lsbCurrentGames";
            this.lsbCurrentGames.Size = new System.Drawing.Size(262, 228);
            this.lsbCurrentGames.TabIndex = 8;
            this.lsbCurrentGames.SelectedIndexChanged += new System.EventHandler(this.lsbCurrentGames_SelectedIndexChanged);
            // 
            // btnAdminConsole
            // 
            this.btnAdminConsole.Location = new System.Drawing.Point(550, 9);
            this.btnAdminConsole.Name = "btnAdminConsole";
            this.btnAdminConsole.Size = new System.Drawing.Size(97, 46);
            this.btnAdminConsole.TabIndex = 9;
            this.btnAdminConsole.Text = "Admin Console";
            this.btnAdminConsole.UseVisualStyleBackColor = true;
            this.btnAdminConsole.Click += new System.EventHandler(this.btnAdminConsole_Click);
            // 
            // GameLobby
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 450);
            this.Controls.Add(this.btnAdminConsole);
            this.Controls.Add(this.lsbCurrentGames);
            this.Controls.Add(this.lsbPlayersOnline);
            this.Controls.Add(this.lblCurrentGames);
            this.Controls.Add(this.lblPlayer);
            this.Controls.Add(this.lblPlayersOnline);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnNewGame);
            this.Controls.Add(this.btnJoinGame);
            this.Name = "GameLobby";
            this.Text = "Game Lobby";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnJoinGame;
        private System.Windows.Forms.Button btnNewGame;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Label lblPlayersOnline;
        private System.Windows.Forms.Label lblPlayer;
        private System.Windows.Forms.Label lblCurrentGames;
        private System.Windows.Forms.ListBox lsbPlayersOnline;
        private System.Windows.Forms.ListBox lsbCurrentGames;
        private System.Windows.Forms.Button btnAdminConsole;
    }
}