namespace Coreficent.Generator
{
    using Coreficent.Utility;
    using UnityEngine;

    public class TileFactory : ReinforcedBehavior
    {
        public Genesis Tile;
        public Genesis Next(Genesis current)
        {
            return Tile;
        }

        public void NextTile(Vector2 position)
        {
            Instantiate(Tile, new Vector3(position.x, 0.0f, position.y), Quaternion.identity);
        }
    }
}
