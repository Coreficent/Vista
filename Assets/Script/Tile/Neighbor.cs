namespace Coreficent.Tile
{
    using UnityEngine;

    public class Neighbor
    {
        public static float North = 0.0f;
        public static float West = 90.0f;
        public static float South = 180.0f;
        public static float East = 270.0f;

        public Vector3 Offset;
        public TileBase Tile;
        public float Rotation;
        public int weight;
    }
}