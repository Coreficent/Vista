namespace Coreficent.Tile
{
    using Coreficent.Generator;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class RiverCorner : TileBase
    {
        public override List<Neighbor> Neighbors
        {
            get
            {
                List<Neighbor> neighbors = new List<Neighbor>();

                Neighbor[] north = new Neighbor[] {
                    new Neighbor() { Offset = RotateOffset(1.0f, 0.0f), Tile = Factory.RiverStraight, Rotation = CumulateRotation(0.0f) },
                };

                neighbors.Add(Pick(north));

                Neighbor[] west = new Neighbor[] {
                    new Neighbor() { Offset = RotateOffset(1.0f, 90.0f), Tile = Factory.RiverStraight, Rotation = CumulateRotation(270.0f) },
                };

                neighbors.Add(Pick(west));

                return neighbors;
            }
        }
    }
}
