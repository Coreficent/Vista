namespace Coreficent.Generator
{
    using Coreficent.Tile;
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;

    public class Factory : ReinforcedBehavior
    {
        public static string Grass = "Grass";
        public static string Tower = "Tower";

        [SerializeField]
        private TileBase grass;

        [SerializeField]
        private TileBase tower;

        private Dictionary<string, TileBase> tiles = new Dictionary<string, TileBase>();

        protected virtual void Start()
        {
            tiles[Grass] = grass;
            tiles[Tower] = tower;
        }

        public TileBase Create(string name)
        {
            if (!tiles.ContainsKey(name))
            {
                DebugUtility.Warn("Trying to access undefined tile", name);
            }
            return tiles[name];
        }
    }
}
