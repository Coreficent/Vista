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
        private List<Vector2Int> positions = new List<Vector2Int>();

        private Genesis[,] board;

        private Queue<Vector2Int> queue = new Queue<Vector2Int>();

        public void Start()
        {
            for (var x = 0; x < size; ++x)
            {
                for (var y = 0; y < size; ++y)
                {
                    positions.Add(new Vector2Int(x, y));
                }
            }
            board = new Genesis[size, size];
        }

        public bool HasNext()
        {
            return index < positions.Count;
        }

        public Vector2Int Next()
        {
            return positions[index++];
        }

        public void NextTile(Genesis type, Vector2Int position)
        {
            if (board[position.x, position.y])
            {
                Destroy(board[position.x, position.y].gameObject);
            }

            board[position.x, position.y] = Instantiate(type, new Vector3(position.x, 0.0f, position.y), Quaternion.identity);
        }

        public Vector2Int Random()
        {
            return positions[UnityEngine.Random.Range(0, board.Length)];
        }


        public Vector2Int Dequeue()
        {
            return queue.Dequeue();
        }

        public void Enqueue(Vector2Int position)
        {
            if (position.x < board.GetLength(0) && position.y < board.GetLength(1) && position.x >= 0 && position.y >= 0)
            {
                queue.Enqueue(position);
            }
        }

        public int QueueCount()
        {
            return queue.Count;
        }
    }
}
