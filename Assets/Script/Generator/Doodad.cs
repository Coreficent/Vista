namespace Coreficent.Generator
{
    using UnityEngine;

    public class Doodad
    {
        private int radius = 0;
        private int doodadCount = 0;

        public int Radius
        {
            get { return radius; }
            set
            {
                radius = value;
                doodadCount = radius + radius;
            }
        }

        public Vector3 Random()
        {
            return new Vector3(Mathf.RoundToInt(UnityEngine.Random.Range(0.0f, Radius - 1)), Mathf.RoundToInt(UnityEngine.Random.Range(0.0f, Radius - 1)), 0.0f);
        }

        public bool HasNext()
        {
            return doodadCount > 0;
        }

        public Vector3 Next()
        {
            --doodadCount;
            return Random();
        }
    }
}
