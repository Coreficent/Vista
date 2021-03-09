namespace Coreficent.Generator
{
    using Coreficent.Tile;
    using Coreficent.Utility;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class Board : ReinforcedBehavior
    {
        [SerializeField]
        private int radius;

        public List<TileBase> geneses;



        private int index = 0;
        private List<Vector2Int> positions = new List<Vector2Int>();

        private TileBase[,] board;

        private Stack<Vector2Int> queue = new Stack<Vector2Int>();


        

        public int Radius
        {
            get { return radius; }
        }


        public void Start()
        {
            for (var x = 0; x < radius; ++x)
            {
                for (var y = 0; y < radius; ++y)
                {
                    positions.Add(new Vector2Int(x, y));
                }
            }
            board = new TileBase[radius, radius];
        }

        public bool HasNext()
        {
            return index < positions.Count;
        }

        public Vector2Int Next()
        {
            return positions[index++];
        }

        public void PlaceTile(Vector2Int position, TileBase tile)
        {
            Destroy(board[position.x, position.y].gameObject);
            TileBase tower1 = Instantiate(tile);
            tower1.transform.position = new Vector3(position.x, 0, position.y);
            board[position.x, position.y] = tower1;
        }


        public void RepairTile(Vector2Int position)
        {

            if (board[position.x, position.y])
            {
                Destroy(board[position.x, position.y].gameObject);

                TileBase north = GetTile(new Vector2Int(position.x, position.y + 1));
                TileBase south = GetTile(new Vector2Int(position.x, position.y - 1));
                TileBase west = GetTile(new Vector2Int(position.x - 1, position.y));
                TileBase east = GetTile(new Vector2Int(position.x + 1, position.y));


                //north.transform.position += new Vector3(0, 1, 0);
                //south.transform.position += new Vector3(0, 2, 0);
                //west.transform.position += new Vector3(0, 3, 0);
                //east.transform.position += new Vector3(0, 4, 0);


                List<TileBase> validTiles = new List<TileBase>();
                if (north)
                {
                    validTiles.AddRange(north.North);
                }
                if (south)
                {
                    validTiles.AddRange(south.South);
                }
                if (west)
                {
                    validTiles.AddRange(west.West);
                }
                if (east)
                {
                    validTiles.AddRange(east.East);
                }


                if (north)
                {
                    validTiles = validTiles.Intersect(north.North).ToList();
                }
                if (south)
                {
                    validTiles = validTiles.Intersect(south.South).ToList();
                }
                if (west)
                {
                    validTiles = validTiles.Intersect(west.West).ToList();
                }
                if (east)
                {
                    validTiles = validTiles.Intersect(east.East).ToList();
                }




                //DebugUtility.Log("north", north.North);
                //DebugUtility.Log("south", south.South);
                //DebugUtility.Log("west", west.West);
                //DebugUtility.Log("east", east.East);


                DebugUtility.Log("validTiles", validTiles);

                if (validTiles.Count > 0)
                {
                    TileBase clone = validTiles[UnityEngine.Random.Range(0, validTiles.Count)];

                    board[position.x, position.y] = Instantiate(clone);
                    board[position.x, position.y].transform.position = new Vector3(position.x, 0.0f, position.y);
                    board[position.x, position.y].transform.rotation = clone.transform.rotation;
                }
            }
            else
            {
                board[position.x, position.y] = Instantiate(geneses[0], new Vector3(position.x, 0.0f, position.y), Quaternion.identity);
            }


            //board[position.x, position.y].transform.eulerAngles = new Vector3(0.0f, 90.0f * UnityEngine.Random.Range(0, 4), 0.0f);
        }

        public Vector2Int Random()
        {
            return new Vector2Int(UnityEngine.Random.Range(0, board.GetLength(0)), UnityEngine.Random.Range(0, board.GetLength(1)));
        }


        public Vector2Int Remove()
        {
            return queue.Pop();
        }

        public void Add(Vector2Int position)
        {
            if (ValidRange(position))
            {
                queue.Push(position);
            }
        }

        public int QueueCount()
        {
            return queue.Count;
        }

        public TileBase GetTile(Vector2Int position)
        {
            if (ValidRange(position))
            {
                return board[position.x, position.y];
            }

            return null;
        }

        public bool ValidRange(Vector2Int position)
        {
            return position.x < board.GetLength(0) && position.y < board.GetLength(1) && position.x >= 0 && position.y >= 0;
        }
    }
}
