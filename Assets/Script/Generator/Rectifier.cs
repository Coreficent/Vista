namespace Coreficent.Generator
{
    using Coreficent.Tile;
    using Coreficent.Utility;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Rectifier : IIterator
    {

        private Board board;
        private Factory factory;

        private List<Vector3> positions = new List<Vector3>();
        private int currentIndex = 0;

        public Rectifier(Board board, Factory factory)
        {
            this.board = board;
            this.factory = factory;
        }

        public void Stage()
        {
            positions = board.GetAllPositions();
        }

        public bool HasNext()
        {
            return currentIndex < positions.Count;
        }

        public void Next()
        {
            Vector3 position = positions[currentIndex++];

            TileBase middle = board.GetTile(position);
            TileBase north = board.GetNorthTile(position);
            TileBase west = board.GetWestTile(position);
            TileBase south = board.GetSouthTile(position);
            TileBase east = board.GetEastTile(position);

            // DebugUtility.Log("all tiles", middle, north, south, west, east);

            Rectify(middle, north, west, south, east, 0.0f);
            Rectify(middle, west, south, east, north, 90.0f);
            Rectify(middle, south, east, north, west, 180.0f);
            Rectify(middle, east, north, west, south, 270.0f);
        }

        private void Rectify(TileBase middle, TileBase north, TileBase west, TileBase south, TileBase east, float angle)
        {
            if (middle is RoadStraight)
            {
                if (Mathf.Approximately(middle.transform.eulerAngles.z, angle))
                {
                    if ((west is RiverCorner || west is RiverStraight) && (east is RiverCorner || east is RiverStraight))
                    {
                        DebugUtility.Log("replacing");
                        TileBase rectifiedTile = board.Replace(middle.transform.position, factory.Bridge);
                        rectifiedTile.transform.eulerAngles = new Vector3(0.0f, 0.0f, angle);
                    }
                }
            }
        }
    }
}
