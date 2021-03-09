namespace Coreficent.Generator
{
    using System.Collections.Generic;
    using UnityEngine;
    public class Tile : Genesis
    {
        public override List<Genesis> Generate(Genesis genesis)
        {
            List<Genesis> result = new List<Genesis>();

            //if(hitColliders = Physics.OverlapSphere(spawnPoint, 1); //1 is purely chosen arbitrarly)

            result.AddRange(Create(genesis, new Vector3(0.0f, 0.0f, 1.0f)));
            result.AddRange(Create(genesis, new Vector3(0.0f, 0.0f, -1.0f)));
            result.AddRange(Create(genesis, new Vector3(1.0f, 0.0f, 0.0f)));
            result.AddRange(Create(genesis, new Vector3(-1.0f, 0.0f, 0.0f)));

            return result;
        }
    }
}
