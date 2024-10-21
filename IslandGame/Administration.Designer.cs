namespace IslandGame
{
    partial class Administration
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
            this.btnGameLobby = new System.Windows.Forms.Button();
            this.lsbCurrentGames = new System.Windows.Forms.ListBox();
            this.lsbPlayersOnline = new System.Windows.Forms.ListBox();
            this.lblCurrentGames = new System.Windows.Forms.Label();
            this.lblPlayer = new System.Windows.Forms.Label();
            this.lblRegisteredPlayers = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnUpdateUser = new System.Windows.Forms.Button();
            this.btnDeleteUser = new System.Windows.Forms.Button();
            this.btnNewUser = new System.Windows.Forms.Button();
            this.btnKillGame = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnGameLobby
            // 
            this.btnGameLobby.Location = new System.Drawing.Point(509, 17);
            this.btnGameLobby.Name = "btnGameLobby";
            this.btnGameLobby.Size = new System.Drawing.Size(126, 46);
            this.btnGameLobby.TabIndex = 18;
            this.btnGameLobby.Text = "Return to Game Lobby";
            this.btnGameLobby.UseVisualStyleBackColor = true;
            this.btnGameLobby.Click += new System.EventHandler(this.btnAdminConsole_Click);
            // 
            // lsbCurrentGames
            // 
            this.lsbCurrentGames.FormattingEnabled = true;
            this.lsbCurrentGames.ItemHeight = 16;
            this.lsbCurrentGames.Location = new System.Drawing.Point(32, 109);
            this.lsbCurrentGames.Name = "lsbCurrentGames";
            this.lsbCurrentGames.Size = new System.Drawing.Size(282, 228);
            this.lsbCurrentGames.TabIndex = 17;
            // 
            // lsbPlayersOnline
            // 
            this.lsbPlayersOnline.FormattingEnabled = true;
            this.lsbPlayersOnline.ItemHeight = 16;
            this.lsbPlayersOnline.Location = new System.Drawing.Point(351, 109);
            this.lsbPlayersOnline.Name = "lsbPlayersOnline";
            this.lsbPlayersOnline.Size = new System.Drawing.Size(284, 228);
            this.lsbPlayersOnline.TabIndex = 16;
            // 
            // lblCurrentGames
            // 
            this.lblCurrentGames.AutoSize = true;
            this.lblCurrentGames.Location = new System.Drawing.Point(29, 84);
            this.lblCurrentGames.Name = "lblCurrentGames";
            this.lblCurrentGames.Size = new System.Drawing.Size(96, 16);
            this.lblCurrentGames.TabIndex = 15;
            this.lblCurrentGames.Text = "Current Games";
            // 
            // lblPlayer
            // 
            this.lblPlayer.AutoSize = true;
            this.lblPlayer.Location = new System.Drawing.Point(12, 17);
            this.lblPlayer.Name = "lblPlayer";
            this.lblPlayer.Size = new System.Drawing.Size(100, 16);
            this.lblPlayer.TabIndex = 14;
            this.lblPlayer.Text = "Player : Joseph";
            // 
            // lblRegisteredPlayers
            // 
            this.lblRegisteredPlayers.AutoSize = true;
            this.lblRegisteredPlayers.Location = new System.Drawing.Point(348, 84);
            this.lblRegisteredPlayers.Name = "lblRegisteredPlayers";
            this.lblRegisteredPlayers.Size = new System.Drawing.Size(123, 16);
            this.lblRegisteredPlayers.TabIndex = 13;
            this.lblRegisteredPlayers.Text = "Registered Players";
            this.lblRegisteredPlayers.Click += new System.EventHandler(this.lblPlayersOnline_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(118, 12);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(111, 35);
            this.btnLogout.TabIndex = 12;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnUpdateUser
            // 
            this.btnUpdateUser.Location = new System.Drawing.Point(444, 377);
            this.btnUpdateUser.Name = "btnUpdateUser";
            this.btnUpdateUser.Size = new System.Drawing.Size(97, 46);
            this.btnUpdateUser.TabIndex = 11;
            this.btnUpdateUser.Text = "Update User";
            this.btnUpdateUser.UseVisualStyleBackColor = true;
            // 
            // btnDeleteUser
            // 
            this.btnDeleteUser.Location = new System.Drawing.Point(547, 377);
            this.btnDeleteUser.Name = "btnDeleteUser";
            this.btnDeleteUser.Size = new System.Drawing.Size(97, 46);
            this.btnDeleteUser.TabIndex = 10;
            this.btnDeleteUser.Text = "Delete User";
            this.btnDeleteUser.UseVisualStyleBackColor = true;
            // 
            // btnNewUser
            // 
            this.btnNewUser.Location = new System.Drawing.Point(341, 377);
            this.btnNewUser.Name = "btnNewUser";
            this.btnNewUser.Size = new System.Drawing.Size(97, 46);
            this.btnNewUser.TabIndex = 19;
            this.btnNewUser.Text = "New User";
            this.btnNewUser.UseVisualStyleBackColor = true;
            this.btnNewUser.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnKillGame
            // 
            this.btnKillGame.Location = new System.Drawing.Point(32, 377);
            this.btnKillGame.Name = "btnKillGame";
            this.btnKillGame.Size = new System.Drawing.Size(97, 46);
            this.btnKillGame.TabIndex = 20;
            this.btnKillGame.Text = "Kill Game";
            this.btnKillGame.UseVisualStyleBackColor = true;
            // 
            // Administration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664, 450);
            this.Controls.Add(this.btnKillGame);
            this.Controls.Add(this.btnNewUser);
            this.Controls.Add(this.btnGameLobby);
            this.Controls.Add(this.lsbCurrentGames);
            this.Controls.Add(this.lsbPlayersOnline);
            this.Controls.Add(this.lblCurrentGames);
            this.Controls.Add(this.lblPlayer);
            this.Controls.Add(this.lblRegisteredPlayers);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnUpdateUser);
            this.Controls.Add(this.btnDeleteUser);
            this.Name = "Administration";
            this.Text = "Administration";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGameLobby;
        private System.Windows.Forms.ListBox lsbCurrentGames;
        private System.Windows.Forms.ListBox lsbPlayersOnline;
        private System.Windows.Forms.Label lblCurrentGames;
        private System.Windows.Forms.Label lblPlayer;
        private System.Windows.Forms.Label lblRegisteredPlayers;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnUpdateUser;
        private System.Windows.Forms.Button btnDeleteUser;
        private System.Windows.Forms.Button btnNewUser;
        private System.Windows.Forms.Button btnKillGame;
    }
}