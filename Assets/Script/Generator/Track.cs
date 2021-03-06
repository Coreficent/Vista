namespace Coreficent.Generator
{
    using Coreficent.Tile;
    using System.Collections.Generic;
    using UnityEngine;

    public class Track : IIterator
    {
        private readonly Board board;
        private readonly Factory factory;
        private readonly TileBase tileType;
        private readonly float coverage;

        private readonly Stack<TileBase> task = new Stack<TileBase>();
        private readonly HashSet<string> set = new HashSet<string>();
        private int currentCount = 0;

        public Track(Board board, Factory factory, TileBase tileType, float coverage)
        {
            this.board = board;
            this.factory = factory;
            this.tileType = tileType;
            this.coverage = coverage;
        }

        public bool HasNext()
        {
            if (!(task.Count > 0))
            {
                if (currentCount < board.Size * board.Size * coverage)
                {
                    Stage();
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public void Next()
        {
            TileBase parent = task.Pop();
            ++currentCount;

            if (parent)
            {
                set.Add(TileBase.HashNameFromPosition(parent.transform.position));
            }
            else
            {
                return;
            }

            foreach (Neighbor i in parent.FindNeighbors(factory))
            {

                Vector3 position = parent.transform.position + i.Offset;

                if (board.ValidPosition(position))
                {
                    if (!set.Contains(TileBase.HashNameFromPosition(position)))
                    {
                        TileBase tileBase = board.Replace(position, i.Tile);
                        tileBase.transform.eulerAngles = new Vector3(0.0f, 0.0f, i.Rotation);

                        task.Push(tileBase);
                    }
                }
            }
        }

        public void Stage()
        {
            Vector3 position = board.RandomPosition();
            TileBase riverTile = board.Replace(position, tileType);
            task.Push(riverTile);
        }
    }
}
