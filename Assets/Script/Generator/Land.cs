namespace Coreficent.Generator
{
    using System.Collections.Generic;
    using UnityEngine;

    public class Land : IIterator
    {
        private readonly Board board;
        private readonly Factory factory;
        private readonly float coverage;

        private List<Vector3> positions = new List<Vector3>();
        private int size = 0;
        private int index = 0;

        public Land(Board board, Factory factory, float coverage)
        {
            this.board = board;
            this.factory = factory;
            this.coverage = coverage;
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

            if (chance < 0.05f)
            {
                board.Place(positions[index++], factory.Hill);
            }
            else if (chance < 0.1f * coverage)
            {
                board.Place(positions[index++], factory.Rock);
            }
            else if (chance < 0.25f * coverage)
            {
                board.Place(positions[index++], factory.Bush);
            }
            else if (chance < 0.5f * coverage)
            {
                board.Place(positions[index++], factory.TreeSingle);
            }
            else if (chance < 0.8f * coverage)
            {
                board.Place(positions[index++], factory.TreeDouble);
            }
            else if (chance < 1.0f * coverage)
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
