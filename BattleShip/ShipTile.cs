namespace BattleShip
{
    public class ShipTile
    {
        public ShipTile(int x, int y)
        {
            this.X = x;
            this.Y = y;
            this.Hit = false;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public bool Hit { get; set; }
    }
}
