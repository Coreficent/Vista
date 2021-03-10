namespace Coreficent.Tile
{
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;

    public class TileBase : ReinforcedBehavior
    {

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
                string result = "";
                string delimiter = ":";

                result += Mathf.RoundToInt(transform.position.x * 10.0f);
                result += delimiter;
                result += Mathf.RoundToInt(transform.position.y * 10.0f);
                result += delimiter;
                result += Mathf.RoundToInt(transform.position.z * 10.0f);

                return result;
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
    }
}
