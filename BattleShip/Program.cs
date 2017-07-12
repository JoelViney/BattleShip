using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    class Program
    {
        static void Main(string[] args)
        {
            GameEngine engine = new GameEngine();

            engine.Setup();

            engine.PrintBoard();
            Console.ReadLine();

            while (!engine.GameOver)
            {
                PlayerAction action = engine.PlayTurn();

                engine.PrintAction(action);

                engine.PrintShotBoard(engine.DefendingPlayer);
            }

            engine.PrintWinner();

            Console.ReadLine();
        }
    }
}
