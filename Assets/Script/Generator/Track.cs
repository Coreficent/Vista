﻿namespace Coreficent.Generator
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
            set.Add(TileBase.HashNameFromPosition(parent.transform.position));

            foreach (Neighbor i in parent.Neighbors)
            {

                Vector3 position = parent.transform.position + i.Offset;

                if (board.ValidPosition(position))
                {
                    if (!set.Contains(TileBase.HashNameFromPosition(position)))
                    {
                        TileBase tileBase = board.Replace(position, factory.Create(i.Tile));
                        tileBase.transform.eulerAngles = new Vector3(0.0f, 0.0f, i.Rotation);

                        DebugUtility.ToDo("set rotation");
                        queue.Enqueue(tileBase);
                    }
                }
            }
            // put neighbor in
        }
    }
}
