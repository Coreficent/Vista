namespace Coreficent.Generator
{
    using UnityEngine;

    public class Doodad
    {
        private int radius = 0;
        private int doodadCount = 0;

        private Board board;

        public Doodad(Board board)
        {
            this.board = board;
        }

        public int Radius
        {
            get { return radius; }
            set
            {
                radius = value;
                doodadCount = radius + radius;
            }
        }

        public bool HasNext()
        {
            return doodadCount > 0;
        }

        public Vector3 Next()
        {
            --doodadCount;
            return board.Random();
        }
    }
}
