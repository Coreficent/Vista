namespace Coreficent.Generator
{
    using Coreficent.Tile;
    using Coreficent.Utility;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class Rectifier : IIterator
    {

        private readonly Board board;
        private readonly Factory factory;

        private List<Vector3> positions = new List<Vector3>();
        private int currentIndex = 0;

        static private float GetPositiveAngle(float angle)
        {
            return angle < 0.0f ? angle + 360.0f : angle;
        }

        private Func<TileBase, TileBase, TileBase, TileBase, TileBase, float, bool> bridge = (middle, north, west, south, east, angle) =>
        {
            if (middle is RoadStraight)
            {
                if (Mathf.Approximately(middle.transform.eulerAngles.z, angle))
                {
                    if ((west is RiverCorner || west is RiverStraight) && (east is RiverCorner || east is RiverStraight))
                    {
                        if (west is RiverStraight)
                        {
                            float difference = Mathf.Abs(middle.transform.eulerAngles.z - west.transform.eulerAngles.z);
                            if (Mathf.Approximately(difference, 0.0f) || Mathf.Approximately(difference, 180.0f))
                            {
                                return false;
                            }
                        }
                        if (east is RiverStraight)
                        {
                            float difference = Mathf.Abs(middle.transform.eulerAngles.z - east.transform.eulerAngles.z);
                            if (Mathf.Approximately(difference, 0.0f) || Mathf.Approximately(difference, 180.0f))
                            {
                                return false;
                            }
                        }
                        return true;
                    }
                }
            }
            return false;
        };

        private Func<TileBase, TileBase, TileBase, TileBase, TileBase, float, bool> tIntersection = (middle, north, west, south, east, angle) =>
        {
            if (middle is RoadCorner || middle is RoadStraight)
            {
                if (north is RoadCorner || north is RoadStraight)
                {
                    if (west is RoadCorner || west is RoadStraight)
                    {
                        if (east is RoadCorner || east is RoadStraight)
                        {
                            if (!(south is RoadCorner) && !(south is RoadStraight))
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        };

        private Func<TileBase, TileBase, TileBase, TileBase, TileBase, float, bool> template = (middle, north, west, south, east, angle) =>
        {
            return false;
        };

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

            Rectify(middle, north, west, south, east, factory.Bridge, bridge);
            // Rectify(middle, north, west, south, east, factory.RoadTIntersection, tIntersection);
        }

        private void Rectify(TileBase middle, TileBase north, TileBase west, TileBase south, TileBase east, TileBase rectification, Func<TileBase, TileBase, TileBase, TileBase, TileBase, float, bool> condition)
        {
            //Rectify(middle, west, south, east, north, -270.0f, rectification, condition);
            //Rectify(middle, south, east, north, west, -180.0f, rectification, condition);
            //Rectify(middle, east, north, west, south, -90.0f, rectification, condition);
            Rectify(middle, north, west, south, east, 360.0f, rectification, condition);
            Rectify(middle, north, west, south, east, 0.0f, rectification, condition);
            Rectify(middle, west, south, east, north, 90.0f, rectification, condition);
            Rectify(middle, south, east, north, west, 180.0f, rectification, condition);
            Rectify(middle, east, north, west, south, 270.0f, rectification, condition);
        }

        private void Rectify(TileBase middle, TileBase north, TileBase west, TileBase south, TileBase east, float angle, TileBase rectification, Func<TileBase, TileBase, TileBase, TileBase, TileBase, float, bool> condition)
        {
            if (condition(middle, north, west, south, east, angle))
            {
                DebugUtility.Log("replacing", rectification);
                TileBase rectifiedTile = board.Replace(middle.transform.position, rectification);
                rectifiedTile.transform.eulerAngles = new Vector3(0.0f, 0.0f, angle);
            }
        }


    }
}
