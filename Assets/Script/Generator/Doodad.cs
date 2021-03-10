namespace Coreficent.Generator
{
    using UnityEngine;

    public class Doodad : IIterator
    {
        private int size = 0;
        private int doodadCount = 0;

        private Board board;
        private Factory factory;

        public Doodad(Board board, Factory factory)
        {
            this.board = board;
            this.factory = factory;
        }

        public int Size
        {
            get { return size; }
            set
            {
                size = value;
                doodadCount = size + size;
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
