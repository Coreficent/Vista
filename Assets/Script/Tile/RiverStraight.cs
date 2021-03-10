namespace Coreficent.Tile
{
    using Coreficent.Generator;
    using System.Collections.Generic;
    using UnityEngine;

    public class RiverStraight : TileBase
    {
        public override List<Neighbor> Neighbors
        {
            get
            {
                List<Neighbor> neighbors = new List<Neighbor>();

                Neighbor[] north = new Neighbor[] {
                    new Neighbor() { Offset = new Vector3(0.0f, 1.0f, 0.0f), Tile = Factory.RiverStraight, Rotation = 0.0f }
                };

                neighbors.Add(Pick(north));

                Neighbor[] south = new Neighbor[] {
                    new Neighbor() { Offset = new Vector3(0.0f, -1.0f, 0.0f), Tile = Factory.RiverStraight, Rotation = 0.0f },
                    new Neighbor() { Offset = new Vector3(0.0f, -1.0f, 0.0f), Tile = Factory.RiverCorner, Rotation = 0.0f }
                };

                neighbors.Add(Pick(south));

                return neighbors;
            }
        }
    }
}