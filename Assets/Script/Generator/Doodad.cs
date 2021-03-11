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

            Vector3 position = board.RandomPosition();

            if (position.x > Random.Range(0.0f, board.Size))
            {
                if (board.Size - position.x < Random.Range(0.0f, board.Size / 10.0f))
                {
                    board.Replace(position, factory.RedTowerHuge);
                }
                else if (board.Size - position.x < Random.Range(0.0f, board.Size / 5.0f))
                {
                    board.Replace(position, factory.RedTowerLarge);
                }
                else if (board.Size - position.x < Random.Range(0.0f, board.Size / 2.0f))
                {
                    board.Replace(position, factory.RedTowerMedium);
                }
                else
                {
                    board.Replace(position, factory.RedTowerSmall);
                }
            }
            else
            {
                if (position.x < Random.Range(0.0f, board.Size / 10.0f))
                {
                    board.Replace(position, factory.BlueTowerHuge);
                }
                else if (position.x < Random.Range(0.0f, board.Size / 5.0f))
                {
                    board.Replace(position, factory.BlueTowerLarge);
                }
                else if (position.x < Random.Range(0.0f, board.Size / 2.0f))
                {
                    board.Replace(position, factory.BlueTowerMedium);
                }
                else
                {
                    board.Replace(position, factory.BlueTowerSmall);
                }
            }
        }
    }
}
