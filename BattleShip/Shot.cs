namespace BattleShip
{
    class Shot
    {
        public Shot()
        {
            ShipClassification = ShipClassification.None;
        }

        public ShipClassification ShipClassification { get; set; }

        public bool Hit
        {
            get
            {
                return this.ShipClassification != ShipClassification.None;
            }
        }
    }
}
