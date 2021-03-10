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
        private int size;

        private Dictionary<string, TileBase> map = new Dictionary<string, TileBase>();

        protected virtual void Start()
        {
            DebugUtility.Assert("board size validation", size != 0);
        }

        public int Size
        {
            get { return size; }
        }

        public TileBase Place(Vector3 position, TileBase tileType)
        {
            TileBase instance = Instantiate(tileType, transform);
            instance.transform.position = position;
            map[instance.HashName] = instance;

            return instance;
        }

        public TileBase Replace(Vector3 position, TileBase tile)
        {
            Destroy(map[TileBase.HashNameFromPosition(position)].gameObject);
            return Place(position, tile);
        }

        public Vector3 RandomPosition()
        {
            return new Vector3(Mathf.RoundToInt(Random.Range(0.0f, Size - 1)), Mathf.RoundToInt(Random.Range(0.0f, Size - 1)), 0.0f);
        }

        public List<Vector3> GetAllPositions()
        {
            return map.Keys.ToList().ConvertAll<Vector3>(hashName => TileBase.PositionFromHashName(hashName));
        }
        public bool ValidPosition(Vector3 position)
        {
            return map.ContainsKey(TileBase.HashNameFromPosition(position));
        }

        public TileBase GetTile(Vector3 position)
        {
            if (map.ContainsKey(TileBase.HashNameFromPosition(position)))
            {
                return map[TileBase.HashNameFromPosition(position)];
            }
            else
            {
                return null;
            }
        }

        public TileBase GetNorthTile(Vector3 position)
        {
            return GetTile(new Vector3(0.0f, 1.0f, 0.0f) + position);
        }

        public TileBase GetSouthTile(Vector3 position)
        {
            return GetTile(new Vector3(0.0f, -1.0f, 0.0f) + position);
        }

        public TileBase GetWestTile(Vector3 position)
        {
            return GetTile(new Vector3(-1.0f, 0.0f, 0.0f) + position);
        }

        public TileBase GetEastTile(Vector3 position)
        {
            return GetTile(new Vector3(1.0f, 0.0f, 0.0f) + position);
        }
    }
}
