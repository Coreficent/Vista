namespace Coreficent.Generator
{
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;
    public class Tile : Genesis
    {
        [SerializeField]
        private List<Genesis> geneses = new List<Genesis>();

        protected override void Start()
        {
            Neighbors.Add(new Vector3(0.0f, 0.0f, 1.0f));
            Neighbors.Add(new Vector3(0.0f, 0.0f, -1.0f));
            Neighbors.Add(new Vector3(1.0f, 0.0f, 0.0f));
            Neighbors.Add(new Vector3(-1.0f, 0.0f, 0.0f));
        }

        public override List<Genesis> Generate()
        {
            DebugUtility.Log("Generate Tile");
            List<Genesis> result = new List<Genesis>();

            result.Add(Create(geneses[0], new Vector3(0.0f, 0.0f, 1.0f)));
            result.Add(Create(geneses[0], new Vector3(0.0f, 0.0f, -1.0f)));
            result.Add(Create(geneses[0], new Vector3(1.0f, 0.0f, 0.0f)));
            result.Add(Create(geneses[0], new Vector3(-1.0f, 0.0f, 0.0f)));

            return result;
        }
    }
}
