namespace Coreficent.Generator
{
    using Coreficent.Tile;
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;

    public class Track
    {
        private Queue<TileBase> queue = new Queue<TileBase>();
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
            queue.Enqueue(tileBase);
        }

        public bool HasNext()
        {
            return queue.Count > 0;
        }

        public void Next()
        {
            TileBase parent = queue.Dequeue();
            set.Add(parent.HashName);

            foreach (Neighbor i in parent.Neighbors)
            {

                Vector3 position = parent.transform.position + i.Offset;

                if (board.ValidPosition(position))
                {
                    TileBase tileBase = board.Replace(position, factory.Create(i.Tile));
                    //TODO

                    DebugUtility.ToDo("set rotation");
                    queue.Enqueue(tileBase);
                }
            }
            // put neighbor in
        }
    }
}
