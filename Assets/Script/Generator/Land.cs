namespace Coreficent.Generator
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Land
    {
        private List<Vector2Int> positions = new List<Vector2Int>();
        private int radius = 0;
        private int index = 0;



        public int Radius
        {
            get { return radius; }
            set
            {
                radius = value;
                for (var x = 0; x < radius; ++x)
                {
                    for (var y = 0; y < radius; ++y)
                    {
                        positions.Add(new Vector2Int(x, y));
                    }
                }
            }
        }

        public bool HasNext()
        {
            return index < radius * radius;
        }

        public Vector2Int Next()
        {
            return positions[index++];
        }
    }
}