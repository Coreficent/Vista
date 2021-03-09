namespace Coreficent.Generator
{
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;

    public class TileBase : ReinforcedBehavior
    {
        public List<TileBase> North;
        public List<TileBase> South;
        public List<TileBase> West;
        public List<TileBase> East;

        protected List<Vector2Int> neighbors = new List<Vector2Int> { new Vector2Int(0, 1), new Vector2Int(1, 0), new Vector2Int(0, -1), new Vector2Int(-1, 0) };

        protected virtual void Start()
        {

        }

        public virtual List<Vector2Int> GetNeighbors(Vector2Int position)
        {
            return new List<Vector2Int>();
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
