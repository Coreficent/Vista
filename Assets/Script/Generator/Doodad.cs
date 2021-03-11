namespace Coreficent.Generator
{
    using UnityEngine;

    public class Doodad : IIterator
    {
        private int doodadCount = 0;

        private readonly Board board;
        private readonly Factory factory;

        private bool placedRed = false;
        private bool placedBlue = false;

        private float towerCoverage = 0.25f;

        public Doodad(Board board, Factory factory)
        {
            this.board = board;
            this.factory = factory;
        }

        public bool HasNext()
        {
            return doodadCount < board.Size * board.Size * towerCoverage;
        }

        public void Next()
        {
            ++doodadCount;

            Vector3 position = board.RandomPosition();

            if (position.x > Random.Range(0.0f, board.Size))
            {
                if (!placedRed && board.Size - position.x < Random.Range(0.0f, board.Size / 20.0f))
                {
                    if (board.GetEastTile(position))
                    {
                        board.Replace(position, factory.RedTowerMassive);
                        placedRed = true;
                    }
                }
                else if (board.Size - position.x < Random.Range(0.0f, board.Size / 10.0f))
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
                if (!placedBlue && position.x < Random.Range(0.0f, board.Size / 20.0f))
                {
                    if (board.GetWestTile(position))
                    {
                        board.Replace(position, factory.BlueTowerMassive);
                        placedBlue = true;
                    }
                }
                else if (position.x < Random.Range(0.0f, board.Size / 10.0f))
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
