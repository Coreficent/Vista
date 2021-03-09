namespace Coreficent.Generator
{
    using Coreficent.Tile;
    using Coreficent.Utility;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Factory : ReinforcedBehavior
    {



        private Dictionary<string, TileBase> tiles = new Dictionary<string, TileBase>();

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
