using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
