namespace Coreficent.Tile
{
    using Coreficent.Generator;
    using System.Collections.Generic;

    public class RoadStraight : TileBase
    {
        public override List<Neighbor> FindNeighbors(Factory factory)
        {
            List<Neighbor> neighbors = new List<Neighbor>();

            Neighbor northStraightRoad = new Neighbor() { Offset = RotateOffset(1.0f, Neighbor.North), Tile = factory.RoadStraight, Rotation = CumulateRotation(0.0f) };

            Neighbor[] north = new Neighbor[] {
                    northStraightRoad,
                    northStraightRoad,
                    northStraightRoad,
                    northStraightRoad,
                    northStraightRoad,
                    northStraightRoad,
                    northStraightRoad,
                    northStraightRoad,
                    new Neighbor() { Offset = RotateOffset(1.0f, Neighbor.North), Tile = factory.RoadCorner, Rotation = CumulateRotation(90.0f) },
                    new Neighbor() { Offset = RotateOffset(1.0f, Neighbor.North), Tile = factory.RoadCorner, Rotation = CumulateRotation(180.0f) }
                };

            neighbors.Add(Pick(north));

            Neighbor southStraightRoad = new Neighbor() { Offset = RotateOffset(1.0f, Neighbor.South), Tile = factory.RoadStraight, Rotation = CumulateRotation(0.0f) };

            Neighbor[] south = new Neighbor[] {
                    southStraightRoad,
                    southStraightRoad,
                    southStraightRoad,
                    southStraightRoad,
                    southStraightRoad,
                    southStraightRoad,
                    southStraightRoad,
                    southStraightRoad,
                    new Neighbor() { Offset = RotateOffset(1.0f, Neighbor.South), Tile = factory.RoadCorner, Rotation = CumulateRotation(0.0f) },
                    new Neighbor() { Offset = RotateOffset(1.0f, Neighbor.South), Tile = factory.RoadCorner, Rotation = CumulateRotation(270.0f) }
                };

            neighbors.Add(Pick(south));

            return neighbors;
        }
    }
}
