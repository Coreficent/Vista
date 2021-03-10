namespace Coreficent.Tile
{
    using Coreficent.Generator;
    using System.Collections.Generic;

    public class RiverCorner : TileBase
    {
        public override List<Neighbor> FindNeighbors(Factory factory)
        {
            List<Neighbor> neighbors = new List<Neighbor>();

            Neighbor[] north = new Neighbor[] {
                    new Neighbor() { Offset = RotateOffset(1.0f, Neighbor.North), Tile = factory.RiverStraight, Rotation = CumulateRotation(0.0f) },
                };

            neighbors.Add(Pick(north));

            Neighbor[] west = new Neighbor[] {
                    new Neighbor() { Offset = RotateOffset(1.0f, Neighbor.West), Tile = factory.RiverStraight, Rotation = CumulateRotation(270.0f) },
                };

            neighbors.Add(Pick(west));

            return neighbors;
        }
    }
}
