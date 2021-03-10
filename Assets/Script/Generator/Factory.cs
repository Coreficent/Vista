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
        public static string RiverStraight = "RiverStraight";

        [SerializeField]
        private TileBase grass;

        [SerializeField]
        private TileBase tower;

        [SerializeField]
        private TileBase riverStraight;

        private Dictionary<string, TileBase> tiles = new Dictionary<string, TileBase>();

        protected virtual void Start()
        {
            tiles[Grass] = grass;
            tiles[Tower] = tower;
            tiles[RiverStraight] = riverStraight;
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
