namespace Coreficent.Tile
{
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;

    public class TileBase : ReinforcedBehavior
    {

        public static string HashNameFromPosition(Vector3 position)
        {
            string result = "";
            string delimiter = ":";

            result += Mathf.RoundToInt(position.x * 10.0f);
            result += delimiter;
            result += Mathf.RoundToInt(position.y * 10.0f);
            result += delimiter;
            result += Mathf.RoundToInt(position.z * 10.0f);

            return result;
        }
        public virtual List<Neighbor> Neighbors
        {
            get
            {
                return new List<Neighbor>();
            }
        }

        public string HashName
        {
            get
            {
                return HashNameFromPosition(transform.position);
            }
        }

        protected virtual void Start()
        {

        }

        protected int GetDirectionIndex(int input)
        {
            return (Mathf.RoundToInt(transform.eulerAngles.y / 90.0f) + input) % 4;
        }

        public override bool Equals(object other)
        {
            if (other == null)
            {
                return false;
            }
            if (!(other is TileBase))
            {
                return false;
            }

            return other.GetType().Name == GetType().Name;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        protected Neighbor Pick(Neighbor[] neighbors)
        {
            return neighbors[Random.Range(0, neighbors.Length)];
        }

        protected float CumulateRotation(float rotation)
        {
            return transform.eulerAngles.z + rotation;
        }

        protected Vector3 RotateOffset(float offset, float angle)
        {
            return new Vector3(Mathf.Sin((transform.eulerAngles.z + angle) * Mathf.Deg2Rad) * offset, Mathf.Cos((transform.eulerAngles.z + angle) * Mathf.Deg2Rad) * offset, 0.0f);
        }
    }
}
