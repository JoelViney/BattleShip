namespace BattleShip
{
    public enum ShipClassification
    {
        None,
        Destroyer,
        Submarine,
        Cruiser,
        Battleship,
        Carrier
    }

    public class Ship
    {
        public Ship(ShipClassification classification)
        {
            this.Classification = classification;
            this.Tiles = new List<ShipTile>();
        }

        #region Properties...

        public ShipClassification Classification { get; private set; }

        public List<ShipTile> Tiles { get; private set; }

        public string Name
        {
            get
            {
                return this.Classification.ToString();
            }
        }

        public static char ShipDisplayChar(ShipClassification classification)
        {
            switch (classification)
            {
                case ShipClassification.Destroyer: return 'd';
                case ShipClassification.Submarine: return 's';
                case ShipClassification.Cruiser: return 'c';
                case ShipClassification.Battleship: return 'b';
                case ShipClassification.Carrier: return 'C';
            }
            throw new Exception("Invalid case reached in Ship.Char.");
        }

        public int Length
        {
            get
            {
                switch (this.Classification)
                {
                    case ShipClassification.Destroyer: return 2;
                    case ShipClassification.Submarine: return 3;
                    case ShipClassification.Cruiser: return 3;
                    case ShipClassification.Battleship: return 4;
                    case ShipClassification.Carrier: return 5;
                }
                throw new Exception("Invalid case reached in Ship.Length.");
            }
        }

        #endregion

        public void Hit(int x, int y)
        {
            var tile = this.Tiles.Where(obj => obj.X == x && obj.Y == y).FirstOrDefault();
            tile.Hit = true;
        }

        /// <summary>Returns true if the Ship exists at a specified location.</summary>
        public bool IsAtTile(int x, int y)
        {
            foreach (var location in this.Tiles)
            {
                if (location.X == x && location.Y == y)
                    return true;
            }

            return false;
        }

        /// <summary>Returns true if all of the Ships tiles have been hit.</summary>
        public bool IsSunk()
        {
            return !this.Tiles.Any(x => x.Hit == false);
        }
    }
}
