namespace Coreficent.Generator
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class TileRiverStraightWE : Genesis
    {
        public override List<Vector2Int> GetNeighbors(Vector2Int position)
        {
            List<Vector2Int> result = new List<Vector2Int>();

            result.Add(neighbors[GetDirectionIndex(1)] + position);
            result.Add(neighbors[GetDirectionIndex(3)] + position);

            return result;
        }
    }
}
