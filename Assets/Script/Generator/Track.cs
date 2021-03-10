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
        public Track(Board board, Factory factory)
        {
            this.board = board;
            this.factory = factory;
        }

        public void Add(TileBase tileBase)
        {
            task.Push(tileBase);
        }

        public bool HasNext()
        {
            return task.Count > 0;
        }

        public void Next()
        {
            TileBase parent = task.Pop();

            if (parent)
            {
                set.Add(TileBase.HashNameFromPosition(parent.transform.position));
            }
            else
            {
                DebugUtility.Log("formed a loop");
                return;
            }

            foreach (Neighbor i in parent.Neighbors(factory))
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
    }
}
