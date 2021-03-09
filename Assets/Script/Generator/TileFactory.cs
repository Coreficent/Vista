﻿namespace Coreficent.Generator
{
    using Coreficent.Utility;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class TileFactory : ReinforcedBehavior
    {
        public Genesis Tile;
        public Genesis TileRiverStraight;
        public Genesis TileRiverCorner;

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

        public void PlaceTile(Vector2Int position)
        {
            Genesis type;

            if (board[position.x, position.y])
            {
                Destroy(board[position.x, position.y].gameObject);

                Genesis north = GetTile(new Vector2Int(position.x, position.y + 1));
                Genesis south = GetTile(new Vector2Int(position.x, position.y - 1));
                Genesis west = GetTile(new Vector2Int(position.x - 1, position.y));
                Genesis east = GetTile(new Vector2Int(position.x + 1, position.y));


                //TODO need to use interception
                DebugUtility.ToDo("need to use interception");
                List<Genesis> validTiles = new List<Genesis>();
                if (north)
                {
                    validTiles.AddRange(north.South);
                }
                if (south)
                {
                    validTiles.AddRange(south.North);
                }
                if (west)
                {
                    validTiles.AddRange(west.East);
                }
                if (east)
                {
                    validTiles.AddRange(east.West);
                }



                string r = "";
                foreach (var i in validTiles)
                {
                    r += i;
                }

                DebugUtility.Log("validTilesrrrr", r);

                if (validTiles.Count > 0)
                {
                    board[position.x, position.y] = Instantiate(validTiles[UnityEngine.Random.Range(0, validTiles.Count)], new Vector3(position.x, 0.0f, position.y), Quaternion.identity);
                }


            }
            else
            {
                board[position.x, position.y] = Instantiate(Tile, new Vector3(position.x, 0.0f, position.y), Quaternion.identity);
            }


            //board[position.x, position.y].transform.eulerAngles = new Vector3(0.0f, 90.0f * UnityEngine.Random.Range(0, 4), 0.0f);
        }

        public Vector2Int Random()
        {
            return new Vector2Int(UnityEngine.Random.Range(0, board.GetLength(0)), UnityEngine.Random.Range(0, board.GetLength(1)));
        }


        public Vector2Int Dequeue()
        {
            return queue.Dequeue();
        }

        public void Enqueue(Vector2Int position)
        {
            if (ValidRange(position))
            {
                queue.Enqueue(position);
            }
        }

        public int QueueCount()
        {
            return queue.Count;
        }

        public Genesis GetTile(Vector2Int position)
        {
            if (ValidRange(position))
            {
                return board[position.x, position.y];
            }
            return null;
        }

        private bool ValidRange(Vector2Int position)
        {
            return position.x < board.GetLength(0) && position.y < board.GetLength(1) && position.x >= 0 && position.y >= 0;
        }
    }
}
