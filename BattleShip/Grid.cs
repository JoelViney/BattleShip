using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip
{
    /// <summary>
    /// Represents either a Grid containing ships or a Grid containing 'Shots'
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class Grid<T>
    {
        public T[,] Map { get; set; }

        public Grid()
        {
            Map = new T[GameEngine.GridSize, GameEngine.GridSize];
        }

        public bool IsEmpty(int x, int y)
        {
            return Map[x, y] == null;
        }
    }
}
