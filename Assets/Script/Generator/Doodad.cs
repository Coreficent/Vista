namespace Coreficent.Generator
{
    using UnityEngine;

    public class Doodad
    {
        private int radius = 0;
        private int doodadCount = 0;

        private Board board;
        private Factory factory;

        public Doodad(Board board, Factory factory)
        {
            this.board = board;
            this.factory = factory;
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

        public void Next()
        {
            --doodadCount;
            board.Replace(board.RandomPosition(), factory.Create(Factory.Tower));
        }
    }
}
