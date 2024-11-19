using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IslandGame
{
    public class Player
    {
        public int PlayerID { get; set; }
        public string Username { get; set; }
        public int Health { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }

        public Player(int playerID, string username, int health, int startRow, int startCol)
        {
            PlayerID = playerID;
            Username = username;
            Health = health;
            Row = startRow;
            Col = startCol;
        }

        // Method to move the player in memory
        public void Move(int newRow, int newCol)
        {
            Row = newRow;
            Col = newCol;
        }
    }
}

