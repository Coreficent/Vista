namespace Coreficent.Tile
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class TileRiverStraight : TileBase
    {

        public override List<Vector2Int> GetNeighbors(Vector2Int position)
        {
            List<Vector2Int> result = new List<Vector2Int>();

            result.Add(neighbors[GetDirectionIndex(0)] + position);
            result.Add(neighbors[GetDirectionIndex(2)] + position);

            return result;
        }
    }
}