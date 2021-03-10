namespace Coreficent.Generator
{
    using System.Collections.Generic;
    using UnityEngine;

    public class Land : IIterator
    {
        private List<Vector3> positions = new List<Vector3>();
        private int size = 0;
        private int index = 0;

        private Board board;
        private Factory factory;
        public Land(Board board, Factory factory)
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
                for (var x = 0; x < size; ++x)
                {
                    for (var y = 0; y < size; ++y)
                    {
                        positions.Add(new Vector3(x, y, 0.0f));
                    }
                }
            }
        }

        public bool HasNext()
        {
            return index < size * size;
        }

        public void Next()
        {
            board.Place(positions[index++], factory.Create(Factory.Grass));
        }
    }
}
