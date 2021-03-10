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

        public TileBase RandomTile()
        {
            return map.ElementAt(Random.Range(0, map.Count)).Value;
        }

        public bool ValidPosition(Vector3 position)
        {
            return map.ContainsKey(TileBase.HashNameFromPosition(position));
        }
    }
}
