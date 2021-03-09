namespace Coreficent.Generator
{
    using Coreficent.Tile;
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;

    public class Factory : ReinforcedBehavior
    {
        public static string GRASS = "Grass";


        [SerializeField]
        private TileBase grass;

        private Dictionary<string, TileBase> tiles = new Dictionary<string, TileBase>();

        protected virtual void Start()
        {
            tiles[GRASS] = grass;
        }

        public TileBase Create(string name)
        {
            if (tiles.ContainsKey(name))
            {
                DebugUtility.Warn("Trying to access undefined tile", name);
            }
            return tiles[name];
        }
    }
}
