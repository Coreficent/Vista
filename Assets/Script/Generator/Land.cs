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
            float chance = Random.Range(0.0f, 1.0f);

            if (chance < 0.0125f)
            {
                board.Place(positions[index++], factory.Hill);
            }
            else if (chance < 0.025f)
            {
                board.Place(positions[index++], factory.Rock);
            }
            else if (chance < 0.05f)
            {
                board.Place(positions[index++], factory.Bush);
            }
            else if (chance < 0.1f)
            {
                board.Place(positions[index++], factory.TreeSingle);
            }
            else if (chance < 0.2f)
            {
                board.Place(positions[index++], factory.TreeDouble);
            }
            else if (chance < 0.3f)
            {
                board.Place(positions[index++], factory.TreeTriple);
            }
            else
            {
                board.Place(positions[index++], factory.Grass);
            }
        }
    }
}
