namespace Coreficent.Generator
{
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;

    public class TileFactory : ReinforcedBehavior
    {
        public Genesis Tile;
        public int size;

        private int index = 0;
        private List<Vector2> positions = new List<Vector2>();

        public void Start()
        {
            for (var x = 0; x < size; ++x)
            {
                for (var y = 0; y < size; ++y)
                {
                    positions.Add(new Vector2(x - size / 2, y - size / 2));
                }
            }
        }

        public bool HasNext()
        {
            return index < positions.Count;
        }

        public Vector2 Next()
        {
            return positions[index++];
        }

        public void NextTile(Vector2 position)
        {
            Instantiate(Tile, new Vector3(position.x, 0.0f, position.y), Quaternion.identity);
        }
    }
}
