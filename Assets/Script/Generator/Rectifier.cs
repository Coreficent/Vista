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
            TileBase south = board.GetSouthTile(position);
            TileBase west = board.GetWestTile(position);
            TileBase east = board.GetEastTile(position);

            // DebugUtility.Log("all tiles", middle, north, south, west, east);

            if (middle is RoadStraight)
            {
                if (Mathf.Approximately(middle.transform.eulerAngles.z, 0.0f))
                {
                    if ((west is RiverCorner || west is RiverStraight) && (east is RiverCorner || east is RiverStraight))
                    {
                        DebugUtility.Log("replacing");
                        board.Replace(position, factory.Bridge);
                    }
                }
            }
        }
    }
}
