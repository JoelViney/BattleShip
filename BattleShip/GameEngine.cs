using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    /// <summary>
    /// Structure
    /// 
    ///     GameEngine
    ///         Player1
    ///         Player2
    ///             List<Ship>
    ///                 List<ShipTile>
    ///             Map<Shot>
    ///             List<PlayerAction>
    /// 
    /// </summary>
    class GameEngine
    {
        public const int GridSize = 10;

        private Random Rand;
        public bool GameOver { get; private set; }

        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }

        public Player AttackingPlayer { get; private set; }
        public Player DefendingPlayer { get; private set; }


        #region Setup Methods...

        public void Setup()
        {
            this.Rand = new Random();
            this.GameOver = false;
            this.Player1 = new Player("Player 1");
            this.Player2 = new Player("Player 2");

            SetupPlayer(this.Player1);
            SetupPlayer(this.Player2);

            // Randomise who goes first.
            if (Rand.Next(0, 1) == 0)
            {
                this.AttackingPlayer = this.Player1;
                this.DefendingPlayer = this.Player2;
            }
            else
            {
                this.AttackingPlayer = this.Player2;
                this.DefendingPlayer = this.Player1;
            }
        }

        private void SetupPlayer(Player player)
        {
            // Setup the board
            PlaceShip(player, new Ship(ShipClassification.Carrier));
            PlaceShip(player, new Ship(ShipClassification.Battleship));
            PlaceShip(player, new Ship(ShipClassification.Cruiser));
            PlaceShip(player, new Ship(ShipClassification.Submarine));
            PlaceShip(player, new Ship(ShipClassification.Destroyer));
        }

        private void PlaceShip(Player player, Ship ship)
        {
            bool canPlace = false;
            while (canPlace == false)
            {
                int startX = Rand.Next(0, GameEngine.GridSize);
                int startY = Rand.Next(0, GameEngine.GridSize);
                int direction = Rand.Next(0, 3); // North, East, South, West

                // Try to place
                int x = startX;
                int y = startY;
                canPlace = true;

                for (int i = 0; i < ship.Length; i++)
                {
                    if (x < 0 || x >= GameEngine.GridSize || y < 0 || y >= GameEngine.GridSize)
                    {
                        canPlace = false;
                        break;
                    }

                    if (player.IsShipAtTile(x, y))
                    {
                        canPlace = false;
                        break;
                    }

                    switch (direction)
                    {
                        case 0: y = y - 1; break;
                        case 1: x = x + 1; break;
                        case 2: y = y + 1; break;
                        case 3: x = x - 1; break;
                    }
                }

                if (canPlace)
                {
                    x = startX;
                    y = startY;
                    for (int i = 0; i < ship.Length; i++)
                    {
                        // Place the Ship down.
                        ship.Tiles.Add(new ShipTile(x, y));

                        switch (direction)
                        {
                            case 0: y = y - 1; break;
                            case 1: x = x + 1; break;
                            case 2: y = y + 1; break;
                            case 3: x = x - 1; break;
                        }
                    }

                    player.Ships.Add(ship);
                }
            }
        }

        #endregion

        public PlayerAction PlayTurn()
        {
            PlayerAction action = null;

            while (action == null)
            {
                int x = Rand.Next(0, GameEngine.GridSize);
                int y = Rand.Next(0, GameEngine.GridSize);

                if (AttackingPlayer.FiringGrid.IsEmpty(x, y))
                {
                    action = new PlayerAction(this.AttackingPlayer.Name, x, y);

                    // Take the shot
                    Shot shot = new Shot();

                    Ship ship = DefendingPlayer.GetShip(x, y);
                    if (ship != null)
                    {
                        ship.Hit(x, y);
                        shot.ShipClassification = ship.Classification;

                        action.Hit = true;
                        action.ShipClassification = ship.Classification;
                        action.Sunk = ship.IsSunk();
                    }

                    AttackingPlayer.FiringGrid.Map[x, y] = shot;
                }
            }

            this.AttackingPlayer.Actions.Add(action);

            if (this.DefendingPlayer.AreAllShipsSunk())
            {
                this.GameOver = true;
            }
            else
            {
                // Flip the attacking and defending players..
                var temp = AttackingPlayer;
                AttackingPlayer = DefendingPlayer;
                DefendingPlayer = temp;
            }

            return action;
        }

        public void PrintBoard()
        {
            PrintBoard(Player1);
            Console.WriteLine("");
            PrintBoard(Player2);
        }

        void PrintBoard(Player player)
        {
            Console.WriteLine(player.Name);

            for (int x = 0; x < GameEngine.GridSize; x++)
            {
                string line = "";
                for (int y = 0; y < GameEngine.GridSize; y++)
                {
                    var ship = player.GetShip(x, y);
                    if (ship != null)
                        line += Ship.ShipDisplayChar(ship.Classification);
                    else
                        line += "~";
                }
                Console.WriteLine(line);
            }
        }

        public void PrintShotBoard(Player player)
        {
            Console.WriteLine(player.Name);

            for (int x = 0; x < GameEngine.GridSize; x++)
            {
                string line = "";
                for (int y = 0; y < GameEngine.GridSize; y++)
                {
                    var shot = player.FiringGrid.Map[x, y];

                    if (shot != null && shot.Hit)
                        line += Ship.ShipDisplayChar(shot.ShipClassification);
                    else if (shot != null)
                        line += "X";
                    else
                        line += "~";
                }

                Console.WriteLine(line);
            }
        }

        public void PrintAction(PlayerAction action)
        {
            Console.Write("Player {0} attacks {1} x {2}... ", action.PlayerName, action.X, action.Y);

            if (action.Hit && action.Sunk)
                Console.WriteLine("and SINKS a {0}!!!", action.ShipClassification);
            else if (action.Hit)
                Console.WriteLine("and HITS a {0}!", action.ShipClassification);
            else
                Console.WriteLine("and misses.");
        }

        public void PrintWinner()
        {
            Console.WriteLine("");
            Console.WriteLine("*** GAME OVER ***");
            Console.WriteLine("");
            Console.WriteLine("Player {0} wins in {1} turns", this.AttackingPlayer.Name, this.AttackingPlayer.Actions.Count);
        }
    }
}
