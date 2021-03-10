namespace Coreficent.Generator
{
    using System.Collections.Generic;
    using UnityEngine;

    public class Land
    {
        private List<Vector3> positions = new List<Vector3>();
        private int radius = 0;
        private int index = 0;

        private Board board;
        private Factory factory;
        public Land(Board board, Factory factory)
        {
            this.board = board;
            this.factory = factory;
        }

        public int Radius
        {
            get { return radius; }
            set
            {
                radius = value;
                for (var x = 0; x < radius; ++x)
                {
                    for (var y = 0; y < radius; ++y)
                    {
                        positions.Add(new Vector3(x, y, 0.0f));
                    }
                }
            }
        }

        public bool HasNext()
        {
            return index < radius * radius;
        }

        public void Next()
        {
            board.Place(positions[index++], factory.Create(Factory.Grass));
        }
    }
}
