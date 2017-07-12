using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    public class PlayerAction
    {
        public PlayerAction(string playerName, int x, int y)
        {
            this.PlayerName = playerName;
            this.X = x;
            this.Y = y;
            this.Hit = false;
            this.ShipClassification = ShipClassification.None;
            this.Sunk = false;
        }

        public string PlayerName { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public bool Hit { get; set; }
        public ShipClassification ShipClassification { get; set; }
        public bool Sunk { get; set; }
    }
}
