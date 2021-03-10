namespace Coreficent.Tile
{
    using Coreficent.Generator;
    using System.Collections.Generic;

    public class RoadStraight : TileBase
    {
        public override List<Neighbor> FindNeighbors(Factory factory)
        {
            List<Neighbor> neighbors = new List<Neighbor>();

            Neighbor[] north = new Neighbor[] {
                    new Neighbor() { Offset = RotateOffset(1.0f, 0.0f), Tile = factory.RoadStraight, Rotation = CumulateRotation(0.0f) },
                    new Neighbor() { Offset = RotateOffset(1.0f, 0.0f), Tile = factory.RoadCorner, Rotation = CumulateRotation(90.0f) },
                    new Neighbor() { Offset = RotateOffset(1.0f, 0.0f), Tile = factory.RoadCorner, Rotation = CumulateRotation(180.0f) }
                };

            neighbors.Add(Pick(north));

            Neighbor[] south = new Neighbor[] {
                    new Neighbor() { Offset = RotateOffset(1.0f, 180.0f), Tile = factory.RoadStraight, Rotation = CumulateRotation(0.0f)},
                    new Neighbor() { Offset = RotateOffset(1.0f, 180.0f), Tile = factory.RoadCorner, Rotation = CumulateRotation(0.0f) },
                    new Neighbor() { Offset = RotateOffset(1.0f, 180.0f), Tile = factory.RoadCorner, Rotation = CumulateRotation(270.0f) }
                };

            neighbors.Add(Pick(south));

            return neighbors;
        }
    }
}
