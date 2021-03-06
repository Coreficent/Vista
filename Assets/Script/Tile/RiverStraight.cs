namespace Coreficent.Tile
{
    using Coreficent.Generator;
    using System.Collections.Generic;

    public class RiverStraight : TileBase
    {
        public override List<Neighbor> FindNeighbors(Factory factory)
        {
            List<Neighbor> neighbors = new List<Neighbor>();

            Neighbor northStraightRiver = new Neighbor() { Offset = RotateOffset(1.0f, Neighbor.North), Tile = factory.RiverStraight, Rotation = CumulateRotation(0.0f) };

            Neighbor[] north = new Neighbor[] {
                    northStraightRiver,
                    northStraightRiver,
                    northStraightRiver,
                    new Neighbor() { Offset = RotateOffset(1.0f, Neighbor.North), Tile = factory.RiverCorner, Rotation = CumulateRotation(90.0f) },
                    new Neighbor() { Offset = RotateOffset(1.0f, Neighbor.North), Tile = factory.RiverCorner, Rotation = CumulateRotation(180.0f) }
                };

            neighbors.Add(Pick(north));

            Neighbor southStraightRiver = new Neighbor() { Offset = RotateOffset(1.0f, Neighbor.South), Tile = factory.RiverStraight, Rotation = CumulateRotation(0.0f) };

            Neighbor[] south = new Neighbor[] {
                    southStraightRiver,
                    southStraightRiver,
                    southStraightRiver,
                    new Neighbor() { Offset = RotateOffset(1.0f, Neighbor.South), Tile = factory.RiverCorner, Rotation = CumulateRotation(0.0f) },
                    new Neighbor() { Offset = RotateOffset(1.0f, Neighbor.South), Tile = factory.RiverCorner, Rotation = CumulateRotation(270.0f) }
                };

            neighbors.Add(Pick(south));

            return neighbors;
        }
    }
}