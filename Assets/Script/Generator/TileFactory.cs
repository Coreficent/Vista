namespace Coreficent.Generator
{
    using Coreficent.Utility;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class TileFactory : ReinforcedBehavior
    {
        public Genesis Tile;
        public int size;

        private int index = 0;
        private List<Tuple<int, int>> positions = new List<Tuple<int, int>>();

        private Genesis[,] board;

        public void Start()
        {
            for (var x = 0; x < size; ++x)
            {
                for (var y = 0; y < size; ++y)
                {
                    positions.Add(new Tuple<int, int>(x - size / 2, y - size / 2));
                }
            }
            board = new Genesis[size, size];
        }

        public bool HasNext()
        {
            return index < positions.Count;
        }

        public Tuple<int, int> Next()
        {
            return positions[index++];
        }

        public void NextTile(Tuple<int, int> position)
        {
            board[position.Item1, position.Item2] = Instantiate(Tile, new Vector3(position.Item1, 0.0f, position.Item2), Quaternion.identity);
        }
    }
}
