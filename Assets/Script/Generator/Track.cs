namespace Coreficent.Generator
{
    using Coreficent.Tile;
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;

    public class Track : IIterator
    {
        private Stack<TileBase> task = new Stack<TileBase>();
        private HashSet<string> set = new HashSet<string>();

        private Board board;
        private Factory factory;

        private TileBase tileType;

        private int minimumCount;
        private int currentCount = 0;

        public Track(Board board, Factory factory, TileBase tileType, int minimumCount)
        {
            this.board = board;
            this.factory = factory;
            this.tileType = tileType;
            this.minimumCount = minimumCount;
        }

        public bool HasNext()
        {
            if (!(task.Count > 0))
            {
                if (currentCount < minimumCount)
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
                DebugUtility.Log("formed a loop");
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
