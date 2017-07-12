using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    class Player
    {
        public string Name { get; private set; }

        public List<Ship> Ships { get; set; }

        public Grid<Shot> FiringGrid { get; set; }

        public List<PlayerAction> Actions { get; set; }

        public Player(string name)
        {
            this.Name = name;
            this.Ships = new List<Ship>();
            this.FiringGrid = new Grid<Shot>();
            this.Actions = new List<PlayerAction>();
        }

        public bool IsShipAtTile(int x, int y)
        {
            foreach (var ship in this.Ships)
            {
                if (ship.IsAtTile(x, y))
                    return true;
            }
            return false;
        }

        public Ship GetShip(int x, int y)
        {
            return this.Ships.Where(obj => obj.IsAtTile(x, y)).FirstOrDefault();
        }

        public bool AreAllShipsSunk()
        {
            bool notSunk = this.Ships.Any(x => x.IsSunk() == false);
            return !notSunk;
        }
    }
}
