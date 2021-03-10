namespace Coreficent.Tile
{
    using Coreficent.Generator;
    using System.Collections.Generic;

    public class RiverStraight : TileBase
    {
        public override List<Neighbor> Neighbors(Factory factory)
        {
            List<Neighbor> neighbors = new List<Neighbor>();

            Neighbor[] north = new Neighbor[] {
                    new Neighbor() { Offset = RotateOffset(1.0f, 0.0f), Tile = factory.riverStraight, Rotation = CumulateRotation(0.0f) },
                    new Neighbor() { Offset = RotateOffset(1.0f, 0.0f), Tile = factory.riverCorner, Rotation = CumulateRotation(90.0f) },
                    new Neighbor() { Offset = RotateOffset(1.0f, 0.0f), Tile = factory.riverCorner, Rotation = CumulateRotation(180.0f) }
                };

            neighbors.Add(Pick(north));

            Neighbor[] south = new Neighbor[] {
                    new Neighbor() { Offset = RotateOffset(1.0f, 180.0f), Tile = factory.riverStraight, Rotation = CumulateRotation(0.0f)},
                    new Neighbor() { Offset = RotateOffset(1.0f, 180.0f), Tile = factory.riverCorner, Rotation = CumulateRotation(0.0f) },
                    new Neighbor() { Offset = RotateOffset(1.0f, 180.0f), Tile = factory.riverCorner, Rotation = CumulateRotation(270.0f) }
                };

            neighbors.Add(Pick(south));

            return neighbors;

        }
    }
}