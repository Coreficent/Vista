namespace Coreficent.Generator
{
    using System.Collections.Generic;
    using UnityEngine;

    public class Board
    {
        private int index = 0;
        private List<Vector2> positions = new List<Vector2>();

        public Board(int size)
        {
            for (var x = 0; x < size; ++x)
            {
                for (var y = 0; y < size; ++y)
                {
                    positions.Add(new Vector2(x - size / 2, y - size / 2));
                }
            }
        }

        public bool HasNext()
        {
            return index < positions.Count;
        }

        public Vector2 Next()
        {
            return positions[index++];
        }
    }
}

