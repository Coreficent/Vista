namespace Coreficent.Generator
{
    using System.Collections.Generic;
    using UnityEngine;
    public class Tile : Genesis
    {
        [SerializeField]
        private List<Genesis> geneses = new List<Genesis>();

        public override List<Genesis> Generate()
        {
            List<Genesis> result = new List<Genesis>();

            //if(hitColliders = Physics.OverlapSphere(spawnPoint, 1); //1 is purely chosen arbitrarly)

            result.AddRange(Create(geneses[0], new Vector3(0.0f, 0.0f, 1.0f)));
            result.AddRange(Create(geneses[0], new Vector3(0.0f, 0.0f, -1.0f)));
            result.AddRange(Create(geneses[0], new Vector3(1.0f, 0.0f, 0.0f)));
            result.AddRange(Create(geneses[0], new Vector3(-1.0f, 0.0f, 0.0f)));

            return result;
        }
    }
}
