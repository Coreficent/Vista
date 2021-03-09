namespace Coreficent.Generator
{
    using Coreficent.Utility;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class TileFactory : ReinforcedBehavior
    {
        public Genesis Tile;
        public Genesis TileRiverStraight;

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
                    positions.Add(new Tuple<int, int>(x, y));
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

        public void NextTile(Genesis type, Tuple<int, int> position)
        {
            if (board[position.Item1, position.Item2])
            {
                Destroy(board[position.Item1, position.Item2].gameObject);
            }

            board[position.Item1, position.Item2] = Instantiate(type, new Vector3(position.Item1, 0.0f, position.Item2), Quaternion.identity);
        }

        public Tuple<int, int> Random()
        {
            return positions[UnityEngine.Random.Range(0, board.Length)];
        }
    }
}
