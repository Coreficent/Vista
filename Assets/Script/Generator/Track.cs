namespace Coreficent.Generator
{
    using Coreficent.Tile;
    using System.Collections.Generic;
    using UnityEngine;

    public class Track
    {
        private Queue<TileBase> queue = new Queue<TileBase>();

        private Board board;

        public Track(Board board)
        {
            this.board = board;
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
            // pop
            // get neighbor
            // put neighbor in
        }
    }
}
